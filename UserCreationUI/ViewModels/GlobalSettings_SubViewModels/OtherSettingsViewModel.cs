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

        private Dictionary<int, string> _locations = Program.GlobalConfig.Locations;
        private Dictionary<int, string> _employeeTypes = Program.GlobalConfig.EmployeeTypes;

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }
        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "OtherSettingsEdit";

        public OtherSettingsViewModel(IScreen screen)
        {
            HostScreen = screen;

            SelectedEmployeeTypes = new SelectionModel<KeyValuePair<int, string>>();
            SelectedLocations = new SelectionModel<KeyValuePair<int, string>>();
            SelectedEmployeeTypes.SingleSelect = false;
            SelectedLocations.SingleSelect = false;

            // TODO: Save using:
            // SelectedLocations.SelectedIndexes
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


        public Dictionary<int, string> Locations
        {
            get => _locations;
            set => this.RaiseAndSetIfChanged(ref _locations, value);
        }
        public Dictionary<int, string> EmployeeTypes
        {
            get => _employeeTypes;
            set => this.RaiseAndSetIfChanged(ref _employeeTypes, value);
        }
        
        public SelectionModel<KeyValuePair<int, string>> SelectedEmployeeTypes { get; }
        public SelectionModel<KeyValuePair<int, string>> SelectedLocations { get; }


        public void BackButton()
        {
            HostScreen.Router.NavigateBack.Execute(Unit.Default);
        }

    }
}
