using E_Learning.Domain.Abstractions;
using MediatR;


namespace E_Learning.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
