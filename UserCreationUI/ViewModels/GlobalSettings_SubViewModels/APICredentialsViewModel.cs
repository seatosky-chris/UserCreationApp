using Avalonia;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Runtime.InteropServices;
using UserAppSharedLibrary;
using static UserCreationUI.Utilities.UIFunctions;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class APICredentialsViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _o365LoginEmail = Program.GlobalConfig.APICredentials.EmailUsername ?? "";
        private string _o365AppID = Program.GlobalConfig.APICredentials.AppID ?? "";
        private string _o365TenantID = Program.GlobalConfig.APICredentials.TenantID ?? "";
        private string _o365Organization = Program.GlobalConfig.APICredentials.Organization ?? "";
        private string _o365CertThumbprint = Program.GlobalConfig.APICredentials.CertificateThumbprint ?? "";
        private int _itgCompanyID = Program.GlobalConfig.APICredentials.ITGCompanyID;
        private string _itgURL = Program.GlobalConfig.APICredentials.ITGURL ?? "";
        private string _itgKey = Program.GlobalConfig.APICredentials.ITGKey ?? "";
        private string _emailForwarderURL = Program.GlobalConfig.APICredentials.EmailForwarderURL ?? "";
        private string _emailForwarderKey = Program.GlobalConfig.APICredentials.EmailForwarderKey ?? "";

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "ApiCredentialsEdit";

        public APICredentialsViewModel(IScreen screen)
        {
            HostScreen = screen;
        }

        public string O365LoginEmail
        {
            get => _o365LoginEmail;
            set => this.RaiseAndSetIfChanged(ref _o365LoginEmail, value);
        }
        
        public string O365AppID
        {
            get => _o365AppID;
            set => this.RaiseAndSetIfChanged(ref _o365AppID, value);
        }
        
        public string O365TenantID
        {
            get => _o365TenantID;
            set => this.RaiseAndSetIfChanged(ref _o365TenantID, value);
        }
        
        public string O365Organization
        {
            get => _o365Organization;
            set => this.RaiseAndSetIfChanged(ref _o365Organization, value);
        }
        
        public string O365CertThumbprint
        {
            get => _o365CertThumbprint;
            set => this.RaiseAndSetIfChanged(ref _o365CertThumbprint, value);
        }
        
        public int ITGCompanyID
        {
            get => _itgCompanyID;
            set => this.RaiseAndSetIfChanged(ref _itgCompanyID, value);
        }
        
        public string ITGURL
        {
            get => _itgURL;
            set => this.RaiseAndSetIfChanged(ref _itgURL, value);
        }
        
        public string ITGKey
        {
            get => _itgKey;
            set => this.RaiseAndSetIfChanged(ref _itgKey, value);
        }
        
        public string EmailForwarderURL
        {
            get => _emailForwarderURL;
            set => this.RaiseAndSetIfChanged(ref _emailForwarderURL, value);
        }
        
        public string EmailForwarderKey
        {
            get => _emailForwarderKey;
            set => this.RaiseAndSetIfChanged(ref _emailForwarderKey, value);
        }

        public void SaveAPICredentials()
        {
            // If loaded, modify instead (to keep data from disablement app)
            Program.GlobalConfig.APICredentials.ITGCompanyID = ITGCompanyID;
            Program.GlobalConfig.APICredentials.ITGURL = ITGURL;
            Program.GlobalConfig.APICredentials.ITGKey = ITGKey;

            Program.GlobalConfig.APICredentials.EmailForwarderURL = EmailForwarderURL;
            Program.GlobalConfig.APICredentials.EmailForwarderKey = EmailForwarderKey;

            Program.GlobalConfig.APICredentials.EmailUsername = O365LoginEmail;
            Program.GlobalConfig.APICredentials.AppID = O365AppID;
            Program.GlobalConfig.APICredentials.TenantID = O365TenantID;
            Program.GlobalConfig.APICredentials.Organization = O365Organization;
            Program.GlobalConfig.APICredentials.CertificateThumbprint = O365CertThumbprint;

            // Code to save data

            System.Diagnostics.Debug.WriteLine("Saving API Creds");
            HostScreen.Router.NavigateBack.Execute(Unit.Default);
        }

        public void BackButton()
        {
            HostScreen.Router.NavigateBack.Execute(Unit.Default);
        }

        public void OpenUrl(string url)
        {
            _openUrl(url);
        }
    }
}
