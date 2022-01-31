using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class CompanyDefaultsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "CompanyDefaultsEdit";

        public CompanyDefaultsViewModel(IScreen screen) : base(screen)
        {
            // TEST data
            CurrentCompanies.Add("Canmine (Location: Canmine Contracting)");
            CurrentCompanies.Add("High Standard (Location: High Standard Scaffolding)");
            CurrentCompanies.Add("Pinnacle Drilling (Location: Pinnacle Drilling Products - Calgary, Pinnacle Drilling Products - Burnaby)");
            CurrentCompanies.Add("Traxxon (Location: Traxxon Rock Drills, Traxxon Foundation Equipment)");
        }

        public ObservableCollection<string> CurrentCompanies { get; } = new();

        public void AddCompany()
        {
            // TODO: Add in validation
            var LocationIndexes = SelectedLocations.SelectedIndexes;
            string NewFormatString = AddNewPrimary;

            if (LocationIndexes.Count > 0)
            {
                NewFormatString += " (Locations:";
                foreach (var i in LocationIndexes)
                {
                    NewFormatString += $" {Locations[i]}";
                }
                NewFormatString += ")";
            }

            CurrentCompanies.Add(NewFormatString);
            AddNewPrimary = "";
            Saveable = true;
        }

        public void DeleteCompany(int index)
        {
            CurrentCompanies.RemoveAt(index);
            Saveable = true;
        }

        public void SaveCompanies()
        {
            if (Saveable)
            {
                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Company Default Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
