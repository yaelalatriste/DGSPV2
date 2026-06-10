using MediatR;

namespace DGSP.Shared.Abstractions.Queries
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
