using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using UserCreationUI.GlobalSettings.ViewModels;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class APICredentialsView : ReactiveUserControl<APICredentialsViewModel>
    {
        private double SWidth = 700;
        private double SHeight = 650;
        private double SMinWidth = 440;
        private double SMinHeight = 505;

        public APICredentialsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);

            // On load, resize window
            this.AttachedToVisualTree += new System.EventHandler<VisualTreeAttachmentEventArgs>(ResizeWindow);
        }

        public void ResizeWindow(object sender, System.EventArgs e)
        {
            SettingsWindowResize.ResizeWindow(this, SWidth, SHeight, SMinWidth, SMinHeight);
        }
    }
}
