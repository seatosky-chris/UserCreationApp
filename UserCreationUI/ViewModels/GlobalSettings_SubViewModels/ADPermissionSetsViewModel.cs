using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using FluentValidation.Results;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UserCreationLibrary;
using UserCreationUI.Models.DataGridFilterModels;
using UserCreationUI.Utilities;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class ADPermissionSetsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "ADPermissionSetsEdit";

        private string _editID = "";

        public ADPermissionSetsViewModel(IScreen screen) : base(screen)
        {
            this.WhenAnyValue(x => x.ADPermissionsGridFilters.Name, x => x.ADPermissionsGridFilters.ITGType, x => x.ADPermissionsGridFilters.ITGDescription, x => x.ADPermissionsGridFilters.ITGWhoToAddAndApprover)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => DoFilter());
        }

        public ObservableCollection<ADPermissionSetModel> CurrentPermissionSets { get; } = new ObservableCollection<ADPermissionSetModel>(Program.GlobalConfig.ADPermissionSets);

        public DataGridCollectionView ADPermissions_All { get; } = new DataGridCollectionView(Program.GlobalConfig.ADPermissions);
        public ObservableCollection<ADPermissionModel> ADPermissions_Selected { get; } = new();
        public ADPermissionFiltersModel ADPermissionsGridFilters { get; set; } = new ADPermissionFiltersModel();

        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }


        public void AddPermissionSet()
        {
            AddEditPermissionSet(null);
        }

        public void EditPermissionSet()
        {
            if (CurrentPermissionSets is null)
                return;

            ADPermissionSetModel? CurrentPermissionSetItem = CurrentPermissionSets.Where(x => x.Id == EditID).FirstOrDefault();

            if (CurrentPermissionSetItem is null)
                return;

            AddEditPermissionSet(CurrentPermissionSetItem.Id);

            CurrentPermissionSets.Remove(CurrentPermissionSetItem);
            EditID = "";
        }

        public void CancelEditPermissionSet()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditPermissionSet(string? Id)
        {
            ADPermissionSetValidator validator = new();
            var newPermissionSet = new ADPermissionSetModel
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Name = AddNewPrimary,
                Permissions = ADPermissions_Selected.ToList(),
                EmployeeTypes = (from employee in SelectedEmployeeTypes.SelectedItems select employee.Key).Distinct().ToList(),
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            };

            if (CurrentPermissionSets.Where(set => set.Name == newPermissionSet.Name).Any())
            {
                ShowError("That Set Name is already in use!", "Please choose a different name.");
                return;
            }
            var existingSet = CurrentPermissionSets.Where(set => set.Permissions.All(newPermissionSet.Permissions.Contains)).Where(set => set.Permissions.Count == newPermissionSet.Permissions.Count);
            if (existingSet.Any())
            {
                ShowError("A permission set with those permissions already exists!", "You tried creating a duplicate. Take a look at: " + existingSet.First().Name);
                return;
            }

            ValidationResult validationResult = validator.Validate(newPermissionSet);

            if (validationResult.IsValid)
            {
                CurrentPermissionSets.Add(newPermissionSet);

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
            ADPermissions_Selected.Clear();
            SelectedLocations.Clear();
            SelectedEmployeeTypes.Clear();
        }

        public void DeletePermissionSet(int index)
        {
            CurrentPermissionSets.RemoveAt(index);
            Saveable = true;
        }

        public void RemoveADPermissions(int index)
        {
            ADPermissions_Selected.RemoveAt(index);
        }

        public void SavePermissionSets()
        {
            if (Saveable)
            {
                Program.GlobalConfig.ADPermissionSets = CurrentPermissionSets.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Software Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }

        public void CurrentPermissionSet_DoubleClick(ListBoxItem row)
        {
            if (row.DataContext is null)
                return;

            // Load software details
            ADPermissionSetModel SelectedADPermissionSet = (ADPermissionSetModel)row.DataContext;
            EditID = SelectedADPermissionSet.Id;
            int? SelectedIndex = CurrentPrimarySelected;

            ClearForm();

            CurrentPrimarySelected = SelectedIndex;
            AddNewPrimary = SelectedADPermissionSet.Name;

            foreach (var TypeIndex in SelectedADPermissionSet.EmployeeTypes)
            {
                SelectedEmployeeTypes.Select(TypeIndex);
            }

            foreach (var LocIndex in SelectedADPermissionSet.Locations)
            {
                SelectedLocations.Select(LocIndex);
            }

            foreach (ADPermissionModel Permission in SelectedADPermissionSet.Permissions)
            {
                ADPermissions_Selected.Add(Permission);
            }

        }

        public void ADGroupRow_DoubleClick(DataGridRow row)
        {
            if (row.DataContext is null)
                return;

            ADPermissionModel ADGroup = (ADPermissionModel)row.DataContext;
            ADPermissions_Selected.Add(ADGroup);
        }

        public bool FilterPermissions(object permission) 
        {
            return UIFunctions.FilterDataGrid(permission, ADPermissionsGridFilters);
        }

        private void DoFilter()
        {
            if (ADPermissions_All.CanFilter)
            {
                ADPermissions_All.Filter = FilterPermissions;
                ADPermissions_All.Refresh();
            }
        }

        public void ClearTypeFilter()
        {
            ADPermissionsGridFilters.ITGType = null;
        }
    }
}
