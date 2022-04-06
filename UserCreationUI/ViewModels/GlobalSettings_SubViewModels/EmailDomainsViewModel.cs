using FluentValidation.Results;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using UserCreationLibrary;

namespace UserCreationUI.GlobalSettings.ViewModels
{
    public class EmailDomainsViewModel : OtherSettingsViewModel
    {
        // Unique identifier for the routable view model.
        public new string UrlPathSegment { get; } = "EmailFormatsEdit";

        public EmailDomainsViewModel(IScreen screen) : base(screen)
        {

        }

        public ObservableCollection<string> CurrentDomains { get; } = new ObservableCollection<string>(Program.GlobalConfig.EmailDomains);

        public void AddDomain()
        {
            if (AddNewPrimary is null)
                return;
            AddNewPrimary = AddNewPrimary.Trim();

            EmailDomainValidator validator = new();
            ValidationResult validationResult = validator.Validate(AddNewPrimary);

            if (validationResult.IsValid)
            {
                CurrentDomains.Add(AddNewPrimary);
                AddNewPrimary = "";
                Saveable = true;
            }
            else
            {
                ShowError("Data Validation Failed. Please fix the following errors:", (validationResult.Errors.Count > 2 ? validationResult.ToString(" ") : validationResult.ToString()));
            }
        }

        public void DeleteDomain(int index)
        {
            CurrentDomains.RemoveAt(index);
            Saveable = true;
        }

        public void SaveDomains()
        {
            if (Saveable)
            {
                Program.GlobalConfig.EmailDomains = CurrentDomains.ToList();

                // Code to save to DB here
                System.Diagnostics.Debug.WriteLine("Saving Domain Settings");
                HostScreen.Router.NavigateBack.Execute(Unit.Default);
            }
        }
    }
}
