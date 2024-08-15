using FluentValidation;

namespace MathHunt.DataAccess.Skill.Command.CreateSkillCommand;

public class CreateSkillValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillValidator()
    {
        RuleFor(s => s.Id)
            .NotEmpty()
            .WithMessage("Id isn't be null");

        RuleFor(s => s.SkillName)
            .NotEmpty()
            .WithMessage("Skill name isn't be null")
            .MaximumLength(20).WithMessage("Maximum length is 20");
    }
}