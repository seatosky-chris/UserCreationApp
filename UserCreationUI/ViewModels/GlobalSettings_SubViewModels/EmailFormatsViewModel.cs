using Avalonia.Controls;
using FluentValidation.Results;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using UserCreationLibrary;
using UserCreationUI.Models.ExtendedModels;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class EmailFormatsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "EmailFormatsEdit";

        private List<string> _domains = Program.GlobalConfig.EmailDomains;
        private int _selectedDomain = 0;
        private string _editID = "";

        public EmailFormatsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<EmailDefaultModelExtended> CurrentFormats { get; } = new ObservableCollection<EmailDefaultModelExtended>(Program.GlobalConfig.EmailFormats);

        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }

        public void AddFormat()
        {
            AddEditFormat(null);
        }

        public void EditFormat()
        {
            if (CurrentFormats is null)
                return;

            EmailDefaultModelExtended? CurrentFormatItem = CurrentFormats.Where(x => x.Id == EditID).FirstOrDefault();

            if (CurrentFormatItem is null)
                return;

            AddEditFormat(CurrentFormatItem.Id);

            CurrentFormats.Remove(CurrentFormatItem);
            EditID = "";
        }

        public void CancelEditFormat()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditFormat(string? Id)
        {
            // TODO: Add in check to ensure this is a proper format
            // also only enable the button if AddNewPrimary is a proper format
            EmailDefaultValidator validator = new();
            var newEmailDefault = new EmailDefaultModelExtended
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Priority = 1,
                EmailFormat = AddNewPrimary,
                Domain = Domains[SelectedDomain],
                EmployeeTypes = (from employee in SelectedEmployeeTypes.SelectedItems select employee.Key).Distinct().ToList(),
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            };

            if (CurrentFormats.Where(set => set.EmailTemplate == $"{newEmailDefault.EmailFormat}@{newEmailDefault.Domain}").Any())
            {
                ShowError("That Email Format / Domain combo is already in use!", "You tried to create a duplicate.");
                return;
            }

            ValidationResult validationResult = validator.Validate(newEmailDefault);

            if (validationResult.IsValid)
            {
                CurrentFormats.Add(newEmailDefault);

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
            SelectedDomain = 0;
            SelectedLocations.Clear();
            SelectedEmployeeTypes.Clear();
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
                Program.GlobalConfig.EmailFormats = CurrentFormats.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Email Format Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }

        public void CurrentFormat_DoubleClick(ListBoxItem row)
        {
            if (row.DataContext is null)
                return;

            // Load format details
            EmailDefaultModelExtended SelectedFormat = (EmailDefaultModelExtended)row.DataContext;
            EditID = SelectedFormat.Id;
            int? SelectedIndex = CurrentPrimarySelected;

            ClearForm();

            CurrentPrimarySelected = SelectedIndex;
            AddNewPrimary = SelectedFormat.EmailFormat;
            SelectedDomain = Domains.IndexOf(SelectedFormat.Domain);

            foreach (var TypeIndex in SelectedFormat.EmployeeTypes)
            {
                SelectedEmployeeTypes.Select(TypeIndex);
            }

            foreach (var LocIndex in SelectedFormat.Locations)
            {
                SelectedLocations.Select(LocIndex);
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
