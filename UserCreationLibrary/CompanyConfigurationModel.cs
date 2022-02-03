using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserAppSharedLibrary;
using static UserAppSharedLibrary.CompanyConfigurationSharedModel;

namespace UserCreationLibrary
{

    public class CompanyConfigurationModel
    {
        /// <summary>
        /// The default username template to use for all users in AD.
        /// </summary>
        /// <example>[First].[Last]</example>
        public string UsernameFormat { get; set; }

        /// <summary>
        /// A list of all the email template defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="EmailDefaultModel"/>
        public List<EmailDefaultModel> EmailFormats { get; set; } = new List<EmailDefaultModel>();

        /// <summary>
        /// A list of all the email domains. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// If empty in the db, pull all from ITG.
        /// </remarks>
        public List<string> EmailDomains { get; set; } = new List<string>();

        /// <summary>
        /// A list of all the AD Department defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="DepartmentDefaultModel"/>
        public List<DepartmentDefaultModel> Departments { get; set; } = new List<DepartmentDefaultModel>();

        /// <summary>
        /// A list of all the AD Company defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="CompanyDefaultModel"/>
        public List<CompanyDefaultModel> Companies { get; set; } = new List<CompanyDefaultModel>();

        /// <summary>
        /// A list of all the AD Folder defaults. 
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="ADFolderDefaultModel"/>
        public List<ADFolderDefaultModel> ADFolders { get; set; } = new List<ADFolderDefaultModel>();

        /// <summary>
        /// The FQDN of the exchange server, if using Exchange rather than O365.
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        public string ExchangeServerFQDN { get; set; }



        /// <summary>
        /// A dictionary of Employee Types loaded from ITG.
        /// </summary>
        /// <value>Dictionary<TypeID, TypeName></value>
        public Dictionary<int, string> EmployeeTypes { get; set; } = new Dictionary<int, string>();

        /// <summary>
        /// A dictionary of Locations loaded from ITG.
        /// </summary>
        /// <value>Dictionary<LocationID, LocationName></value>
        public Dictionary<int, string> Locations { get; set; } = new Dictionary<int, string>();

        /// <summary>
        /// A dictionary of Computers loaded from ITG.
        /// </summary>
        /// <value>Dictionary<ComputerID, ComputerName></value>
        public Dictionary<int, string> Computers { get; set; } = new Dictionary<int, string>();



        /// <summary>
        /// True if password expiry is enabled.
        /// Applies to all new users.
        /// </summary>
        public bool PasswordExpiryOn { get; set; }

        /// <summary>
        /// True if AD to O365 sync is setup and running.
        /// </summary>
        public bool ADO365SyncOn { get; set; }

        /// <summary>
        /// The AD type in-use, if any. Is it On Premise, Azure, or No AD?
        /// </summary>
        /// <value>OnPremise, Azure, or None</value>
        public ADTypeConfiguration ADType { get; set; }

        /// <summary>
        /// The Email type in-use, if any. Is it Office 365, Exchange, or No Email?
        /// </summary>
        /// <value>O365, Exchange, or None</value>
        public EmailTypeConfiguration EmailType { get; set; }



        /// <summary>
        /// The API credential details needed to connect to Azure / O365.
        /// </summary>
        public APICredentialsModel AzureO365APICredentials { get; set; }

        /// <summary>
        /// The API credential details needed to connect to IT Glue.
        /// </summary>
        public APICredentialsModel ITGAPICredentials { get; set; }

        /// <summary>
        /// The API credential details needed to connect to the Email Forwarder.
        /// </summary>
        public APICredentialsModel EmailForwarderAPICredentials { get; set; }

        /// <summary>
        /// The API URL needed to connect to PW Push.
        /// This is hardcoded in the Company Configuration Model.
        /// </summary>
        public string PWPushURL { get; set; } = "https://pwpush.com/p.json";



        /// <summary>
        /// A list of all the AD permissions loaded.
        /// </summary>
        /// <see cref="ADPermissionModel"/>
        public List<ADPermissionModel> ADPermissions { get; set; } = new List<ADPermissionModel>();

        /// <summary>
        /// A list of all the O365 Groups loaded.
        /// </summary>
        /// <see cref="O365GroupModel"/>
        public List<O365GroupModel> O365Groups { get; set; } = new List<O365GroupModel>();

        /// <summary>
        /// A list of all the O365 Licenses loaded.
        /// </summary>
        /// <see cref="O365LicenseModel"/>
        public List<O365LicenseModel> O365Licenses { get; set; } = new List<O365LicenseModel>();

        /// <summary>
        /// A list of all the AD permission sets loaded.
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="ADPermissionSetModel"/>
        public List<ADPermissionSetModel> ADPermissionSets { get; set; } = new List<ADPermissionSetModel>();

        /// <summary>
        /// A list of all the O365 group sets loaded.
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="O365GroupSetModel"/>
        public List<O365GroupSetModel> O365GroupSets { get; set; } = new List<O365GroupSetModel>();

        /// <summary>
        /// A list of all the O365 license sets loaded.
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="O365LicenseSetModel"/>
        public List<O365LicenseSetModel> O365LicenseSets { get; set; } = new List<O365LicenseSetModel>();

        /// <summary>
        /// A list of all the software loaded.
        /// </summary>
        /// <remarks>
        /// This is configured in the global settings.
        /// It is stored and loaded from the DB on load.
        /// </remarks>
        /// <see cref="SoftwareModel"/>
        public List<SoftwareModel> Software { get; set; } = new List<SoftwareModel>();



        /// <summary>
        /// A custom field that is enabled if this variable is set.
        /// If enabled, the corresponding field can be set in the User Model.
        /// </summary>
        /// <remarks>
        /// If enabled, the value can be set during the user creation process.
        /// </remarks>
        public string CustomFieldName1 { get; set; }

        /// <summary>
        /// A custom field that is enabled if this variable is set.
        /// If enabled, the corresponding field can be set in the User Model.
        /// </summary>
        /// <remarks>
        /// If enabled, the value can be set during the user creation process.
        /// </remarks>
        public string CustomFieldName2 { get; set; }

        /// <summary>
        /// A custom field that is enabled if this variable is set.
        /// If enabled, the corresponding field can be set in the User Model.
        /// </summary>
        /// <remarks>
        /// If enabled, the value can be set during the user creation process.
        /// </remarks>
        public string CustomFieldName3 { get; set; }

        /// <summary>
        /// A custom field that is enabled if this variable is set.
        /// If enabled, the corresponding field can be set in the User Model.
        /// </summary>
        /// <remarks>
        /// If enabled, the value can be set during the user creation process.
        /// </remarks>
        public string CustomFieldName4 { get; set; }

        /// <summary>
        /// A custom field that is enabled if this variable is set.
        /// If enabled, the corresponding field can be set in the User Model.
        /// </summary>
        /// <remarks>
        /// If enabled, the value can be set during the user creation process.
        /// </remarks>
        public string CustomFieldName5 { get; set; }

    }
}
