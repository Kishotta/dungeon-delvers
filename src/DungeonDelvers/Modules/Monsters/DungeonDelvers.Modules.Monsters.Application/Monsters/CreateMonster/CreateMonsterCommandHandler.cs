using DungeonDelvers.Common.Application.Messaging;
using DungeonDelvers.Common.Domain;
using DungeonDelvers.Modules.Monsters.Application.Abstractions.Data;
using DungeonDelvers.Modules.Monsters.Domain.Monsters;

namespace DungeonDelvers.Modules.Monsters.Application.Monsters.CreateMonster;

internal sealed class CreateMonsterCommandHandler(
    IMonsterRepository monsterRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateMonsterCommand, MonsterResponse>
{
    public async Task<Result<MonsterResponse>> Handle(CreateMonsterCommand request, CancellationToken cancellationToken)
    {
        var monster = Monster.Create(request.Name);
        
        monsterRepository.Add(monster);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return new MonsterResponse(monster.Id, monster.Name);
    }
}