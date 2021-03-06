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
using UserCreationUI.Utilities;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class SoftwareSettingsView : ReactiveUserControl<SoftwareSettingsViewModel>
    {
        private readonly double SWidth = 850;
        private readonly double SHeight = 940;
        private readonly double SMinWidth = 450;
        private readonly double SMinHeight = 580;

        public SoftwareSettingsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);

            // On load, resize window
            this.AttachedToVisualTree += new System.EventHandler<VisualTreeAttachmentEventArgs>(ResizeWindow);

            // Setup filter combo boxes
            ComboBox ADTypeFilterComboBox = this.FindControl<ComboBox>("ADTypeFilterComboBox");
            ADTypeFilterComboBox.Items = Program.GlobalConfig.ADPermissions.Select(perm => perm.ITGType).ToList().Distinct();
            ADTypeFilterComboBox.PointerPressed += new System.EventHandler<PointerPressedEventArgs>(UIFunctions.EventHandled);

            ComboBox O365TypeFilterComboBox = this.FindControl<ComboBox>("O365TypeFilterComboBox");
            O365TypeFilterComboBox.Items = Program.GlobalConfig.O365Groups.Select(group => group.GroupType).ToList().Distinct();
            O365TypeFilterComboBox.PointerPressed += new System.EventHandler<PointerPressedEventArgs>(UIFunctions.EventHandled);

            // Handle double click on group/license data grid selectors & current software
            ListBox CurrentSoftwareListBox = this.FindControl<ListBox>("CurrentSoftwareListBox");
            CurrentSoftwareListBox.AddHandler(
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
                        ViewModel.CurrentSoftware_DoubleClick(row);
                    }
                },
                handledEventsToo: true);

            DataGrid ADGroupsDataGrid = this.FindControl<DataGrid>("ADGroupsDataGrid");
            ADGroupsDataGrid.AddHandler(
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
                        ViewModel.ADGroupRow_DoubleClick(row);
                    }
                },
                handledEventsToo: true);

            DataGrid O365GroupsDataGrid = this.FindControl<DataGrid>("O365GroupsDataGrid");
            O365GroupsDataGrid.AddHandler(
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
                        ViewModel.O365GroupRow_DoubleClick(row);
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
                case "CurrentSoftwareListBox":
                    ViewModel.DeleteSoftware(index);
                    break;
                case "O365GroupsListBox":
                    ViewModel.RemoveO365Group(index);
                    break;
                case "ADPermissionsListBox":
                    ViewModel.RemoveADPermissions(index);
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
