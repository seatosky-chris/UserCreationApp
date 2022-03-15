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
using UserCreationUI.Utilities;
using UserCreationUI.GlobalSettings.ViewModels;

namespace UserCreationUI.GlobalSettings.Views
{
    public partial class O365GroupSetsView : ReactiveUserControl<O365GroupSetsViewModel>
    {
        private readonly double SWidth = 850;
        private readonly double SHeight = 940;
        private readonly double SMinWidth = 450;
        private readonly double SMinHeight = 580;

        public O365GroupSetsView()
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
            TypeFilterComboBox.Items = Program.GlobalConfig.O365Groups.Select(group => group.GroupType).ToList().Distinct();
            TypeFilterComboBox.PointerPressed += new System.EventHandler<PointerPressedEventArgs>(UIFunctions.EventHandled);

            // Handle double click on group/license data grid selectors & current software
            ListBox CurrentGroupSetsListBox = this.FindControl<ListBox>("CurrentGroupSetsListBox");
            CurrentGroupSetsListBox.AddHandler(
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
                        ViewModel.CurrentGroupSet_DoubleClick(row);
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
                case "CurrentGroupSetsListBox":
                    ViewModel.DeleteGroupSet(index);
                    break;
                case "O365GroupsListBox":
                    ViewModel.RemoveO365Group(index);
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
