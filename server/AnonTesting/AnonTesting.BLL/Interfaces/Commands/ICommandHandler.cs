using MediatR;

namespace AnonTesting.BLL.Interfaces.Commands
{
    public interface ICommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
    }
}
