using Avalonia;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ReactiveCommand<Unit, IRoutableViewModel> GoAPICredentialsEditView { get; private set; }

        public ReactiveCommand<string, Unit> GoEditView { get; private set; }

        /// <summary>
        /// Initializes all of the navigation commands
        /// Ran in the constructor.
        /// </summary>
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
                default:
                    break;
            }
        }
    }
}
