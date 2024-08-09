using DungeonDelvers.Common.Domain;
using MediatR;

namespace DungeonDelvers.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
