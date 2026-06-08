using E_Learning.Application.Abstractions.Messaging;


namespace E_Learning.Application.Classes.DeleteClass
{
    public sealed record DeleteClassCommand(Guid Id) : ICommand;
}
