using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary;

namespace UserCreationUI.Models.ExtendedModels
{
    public class CompanyConfigurationModelExtended : CompanyConfigurationModel
    {
        /// <summary>
        /// A list of all the email template defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="EmailDefaultModelExtended"/>
        public List<EmailDefaultModelExtended> EmailFormats { get; set; } = new List<EmailDefaultModelExtended>();
    }
}
