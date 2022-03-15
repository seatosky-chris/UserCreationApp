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
    public partial class O365LicenseSetsView : ReactiveUserControl<O365LicenseSetsViewModel>
    {
        private readonly double SWidth = 850;
        private readonly double SHeight = 940;
        private readonly double SMinWidth = 450;
        private readonly double SMinHeight = 580;

        public O365LicenseSetsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);

            // On load, resize window
            this.AttachedToVisualTree += new System.EventHandler<VisualTreeAttachmentEventArgs>(ResizeWindow);

            // Handle double click on group/license data grid selectors & current software
            ListBox CurrentLicenseSetsListBox = this.FindControl<ListBox>("CurrentLicenseSetsListBox");
            CurrentLicenseSetsListBox.AddHandler(
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
                        ViewModel.CurrentLicenseSet_DoubleClick(row);
                    }
                },
                handledEventsToo: true);

            DataGrid O365LicensesDataGrid = this.FindControl<DataGrid>("O365LicensesDataGrid");
            O365LicensesDataGrid.AddHandler(
                InputElement.DoubleTappedEvent,
                (sender, e) =>
                {
                    if (e.Source is null || ViewModel is null)
                        return;

                    var row = ((IControl)e.Source).GetSelfAndVisualAncestors()
                        .OfType<DataGridRow>()
                        .FirstOrDefault();

                    if (row != null)
                    {
                        ViewModel.O365LicenseRow_DoubleClick(row);
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

            switch (PrimaryListBox.Name)
            {
                case "CurrentLicenseSetsListBox":
                    ViewModel.DeleteLicenseSet(index);
                    break;
                case "O365LicensesListBox":
                    ViewModel.RemoveO365License(index);
                    break;
                default:
                    break;
            }
        }

        public void ResizeWindow(object? sender, System.EventArgs e)
        {
            SettingsWindowResize.ResizeWindow(this, SWidth, SHeight, SMinWidth, SMinHeight);
        }
    }
}
