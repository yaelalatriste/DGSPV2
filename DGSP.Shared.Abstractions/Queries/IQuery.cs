using MediatR;

namespace DGSP.Shared.Abstractions.Queries
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
