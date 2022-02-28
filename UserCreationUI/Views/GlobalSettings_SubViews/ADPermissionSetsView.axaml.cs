using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UserCreationUI.GlobalSettings.ViewModels;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class ADPermissionSetsView : ReactiveUserControl<ADPermissionSetsViewModel>
    {
        private double SWidth = 850;
        private double SHeight = 940;
        private double SMinWidth = 450;
        private double SMinHeight = 580;

        public ADPermissionSetsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.WhenActivated(disposables => { });
            AvaloniaXamlLoader.Load(this);

            // On load, resize window
            this.AttachedToVisualTree += new System.EventHandler<VisualTreeAttachmentEventArgs>(ResizeWindow);

            // Setup filter combo box
            ComboBox TypeFilterComboBox = this.FindControl<ComboBox>("TypeFilterComboBox");
            TypeFilterComboBox.Items = Program.GlobalConfig.ADPermissions.Select(perm => perm.ITGType).ToList().Distinct();
            TypeFilterComboBox.PointerPressed += new System.EventHandler<PointerPressedEventArgs>(EventHandled);

            // Handle double click on group/license data grid selectors & current software
            ListBox CurrentPermissionSetsListBox = this.FindControl<ListBox>("CurrentPermissionSetsListBox");
            CurrentPermissionSetsListBox.AddHandler(
                InputElement.DoubleTappedEvent,
                (sender, e) =>
                {
                    var row = ((IControl)e.Source).GetSelfAndVisualAncestors()
                        .OfType<ListBoxItem>()
                        .FirstOrDefault();

                    if (row != null)
                    {
                        ViewModel.CurrentPermissionSet_DoubleClick(row);
                    }
                },
                handledEventsToo: true);

            DataGrid ADGroupsDataGrid = this.FindControl<DataGrid>("ADGroupsDataGrid");
            ADGroupsDataGrid.AddHandler(
                InputElement.DoubleTappedEvent,
                (sender, e) =>
                {
                    var row = ((IControl)e.Source).GetSelfAndVisualAncestors()
                        .OfType<DataGridRow>()
                        .FirstOrDefault();

                    if (row != null)
                    {
                        ViewModel.ADGroupRow_DoubleClick(row);
                    }
                },
                handledEventsToo: true);
        }

        public void DeleteListItem(object sender, RoutedEventArgs e)
        {
            Button DeleteBtn = (Button)sender;
            ListBox PrimaryListBox = DeleteBtn.FindAncestorOfType<ListBox>();
            ListBoxItem CurrentListItem = DeleteBtn.FindAncestorOfType<ListBoxItem>();

            int index = PrimaryListBox.ItemContainerGenerator.IndexFromContainer(CurrentListItem);

            switch (PrimaryListBox.Name)
            {
                case "CurrentPermissionSetsListBox":
                    ViewModel.DeletePermissionSet(index);
                    break;
                case "ADPermissionsListBox":
                    ViewModel.RemoveADPermissions(index);
                    break;
                default:
                    break;
            }
        }

        public void ResizeWindow(object sender, System.EventArgs e)
        {
            SettingsWindowResize.ResizeWindow(this, SWidth, SHeight, SMinWidth, SMinHeight);
        }

        // This functions can be used to stop the event from bubbling upwards
        private void EventHandled(object? sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

    }
}
