using FluentValidation;
using System;

namespace UserCreationLibrary.CustomValidators
{
    public class UUIDValidator: AbstractValidator<string>
    {
        public UUIDValidator()
        {
            RuleFor(uuid => uuid).NotEmpty().Custom((uuid, context) =>
            {
                if (!Guid.TryParse(uuid, out var tempGuid))
                {
                    context.AddFailure("That is not a valid UUID");
                }
            });
        }
    }
}
