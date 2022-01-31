using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class DepartmentDefaultsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "DepartmentDefaultsEdit";

        public DepartmentDefaultsViewModel(IScreen screen) : base(screen)
        {
            // TEST data
            CurrentDepartments.Add("Demolition (Location: Norland Limited)");
            CurrentDepartments.Add("Business Development (Location: Norland Limited)");
            CurrentDepartments.Add("High Standard Scaffolding (Location: High Standard Scaffolding)");
            CurrentDepartments.Add("Civil (Location: BEL Contracting)");
        }

        public ObservableCollection<string> CurrentDepartments { get; } = new();

        public void AddDepartment()
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

            CurrentDepartments.Add(NewFormatString);
            AddNewPrimary = "";
            Saveable = true;
        }

        public void DeleteDepartment(int index)
        {
            CurrentDepartments.RemoveAt(index);
            Saveable = true;
        }

        public void SaveDepartments()
        {
            if (Saveable)
            {
                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Department Default Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
