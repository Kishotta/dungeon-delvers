using CharacterManagement.Domain;
using MediatR;

namespace CharacterManagement.Application.Contracts;

public interface IQueryHandler<in TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IRequest<Result<TResult>>;
