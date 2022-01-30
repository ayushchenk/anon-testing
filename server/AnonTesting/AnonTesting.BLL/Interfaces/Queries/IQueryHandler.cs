using MediatR;

namespace AnonTesting.BLL.Interfaces
{
    public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
    }
}
