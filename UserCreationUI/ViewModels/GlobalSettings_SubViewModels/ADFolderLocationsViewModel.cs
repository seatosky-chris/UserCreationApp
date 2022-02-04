using Avalonia.Controls;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using UserCreationUI.Models.ExtendedModels;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class ADFolderLocationsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "ADFolderLocationsEdit";

        private bool _includeSubfolders = false;
        private string _editID = "";

        public ADFolderLocationsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<ADFolderDefaultModelExtended> CurrentFolderLocations { get; } = new ObservableCollection<ADFolderDefaultModelExtended>(Program.GlobalConfig.ADFolders);

        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }

        public bool IncludeSubfolders
        {
            get => _includeSubfolders;
            set => this.RaiseAndSetIfChanged(ref _includeSubfolders, value);
        }

        public void AddFolderLocation()
        {
            AddEditFolderLocation(null);
        }

        public void EditFolder()
        {
            ADFolderDefaultModelExtended CurrentFolderItem = CurrentFolderLocations.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditFolderLocation(CurrentFolderItem.Id);

            CurrentFolderLocations.Remove(CurrentFolderLocations.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditFolder()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditFolderLocation(string? Id)
        {
            // TODO: Add in validation

            CurrentFolderLocations.Add(new ADFolderDefaultModelExtended
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Priority = 1,
                FolderName = AddNewPrimary,
                FolderLocation = AddNewSecondary,
                IncludeSubfolders = IncludeSubfolders,
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            });

            ClearForm();
            Saveable = true;
        }

        private void ClearForm()
        {
            AddNewPrimary = "";
            AddNewSecondary = "";
            IncludeSubfolders = false;
            SelectedLocations.Clear();
        }

        public void DeleteFolderLocation(int index)
        {
            CurrentFolderLocations.RemoveAt(index);
            Saveable = true;
        }

        public void CurrentFolder_DoubleClick(ListBoxItem row)
        {
            // Load format details
            ADFolderDefaultModelExtended CurrentFolder = (ADFolderDefaultModelExtended)row.DataContext;
            EditID = CurrentFolder.Id;

            ClearForm();

            AddNewPrimary = CurrentFolder.FolderName;
            AddNewSecondary = CurrentFolder.FolderLocation;
            IncludeSubfolders = CurrentFolder.IncludeSubfolders;

            foreach (var LocIndex in CurrentFolder.Locations)
            {
                SelectedLocations.Select(LocIndex);
            }
        }

        public void SaveADFolderLocations()
        {
            if (Saveable)
            {
                Program.GlobalConfig.ADFolders = CurrentFolderLocations.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving AD Folder Location Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
