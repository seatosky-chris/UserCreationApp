using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCreationLibrary.CustomValidators;

namespace UserCreationLibrary
{
    public class DepartmentDefaultModel
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
        /// The Department name to fill into the Department field in AD.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// A list of ITG locations to apply this Department to by default.
        /// </summary>
        /// <see cref="CompanyConfigurationModel.Locations"/>
        public List<int> Locations { get; set; } = new List<int>();

    }

    public class DepartmentDefaultValidator : AbstractValidator<DepartmentDefaultModel>
    {
        public DepartmentDefaultValidator()
        {
            RuleFor(departmentDefault => departmentDefault.Id).SetValidator(new UUIDValidator()).WithMessage("The ID is not a valid UUID. Something went horribly wrong!");
            RuleFor(departmentDefault => departmentDefault.Priority).GreaterThan(0);
            RuleFor(departmentDefault => departmentDefault.Department).NotEmpty();
        }
    }
}
