using Avalonia.Controls;
using FluentValidation.Results;
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
        public new string UrlPathSegment { get; } = "DepartmentDefaultsEdit";

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
            if (CurrentDepartments is null)
                return;

            DepartmentDefaultModelExtended? CurrentDepartmentItem = CurrentDepartments.Where(x => x.Id == EditID).FirstOrDefault();

            if (CurrentDepartmentItem is null)
                return;

            AddEditDepartment(CurrentDepartmentItem.Id);

            CurrentDepartments.Remove(CurrentDepartmentItem);
            EditID = "";
        }

        public void CancelEditDepartment()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditDepartment(string? Id)
        {
            DepartmentDefaultValidator validator = new();
            var newDepartmentDefault = new DepartmentDefaultModelExtended
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Priority = 1,
                Department = AddNewPrimary,
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            };

            if (CurrentDepartments.Where(department => department.Department == newDepartmentDefault.Department).Any())
            {
                ShowError("That Department Name is already in use!", "Please choose a different department name.");
                return;
            }

            ValidationResult validationResult = validator.Validate(newDepartmentDefault);

            if (validationResult.IsValid)
            {
                CurrentDepartments.Add(newDepartmentDefault);

                ClearForm();
                Saveable = true;
            }
            else
            {
                ShowError("Data Validation Failed. Please fix the following errors:", (validationResult.Errors.Count > 2 ? validationResult.ToString(" ") : validationResult.ToString()));
            }
        }

        private void ClearForm()
        {
            AddNewPrimary = "";
            CurrentPrimarySelected = -1;
            SelectedLocations.Clear();
        }

        public void DeleteDepartment(int index)
        {
            CurrentDepartments.RemoveAt(index);
            Saveable = true;
        }

        public void CurrentDepartment_DoubleClick(ListBoxItem row)
        {
            if (row.DataContext is null)
                return;

            // Load format details
            DepartmentDefaultModelExtended SelectedDepartment = (DepartmentDefaultModelExtended)row.DataContext;
            EditID = SelectedDepartment.Id;
            int? SelectedIndex = CurrentPrimarySelected;

            ClearForm();

            CurrentPrimarySelected = SelectedIndex;
            AddNewPrimary = SelectedDepartment.Department;

            foreach (var LocIndex in SelectedDepartment.Locations)
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
