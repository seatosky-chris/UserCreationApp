using Avalonia.Collections;
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using UserCreationLibrary;
using UserCreationUI.Models.DataGridFilterModels;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class SoftwareSettingsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "SoftwareSettingsEdit";

        private int _currentSelectionType = 0;
        private string _editID = "";

        public SoftwareSettingsViewModel(IScreen screen) : base(screen)
        {
            this.WhenAnyValue(x => x.ADPermissionsGridFilters.Name, x => x.ADPermissionsGridFilters.ITGType, x => x.ADPermissionsGridFilters.ITGDescription, x => x.ADPermissionsGridFilters.ITGWhoToAddAndApprover)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => DoADPermsFilter());

            this.WhenAnyValue(x => x.O365GroupGridFilters.Name, x => x.O365GroupGridFilters.Email, x => x.O365GroupGridFilters.GroupType, x => x.O365GroupGridFilters.ITGDescription, x => x.O365GroupGridFilters.ITGWhoToAddAndApprover)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => DoO365GroupFilter());

            this.WhenAnyValue(x => x.O365LicenseGridFilters.Name)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => DoLicenseFilter());
        }

        public ObservableCollection<SoftwareModel> CurrentSoftware { get; } = new ObservableCollection<SoftwareModel>(Program.GlobalConfig.Software);

        public DataGridCollectionView O365Groups_All { get; } = new DataGridCollectionView(Program.GlobalConfig.O365Groups);
        public DataGridCollectionView ADPermissions_All { get; } = new DataGridCollectionView(Program.GlobalConfig.ADPermissions);
        public DataGridCollectionView O365Licenses_All { get; } = new DataGridCollectionView(Program.GlobalConfig.O365Licenses);
        public ObservableCollection<O365GroupModel> O365Groups_Selected { get; } = new();
        public ObservableCollection<ADPermissionModel> ADPermissions_Selected { get; } = new();
        public ObservableCollection<O365LicenseModel> O365Licenses_Selected { get; } = new();

        public ADPermissionFiltersModel ADPermissionsGridFilters { get; set; } = new ADPermissionFiltersModel();
        public O365GroupFiltersModel O365GroupGridFilters { get; set; } = new O365GroupFiltersModel();
        public O365LicenseFiltersModel O365LicenseGridFilters { get; set; } = new O365LicenseFiltersModel();

        public int CurrentSelectionType
        {
            get => _currentSelectionType;
            set
            {
                if (_currentSelectionType != value)
                {
                    DataGridSelection = -1;
                }
                this.RaiseAndSetIfChanged(ref _currentSelectionType, value);
            }
        }

        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }

        public void AddSoftware()
        {
            AddEditSoftware(null);
        }

        public void EditSoftware()
        {
            SoftwareModel CurrentSoftwareItem = CurrentSoftware.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditSoftware(CurrentSoftwareItem.Id);

            CurrentSoftware.Remove(CurrentSoftware.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditSoftware()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditSoftware(string? Id)
        {
            // TODO: Add in validation
            CurrentSoftware.Add(new SoftwareModel
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Name = AddNewPrimary,
                Permissions = ADPermissions_Selected.ToList(),
                O365Groups = O365Groups_Selected.ToList(),
                O365Licenses = O365Licenses_Selected.ToList()
            });

            ClearForm();
            Saveable = true;
        }
        
        private void ClearForm()
        {
            AddNewPrimary = "";
            CurrentPrimarySelected = -1;
            DataGridSelection = -1;
            O365Groups_Selected.Clear();
            ADPermissions_Selected.Clear();
            O365Licenses_Selected.Clear();
        }

        public void DeleteSoftware(int index)
        {
            CurrentSoftware.RemoveAt(index);
            Saveable = true;
        }

        public void RemoveO365Group(int index)
        {
            O365Groups_Selected.RemoveAt(index);
        }
        public void RemoveADPermissions(int index)
        {
            ADPermissions_Selected.RemoveAt(index);
        }
        public void RemoveO365License(int index)
        {
            O365Licenses_Selected.RemoveAt(index);
        }

        public void SaveSoftware()
        {
            if (Saveable)
            {
                Program.GlobalConfig.Software = CurrentSoftware.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Software Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }

        public void CurrentSoftware_DoubleClick(ListBoxItem row)
        {
            // Load software details
            SoftwareModel SelectedSoftware = (SoftwareModel)row.DataContext;
            EditID = SelectedSoftware.Id;
            int? SelectedIndex = CurrentPrimarySelected;

            ClearForm();

            CurrentPrimarySelected = SelectedIndex;
            AddNewPrimary = SelectedSoftware.Name;

            foreach (ADPermissionModel Permission in SelectedSoftware.Permissions)
            {
                ADPermissions_Selected.Add(Permission);
            }

            foreach (O365GroupModel Group in SelectedSoftware.O365Groups)
            {
                O365Groups_Selected.Add(Group);
            }

            foreach (O365LicenseModel License in SelectedSoftware.O365Licenses)
            {
                O365Licenses_Selected.Add(License);
            }
        }

        public void ADGroupRow_DoubleClick(DataGridRow row)
        {
            ADPermissionModel ADGroup = (ADPermissionModel)row.DataContext;
            ADPermissions_Selected.Add(ADGroup);
        }

        public void O365GroupRow_DoubleClick(DataGridRow row)
        {
            O365GroupModel O365Group = (O365GroupModel)row.DataContext;
            O365Groups_Selected.Add(O365Group);
        }

        public void O365LicenseRow_DoubleClick(DataGridRow row)
        {
            O365LicenseModel O365License = (O365LicenseModel)row.DataContext;
            O365Licenses_Selected.Add(O365License);
        }

        public bool FilterADPermissions(object permission)
        {
            foreach (var filterProp in ADPermissionsGridFilters.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
            {
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
                if (curValString == null || curValString.ToString() == null || string.IsNullOrWhiteSpace(curValString.ToString()) || !curValString.ToString().ToLower().Contains(filterVal.ToString().ToLower()))
                {
                    return false;
                }
            }

            return true;
        }
        public bool FilterO365Groups(object group)
        {
            foreach (var filterProp in O365GroupGridFilters.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
            {
                var filterVal = filterProp.GetValue(O365GroupGridFilters, null);
                if (filterVal == null || string.IsNullOrWhiteSpace(filterVal.ToString()))
                {
                    continue;
                }

                var curValue = group.GetType().GetProperty(filterProp.Name);
                if (curValue == null)
                {
                    return false;
                }
                var curValString = curValue.GetValue(group, null);
                if (curValString == null || curValString.ToString() == null || string.IsNullOrWhiteSpace(curValString.ToString()) || !curValString.ToString().ToLower().Contains(filterVal.ToString().ToLower()))
                {
                    return false;
                }
            }

            return true;
        }
        public bool FilterLicenses(object license)
        {
            foreach (var filterProp in O365LicenseGridFilters.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
            {
                var filterVal = filterProp.GetValue(O365LicenseGridFilters, null);
                if (filterVal == null || string.IsNullOrWhiteSpace(filterVal.ToString()))
                {
                    continue;
                }

                var curValue = license.GetType().GetProperty(filterProp.Name);
                if (curValue == null)
                {
                    return false;
                }
                var curValString = curValue.GetValue(license, null);
                if (curValString == null || curValString.ToString() == null || string.IsNullOrWhiteSpace(curValString.ToString()) || !curValString.ToString().ToLower().Contains(filterVal.ToString().ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        private async void DoADPermsFilter()
        {
            if (ADPermissions_All.CanFilter)
            {
                ADPermissions_All.Filter = FilterADPermissions;
                ADPermissions_All.Refresh();
            }
        }
        private async void DoO365GroupFilter()
        {
            if (O365Groups_All.CanFilter)
            {
                O365Groups_All.Filter = FilterO365Groups;
                O365Groups_All.Refresh();
            }
        }
        private async void DoLicenseFilter()
        {
            if (O365Licenses_All.CanFilter)
            {
                O365Licenses_All.Filter = FilterLicenses;
                O365Licenses_All.Refresh();
            }
        }

        public void ClearADTypeFilter()
        {
            ADPermissionsGridFilters.ITGType = null;
        }
        public void ClearO365TypeFilter()
        {
            O365GroupGridFilters.GroupType = null;
        }
    }
}
