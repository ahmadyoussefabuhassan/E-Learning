using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Users.Commands.ChangePassword
{
    public sealed record ChangePasswordCommand(
        string OldPassword,
        string NewPassword,
        string ChekPassword
    ) : ICommand<bool>;
}
