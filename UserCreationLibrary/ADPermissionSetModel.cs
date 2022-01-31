using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public class ADPermissionSetModel
    {
        /// <summary>
        /// A randomly generated uuid that is unique to this entry.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A friendly name for this set/grouping of permissions.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of EmployeeTypes to apply this permission set to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.EmployeeTypes"/>
        public List<int> EmployeeTypes { get; set; } = new List<int>();

        /// <summary>
        /// A list of Locations to apply this permission set to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.Locations"/>
        public List<int> Locations { get; set; } = new List<int>();

        /// <summary>
        /// A list of permissions to apply when this permission set is chosen.
        /// </summary>
        /// <see cref="ADPermissionModel"/>
        public List<ADPermissionModel> Permissions { get; set; } = new List<ADPermissionModel>();

    }
}
