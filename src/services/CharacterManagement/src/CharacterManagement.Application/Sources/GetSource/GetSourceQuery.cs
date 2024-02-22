using CharacterManagement.Domain.Sources;

namespace CharacterManagement.Application.Sources.GetSource;

public record GetSourceQuery(Guid SourceId, Guid OwnerId) : IQuery<Source>;
