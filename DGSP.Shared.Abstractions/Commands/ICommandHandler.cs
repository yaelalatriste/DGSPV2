using MediatR;

namespace DGSP.Shared.Abstractions.Commands
{
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
    }
}
