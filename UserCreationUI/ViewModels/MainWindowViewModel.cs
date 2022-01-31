using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Text;
using System.Windows.Input;
using UserCreationUI.Views;

namespace UserCreationUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string? _incompleteUserCreationSelected;

        public MainWindowViewModel()
        {
            // TEST data
            IncompleteUserCreations.Add("Dawna Shultz");
            IncompleteUserCreations.Add("Bill Mendoza");
            IncompleteUserCreations.Add("Saturnina Perryman");
            IncompleteUserCreations.Add("Beverlee Brice - Scheduled (05/10/2021)");

            OpenWindow = new Interaction<SettingsWindowViewModel, Unit>();

            OpenSettingsWindow = ReactiveCommand.Create(() =>
            {
                var settingsWindow = new SettingsWindowViewModel();
                OpenWindow.Handle(settingsWindow).Subscribe();
                System.Diagnostics.Debug.WriteLine("Open Window Handle");
            });
        }

        public ObservableCollection<string> IncompleteUserCreations { get; } = new();

        public string? IncompleteUserCreationSelected
        {
            get => _incompleteUserCreationSelected;
            set => this.RaiseAndSetIfChanged(ref _incompleteUserCreationSelected, value);
        }

        public ICommand OpenSettingsWindow { get; }

        public Interaction<SettingsWindowViewModel, Unit> OpenWindow { get; }

        public void DeleteUserCreation()
        {

        }

        public void LoadUserCreation()
        {

        }

        public void NewUserCreation()
        {

        }
    }
}
