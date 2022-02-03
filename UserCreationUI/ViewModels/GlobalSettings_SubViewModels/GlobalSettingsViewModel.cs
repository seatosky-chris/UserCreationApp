using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary;
using UserAppSharedLibrary;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class GlobalSettingsViewModel: ReactiveObject, IRoutableViewModel
    {
        private string _usernameFormat = Program.GlobalConfig.UsernameFormat ?? "[First].[Last]";
        private int _ADTypeSelected = (int)Enum.Parse(typeof(CompanyConfigurationSharedModel.ADTypeConfiguration), Program.GlobalConfig.ADType.ToString());
        private int _emailTypeSelected = (int)Enum.Parse(typeof(CompanyConfigurationSharedModel.EmailTypeConfiguration), Program.GlobalConfig.EmailType.ToString());
        private string _exchangeServerFQDN = Program.GlobalConfig.ExchangeServerFQDN ?? "";
        private bool _exchangeServerFQDNIsEnabled = Program.GlobalConfig.EmailType == CompanyConfigurationSharedModel.EmailTypeConfiguration.Exchange ? true : false;
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

        public void SaveGlobalSettings()
        {
            // Update loaded global config
            Program.GlobalConfig.UsernameFormat = UsernameFormat;
            Program.GlobalConfig.ExchangeServerFQDN = ExchangeServerFQDN;
            Program.GlobalConfig.PasswordExpiryOn = PasswordExpiry;
            Program.GlobalConfig.ADO365SyncOn = ADO365Sync;
            Program.GlobalConfig.ADType = (CompanyConfigurationSharedModel.ADTypeConfiguration)ADTypeSelected;
            Program.GlobalConfig.EmailType = (CompanyConfigurationSharedModel.EmailTypeConfiguration)EmailTypeSelected;

            // Code to save to DB here
            System.Diagnostics.Debug.WriteLine("Saving Global Settings");
        }
    }
}
