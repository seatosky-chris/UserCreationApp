using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCreationLibrary
{
    public class UserModel
    {
        /// <summary>
        /// The first name of the user to add.
        /// </summary>
        /// <remarks>Entered during the creation process. Mandatory.</remarks>
        public string FirstName { get; set; }

        /// <summary>
        /// The middle initial of the name of the user to add.
        /// </summary>
        /// <remarks>Entered during the creation process. Optional.</remarks>
        public string MiddleInitial { get; set; }

        /// <summary>
        /// The last name of the user to add.
        /// </summary>
        /// <remarks>Entered during the creation process. Mandatory.</remarks>
        public string LastName { get; set; }

        /// <summary>
        /// The AD username of the new user we are creating.
        /// </summary>
        /// <remarks>
        /// Either generated based on the username format global variable,
        /// or a custom username is entered during the creation process.
        /// Mandatory.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.UsernameFormat"/>
        public string Username { get; set; }

        /// <summary>
        /// The password for the new user we are creating.
        /// </summary>
        /// <remarks>Randomly generated during the creation process. Mandatory.</remarks>
        public string Password { get; set; }

        /// <summary>
        /// The ticket # in our PSA that led to the creation of this new user.
        /// </summary>
        /// <remarks>Entered during the creation process. Optional.</remarks>
        public string TicketNumber { get; set; }

        /// <summary>
        /// The primary email for the new user we are creating.
        /// </summary>
        /// <remarks>
        /// Either generated based on the email format global settings 
        /// (the matching one with highest priority), 
        /// or a custom email is entered during the creation process.
        /// Mandatory.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.EmailFormats"/>
        public string EmailPrimary { get; set; }

        /// <summary>
        /// A list of alternate emails for the new user we are creating.
        /// </summary>
        /// <remarks>
        /// Either generated based on the email format global settings, 
        /// or custom emails are entered during the creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.EmailFormats"/>
        public List<string> EmailsAlternate { get; set; } = new List<string>();

        /// <summary>
        /// The AD Company field for the new user we are creating.
        /// </summary>
        /// <remarks>
        /// Either generated based on the CompanyDefaults global variable,
        /// or a custom Company is entered during the creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.Companies"/>
        public string Company { get; set; }

        /// <summary>
        /// The AD Department field for the new user we are creating.
        /// </summary>
        /// <remarks>
        /// Either generated based on the DepartmentDefaults global variable,
        /// or a custom Department is entered during the creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.Departments"/>
        public string Department { get; set; }

        /// <summary>
        /// The job title of the user to add.
        /// </summary>
        /// <remarks>Entered during the creation process. Optional.</remarks>
        public string Title { get; set; }

        /// <summary>
        /// The office phone number of the user to add.
        /// </summary>
        /// <remarks>Entered during the creation process. Optional.</remarks>
        public string PhoneOffice { get; set; }

        /// <summary>
        /// The alternate phone number of the user to add.
        /// </summary>
        /// <remarks>Entered during the creation process. Optional.</remarks>
        public string PhoneAlternate { get; set; }

        /// <summary>
        /// The mobile phone number of the user to add.
        /// </summary>
        /// <remarks>Entered during the creation process. Optional.</remarks>
        public string PhoneMobile { get; set; }



        /// <summary>
        /// The contact type ID from IT Glue.
        /// A list of contact types is pulled from IT Glue and a selection is made from those options.
        /// </summary>
        /// <remarks>
        /// Selection list pulled from ITG on startup.
        /// Value chosen during the creation process.
        /// Mandatory.
        /// </remarks>
        public int EmployeeTypeID { get; set; }

        /// <summary>
        /// The location ID from IT Glue.
        /// A list of locations is pulled from IT Glue and a selection is made from those options.
        /// </summary>
        /// <remarks>
        /// Selection list pulled from ITG on startup.
        /// Value chosen during the creation process.
        /// Mandatory.
        /// </remarks>
        public int LocationID { get; set; }

        /// <summary>
        /// The computer ID from IT Glue.
        /// A list of devices is pulled from IT Glue and a selection is made from those options.
        /// </summary>
        /// <remarks>
        /// Selection list pulled from ITG on startup.
        /// Value chosen during the creation process.
        /// Optional.
        /// </remarks>
        public int ComputerID { get; set; }

        /// <summary>
        /// The list of Office 365 licenses to apply to the new email account.
        /// </summary>
        /// <remarks>
        /// This is dynamically generated based on permission/group/software selections
        /// and can also be manually modified during the creation process.
        /// </remarks>
        /// <see cref="O365LicenseModel"/>
        public List<O365LicenseModel> O365Licenses { get; set; } = new List<O365LicenseModel>();

        /// <summary>
        /// The date the new user will start on. Can be used to schedule creation.
        /// </summary>
        /// <remarks>Value chosen during the creation process. Optional.</remarks>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// True if this is an email only account (no AD).
        /// </summary>
        /// <remarks>
        /// Value chosen during the creation process but
        /// can be forced to true if the global settings have ADType = None.
        /// Mandatory.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.ADType"/>
        public bool EmailOnly { get; set; }

        /// <summary>
        /// The date the user account should expire on. Can be used to schedule disablement.
        /// </summary>
        /// <remarks>Value chosen during the creation process. Optional.</remarks>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// True if password expiry is enabled.
        /// </summary>
        /// <remarks>
        /// Value is set by the global setting PasswordExpiryOn.
        /// Mandatory.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.PasswordExpiryOn"/>
        public bool PasswordExpiryOn { get; set; }



        /// <summary>
        /// A custom field that can be enabled in the global configuration.
        /// Data is placed in the ITG Contact Notes.
        /// </summary>
        /// <remarks>
        /// If a name is set in the global configuration for this custom field,
        /// it will be enabled. If enabled, the value can be set during the
        /// user creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.CustomFieldName1"/>
        public string CustomField1 { get; set; }

        /// <summary>
        /// A custom field that can be enabled in the global configuration.
        /// Data is placed in the ITG Contact Notes.
        /// </summary>
        /// <remarks>
        /// If a name is set in the global configuration for this custom field,
        /// it will be enabled. If enabled, the value can be set during the
        /// user creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.CustomFieldName2"/>
        public string CustomField2 { get; set; }

        /// <summary>
        /// A custom field that can be enabled in the global configuration.
        /// Data is placed in the ITG Contact Notes.
        /// </summary>
        /// <remarks>
        /// If a name is set in the global configuration for this custom field,
        /// it will be enabled. If enabled, the value can be set during the
        /// user creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.CustomFieldName3"/>
        public string CustomField3 { get; set; }

        /// <summary>
        /// A custom field that can be enabled in the global configuration.
        /// Data is placed in the ITG Contact Notes.
        /// </summary>
        /// <remarks>
        /// If a name is set in the global configuration for this custom field,
        /// it will be enabled. If enabled, the value can be set during the
        /// user creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.CustomFieldName4"/>
        public string CustomField4 { get; set; }

        /// <summary>
        /// A custom field that can be enabled in the global configuration.
        /// Data is placed in the ITG Contact Notes.
        /// </summary>
        /// <remarks>
        /// If a name is set in the global configuration for this custom field,
        /// it will be enabled. If enabled, the value can be set during the
        /// user creation process.
        /// Optional.
        /// </remarks>
        /// <see cref="CompanyConfigurationModel.CustomFieldName5"/>
        public string CustomField5 { get; set; }



       /// <summary>
       /// A list of AD permissions that are selected during the user creation process.
       /// </summary>
       /// <see cref="ADPermissionModel"/>
       public List<ADPermissionModel> Permissions { get; set; } = new List<ADPermissionModel>();

       /// <summary>
       /// A list of O365 groups that are selected during the user creation process.
       /// </summary>
       /// <see cref="O365GroupModel"/>
        public List<O365GroupModel> O365Groups { get; set; } = new List<O365GroupModel>();

    }
}
