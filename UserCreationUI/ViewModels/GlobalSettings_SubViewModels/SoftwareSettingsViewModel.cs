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
            // TEST data
            CurrentSoftware.Add(new SoftwareModel 
            {
                Id = "1",
                Name = "Microsoft Project"
            });
            CurrentSoftware.Add(new SoftwareModel
            {
                Id = "2",
                Name = "Microsoft Project Pro"
            });
            CurrentSoftware.Add(new SoftwareModel
            {
                Id = "3",
                Name = "Spectrum"
            });
            CurrentSoftware.Add(new SoftwareModel
            {
                Id = "4",
                Name = "Foundation 3000"
            });

            // Load data
            LoadData();
        }

        public ObservableCollection<SoftwareModel> CurrentSoftware { get; } = new();

        public ObservableCollection<O365GroupModel> O365Groups_All { get; } = new();
        public ObservableCollection<ADPermissionModel> ADPermissions_All { get; } = new();
        public ObservableCollection<O365LicenseModel> O365Licenses_All { get; } = new();
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

        public void AddEditSoftware(string? Id)
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
        
        public void ClearForm()
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

        private void LoadData()
        {
            LoadADGroups();
            LoadO365Groups();
            LoadO365Licenses();
        }

        private void LoadADGroups()
        {
            // Loads fake test data AD groups
            ADPermissions_All.Add(new ADPermissionModel
            {
                Guid = "72774d1f-3385-423e-ab59-4e42292fae0c",
                Name = "10GB_UserFolder_Quota",
                ADDescription = "Creates&maps a users folder and limits it to 10GB",
                ITGType = "File Share / Drive Mappings",
                ITGDescription = "Creates&maps a users folder and limits it to 10GB",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927420"
            });

            ADPermissions_All.Add(new ADPermissionModel
            {
                Guid = "4bd6e9a9-2291-439e-b797-fc8de1818dcd",
                Name = "AccountDetailRW",
                ADDescription = "",
                ITGType = "Other",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/20343806"
            });

            ADPermissions_All.Add(new ADPermissionModel
            {
                Guid = "cdaa64b1-552f-4d8c-9e5d-6b638f016b7a",
                Name = "B2W Addin Users",
                ADDescription = "",
                ITGType = "Applications",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927454"
            });

            ADPermissions_All.Add(new ADPermissionModel
            {
                Guid = "4efd4fcd-ac8d-4deb-b71b-29c59f3b563b",
                Name = "Cheque Printer Users",
                ADDescription = "",
                ITGType = "Printers",
                ITGDescription = "This group appears to do nothing anymore. At least it is not tied to any printer's from what I can tell.",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17925832"
            });
        }

        private void LoadO365Groups()
        {
            // Loads fake test data O365 groups
            O365Groups_All.Add(new O365GroupModel
            {
                ObjectID = "ef24c6fc-5046-4c28-8efe-aa0af0907133",
                Name = "NLL Employees Hourly",
                Email = "All-HourlyEmployees@norlandlimited.com",
                GroupType = O365GroupType.DL,
                O365Description = "",
                ITGDescription = "All employee's who are paid by the hour. This group is updated automatically by a PowerShell script.",
                ITGWhoToAdd = "Employee's who are paid by the hour",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/29950472"
            });

            O365Groups_All.Add(new O365GroupModel
            {
                ObjectID = "844a4b80-7acf-4d46-80e1-fa97a2e9f380",
                Name = "NLL Employees Salary",
                Email = "All-SalariedEmployees@norlandlimited.com",
                GroupType = O365GroupType.DL,
                O365Description = "",
                ITGDescription = "All employee's who are on salary. This group is updated automatically by a PowerShell script.",
                ITGWhoToAdd = "Employee's who are paid on a salary",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/29950475"
            });

            O365Groups_All.Add(new O365GroupModel
            {
                ObjectID = "f25f774c-e8f9-4ddf-ba1d-3135bd43524e",
                Name = "Pinnacle Accounting & Finance",
                Email = "PinnacleAccountingFinance@norlandlimited.com",
                GroupType = O365GroupType.M365Group,
                O365Description = "",
                ITGDescription = "Pinnacle Accounting & Finance",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/36778356"
            });

            O365Groups_All.Add(new O365GroupModel
            {
                ObjectID = "6e2f18ab-d7d6-40c8-8390-1d60d1166204",
                Name = "Spectrum Users",
                Email = "Spectrum-Users@norlandlimited.com",
                GroupType = O365GroupType.DL,
                O365Description = "",
                ITGDescription = "Sends email to all users of Spectrum.",
                ITGWhoToAdd = "Any users with a spectrum account.",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/17916087"
            });

            O365Groups_All.Add(new O365GroupModel
            {
                ObjectID = "e674d876-249b-4ab3-a2df-db951611c786",
                Name = "AP - High Standard",
                Email = "ap@highstandard.ca",
                GroupType = O365GroupType.SharedMailbox,
                O365Description = "",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/36779171"
            });

            O365Groups_All.Add(new O365GroupModel
            {
                ObjectID = "a2fd9dca-bbb2-4853-9533-fd5830b1d91e",
                Name = "B2WSupport",
                Email = "b2wsupport@norlandlimited.com",
                GroupType = O365GroupType.SharedMailbox,
                O365Description = "",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/36779129"
            });
        }

        private void LoadO365Licenses()
        {
            // Load fake test data O365 Licenses
            O365Licenses_All.Add(new O365LicenseModel
            {
                IDName = "AAD_PREMIUM",
                Name = "AZURE ACTIVE DIRECTORY PREMIUM P1"
            });

            O365Licenses_All.Add(new O365LicenseModel
            {
                IDName = "SMB_APPS",
                Name = "Business Apps (free)"
            });

            O365Licenses_All.Add(new O365LicenseModel
            {
                IDName = "EXCHANGESTANDARD",
                Name = "Exchange Online (Plan 1)"
            });

            O365Licenses_All.Add(new O365LicenseModel
            {
                IDName = "O365_BUSINESS",
                Name = "MICROSOFT 365 APPS FOR BUSINESS"
            });

            O365Licenses_All.Add(new O365LicenseModel
            {
                IDName = "O365_BUSINESS_PREMIUM",
                Name = "MICROSOFT 365 BUSINESS STANDARD"
            });

            O365Licenses_All.Add(new O365LicenseModel
            {
                IDName = "TEAMS_FREE",
                Name = "MICROSOFT TEAMS (FREE)"
            });
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
