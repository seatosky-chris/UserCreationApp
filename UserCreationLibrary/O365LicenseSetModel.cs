using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary.CustomValidators;

namespace UserCreationLibrary
{
    public class O365LicenseSetModel
    {
        /// <summary>
        /// A randomly generated uuid that is unique to this entry.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A friendly name for this set/grouping of licenses.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of EmployeeTypes to apply this license set to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.EmployeeTypes"/>
        public List<int> EmployeeTypes { get; set; } = new List<int>();

        /// <summary>
        /// A list of Locations to apply this license set to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.Locations"/>
        public List<int> Locations { get; set; } = new List<int>();

        /// <summary>
        /// A list of O365 licenses to apply when this license set is chosen.
        /// </summary>
        /// <see cref="O365LicenseModel"/>
        public List<O365LicenseModel> O365Licenses { get; set; } = new List<O365LicenseModel>();

    }

    public class O365LicenseSetValidator : AbstractValidator<O365LicenseSetModel>
    {
        public O365LicenseSetValidator()
        {
            RuleFor(o365LicenseSet => o365LicenseSet.Id).SetValidator(new UUIDValidator()).WithMessage("The ID is not a valid UUID. Something went horribly wrong!");
            RuleFor(o365LicenseSet => o365LicenseSet.Name).NotEmpty();
            RuleFor(o365LicenseSet => o365LicenseSet.O365Licenses).NotEmpty();
        }
    }
}
