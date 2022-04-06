using Avalonia.Collections;
using Avalonia.Controls;
using FluentValidation.Results;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using UserCreationLibrary;
using UserCreationUI.Models.DataGridFilterModels;
using UserCreationUI.Utilities;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class O365LicenseSetsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "O365LicenseSetsEdit";

        private string _editID = "";

        public O365LicenseSetsViewModel(IScreen screen) : base(screen)
        {
            this.WhenAnyValue(x => x.O365LicenseGridFilters.Name)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => DoFilter());
        }

        public ObservableCollection<O365LicenseSetModel> CurrentLicenseSets { get; } = new ObservableCollection<O365LicenseSetModel>(Program.GlobalConfig.O365LicenseSets);

        public DataGridCollectionView O365Licenses_All { get; } = new DataGridCollectionView(Program.GlobalConfig.O365Licenses);
        public ObservableCollection<O365LicenseModel> O365Licenses_Selected { get; } = new();
        public O365LicenseFiltersModel O365LicenseGridFilters { get; set; } = new O365LicenseFiltersModel();


        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }


        public void AddLicenseSet()
        {
            AddEditLicenseSet(null);
        }

        public void EditLicenseSet()
        {
            if (CurrentLicenseSets is null)
                return;

            O365LicenseSetModel? CurrentLicenseSetItem = CurrentLicenseSets.Where(x => x.Id == EditID).FirstOrDefault();

            if (CurrentLicenseSetItem is null)
                return;

            AddEditLicenseSet(CurrentLicenseSetItem.Id);

            CurrentLicenseSets.Remove(CurrentLicenseSetItem);
            EditID = "";
        }

        public void CancelEditLicenseSet()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditLicenseSet(string? Id)
        {
            O365LicenseSetValidator validator = new();
            var newO365LicenseSet = new O365LicenseSetModel
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Name = AddNewPrimary,
                O365Licenses = O365Licenses_Selected.ToList(),
                EmployeeTypes = (from employee in SelectedEmployeeTypes.SelectedItems select employee.Key).Distinct().ToList(),
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            };

            if (CurrentLicenseSets.Where(set => set.Name == newO365LicenseSet.Name).Any())
            {
                ShowError("That Set Name is already in use!", "Please choose a different name.");
                return;
            }
            var existingSet = CurrentLicenseSets.Where(set => set.O365Licenses.All(newO365LicenseSet.O365Licenses.Contains)).Where(set => set.O365Licenses.Count == newO365LicenseSet.O365Licenses.Count);
            if (existingSet.Any())
            {
                ShowError("A license set with those licenses already exists!", "You tried creating a duplicate. Take a look at: " + existingSet.First().Name);
                return;
            }

            ValidationResult validationResult = validator.Validate(newO365LicenseSet);

            if (validationResult.IsValid)
            {
                CurrentLicenseSets.Add(newO365LicenseSet);

                ClearForm();
                Saveable = true;
            }
            else
            {
                ShowError("Data Validation Failed. Please fix the following errors:", (validationResult.Errors.Count > 2 ? validationResult.ToString(" ") : validationResult.ToString()));
            }
        }

        private void ClearForm()
        {
            AddNewPrimary = "";
            CurrentPrimarySelected = -1;
            DataGridSelection = -1;
            O365Licenses_Selected.Clear();
            SelectedLocations.Clear();
            SelectedEmployeeTypes.Clear();
        }

        public void DeleteLicenseSet(int index)
        {
            CurrentLicenseSets.RemoveAt(index);
            Saveable = true;
        }

        public void RemoveO365License(int index)
        {
            O365Licenses_Selected.RemoveAt(index);
        }

        public void SaveLicenseSets()
        {
            if (Saveable)
            {
                Program.GlobalConfig.O365LicenseSets = CurrentLicenseSets.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving O365 License Set Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }

        public void CurrentLicenseSet_DoubleClick(ListBoxItem row)
        {
            if (row.DataContext is null)
                return;

            // Load software details
            O365LicenseSetModel SelectedO365LicenseSet = (O365LicenseSetModel)row.DataContext;
            EditID = SelectedO365LicenseSet.Id;
            int? SelectedIndex = CurrentPrimarySelected;

            ClearForm();

            CurrentPrimarySelected = SelectedIndex;
            AddNewPrimary = SelectedO365LicenseSet.Name;

            foreach (var TypeIndex in SelectedO365LicenseSet.EmployeeTypes)
            {
                SelectedEmployeeTypes.Select(TypeIndex);
            }

            foreach (var LocIndex in SelectedO365LicenseSet.Locations)
            {
                SelectedLocations.Select(LocIndex);
            }

            foreach (O365LicenseModel O365License in SelectedO365LicenseSet.O365Licenses)
            {
                O365Licenses_Selected.Add(O365License);
            }

        }

        public void O365LicenseRow_DoubleClick(DataGridRow row)
        {
            if (row.DataContext is null)
                return;

            O365LicenseModel O365License = (O365LicenseModel)row.DataContext;
            O365Licenses_Selected.Add(O365License);
        }

        public bool FilterLicenses(object license)
        {
            return UIFunctions.FilterDataGrid(license, O365LicenseGridFilters);
        }

        private void DoFilter()
        {
            if (O365Licenses_All.CanFilter)
            {
                O365Licenses_All.Filter = FilterLicenses;
                O365Licenses_All.Refresh();
            }
        }
    }
}
