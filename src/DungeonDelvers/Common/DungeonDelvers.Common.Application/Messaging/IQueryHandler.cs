using DungeonDelvers.Common.Domain;
using MediatR;

namespace DungeonDelvers.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;