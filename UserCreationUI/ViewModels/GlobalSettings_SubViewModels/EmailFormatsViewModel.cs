using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class EmailFormatsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "EmailFormatsEdit";

        private List<string> _domains = new List<string>();
        private int _selectedDomain = 0;

        public EmailFormatsViewModel(IScreen screen) : base(screen)
        {
            // TEST data
            CurrentFormats.Add("[First].[Last]@canmine.ca (Location: Canmine Contracting)");
            CurrentFormats.Add("[First].[Last]@highstandard.ca (Location: High Standard Scaffolding)");
            CurrentFormats.Add("[First].[Last]@pinnacledrilling.ca (Location: Pinnacle Drilling)");
            CurrentFormats.Add("[First].[Last]@traxxon.ca (Location: Traxxon Rock Drills)");

            // Load domains
            Domains.Add("canmine.ca");
            Domains.Add("highstandard.ca");
            Domains.Add("norlandlimited.com");
            Domains.Add("pinnacledrilling.ca");
            Domains.Add("traxxon.com");
        }

        public ObservableCollection<string> CurrentFormats { get; } = new();

        public void AddFormat()
        {
            // TODO: Add in check to ensure this is a proper format
            // also only enable the button if AddNewPrimary is a proper format

            string Domain = Domains[SelectedDomain];
            var LocationIndexes = SelectedLocations.SelectedIndexes;
            var EmployeeTypeIndexes = SelectedEmployeeTypes.SelectedIndexes;
            string NewFormatString = $"{AddNewPrimary}@{Domain} (";

            if (LocationIndexes.Count > 0)
            {
                NewFormatString += "Locations:";
                foreach (var i in LocationIndexes)
                {
                    NewFormatString += $" {Locations[i]}";
                }
            }

            if (EmployeeTypeIndexes.Count > 0)
            {
                NewFormatString += "Employee Types:";
                foreach (var i in EmployeeTypeIndexes)
                {
                    NewFormatString += $" {EmployeeTypes[i]}";
                }
            }

            NewFormatString += ")";

            CurrentFormats.Add(NewFormatString);
            AddNewPrimary = "";
            Saveable = true;
        }

        public void DeleteFormat(int index)
        {
            CurrentFormats.RemoveAt(index);
            Saveable = true;
        }

        public void SaveFormats()
        {
            if (Saveable)
            {
                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Email Format Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }

        public List<string> Domains
        {
            get => _domains;
            set => this.RaiseAndSetIfChanged(ref _domains, value);
        }
        public int SelectedDomain
        {
            get => _selectedDomain;
            set => this.RaiseAndSetIfChanged(ref _selectedDomain, value);
        }
    }
}
