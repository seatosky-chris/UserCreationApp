using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using UserCreationLibrary;
using UserCreationUI.Models.DataGridFilterModels;

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

        //public ObservableCollection<ADPermissionModel> ADPermissions_All { get; } = new ObservableCollection<ADPermissionModel>(Program.GlobalConfig.ADPermissions);
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
            ADPermissionSetModel CurrentPermissionSetItem = CurrentPermissionSets.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditPermissionSet(CurrentPermissionSetItem.Id);

            CurrentPermissionSets.Remove(CurrentPermissionSets.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditPermissionSet()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditPermissionSet(string? Id)
        {
            // TODO: Add in validation
            CurrentPermissionSets.Add(new ADPermissionSetModel
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Name = AddNewPrimary,
                Permissions = ADPermissions_Selected.ToList(),
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
            ADPermissionModel ADGroup = (ADPermissionModel)row.DataContext;
            ADPermissions_Selected.Add(ADGroup);
        }

        public bool FilterPermissions(object permission) 
        {
            foreach (var filterProp in ADPermissionsGridFilters.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly)) {
                var filterVal = filterProp.GetValue(ADPermissionsGridFilters, null);
                if (filterVal == null || string.IsNullOrWhiteSpace(filterVal.ToString()))
                {
                    continue;
                }

                var curValue = permission.GetType().GetProperty(filterProp.Name);
                if (curValue == null)
                {
                    return false;
                }
                var curValString = curValue.GetValue(permission, null);
                if (curValString == null || curValString.ToString() == null || string.IsNullOrWhiteSpace(curValString.ToString()) || !curValString.ToString().Contains(filterVal.ToString()))
                {
                    return false;
                }
            }

            return true;
        }

        private async void DoFilter()
        {
            if (ADPermissions_All.CanFilter)
            {
                ADPermissions_All.Filter = FilterPermissions;
                ADPermissions_All.Refresh();
            }
        }
    }
}
