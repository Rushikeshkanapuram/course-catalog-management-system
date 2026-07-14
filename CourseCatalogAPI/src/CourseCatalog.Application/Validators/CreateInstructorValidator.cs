using CourseCatalog.Application.DTOs.User;
using FluentValidation;

namespace CourseCatalog.Application.Validators;

public class CreateInstructorValidator
    : AbstractValidator<CreateInstructorDto>
{
    public CreateInstructorValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6);
    }
}