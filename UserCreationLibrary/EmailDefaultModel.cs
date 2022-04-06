using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary.CustomValidators;

namespace UserCreationLibrary
{
    public class EmailDefaultModel
    {
        /// <summary>
        /// A randomly generated uuid that is unique to this entry.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The priority to give this selection if multiple options match the user.
        /// The lower the number, the higher the priority (e.g. 1 is the highest).
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// The email format template to apply.
        /// </summary>
        /// <example>[First].[Last]</example>
        public string EmailFormat { get; set; }

        /// <summary>
        /// The domain to use for this email template.
        /// A selection of configured domains.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.EmailDomains"/>
        public string Domain { get; set; }

        /// <summary>
        ///  The full email template.
        /// </summary>
        public string EmailTemplate { 
            get
            {
                return $"{this.EmailFormat}@{this.Domain}";
            }
        }

        /// <summary>
        /// A list of EmployeeTypes to apply this email template to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.EmployeeTypes"/>
        public List<int> EmployeeTypes { get; set; } = new List<int>();

        /// <summary>
        /// A list of Locations to apply this email template to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.Locations"/>
        public List<int> Locations { get; set; } = new List<int>();

    }

    public class EmailDefaultValidator : AbstractValidator<EmailDefaultModel>
    {
        public EmailDefaultValidator()
        {
            RuleFor(emailDefault => emailDefault.Id).SetValidator(new UUIDValidator()).WithMessage("The ID is not a valid UUID. Something went horribly wrong!");
            RuleFor(emailDefault => emailDefault.Priority).GreaterThan(0);
            RuleFor(emailDefault => emailDefault.EmailFormat).NotEmpty();
            RuleFor(emailDefault => emailDefault.Domain).NotEmpty();
            RuleFor(emailDefault => emailDefault.EmailTemplate).EmailAddress().WithMessage("The email template does not appear to be a valid email address.");
        }
    }
}
