using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using ReactiveUI;
using System;
using UserCreationUI.GlobalSettings.ViewModels;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class CompanyDefaultsView : ReactiveUserControl<CompanyDefaultsViewModel>
    {
        private double SWidth = 760;
        private double SHeight = 450;
        private double SMinWidth = 460;
        private double SMinHeight = 415;

        public CompanyDefaultsView()
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

        public void DeleteListItem(object sender, RoutedEventArgs e)
        {
            Button DeleteBtn = (Button)sender;
            ListBox PrimaryListBox = DeleteBtn.FindAncestorOfType<ListBox>();
            ListBoxItem CurrentListItem = DeleteBtn.FindAncestorOfType<ListBoxItem>();

            int index = PrimaryListBox.ItemContainerGenerator.IndexFromContainer(CurrentListItem);

            ViewModel.DeleteCompany(index);
        }
        public void ResizeWindow(object sender, System.EventArgs e)
        {
            SettingsWindowResize.ResizeWindow(this, SWidth, SHeight, SMinWidth, SMinHeight);
        }
    }
}
