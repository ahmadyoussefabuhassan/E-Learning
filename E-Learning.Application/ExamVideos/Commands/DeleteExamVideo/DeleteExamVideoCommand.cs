
using E_Learning.Application.Abstractions.Messaging;

namespace E_Learning.Application.ExamVideos.Commands.DeleteExamVideo
{
    public sealed record  DeleteExamVideoCommand(Guid Id) :ICommand;
}
