using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public class O365LicenseModel
    {
        /// <summary>
        /// The identifying name of the license in O365 (SKU ID).
        /// </summary>
        /// <remarks>
        /// A full list of licenses can be found at:
        /// https://docs.microsoft.com/en-us/azure/active-directory/enterprise-users/licensing-service-plan-reference
        /// Use the 'String ID'.
        /// </remarks>
        /// <example>ENTERPRISEPACK (for Office 365 E3)</example>
        public string IDName { get; set; }

        /// <summary>
        /// A friendly name for this Office 365 license. 
        /// This does not have to match the name Microsoft has given this license.
        /// </summary>
        public string Name { get; set; }
    }
}
