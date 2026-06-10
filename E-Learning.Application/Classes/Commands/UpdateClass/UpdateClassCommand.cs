using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Classes.Commands.UpdateClass
{
    public sealed record UpdateClassCommand(Guid Id , string Name) : ICommand<Guid>;
}
