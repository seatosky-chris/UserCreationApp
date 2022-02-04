using Avalonia.Controls;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using UserCreationLibrary;
using UserCreationUI.Models.ExtendedModels;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class DepartmentDefaultsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "DepartmentDefaultsEdit";

        private string _editID = "";

        public DepartmentDefaultsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<DepartmentDefaultModelExtended> CurrentDepartments { get; } = new ObservableCollection<DepartmentDefaultModelExtended>(Program.GlobalConfig.Departments);

        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }

        public void AddDepartment()
        {
            AddEditDepartment(null);
        }

        public void EditDepartment()
        {
            DepartmentDefaultModelExtended CurrentDepartmentItem = CurrentDepartments.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditDepartment(CurrentDepartmentItem.Id);

            CurrentDepartments.Remove(CurrentDepartments.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditDepartment()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditDepartment(string? Id)
        {
            // TODO: Add in validation

            CurrentDepartments.Add(new DepartmentDefaultModelExtended
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Priority = 1,
                Department = AddNewPrimary,
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            });

            ClearForm();
            Saveable = true;
        }

        private void ClearForm()
        {
            AddNewPrimary = "";
            SelectedLocations.Clear();
        }

        public void DeleteDepartment(int index)
        {
            CurrentDepartments.RemoveAt(index);
            Saveable = true;
        }

        public void CurrentDepartment_DoubleClick(ListBoxItem row)
        {
            // Load format details
            DepartmentDefaultModelExtended CurrentDepartment = (DepartmentDefaultModelExtended)row.DataContext;
            EditID = CurrentDepartment.Id;

            ClearForm();

            AddNewPrimary = CurrentDepartment.Department;

            foreach (var LocIndex in CurrentDepartment.Locations)
            {
                SelectedLocations.Select(LocIndex);
            }
        }

        public void SaveDepartments()
        {
            if (Saveable)
            {
                Program.GlobalConfig.Departments = CurrentDepartments.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Department Default Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
