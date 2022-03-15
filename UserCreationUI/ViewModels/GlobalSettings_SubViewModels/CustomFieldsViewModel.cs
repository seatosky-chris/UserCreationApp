using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class CustomFieldsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "CustomFieldsEdit";

        private string _customField1 = Program.GlobalConfig.CustomFieldName1;
        private string _customField2 = Program.GlobalConfig.CustomFieldName2;
        private string _customField3 = Program.GlobalConfig.CustomFieldName3;
        private string _customField4 = Program.GlobalConfig.CustomFieldName4;
        private string _customField5 = Program.GlobalConfig.CustomFieldName5;

        public CustomFieldsViewModel(IScreen screen) : base(screen)
        {

        }

        public string CustomField1
        {
            get => _customField1;
            set { this.RaiseAndSetIfChanged(ref _customField1, value); IsSaveable(); }
    }
        public string CustomField2
        {
            get => _customField2;
            set { this.RaiseAndSetIfChanged(ref _customField2, value); IsSaveable(); }
        }
        public string CustomField3
        {
            get => _customField3;
            set { this.RaiseAndSetIfChanged(ref _customField3, value); IsSaveable(); }
        }
        public string CustomField4
        {
            get => _customField4;
            set { this.RaiseAndSetIfChanged(ref _customField4, value); IsSaveable(); }
        }
        public string CustomField5
        {
            get => _customField5;
            set { this.RaiseAndSetIfChanged(ref _customField5, value); IsSaveable(); }
        }

        public void IsSaveable()
        {
            if (CustomField1 != Program.GlobalConfig.CustomFieldName1 || CustomField2 != Program.GlobalConfig.CustomFieldName2 ||
                CustomField3 != Program.GlobalConfig.CustomFieldName3 || CustomField4 != Program.GlobalConfig.CustomFieldName4 ||
                CustomField5 != Program.GlobalConfig.CustomFieldName5)
            {
                Saveable = true;
                return;
            }

            Saveable = false;
            return;
        }

        public void SaveCustomFields()
        {
            if (Saveable)
            {
                Program.GlobalConfig.CustomFieldName1 = CustomField1;
                Program.GlobalConfig.CustomFieldName2 = CustomField2;
                Program.GlobalConfig.CustomFieldName3 = CustomField3;
                Program.GlobalConfig.CustomFieldName4 = CustomField4;
                Program.GlobalConfig.CustomFieldName5 = CustomField5;

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Department Default Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
