using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using FluentValidation;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.UpdateMonster;

internal sealed class UpdateMonsterCommandValidator : AbstractValidator<CreateMonsterCommand>
{
    public UpdateMonsterCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty().MaximumLength(100);
        RuleFor(command => command.HitPoints).NotEmpty().MaximumLength(20);
        RuleFor(command => command.ChallengeRating).NotEmpty();
    }
}