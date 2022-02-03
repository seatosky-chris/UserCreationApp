using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public enum O365GroupType 
    {
        [EnumMember(Value = "Microsoft 365 Group")]
        [Description("Microsoft 365 Group")]
        M365Group,
        [EnumMember(Value = "Distribution List")]
        [Description("Distribution List")]
        DL,
        [EnumMember(Value = "Mail-enabled Security")]
        [Description("Mail-enabled Security")]
        MailSecurity,
        [EnumMember(Value = "Security")]
        [Description("Security")]
        Security,
        [EnumMember(Value = "Shared Mailbox")]
        [Description("Shared Mailbox")]
        SharedMailbox 
    }

    public class O365GroupModel
    {
        /// <summary>
        /// The ID of the group in O365.
        /// </summary>
        public string ObjectID { get; set; }

        /// <summary>
        /// The friendly name for the O365 group.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The email address for the O365 group, if exists.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The type of this O365 group. Is it an O365 Group, Distribution List or Shared Mailbox?
        /// </summary>
        /// <value>M365Group, DL, MailSecurity, Security, or SharedMailbox</value>
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
        ///  A calculated property that contains both WhoToAdd and the Approver.
        /// </summary>
        public string ITGWhoToAddAndApprover
        {
            get { return ITGApprover + " / " + ITGWhoToAdd; }
        }

        /// <summary>
        /// The link to the group in ITG glue, if available.
        /// </summary>
        public string ITGLink { get; set; }

    }
}
