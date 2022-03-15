using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using ReactiveUI;
using UserCreationUI.ViewModels;
using UserCreationUI.GlobalSettings.ViewModels;
using UserCreationUI.Views;
using System.Linq;
using Avalonia.Data;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class GlobalSettingsView : ReactiveUserControl<GlobalSettingsViewModel>, IVisual
    {
        private readonly double SWidth = 800;
        private readonly double SHeight = 650;
        private readonly double SMinWidth = 440;
        private readonly double SMinHeight = 530;

        public enum ADTypes { OnPremise, Azure, None };
        public enum EmailTypes { O365, Exchange, None };

        public GlobalSettingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);

            // On load, resize window
            this.AttachedToVisualTree += new System.EventHandler<VisualTreeAttachmentEventArgs>(ResizeWindow);
           
            // AD Types dropdown
            ComboBox ADTypesComboBox = this.Find<ComboBox>("ADTypes");
            ADTypesComboBox.Items = System.Enum.GetValues(typeof(ADTypes));
            ADTypesComboBox.SelectedIndex = 0;

            // Email Types dropdown
            ComboBox EmailTypesComboBox = this.Find<ComboBox>("EmailTypes");
            EmailTypesComboBox.Items = System.Enum.GetValues(typeof(EmailTypes));
            EmailTypesComboBox.SelectedIndex = 0;
        }

        public void SaveAndClose(object sender, RoutedEventArgs args)
        {
            if (ViewModel is null)
                return;

            ViewModel.SaveGlobalSettings();
            System.Diagnostics.Debug.WriteLine("Now closing");
            SettingsWindow settingsWindow = this.FindAncestorOfType<SettingsWindow>();
            settingsWindow.CloseWindow();
        }

        public void ResizeWindow(object? sender, System.EventArgs e)
        {
            SettingsWindowResize.ResizeWindow(this, SWidth, SHeight, SMinWidth, SMinHeight);
        }
    }
}
