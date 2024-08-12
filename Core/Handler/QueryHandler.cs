using Core.Query;
using MediatR;

namespace Core.Handler;

public class QueryHandlerAdapter<TQuery, TResponse> : IRequestHandler<QueryAdapter<TQuery, TResponse>, TResponse>
    where TQuery : IQuery<TResponse>
{
    private readonly IQueryHandler<TQuery, TResponse> _handler;

    public QueryHandlerAdapter(IQueryHandler<TQuery, TResponse> handler)
    {
        _handler = handler;
    }

    public async Task<TResponse> Handle(QueryAdapter<TQuery, TResponse> request, CancellationToken cancellationToken)
    {
        return await _handler.Handle(request.Query, cancellationToken);
    }
}
