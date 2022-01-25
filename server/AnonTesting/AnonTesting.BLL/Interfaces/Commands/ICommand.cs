using MediatR;

namespace AnonTesting.BLL.Interfaces.Commands
{
    public interface ICommand<TResult> : IRequest<TResult>
    {
    }
}
