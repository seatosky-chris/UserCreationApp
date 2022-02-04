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

        /// <summary>
        /// A list of all the AD Department defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="DepartmentDefaultModelExtended"/>
        public List<DepartmentDefaultModelExtended> Departments { get; set; } = new List<DepartmentDefaultModelExtended>();

        /// <summary>
        /// A list of all the AD Company defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="CompanyDefaultModelExtended"/>
        public List<CompanyDefaultModelExtended> Companies { get; set; } = new List<CompanyDefaultModelExtended>();
    }
}
