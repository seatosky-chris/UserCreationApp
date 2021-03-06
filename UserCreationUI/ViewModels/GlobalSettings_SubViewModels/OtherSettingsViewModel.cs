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
using UserCreationUI.ViewModels;
using static UserCreationUI.Utilities.UIFunctions;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class OtherSettingsViewModel: ReactiveObject, IRoutableViewModel
    {
        private int? _currentPrimarySelected = -1;
        private int? _dataGridSelection = -1;
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

        public int? CurrentPrimarySelected
        {
            get => _currentPrimarySelected;
            set => this.RaiseAndSetIfChanged(ref _currentPrimarySelected, value);
        }
        
        public int? DataGridSelection
        {
            get => _dataGridSelection;
            set => this.RaiseAndSetIfChanged(ref _dataGridSelection, value);
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

        public void ShowError(string header, string message)
        {
            var hostVM = this.HostScreen;

            if (hostVM is SettingsWindowViewModel)
            {
                SettingsWindowViewModel settingsVM = (SettingsWindowViewModel)hostVM;
                settingsVM.ShowNotification(header, message, SettingsWindowViewModel.notificationType.Error);
            }
        }

        public static void OpenUrl(string url)
        {
            UI_OpenUrl(url);
        }
    }
}
