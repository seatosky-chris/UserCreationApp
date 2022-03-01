using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary;

namespace UserCreationUI.Models.DataGridFilterModels
{
    public class O365GroupFiltersModel : ReactiveObject
    {
        /// <summary>
        /// The friendly name for the O365 group.
        /// </summary>
        private string? _name;
        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        /// <summary>
        /// The email address for the O365 group.
        /// </summary>
        private string? _email;
        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        /// <summary>
        /// The type of the O365 group. Is it an O365 Group, Distribution List or Shared Mailbox?
        /// </summary>
        private O365GroupType? _groupType;
        public O365GroupType? GroupType
        {
            get => _groupType;
            set => this.RaiseAndSetIfChanged(ref _groupType, value);
        }

        /// <summary>
        /// The description of the group from the ITG documentation, if available.
        /// </summary>
        private string? _itgDescription;
        public string? ITGDescription
        {
            get => _itgDescription;
            set => this.RaiseAndSetIfChanged(ref _itgDescription, value);
        }

        /// <summary>
        ///  A calculated property that contains both WhoToAdd and the Approver.
        /// </summary>
        private string? _itgWhoToAddAndApprover;
        public string? ITGWhoToAddAndApprover
        {
            get => _itgWhoToAddAndApprover;
            set => this.RaiseAndSetIfChanged(ref _itgWhoToAddAndApprover, value);
        }
    }
}
