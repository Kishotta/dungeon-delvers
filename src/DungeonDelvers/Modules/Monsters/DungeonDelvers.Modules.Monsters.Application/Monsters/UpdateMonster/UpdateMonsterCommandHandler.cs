using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Common.Domain;
using DungeonDelvers.Modules.Monsters.Application.Abstractions.Data;
using DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;
using DungeonDelvers.Modules.Monsters.Domain.ChallengeRatings;
using DungeonDelvers.Modules.Monsters.Domain.DiceExpressions;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.UpdateMonster;

internal sealed class UpdateMonsterCommandHandler(
    IMonsterRepository monsterRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateMonsterCommand, MonsterResponse>
{
    public async Task<Result<MonsterResponse>> Handle(UpdateMonsterCommand request, CancellationToken cancellationToken)
    {
        var monster = await monsterRepository.GetAsync(request.MonsterId, cancellationToken);
        if (monster is null)
            return Result.Failure<MonsterResponse>(MonsterErrors.NotFound(request.MonsterId));
        
        var hitPoints = DiceExpression.Create(request.HitPoints);
        if (hitPoints.IsFailure)
            return Result.Failure<MonsterResponse>(hitPoints.Error);
        
        var challengeRating = ChallengeRating.Create(request.ChallengeRating);
        if (challengeRating.IsFailure)
            return Result.Failure<MonsterResponse>(challengeRating.Error);

        monster.ChangeName(request.Name);
        monster.ChangeHitPoints(hitPoints.Value);
        monster.ChangeChallengeRating(challengeRating.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return (MonsterResponse)monster;
    }
}