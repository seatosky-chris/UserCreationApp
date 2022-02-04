using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using ReactiveUI;
using Splat;
using System;
using System.Collections.Generic;
using System.Reflection;
using UserAppSharedLibrary;
using UserCreationLibrary;
using UserCreationUI.Models.ExtendedModels;

namespace UserCreationUI
{
    class Program
    {
        public static CompanyConfigurationModelExtended GlobalConfig;

        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());

            // Load config
            LoadGlobalConfig();

            return AppBuilder.Configure<App>()
                  .UsePlatformDetect()
                  .LogToTrace()
                  .UseReactiveUI();
        }

        private static void LoadGlobalConfig()
        {
            // TODO: Load from DB, currently all static test data
            GlobalConfig = new CompanyConfigurationModelExtended
            {
                UsernameFormat = "[First].[Last]",
                ExchangeServerFQDN = "",
                PasswordExpiryOn = true,
                ADO365SyncOn = true,
                ADType = CompanyConfigurationSharedModel.ADTypeConfiguration.Azure,
                EmailType = CompanyConfigurationSharedModel.EmailTypeConfiguration.O365,
                APICredentials = new APICredentialsModel()
            };

            // Email formats test data
            GlobalConfig.EmailFormats.Add(new EmailDefaultModelExtended
            {
                Id = "1",
                Priority = 1,
                EmailFormat = "[First].[Last]",
                Domain = "canmine.ca",
                Locations = new List<int> { 0 }
            });
            GlobalConfig.EmailFormats.Add(new EmailDefaultModelExtended
            {
                Id = "2",
                Priority = 1,
                EmailFormat = "[First].[Last]",
                Domain = "highstandard.ca",
                Locations = new List<int> { 1 }
            });
            GlobalConfig.EmailFormats.Add(new EmailDefaultModelExtended
            {
                Id = "3",
                Priority = 1,
                EmailFormat = "[First].[Last]",
                Domain = "pinnacledrilling.ca",
                Locations = new List<int> { 3 }
            });
            GlobalConfig.EmailFormats.Add(new EmailDefaultModelExtended
            {
                Id = "4",
                Priority = 1,
                EmailFormat = "[First].[Last]",
                Domain = "traxxon.ca",
                Locations = new List<int> { 4 }
            });

            // Email domains test data
            GlobalConfig.EmailDomains = new List<string> { "canmine.ca", "highstandard.ca", "pinnacledrilling.ca", "traxxon.com" };

            // Department defaults test data
            GlobalConfig.Departments.Add(new DepartmentDefaultModel
            {
                Id = "1",
                Priority = 1,
                Department = "Business Development",
                Locations = new List<int> { 0 }
            });
            GlobalConfig.Departments.Add(new DepartmentDefaultModel
            {
                Id = "2",
                Priority = 1,
                Department = "High Standard Scaffolding",
                Locations = new List<int> { 2 }
            });
            GlobalConfig.Departments.Add(new DepartmentDefaultModel
            {
                Id = "3",
                Priority = 1,
                Department = "Civil",
                Locations = new List<int> { 7 }
            });

            // Company defaults test data
            GlobalConfig.Companies.Add(new CompanyDefaultModel
            {
                Id = "1",
                Priority = 1,
                Company = "Canmine",
                Locations = new List<int> { 1 }
            });
            GlobalConfig.Companies.Add(new CompanyDefaultModel
            {
                Id = "2",
                Priority = 1,
                Company = "High Standard",
                Locations = new List<int> { 2 }
            });
            GlobalConfig.Companies.Add(new CompanyDefaultModel
            {
                Id = "3",
                Priority = 1,
                Company = "Pinnacle Drilling",
                Locations = new List<int> { 3, 4 }
            });
            GlobalConfig.Companies.Add(new CompanyDefaultModel
            {
                Id = "4",
                Priority = 1,
                Company = "Traxxon",
                Locations = new List<int> { 6 }
            });

            // AD Folders test data
            GlobalConfig.ADFolders.Add(new ADFolderDefaultModel
            {
                Id = "1",
                Priority = 1,
                FolderName = "Canmine - O365",
                FolderLocation = "pacificblasting.local/Divisions/CanMine/Users/Office 365",
                Locations = new List<int> { 1 }
            });
            GlobalConfig.ADFolders.Add(new ADFolderDefaultModel
            {
                Id = "2",
                Priority = 1,
                FolderName = "High Standard Scaffolding",
                FolderLocation = "pacificblasting.local/Divisions/High Standard Scaffolding/Users",
                Locations = new List<int> { 2 }
            });
            GlobalConfig.ADFolders.Add(new ADFolderDefaultModel
            {
                Id = "3",
                Priority = 1,
                FolderName = "Pinnace Drilling - Burnaby",
                FolderLocation = "pacificblasting.local/Divisions/Pinnacle Drilling/Users/Burnaby",
                Locations = new List<int> { 3 }
            });
            GlobalConfig.ADFolders.Add(new ADFolderDefaultModel
            {
                Id = "4",
                Priority = 1,
                FolderName = "Pinnace Drilling - Calgary",
                FolderLocation = "pacificblasting.local/Divisions/Pinnacle Drilling/Users/Calgary",
                Locations = new List<int> { 4 }
            });

            // Employee types test data (load from ITG)
            GlobalConfig.EmployeeTypes = new Dictionary<int, string>()
            {
                [0] = "Employee - Full Time",
                [1] = "Employee - Part Time",
                [2] = "Employee - Email Only",
                [3] = "External User"
            };

            // Locations test data (load from ITG)
            GlobalConfig.Locations = new Dictionary<int, string>()
            {
                [0] = "Norland Limited - Primary Location",
                [1] = "CanMine Contracting",
                [2] = "High Standard Scaffolding",
                [3] = "Pinnacle Drilling Products - Burnaby",
                [4] = "Pinnacle Drilling Products - Calgary",
                [5] = "Traxxon Rock Drills",
                [6] = "Traxxon Foundation Equipment",
                [7] = "Bel Contracting"
            };

            // Computers test data (load from ITG)
            GlobalConfig.Computers = new Dictionary<int, string>()
            {
                [0] = "Computer 1",
                [1] = "Computer 2",
                [2] = "Computer 3",
                [3] = "Computer 4",
                [4] = "Computer 5",
            };

            // AD Permissions test data (load from AD or Azure & ITG)
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "82bc8f7a-874e-4a88-8df0-0f4315d71d73",
                Name = "PGC-TS-Users",
                ADDescription = "",
                ITGType = "Computer Access",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover =  "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927566"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "ef8c503d-5b22-4c40-a28a-6f703b567383",
                Name = "Pinnacle Drilling",
                ADDescription = "",
                ITGType = "Team / Division",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927567"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "ed74ce1d-5c9a-45de-b176-cc0c70be7252",
                Name = "Printer-PDP-CGY2-PRN01",
                ADDescription = "",
                ITGType = "Printers",
                ITGDescription = "Used to deploy printer PDP-CGY2-PRN01. There is no share for this anymore so it will not work.",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/18640289"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "ce997a10-4918-43f2-ae3f-4fbe8fe9db9d",
                Name = "Share-Media-Pinnacle",
                ADDescription = "",
                ITGType = "File Share / Drive Mappings",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927635"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
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
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "41c2ed49-07f9-4c6d-b857-098638697f1c",
                Name = "Administrators",
                ADDescription = "Administrators have complete and unrestricted access to the computer/domain",
                ITGType = "Built-In",
                ITGDescription = "Administrators have complete and unrestricted access to the computer/domain",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927442"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "2ef1f914-3970-458f-97a2-25e6f0528cfd",
                Name = "Bel Contracting2",
                ADDescription = "",
                ITGType = "Team / Division",
                ITGDescription = "Deploys the 3 copiers at Bel and gives access to the Bel private drive.",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927469"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
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
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "8c3dd5ab-de3b-48b5-8b8a-9895bb82926a",
                Name = "CMFTP-Users",
                ADDescription = "",
                ITGType = "Applications",
                ITGDescription = "Gives access to the CanMine OS FTP",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927480"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "3d7fb3c0-7b87-4b95-bdc9-e16162c1b431",
                Name = "Domain Guests",
                ADDescription = "All domain guests",
                ITGType = "Permissions",
                ITGDescription = "All domain guests",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17928475"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "00007864-87bc-4835-9e62-3e6ef7328294",
                Name = "PGC-RDPUsers",
                ADDescription = "",
                ITGType = "Remote Access",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/17927565"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "909da31c-fad8-4634-98e9-222beb5ca085",
                Name = "NorlandFileSharing",
                ADDescription = "",
                ITGType = "Applications",
                ITGDescription = "Access to Liquid Files",
                ITGWhoToAdd = "Anyone authorized to use Liquid Files",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/174160-ad-security-groups/records/13809623"
            });
            GlobalConfig.ADPermissions.Add(new ADPermissionModel
            {
                Guid = "0fba569f-d354-4f42-be50-ac522cf6e9a0",
                Name = "Foundation_Users",
                ADDescription = "",
                ITGType = "Applications",
                ITGDescription = "Gives access to Foundation",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/records/17928493"
            });

            // O365 Groups test data (load from O365 or Exchange & ITG)
            GlobalConfig.O365Groups.Add(new O365GroupModel
            {
                ObjectID = "ef24c6fc-5046-4c28-8efe-aa0af0907133",
                Name = "NLL Employees Hourly",
                Email = "All-HourlyEmployees@norlandlimited.com",
                GroupType = O365GroupType.DL,
                O365Description = "",
                ITGDescription = "All employee's who are paid by the hour. This group is updated automatically by a PowerShell script.",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/29950472"
            });
            GlobalConfig.O365Groups.Add(new O365GroupModel
            {
                ObjectID = "844a4b80-7acf-4d46-80e1-fa97a2e9f380",
                Name = "NLL Employees Salary",
                Email = "NLL-Emps-Salary@norlandlimited.com",
                GroupType = O365GroupType.DL,
                O365Description = "",
                ITGDescription = "All employee's who are on salary. This group is updated automatically by a PowerShell script.",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/29950475"
            });
            GlobalConfig.O365Groups.Add(new O365GroupModel
            {
                ObjectID = "305f568b-794a-4cd1-900a-8e0ad5b08daf",
                Name = "Pinnacle Drilling Burnaby",
                Email = "PinnacleDrillingBurnaby@norlandlimited.com",
                GroupType = O365GroupType.DL,
                O365Description = "",
                ITGDescription = "",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/36778732"
            });
            GlobalConfig.O365Groups.Add(new O365GroupModel
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
            GlobalConfig.O365Groups.Add(new O365GroupModel
            {
                ObjectID = "6e2f18ab-d7d6-40c8-8390-1d60d1166204",
                Name = "Spectrum Users",
                Email = "Spectrum-Users@norlandlimited.com",
                GroupType = O365GroupType.DL,
                O365Description = "",
                ITGDescription = "Sends email to all users of Spectrum.",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/17916087"
            });
            GlobalConfig.O365Groups.Add(new O365GroupModel
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
            GlobalConfig.O365Groups.Add(new O365GroupModel
            {
                ObjectID = "",
                Name = "B2W Support And Operations",
                Email = "2e42701c-55ba-4b96-b15b-2df904d7d458",
                GroupType = O365GroupType.M365Group,
                O365Description = "",
                ITGDescription = "Site to collaborate support and operations items",
                ITGWhoToAdd = "",
                ITGApprover = "",
                ITGLink = "https://seatosky.itglue.com/3136459/assets/197503-email-groups/records/36778324"
            });

            // O365 Licenses test data (load from config or direct from microsofts page)
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "AAD_PREMIUM",
                Name = "AZURE ACTIVE DIRECTORY PREMIUM P1"
            });
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "SMB_APPS",
                Name = "Business Apps (free)"
            });
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "EXCHANGESTANDARD",
                Name = "Exchange Online (Plan 1)"
            });
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "O365_BUSINESS",
                Name = "MICROSOFT 365 APPS FOR BUSINESS"
            });
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "O365_BUSINESS_PREMIUM",
                Name = "MICROSOFT 365 BUSINESS STANDARD"
            });
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "TEAMS_FREE",
                Name = "MICROSOFT TEAMS (FREE)"
            });
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "PROJECTESSENTIALS",
                Name = "Project Online Essentials"
            });
            GlobalConfig.O365Licenses.Add(new O365LicenseModel
            {
                IDName = "PROJECTPROFESSIONAL",
                Name = "Project Plan 3"
            });

            // AD permission sets test data

            // O365 group sets test data

            // O365 license sets test data

            // Software test data
            GlobalConfig.Software.Add(new SoftwareModel
            {
                Id = "1",
                Name = "Microsoft Project",
                O365Licenses = new List<O365LicenseModel> { GlobalConfig.O365Licenses.Find(license => license.IDName == "PROJECTESSENTIALS") }
            });
            GlobalConfig.Software.Add(new SoftwareModel
            {
                Id = "2",
                Name = "Microsoft Project Pro",
                O365Licenses = new List<O365LicenseModel> { GlobalConfig.O365Licenses.Find(license => license.IDName == "PROJECTPROFESSIONAL") }
            });
            GlobalConfig.Software.Add(new SoftwareModel
            {
                Id = "3",
                Name = "Spectrum",
                O365Groups = new List<O365GroupModel> { GlobalConfig.O365Groups.Find(group => group.Name == "Spectrum Users") }
            });
            GlobalConfig.Software.Add(new SoftwareModel
            {
                Id = "4",
                Name = "Foundation 3000",
                Permissions = new List<ADPermissionModel> { GlobalConfig.ADPermissions.Find(permission => permission.Name == "Foundation_Users" || permission.Name == "PGC-RDPUsers") }
            });
        }
    }
}
