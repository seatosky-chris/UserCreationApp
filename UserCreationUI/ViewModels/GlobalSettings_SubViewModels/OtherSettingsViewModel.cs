using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using UserCreationUI.GlobalSettings.Views;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class OtherSettingsViewModel: ReactiveObject, IRoutableViewModel
    {
        private string? _currentPrimarySelected;
        private string? _addNewPrimary;
        private string? _addNewSecondary;
        private bool _saveable;

        private List<string> _locations = new List<string>();
        private List<string> _employeeTypes = new List<string>();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "OtherSettingsEdit";

        public OtherSettingsViewModel(IScreen screen)
        {
            HostScreen = screen;

            SelectedEmployeeTypes = new SelectionModel<string>();
            SelectedLocations = new SelectionModel<string>();
            SelectedEmployeeTypes.SingleSelect = false;
            SelectedLocations.SingleSelect = false;

            // TODO: Save using:
            // SelectedLocations.SelectedIndexes

            // Load Locations
            Locations.Add("Canmine Contracting");
            Locations.Add("High Standard Scaffolding");
            Locations.Add("NorLand Limited");
            Locations.Add("Pinnacle Drilling");
            Locations.Add("Traxxon Rock Drills");

            // Load Employee Types
            EmployeeTypes.Add("Employee - Full Time");
            EmployeeTypes.Add("Employee - Part Time");
            EmployeeTypes.Add("Employee - Email Only");
            EmployeeTypes.Add("External User");
        }

        public bool Saveable
        {
            get => _saveable;
            set => this.RaiseAndSetIfChanged(ref _saveable, value);
        }

        public string? CurrentPrimarySelected
        {
            get => _currentPrimarySelected;
            set => this.RaiseAndSetIfChanged(ref _currentPrimarySelected, value);
        }

        public string? AddNewPrimary
        {
            get => _addNewPrimary;
            set => this.RaiseAndSetIfChanged(ref _addNewPrimary, value);
        }
        public string? AddNewSecondary
        {
            get => _addNewSecondary;
            set => this.RaiseAndSetIfChanged(ref _addNewSecondary, value);
        }


        public List<string> Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }
        public List<string> EmployeeTypes
        {
            get => _employeeTypes;
            set => this.RaiseAndSetIfChanged(ref _employeeTypes, value);
        }
        
        public SelectionModel<string> SelectedEmployeeTypes { get; }
        public SelectionModel<string> SelectedLocations { get; }


        public void BackButton()
        {
            HostScreen.Router.NavigateBack.Execute(Unit.Default);
        }

    }
}
