using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.Invtensives.Commands.AddInvtensive
{
    public sealed record AddInvtensiveCommand(
        Guid CourseId , 
        string Title ,
        string Description ,
        decimal Price
    ) : ICommand<Guid>;
}
