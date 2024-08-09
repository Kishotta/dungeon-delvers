using FluentValidation;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

internal sealed class CreateMonsterCommandValidator : AbstractValidator<CreateMonsterCommand>
{
    public CreateMonsterCommandValidator()
    {
        RuleFor(command => command.Name).MaximumLength(100);
    }
}