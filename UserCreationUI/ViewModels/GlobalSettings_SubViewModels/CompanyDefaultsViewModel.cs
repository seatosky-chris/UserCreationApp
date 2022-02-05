using Avalonia.Controls;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using UserCreationLibrary;
using UserCreationUI.Models.ExtendedModels;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class CompanyDefaultsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "CompanyDefaultsEdit";

        private string _editID = "";

        public CompanyDefaultsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<CompanyDefaultModelExtended> CurrentCompanies { get; } = new ObservableCollection<CompanyDefaultModelExtended>(Program.GlobalConfig.Companies);

        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }

        public void AddCompany()
        {
            AddEditCompany(null);
        }

        public void EditCompany()
        {
            CompanyDefaultModelExtended CurrentCompanyItem = CurrentCompanies.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditCompany(CurrentCompanyItem.Id);

            CurrentCompanies.Remove(CurrentCompanies.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditCompany()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditCompany(string? Id)
        {
            // TODO: Add in validation

            CurrentCompanies.Add(new CompanyDefaultModelExtended
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Priority = 1,
                Company = AddNewPrimary,
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            });

            ClearForm();
            Saveable = true;
        }

        private void ClearForm()
        {
            AddNewPrimary = "";
            DataGridSelection = -1;
            SelectedLocations.Clear();
        }

        public void DeleteCompany(int index)
        {
            CurrentCompanies.RemoveAt(index);
            Saveable = true;
        }

        public void CurrentCompany_DoubleClick(ListBoxItem row)
        {
            // Load format details
            CompanyDefaultModelExtended SelectedCompany = (CompanyDefaultModelExtended)row.DataContext;
            EditID = SelectedCompany.Id;
            int? SelectedIndex = CurrentPrimarySelected;

            ClearForm();

            CurrentPrimarySelected = SelectedIndex;
            AddNewPrimary = SelectedCompany.Company;

            foreach (var LocIndex in SelectedCompany.Locations)
            {
                SelectedLocations.Select(LocIndex);
            }
        }

        public void SaveCompanies()
        {
            if (Saveable)
            {
                Program.GlobalConfig.Companies = CurrentCompanies.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Company Default Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
