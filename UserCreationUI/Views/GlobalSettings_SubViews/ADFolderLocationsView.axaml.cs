using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using ReactiveUI;
using System;
using System.Linq;
using UserCreationUI.GlobalSettings.ViewModels;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class ADFolderLocationsView : ReactiveUserControl<ADFolderLocationsViewModel>
    {
        private readonly double SWidth = 760;
        private readonly double SHeight = 570;
        private readonly double SMinWidth = 460;
        private readonly double SMinHeight = 500;

        public ADFolderLocationsView()
        {
            this.WhenActivated(disposables => { });
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            // On load, resize window
            this.AttachedToVisualTree += new System.EventHandler<VisualTreeAttachmentEventArgs>(ResizeWindow);

            // Handle double click on current formats
            ListBox CurrentADFoldersListBox = this.FindControl<ListBox>("CurrentADFoldersListBox");
            CurrentADFoldersListBox.AddHandler(
                InputElement.DoubleTappedEvent,
                (sender, e) =>
                {
                    if (e.Source is null || ViewModel is null)
                        return;

                    var row = ((IControl)e.Source).GetSelfAndVisualAncestors()
                        .OfType<ListBoxItem>()
                        .FirstOrDefault();

                    if (row != null)
                    {
                        ViewModel.CurrentFolder_DoubleClick(row);
                    }
                },
                handledEventsToo: true);
        }

        public void DeleteListItem(object sender, RoutedEventArgs e)
        {
            if (ViewModel is null)
                return;

            Button DeleteBtn = (Button)sender;
            ListBox PrimaryListBox = DeleteBtn.FindAncestorOfType<ListBox>();
            ListBoxItem CurrentListItem = DeleteBtn.FindAncestorOfType<ListBoxItem>();

            int index = PrimaryListBox.ItemContainerGenerator.IndexFromContainer(CurrentListItem);

            ViewModel.DeleteFolderLocation(index);
        }

        public void ResizeWindow(object? sender, System.EventArgs e)
        {
            SettingsWindowResize.ResizeWindow(this, SWidth, SHeight, SMinWidth, SMinHeight);
        }
    }
}
