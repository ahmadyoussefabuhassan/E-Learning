using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Users.Commands.VerifyResetCode
{
    public sealed record VerifyResetCodeCommand(string Email, string Code) : ICommand<bool>;
}
