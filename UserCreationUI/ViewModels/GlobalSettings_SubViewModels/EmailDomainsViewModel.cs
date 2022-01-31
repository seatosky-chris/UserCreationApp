using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class EmailDomainsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "EmailFormatsEdit";

        public EmailDomainsViewModel(IScreen screen) : base(screen)
        {
            // TEST data
            CurrentDomains.Add("canmine.ca");
            CurrentDomains.Add("highstandard.ca");
            CurrentDomains.Add("pinnacledrilling.ca");
            CurrentDomains.Add("traxxon.com");
        }

        public ObservableCollection<string> CurrentDomains { get; } = new();

        public void AddDomain()
        {
            // TODO: Add in check to ensure this is a domain
            // also only enable the button if AddNewPrimary is a proper domain
            CurrentDomains.Add(AddNewPrimary);
            AddNewPrimary = "";
            Saveable = true;
        }

        public void DeleteDomain(int index)
        {
            CurrentDomains.RemoveAt(index);
            Saveable = true;
        }

        public void SaveDomains()
        {
            if (Saveable)
            {
                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Domain Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
