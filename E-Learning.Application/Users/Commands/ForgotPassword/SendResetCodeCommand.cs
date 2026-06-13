using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Users.Commands.ForgotPassword
{
    public sealed record SendResetCodeCommand(string Email) : ICommand;
}
