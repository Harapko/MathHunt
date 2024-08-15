using FluentValidation;

namespace MathHunt.DataAccess.Skill.Command.UpdateSkillCommand;

public class UpdateSkillValidation : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillValidation()
    {
        RuleFor(s => s.SkillName)
            .NotEmpty().WithMessage("Skill Name must have a new name")
            .MaximumLength(20).WithMessage("Max length is 20");
    }
}