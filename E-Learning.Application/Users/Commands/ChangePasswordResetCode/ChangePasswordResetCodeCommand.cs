using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Users.Commands.ChangePasswordResetCode
{
    public sealed record ChangePasswordResetCodeCommand(string code, string Password) : ICommand<string>;

}
