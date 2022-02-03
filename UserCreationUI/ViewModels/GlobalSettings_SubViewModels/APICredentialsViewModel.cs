using Avalonia;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Runtime.InteropServices;
using UserAppSharedLibrary;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class APICredentialsViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _o365LoginEmail;
        private string _o365AppID;
        private string _o365TenantID;
        private string _o365Organization;
        private string _o365CertThumbprint;
        private int _itgCompanyID;
        private string _itgURL;
        private string _itgKey;
        private string _emailForwarderURL;
        private string _emailForwarderKey;

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
            APICredentialsModel NewAPICreds = new APICredentialsModel
            {
                ITGCompanyID = ITGCompanyID,
                ITGURL = ITGURL,
                ITGKey = ITGKey,

                EmailForwarderURL = EmailForwarderURL,
                EmailForwarderKey = EmailForwarderKey,

                EmailUsername = O365LoginEmail,
                AppID = O365AppID,
                TenantID = O365TenantID,
                Organization = O365Organization,
                CertificateThumbprint = O365CertThumbprint
            };

            // Code to save data

            System.Diagnostics.Debug.WriteLine("Saving API Creds");
            HostScreen.Router.NavigateBack.Execute(Unit.Default);
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

        public void BackButton()
        {
            HostScreen.Router.NavigateBack.Execute(Unit.Default);
        }
    }
}
