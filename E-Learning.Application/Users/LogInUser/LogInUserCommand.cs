using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Users.LogInUser
{
    public sealed record LogInUserCommand(string Email , string Password) : ICommand<AuthenticationResponse>;
}
