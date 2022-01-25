using MediatR;

namespace AnonTesting.BLL.Interfaces
{
    public interface IQuery<TResult> : IRequest<TResult>
    {
    }
}
