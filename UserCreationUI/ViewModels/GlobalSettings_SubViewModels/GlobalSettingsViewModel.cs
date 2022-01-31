using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class GlobalSettingsViewModel: ReactiveObject, IRoutableViewModel
    {
        private string _usernameFormat = "[First].[Last]";
        private int _ADTypeSelected;
        private int _emailTypeSelected;
        private string? _exchangeServerFQDN;
        private bool _exchangeServerFQDNIsEnabled;
        private bool _passwordExpiry;
        private bool _ADO365Sync = false;

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
            // Code to save to DB here
            System.Diagnostics.Debug.WriteLine("Saving Global Settings");
        }
    }
}
