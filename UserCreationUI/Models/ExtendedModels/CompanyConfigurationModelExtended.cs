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
        public new List<EmailDefaultModelExtended> EmailFormats { get; set; } = new List<EmailDefaultModelExtended>();

        /// <summary>
        /// A list of all the AD Department defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="DepartmentDefaultModelExtended"/>
        public new List<DepartmentDefaultModelExtended> Departments { get; set; } = new List<DepartmentDefaultModelExtended>();

        /// <summary>
        /// A list of all the AD Company defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="CompanyDefaultModelExtended"/>
        public new List<CompanyDefaultModelExtended> Companies { get; set; } = new List<CompanyDefaultModelExtended>();

        /// <summary>
        /// A list of all the AD Folder defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="ADFolderDefaultModelExtended"/>
        public new List<ADFolderDefaultModelExtended> ADFolders { get; set; } = new List<ADFolderDefaultModelExtended>();
    }
}
