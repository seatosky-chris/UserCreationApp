using Avalonia;
using Avalonia.Notification;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using UserCreationUI.GlobalSettings.ViewModels;


namespace UserCreationUI.ViewModels
{
    public class SettingsWindowViewModel: ReactiveObject, IScreen
    {


        // The Router for moving between the various settings views
        public RoutingState Router { get; }

        // The notifications manager
        public INotificationMessageManager NotificationsManager { get; } = new NotificationMessageManager();

        public SettingsWindowViewModel()
        {
            // Initialize the Router.
            Router = new RoutingState();

            // Setup navigation commands
            SetupNavigationCommands();

           
        }

        // Navigation commands
        public ReactiveCommand<Unit, IRoutableViewModel> GoGlobalSettingsView { get; private set; }
        public ReactiveCommand<Unit, IRoutableViewModel?> GoBack { get; private set; }

        public ReactiveCommand<string, Unit> GoEditView { get; private set; }

        public enum notificationType
        {
           Info,
           Success,
           Alert,
           Warning,
           Error
        }

        public void ShowNotification(string header, string message, notificationType type, bool animate = true, string buttonTxt = "Close", int autoClose = 5)
        {
            string accent = "";
            string background = "";
            string foreground = "";

            switch (type)
            {
                case notificationType.Success:
                    accent = "#006400";
                    background = "#BCF5BC";
                    foreground = "#006400";
                    break;
                case notificationType.Alert:
                    accent = "#444";
                    background = "#dedede";
                    foreground = "#444";
                    break;
                case notificationType.Warning:
                    accent = "#826200";
                    background = "#FFEAA8";
                    foreground = "#826200";
                    break;
                case notificationType.Error:
                    accent = "#C62828";
                    background = "#F44336";
                    foreground = "#FFFFFF";
                    break;
                case notificationType.Info:
                default:
                    accent = "#3badd6";
                    background = "#78C5E7";
                    foreground = "#FFFFFF";
                    break;
            }
            
            this.NotificationsManager
                   .CreateMessage()
                   .Accent(accent)
                   .Background(background)
                   .Foreground(foreground)
                   .HasBadge(type.ToString())
                   .Animates(animate)
                   .HasHeader(header)
                   .HasMessage(message)
                   .Dismiss().WithButton(buttonTxt, button => { })
                   .Dismiss().WithDelay(System.TimeSpan.FromSeconds(autoClose))
                   .Queue();
            return;
        }

        /// <summary>
        /// Initializes all of the navigation commands
        /// Ran in the constructor.
        /// </summary>
        [MemberNotNull(nameof(GoGlobalSettingsView), nameof(GoBack), nameof(GoEditView))]
        private void SetupNavigationCommands()
        {
            // Setup navigation commands
            GoGlobalSettingsView = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new GlobalSettingsViewModel(this))
            );

            var canGoBack = this
            .WhenAnyValue(x => x.Router.NavigationStack.Count)
            .Select(count => count > 0);

            GoBack = ReactiveCommand.CreateFromObservable(
                () => Router.NavigateBack.Execute(Unit.Default),
                canGoBack
            );
            
            GoEditView = ReactiveCommand.Create<string>(
                (viewType) => ChangeEditView(viewType)
            );
        }

        private void ChangeEditView(string viewType)
        {
            switch (viewType)
            {
                case "APICredentials":
                    Router.Navigate.Execute(new APICredentialsViewModel(this));
                    break;
                case "EmailDomains":
                    Router.Navigate.Execute(new EmailDomainsViewModel(this));
                    break;
                case "EmailFormats":
                    Router.Navigate.Execute(new EmailFormatsViewModel(this));
                    break;
                case "CompanyDefaults":
                    Router.Navigate.Execute(new CompanyDefaultsViewModel(this));
                    break;
                case "DepartmentDefaults":
                    Router.Navigate.Execute(new DepartmentDefaultsViewModel(this));
                    break;
                case "ADFolderLocations":
                    Router.Navigate.Execute(new ADFolderLocationsViewModel(this));
                    break;
                case "SoftwareSettings":
                    Router.Navigate.Execute(new SoftwareSettingsViewModel(this));
                    break;
                case "CustomFields":
                    Router.Navigate.Execute(new CustomFieldsViewModel(this));
                    break;
                case "ADPermissionSets":
                    Router.Navigate.Execute(new ADPermissionSetsViewModel(this));
                    break;
                case "O365GroupSets":
                    Router.Navigate.Execute(new O365GroupSetsViewModel(this));
                    break;
                case "O365LicenseSets":
                    Router.Navigate.Execute(new O365LicenseSetsViewModel(this));
                    break;
                default:
                    break;
            }
        }
    }
}
