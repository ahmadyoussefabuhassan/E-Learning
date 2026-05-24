using E_Learning.Domain.Abstractions;
using MediatR;

namespace E_Learning.Application.Abstractions.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand
    {
    }

    public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand
    {
    }
    public interface IBaseCommand
    {

    }
}
