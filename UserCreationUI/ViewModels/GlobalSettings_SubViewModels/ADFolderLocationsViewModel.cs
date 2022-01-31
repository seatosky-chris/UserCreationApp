using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class ADFolderLocationsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "ADFolderLocationsEdit";

        private bool _includeSubfolders = false;

        public ADFolderLocationsViewModel(IScreen screen) : base(screen)
        {
            // TEST data
            CurrentFolderLocations.Add("Canmine - O365 (Location: Canmine Contracting)");
            CurrentFolderLocations.Add("High Standard Scaffolding - Users (Location: High Standard Scaffolding)");
            CurrentFolderLocations.Add("Pinnace Drilling - Burnaby (Location: Pinnacle Drilling - Burnaby)");
            CurrentFolderLocations.Add("Pinnace Drilling - Calgary (Location: Pinnacle Drilling - Calgary)");
        }

        public ObservableCollection<string> CurrentFolderLocations { get; } = new();

        public bool IncludeSubfolders
        {
            get => _includeSubfolders;
            set => this.RaiseAndSetIfChanged(ref _includeSubfolders, value);
        }

        public void AddFolderLocation()
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

            CurrentFolderLocations.Add(NewFormatString);
            AddNewPrimary = "";
            AddNewSecondary = "";
            IncludeSubfolders = false;
            Saveable = true;
        }

        public void DeleteFolderLocation(int index)
        {
            CurrentFolderLocations.RemoveAt(index);
            Saveable = true;
        }

        public void SaveADFolderLocations()
        {
            if (Saveable)
            {
                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving AD Folder Location Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
