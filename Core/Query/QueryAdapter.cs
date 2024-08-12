using MediatR;

namespace Core.Query;


public class QueryAdapter<TQuery, TResponse> : IRequest<TResponse>
    where TQuery : IQuery<TResponse>
{
    public TQuery Query { get; }

    public QueryAdapter(TQuery query)
    {
        Query = query;
    }
}