using Avalonia.Collections;
using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using UserCreationLibrary;
using UserCreationUI.Models.DataGridFilterModels;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class O365GroupSetsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "O365GroupSetsEdit";

        private string _editID = "";

        public O365GroupSetsViewModel(IScreen screen) : base(screen)
        {
            this.WhenAnyValue(x => x.O365GroupGridFilters.Name, x => x.O365GroupGridFilters.Email, x => x.O365GroupGridFilters.GroupType, x => x.O365GroupGridFilters.ITGDescription, x => x.O365GroupGridFilters.ITGWhoToAddAndApprover)
                .Throttle(TimeSpan.FromMilliseconds(200))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(x => DoFilter());
        }

        public ObservableCollection<O365GroupSetModel> CurrentGroupSets { get; } = new ObservableCollection<O365GroupSetModel>(Program.GlobalConfig.O365GroupSets);

        public DataGridCollectionView O365Groups_All { get; } = new DataGridCollectionView(Program.GlobalConfig.O365Groups);
        public ObservableCollection<O365GroupModel> O365Groups_Selected { get; } = new();
        public O365GroupFiltersModel O365GroupGridFilters { get; set; } = new O365GroupFiltersModel();


        public string EditID
        {
            get => _editID;
            set => this.RaiseAndSetIfChanged(ref _editID, value);
        }


        public void AddGroupSet()
        {
            AddEditGroupSet(null);
        }

        public void EditGroupSet()
        {
            O365GroupSetModel CurrentGroupSetItem = CurrentGroupSets.Where(x => x.Id == EditID).FirstOrDefault();

            AddEditGroupSet(CurrentGroupSetItem.Id);

            CurrentGroupSets.Remove(CurrentGroupSets.Where(x => x.Id == EditID).FirstOrDefault());
            EditID = "";
        }

        public void CancelEditGroupSet()
        {
            ClearForm();
            EditID = "";
        }

        private void AddEditGroupSet(string? Id)
        {
            // TODO: Add in validation
            CurrentGroupSets.Add(new O365GroupSetModel
            {
                Id = Id ?? System.Guid.NewGuid().ToString(),
                Name = AddNewPrimary,
                O365Groups = O365Groups_Selected.ToList(),
                EmployeeTypes = (from employee in SelectedEmployeeTypes.SelectedItems select employee.Key).Distinct().ToList(),
                Locations = (from location in SelectedLocations.SelectedItems select location.Key).Distinct().ToList()
            });

            ClearForm();
            Saveable = true;
        }

        private void ClearForm()
        {
            AddNewPrimary = "";
            CurrentPrimarySelected = -1;
            DataGridSelection = -1;
            O365Groups_Selected.Clear();
            SelectedLocations.Clear();
            SelectedEmployeeTypes.Clear();
        }

        public void DeleteGroupSet(int index)
        {
            CurrentGroupSets.RemoveAt(index);
            Saveable = true;
        }

        public void RemoveO365Group(int index)
        {
            O365Groups_Selected.RemoveAt(index);
        }

        public void SaveGroupSets()
        {
            if (Saveable)
            {
                Program.GlobalConfig.O365GroupSets = CurrentGroupSets.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving O356 Group Set Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }

        public void CurrentGroupSet_DoubleClick(ListBoxItem row)
        {
            // Load software details
            O365GroupSetModel SelectedO365GroupSet = (O365GroupSetModel)row.DataContext;
            EditID = SelectedO365GroupSet.Id;
            int? SelectedIndex = CurrentPrimarySelected;

            ClearForm();

            CurrentPrimarySelected = SelectedIndex;
            AddNewPrimary = SelectedO365GroupSet.Name;

            foreach (var TypeIndex in SelectedO365GroupSet.EmployeeTypes)
            {
                SelectedEmployeeTypes.Select(TypeIndex);
            }

            foreach (var LocIndex in SelectedO365GroupSet.Locations)
            {
                SelectedLocations.Select(LocIndex);
            }

            foreach (O365GroupModel O365Group in SelectedO365GroupSet.O365Groups)
            {
                O365Groups_Selected.Add(O365Group);
            }

        }

        public void O365GroupRow_DoubleClick(DataGridRow row)
        {
            O365GroupModel O365Group = (O365GroupModel)row.DataContext;
            O365Groups_Selected.Add(O365Group);
        }

        public bool FilterGroups(object group)
        {
            foreach (var filterProp in O365GroupGridFilters.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.DeclaredOnly))
            {
                var filterVal = filterProp.GetValue(O365GroupGridFilters, null);
                if (filterVal == null || string.IsNullOrWhiteSpace(filterVal.ToString()))
                {
                    continue;
                }

                var curValue = group.GetType().GetProperty(filterProp.Name);
                if (curValue == null)
                {
                    return false;
                }
                var curValString = curValue.GetValue(group, null);
                if (curValString == null || curValString.ToString() == null || string.IsNullOrWhiteSpace(curValString.ToString()) || !curValString.ToString().ToLower().Contains(filterVal.ToString().ToLower()))
                {
                    return false;
                }
            }

            return true;
        }

        private async void DoFilter()
        {
            if (O365Groups_All.CanFilter)
            {
                O365Groups_All.Filter = FilterGroups;
                O365Groups_All.Refresh();
            }
        }

        public void ClearTypeFilter()
        {
            O365GroupGridFilters.GroupType = null;
        }
    }
}
