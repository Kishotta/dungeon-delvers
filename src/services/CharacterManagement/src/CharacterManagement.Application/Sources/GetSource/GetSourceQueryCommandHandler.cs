using CharacterManagement.Domain;
using CharacterManagement.Domain.Sources;

namespace CharacterManagement.Application.Sources.GetSource;

public class GetSourceQueryCommandHandler(ISourceRepository sourceRepository)
    : IQueryHandler<GetSourceQuery, Source>
{
    public async Task<Result<Source>> Handle(GetSourceQuery query, CancellationToken cancellationToken)
    {
        var source = await sourceRepository.GetByIdAsync(query.SourceId, cancellationToken);

        return source is null || !source.OwnedBy (query.OwnerId) ?
                         Result.Failure<Source>("Character not found") :
                         Result.Success(source);
    }
}
