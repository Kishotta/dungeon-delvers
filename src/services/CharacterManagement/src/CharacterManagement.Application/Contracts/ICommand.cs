using CharacterManagement.Domain;
using MediatR;

namespace CharacterManagement.Application.Contracts;

public interface ICommand : IRequest<Result>;
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
