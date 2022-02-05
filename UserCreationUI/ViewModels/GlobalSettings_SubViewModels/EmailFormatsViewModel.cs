using Avalonia.Controls;
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
            EmailDefaultModelExtended CurrentFormatItem = CurrentFormats.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditFormat(CurrentFormatItem.Id);

            CurrentFormats.Remove(CurrentFormats.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditFormat()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditFormat(string? Id)
        {
            // TODO: Add in validation
            // TODO: Add in check to ensure this is a proper format
            // also only enable the button if AddNewPrimary is a proper format

            CurrentFormats.Add(new EmailDefaultModelExtended
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Priority = 1,
                EmailFormat = AddNewPrimary,
                Domain = Domains[SelectedDomain],
                EmployeeTypes = (from employee in SelectedEmployeeTypes.SelectedItems select employee.Key).Distinct().ToList(),
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            });

            ClearForm();
            Saveable = true;
        }

        private void ClearForm()
        {
            AddNewPrimary = "";
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
            // Load format details
            EmailDefaultModelExtended SelectedFormat = (EmailDefaultModelExtended)row.DataContext;
            EditID = SelectedFormat.Id;

            ClearForm();

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
