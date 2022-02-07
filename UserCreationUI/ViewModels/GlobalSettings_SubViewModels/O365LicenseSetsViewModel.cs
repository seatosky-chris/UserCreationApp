using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using UserCreationLibrary;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class O365LicenseSetsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "O365LicenseSetsEdit";

        private string _editID = "";

        public O365LicenseSetsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<O365LicenseSetModel> CurrentLicenseSets { get; } = new ObservableCollection<O365LicenseSetModel>(Program.GlobalConfig.O365LicenseSets);

        public ObservableCollection<O365LicenseModel> O365Licenses_All { get; } = new ObservableCollection<O365LicenseModel>(Program.GlobalConfig.O365Licenses);
        public ObservableCollection<O365LicenseModel> O365Licenses_Selected { get; } = new();


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
            O365LicenseSetModel CurrentLicenseSetItem = CurrentLicenseSets.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditLicenseSet(CurrentLicenseSetItem.Id);

            CurrentLicenseSets.Remove(CurrentLicenseSets.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditLicenseSet()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditLicenseSet(string? Id)
        {
            // TODO: Add in validation
            CurrentLicenseSets.Add(new O365LicenseSetModel
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Name = AddNewPrimary,
                O365Licenses = O365Licenses_Selected.ToList(),
                EmployeeTypes = (from employee in SelectedEmployeeTypes.SelectedItems select employee.Key).Distinct().ToList(),
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            });

            ClearForm();
            Saveable = true;
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
            O365LicenseModel O365License = (O365LicenseModel)row.DataContext;
            O365Licenses_Selected.Add(O365License);
        }
    }
}
