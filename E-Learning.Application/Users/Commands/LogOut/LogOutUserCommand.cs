using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Users.Commands.LogOut
{
    public sealed record LogOutUserCommand(string token) : ICommand<bool>;
}
