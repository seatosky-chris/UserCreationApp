using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace UserCreationUI.Models.DataGridFilterModels
{
    public class ADPermissionFiltersModel : ReactiveObject
    {
        /// <summary>
        /// The friendly name for the AD permission.
        /// </summary>
        private string? _name;
        public string? Name {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        /// <summary>
        /// The type of the group from the ITG documentation, if available.
        /// </summary>
        private string? _itgType;
        public string? ITGType
        {
            get => _itgType;
            set => this.RaiseAndSetIfChanged(ref _itgType, value);
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
