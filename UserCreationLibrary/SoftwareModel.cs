using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary.CustomValidators;

namespace UserCreationLibrary
{
    public class SoftwareModel
    {
        /// <summary>
        /// A randomly generated uuid that is unique to this entry.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of this piece of software. Only used in the selection list.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A list of AD permissions to apply if this piece of software is chosen.
        /// </summary>
        /// <see cref="ADPermissionModel"/>
        public List<ADPermissionModel> Permissions { get; set; } = new List<ADPermissionModel>();

        /// <summary>
        /// A list of O365 Groups to apply if this piece of software is chosen.
        /// </summary>
        /// <see cref="O365GroupModel"/>
        public List<O365GroupModel> O365Groups { get; set; } = new List<O365GroupModel>();

        /// <summary>
        /// A list of O365 Licenses to apply if this piece of software is chosen.
        /// </summary>
        public List<O365LicenseModel> O365Licenses { get; set; } = new List<O365LicenseModel>();

    }

    public class SoftwareValidator : AbstractValidator<SoftwareModel>
    {
        public SoftwareValidator()
        {
            RuleFor(software => software.Id).SetValidator(new UUIDValidator()).WithMessage("The ID is not a valid UUID. Something went horribly wrong!");
            RuleFor(software => software.Name).NotEmpty();
            RuleFor(software => software).Must(software => (software.Permissions.Any() || software.O365Groups.Any() || software.O365Licenses.Any()))
                .WithName("EmptySoftware")
                .WithMessage("You must select some AD permissions, O365 groups, or O365 licenses to apply for this software.");
        }
    }
}
