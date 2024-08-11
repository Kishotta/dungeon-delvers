using FluentValidation;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

internal sealed class CreateMonsterCommandValidator : AbstractValidator<CreateMonsterCommand>
{
    public CreateMonsterCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().MaximumLength(100);
        RuleFor(command => command.HitPoints).NotEmpty().MaximumLength(20);
        RuleFor(command => command.ChallengeRating).NotEmpty();
    }
}