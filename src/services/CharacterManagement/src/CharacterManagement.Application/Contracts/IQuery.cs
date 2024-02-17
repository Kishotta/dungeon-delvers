using CharacterManagement.Domain;
using MediatR;

namespace CharacterManagement.Application.Contracts;

public interface IQuery<TResult> : IRequest<Result<TResult>>;
