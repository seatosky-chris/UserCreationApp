using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Runtime.InteropServices;
using UserCreationLibrary;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class ADPermissionSetsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "ADPermissionSetsEdit";

        private int _currentSelectionType = 0;
        private string _editID = "";

        public ADPermissionSetsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<ADPermissionSetModel> CurrentPermissionSets { get; } = new ObservableCollection<ADPermissionSetModel>(Program.GlobalConfig.ADPermissionSets);

        public ObservableCollection<ADPermissionModel> ADPermissions_All { get; } = new ObservableCollection<ADPermissionModel>(Program.GlobalConfig.ADPermissions);
        public ObservableCollection<ADPermissionModel> ADPermissions_Selected { get; } = new();


        public int CurrentSelectionType
        {
            get => _currentSelectionType;
            set => this.RaiseAndSetIfChanged(ref _currentSelectionType, value);
        }

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
    }
}
