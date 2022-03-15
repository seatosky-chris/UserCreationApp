using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using UserCreationUI.GlobalSettings.ViewModels;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class CustomFieldsView : ReactiveUserControl<CustomFieldsViewModel>
    {
        private readonly double SWidth = 760;
        private readonly double SHeight = 400;
        private readonly double SMinWidth = 400;
        private readonly double SMinHeight = 350;

        public CustomFieldsView()
        {
            this.WhenActivated(disposables => { });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            // On load, resize window
            this.AttachedToVisualTree += new System.EventHandler<VisualTreeAttachmentEventArgs>(ResizeWindow);
        }

        public void ResizeWindow(object? sender, System.EventArgs e)
        {
            SettingsWindowResize.ResizeWindow(this, SWidth, SHeight, SMinWidth, SMinHeight);
        }
    }
}
