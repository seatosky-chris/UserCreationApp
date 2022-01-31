using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public class O365GroupSetModel
    {
        /// <summary>
        /// A randomly generated uuid that is unique to this entry.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A friendly name for this set/grouping of O365 groups.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of EmployeeTypes to apply this O365 group set to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.EmployeeTypes"/>
        public List<int> EmployeeTypes { get; set; } = new List<int>();

        /// <summary>
        /// A list of Locations to apply this O365 group set to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.Locations"/>
        public List<int> Locations { get; set; } = new List<int>();

        /// <summary>
        /// A list of O365 Groups to apply when this group set is chosen.
        /// </summary>
        /// <see cref="O365GroupModel"/>
        public List<O365GroupModel> O365Groups { get; set; } = new List<O365GroupModel>();

    }
}
