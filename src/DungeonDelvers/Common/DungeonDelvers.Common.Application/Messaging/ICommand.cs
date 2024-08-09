using DungeonDelvers.Common.Domain;
using MediatR;

namespace DungeonDelvers.Common.Application.Messaging;

public interface IBaseCommand;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;
