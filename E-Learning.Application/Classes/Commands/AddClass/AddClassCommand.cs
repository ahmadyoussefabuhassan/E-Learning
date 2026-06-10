using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Classes.Commands.AddClass
{
    public sealed record AddClassCommand(string Name) : ICommand<Guid>;
}
