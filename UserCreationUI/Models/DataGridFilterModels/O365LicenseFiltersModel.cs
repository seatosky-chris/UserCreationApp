using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationUI.Models.DataGridFilterModels
{
    public class O365LicenseFiltersModel : ReactiveObject
    {
        /// <summary>
        /// The friendly name for the Office 365 license. 
        /// </summary>
        private string? _name;
        public string? Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
    }
}
