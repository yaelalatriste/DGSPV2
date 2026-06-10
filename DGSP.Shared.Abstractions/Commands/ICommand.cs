using MediatR;

namespace DGSP.Shared.Abstractions.Commands
{
    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}
