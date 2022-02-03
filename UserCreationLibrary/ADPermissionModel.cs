using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public class ADPermissionModel
    {
        /// <summary>
        /// The GUID of the permission in AD. 
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The friendly name for the AD permission.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the permission found in AD.
        /// </summary>
        public string ADDescription { get; set; }

        /// <summary>
        /// The type of the group from the ITG documentation, if available.
        /// </summary>
        public string ITGType { get; set; }

        /// <summary>
        /// The description of the group from the ITG documentation, if available.
        /// </summary>
        public string ITGDescription { get; set; }

        /// <summary>
        /// The 'who to add' field for the the group from the ITG documentation, if available.
        /// </summary>
        public string ITGWhoToAdd { get; set; }

        /// <summary>
        /// The approver for access to this group from the ITG documentation, if available.
        /// </summary>
        public string ITGApprover { get; set; }

        /// <summary>
        ///  A calculated property that contains both WhoToAdd and the Approver.
        /// </summary>
        public string ITGWhoToAddAndApprover { 
            get { return ITGApprover + " / " + ITGWhoToAdd; } 
        }

        /// <summary>
        /// The link to the group in ITG glue, if available.
        /// </summary>
        public string ITGLink { get; set; }

    }
}
