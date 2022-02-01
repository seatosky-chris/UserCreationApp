using ReactiveUI;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using System.Collections.ObjectModel;
using UserCreationUI.ViewModels;
using UserCreationUI.GlobalSettings.ViewModels;

namespace UserCreationUI.Views
{
    public class SettingsWindow : ReactiveWindow<SettingsWindowViewModel>
    {
        public SettingsWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            this.WhenActivated(disposables =>
            {
                // Open the Global Settings view by default when this window is opened
                ViewModel.GoGlobalSettingsView.Execute();
            });
        }

        public void ResizeWindow(double width, double height)
        {
            var screen = this.Screens.Primary;
            var maxScreenWidth = screen.Bounds.Width;
            var maxScreenHeight = screen.Bounds.Height;

            this.Width = width > maxScreenWidth ? maxScreenWidth : width;
            this.Height = height > maxScreenHeight ? maxScreenHeight : height;
        }

        public void ResizeWindow(double width, double height, double? minWidth, double? minHeight)
        {
            var screen = this.Screens.Primary;
            var maxScreenWidth = screen.Bounds.Width;
            var maxScreenHeight = screen.Bounds.Height;

            this.Width = width > maxScreenWidth ? maxScreenWidth : width;
            this.Height = height > maxScreenHeight ? maxScreenHeight : height;

            if (minWidth.HasValue)
            {
                this.MinWidth = minWidth.Value > maxScreenWidth ? maxScreenWidth : minWidth.Value;
            }
            if (minHeight.HasValue)
            {
                this.MinHeight = minHeight.Value > maxScreenHeight ? maxScreenHeight : minHeight.Value;
            }
        }

        public void ResizeWindow(double width, double height, double? minWidth, double? minHeight, double? maxWidth, double? maxHeight)
        {
            var screen = this.Screens.Primary;
            var maxScreenWidth = screen.Bounds.Width;
            var maxScreenHeight = screen.Bounds.Height;

            this.Width = width > maxScreenWidth ? maxScreenWidth : width;
            this.Height = height > maxScreenHeight ? maxScreenHeight : height;

            if (minWidth.HasValue)
            {
                this.MinWidth = minWidth.Value > maxScreenWidth ? maxScreenWidth : minWidth.Value;
            }
            if (minHeight.HasValue)
            {
                this.MinHeight = minHeight.Value > maxScreenHeight ? maxScreenHeight : minHeight.Value;
            }

            if (maxWidth.HasValue)
            {
                this.MaxWidth = maxWidth.Value > maxScreenWidth ? maxScreenWidth : maxWidth.Value;
            }
            if (maxHeight.HasValue)
            {
                this.MaxHeight = maxHeight.Value > maxScreenHeight ? maxScreenHeight : maxHeight.Value;
            }
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
