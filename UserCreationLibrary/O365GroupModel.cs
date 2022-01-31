using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public enum O365GroupType { O365Group, DL, SharedMailbox }
    public class O365GroupModel
    {
        /// <summary>
        /// The GUID of the group in O365.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// The friendly name for the O365 group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of this O365 group. Is it an O365 Group, Distribution List or Shared Mailbox?
        /// </summary>
        /// <value>O365Group, DL, or SharedMailbox</value>
        public O365GroupType GroupType { get; set; }

        /// <summary>
        /// The description of the group found in O365.
        /// </summary>
        public string O365Description { get; set; }

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
        /// The link to the group in ITG glue, if available.
        /// </summary>
        public string ITGLink { get; set; }

    }
}
