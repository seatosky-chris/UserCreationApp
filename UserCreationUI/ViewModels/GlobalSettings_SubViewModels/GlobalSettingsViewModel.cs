using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary;
using UserAppSharedLibrary;
using FluentValidation.Results;
using UserCreationUI.ViewModels;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class GlobalSettingsViewModel: ReactiveObject, IRoutableViewModel
    {
        private string _usernameFormat = Program.GlobalConfig.UsernameFormat ?? "[First].[Last]";
        private int _ADTypeSelected = (int)Enum.Parse(typeof(CompanyConfigurationSharedModel.ADTypeConfiguration), Program.GlobalConfig.ADType.ToString());
        private int _emailTypeSelected = (int)Enum.Parse(typeof(CompanyConfigurationSharedModel.EmailTypeConfiguration), Program.GlobalConfig.EmailType.ToString());
        private string _exchangeServerFQDN = Program.GlobalConfig.ExchangeServerFQDN ?? "";
        private bool _exchangeServerFQDNIsEnabled = Program.GlobalConfig.EmailType == CompanyConfigurationSharedModel.EmailTypeConfiguration.Exchange;
        private bool _passwordExpiry = Program.GlobalConfig.PasswordExpiryOn;
        private bool _ADO365Sync = Program.GlobalConfig.ADO365SyncOn;

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "GlobalSettings";

        public GlobalSettingsViewModel(IScreen screen)
        {
            HostScreen = screen;

            this.WhenAnyValue(vm => vm.EmailTypeSelected)
                .Subscribe(x => ExchangeServerFQDNIsEnabled = x == 1);
        }

        public string UsernameFormat
        {
            get => _usernameFormat;
            set => this.RaiseAndSetIfChanged(ref _usernameFormat, value);
        }

        public int ADTypeSelected
        {
            get => _ADTypeSelected;
            set => this.RaiseAndSetIfChanged(ref _ADTypeSelected, value);
        }

        public int EmailTypeSelected
        {
            get => _emailTypeSelected;
            set => this.RaiseAndSetIfChanged(ref _emailTypeSelected, value);
        }

        public string ExchangeServerFQDN
        {
            get => _exchangeServerFQDN;
            set => this.RaiseAndSetIfChanged(ref _exchangeServerFQDN, value);
        }

        public bool ExchangeServerFQDNIsEnabled
        {
            get => _exchangeServerFQDNIsEnabled;
            set => this.RaiseAndSetIfChanged(ref _exchangeServerFQDNIsEnabled, value);
        }

        public bool PasswordExpiry
        {
            get => _passwordExpiry;
            set => this.RaiseAndSetIfChanged(ref _passwordExpiry, value);
        }

        public bool ADO365Sync
        {
            get => _ADO365Sync;
            set => this.RaiseAndSetIfChanged(ref _ADO365Sync, value);
        }


        // The counts of each setting type for the UI
        public static int EmailFormatsCount
        {
            get => Program.GlobalConfig.EmailFormats.Count;
        }
        public static int EmailDomainsCount
        {
            get => Program.GlobalConfig.EmailDomains.Count;
        }
        public static int CompaniesCount
        {
            get => Program.GlobalConfig.Companies.Count;
        }
        public static int DeparmentsCount
        {
            get => Program.GlobalConfig.Departments.Count;
        }
        public static int ADFoldersCount
        {
            get => Program.GlobalConfig.ADFolders.Count;
        }
        public static int SoftwareCount
        {
            get => Program.GlobalConfig.Software.Count;
        }
        public static int CustomFieldsCount
        {
            get
            {
                int count = 0;
                if (!String.IsNullOrWhiteSpace(Program.GlobalConfig.CustomFieldName1)) { count++; }
                if (!String.IsNullOrWhiteSpace(Program.GlobalConfig.CustomFieldName2)) { count++; }
                if (!String.IsNullOrWhiteSpace(Program.GlobalConfig.CustomFieldName3)) { count++; }
                if (!String.IsNullOrWhiteSpace(Program.GlobalConfig.CustomFieldName4)) { count++; }
                if (!String.IsNullOrWhiteSpace(Program.GlobalConfig.CustomFieldName5)) { count++; }
                return count;
            }
        }
        public static int ADPermissionSetsCount
        {
            get => Program.GlobalConfig.ADPermissionSets.Count;
        }
        public static int O365GroupSetsCount
        {
            get => Program.GlobalConfig.O365GroupSets.Count;
        }
        public static int O365LicenseSetsCount
        {
            get => Program.GlobalConfig.O365LicenseSets.Count;
        }

        public static string AzureO365APIIndicator
        {
            get => IsAzureO365APISet() ? "Set" : "Not Set";
        }
        public static string ITGAPIIndicator
        {
            get => IsITGAPISet() ? "Set" : "Not Set";
        }
        public static string EmailForwarderAPIIIndicator
        {
            get => IsEmailForwarderAPISet() ? "Set" : "Not Set";
        }

        public static bool AzureO365APIIndicatorClass
        {
            get => IsAzureO365APISet();
        }
        public static bool ITGAPIIndicatorClass
        {
            get => IsITGAPISet();
        }
        public static bool EmailForwarderAPIIIndicatorClass
        {
            get => IsEmailForwarderAPISet();
        }

        public static bool IsAzureO365APISet()
        {
            if (String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.EmailUsername) ||
                String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.AppID) ||
                String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.TenantID) ||
                String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.Organization) ||
                String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.CertificateThumbprint))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsITGAPISet()
        {
            if (Program.GlobalConfig.APICredentials.ITGCompanyID < 1 ||
                String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.ITGURL) ||
                String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.ITGKey))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsEmailForwarderAPISet()
        {
            if (String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.EmailForwarderURL) ||
                String.IsNullOrWhiteSpace(Program.GlobalConfig.APICredentials.EmailForwarderKey))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool SaveGlobalSettings()
        {
            CompanyConfigurationValidator validator = new();

            // Update loaded global config
            var newGlobalConfig = Program.GlobalConfig;
            newGlobalConfig.UsernameFormat = UsernameFormat;
            newGlobalConfig.ExchangeServerFQDN = ExchangeServerFQDN;
            newGlobalConfig.PasswordExpiryOn = PasswordExpiry;
            newGlobalConfig.ADO365SyncOn = ADO365Sync;
            newGlobalConfig.ADType = (CompanyConfigurationSharedModel.ADTypeConfiguration)ADTypeSelected;
            newGlobalConfig.EmailType = (CompanyConfigurationSharedModel.EmailTypeConfiguration)EmailTypeSelected;

            // Validate new global config
            ValidationResult validationResult = validator.Validate(newGlobalConfig);

            if (validationResult.IsValid)
            {
                Program.GlobalConfig = newGlobalConfig;
                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Global Settings");
                return true;
            }
            else
            {
                var hostVM = this.HostScreen;

                if (hostVM is SettingsWindowViewModel)
                {
                    SettingsWindowViewModel settingsVM = (SettingsWindowViewModel)hostVM;
                    settingsVM.ShowNotification("Data Validation Failed. Please fix the following errors:",
                        (validationResult.Errors.Count > 2 ? validationResult.ToString(" ") : validationResult.ToString()),
                        SettingsWindowViewModel.notificationType.Error);
                }
            }
            return false;
        }
    }
}
