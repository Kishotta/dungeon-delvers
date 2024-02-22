using CharacterManagement.Domain;
using CharacterManagement.Domain.Sources;

namespace CharacterManagement.Application.Sources.CreateSource;

public class CreateSourceCommandHandler(ISourceRepository sourceRepository,
                                        IUnitOfWork unitOfWork)
    : ICommandHandler<CreateSourceCommand, Source>
{
    public async Task<Result<Source>> Handle (CreateSourceCommand request, CancellationToken cancellationToken)
    {
        var source = new Source (request.UserId, request.Name);

        await sourceRepository.AddSourceAsync (source, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success (source);
    }
}
