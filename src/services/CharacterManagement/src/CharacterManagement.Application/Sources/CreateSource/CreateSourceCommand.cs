using CharacterManagement.Domain.Sources;

namespace CharacterManagement.Application.Sources.CreateSource;

public record CreateSourceCommand(Guid UserId, string Name) : ICommand<Source>;
