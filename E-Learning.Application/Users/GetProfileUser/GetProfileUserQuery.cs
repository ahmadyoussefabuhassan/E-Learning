using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Users.GetProfileUser
{
    public sealed record GetProfileUserQuery() : IQuery<UserResponse>;
}
