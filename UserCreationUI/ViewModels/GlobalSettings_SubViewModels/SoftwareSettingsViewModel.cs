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
    public class SoftwareSettingsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "SoftwareSettingsEdit";

        private int _currentSelectionType = 0;
        private string _editID = "";

        public SoftwareSettingsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<SoftwareModel> CurrentSoftware { get; } = new ObservableCollection<SoftwareModel>(Program.GlobalConfig.Software);

        public ObservableCollection<O365GroupModel> O365Groups_All { get; } = new ObservableCollection<O365GroupModel>(Program.GlobalConfig.O365Groups);
        public ObservableCollection<ADPermissionModel> ADPermissions_All { get; } = new ObservableCollection<ADPermissionModel>(Program.GlobalConfig.ADPermissions);
        public ObservableCollection<O365LicenseModel> O365Licenses_All { get; } = new ObservableCollection<O365LicenseModel>(Program.GlobalConfig.O365Licenses);
        public ObservableCollection<O365GroupModel> O365Groups_Selected { get; } = new();
        public ObservableCollection<ADPermissionModel> ADPermissions_Selected { get; } = new();
        public ObservableCollection<O365LicenseModel> O365Licenses_Selected { get; } = new();

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
            SoftwareModel CurrentSoftware = (SoftwareModel)row.DataContext;
            EditID = CurrentSoftware.Id;
            AddNewPrimary = CurrentSoftware.Name;

            O365Groups_Selected.Clear();
            ADPermissions_Selected.Clear();
            O365Licenses_Selected.Clear();

            foreach (ADPermissionModel Permission in CurrentSoftware.Permissions)
            {
                ADPermissions_Selected.Add(Permission);
            }

            foreach (O365GroupModel Group in CurrentSoftware.O365Groups)
            {
                O365Groups_Selected.Add(Group);
            }

            foreach (O365LicenseModel License in CurrentSoftware.O365Licenses)
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

        public void OpenUrl(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
