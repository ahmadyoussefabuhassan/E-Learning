using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;

namespace E_Learning.Application.Classes.Commands.DeleteClass
{
    public sealed class DeleteClassCommandHandler : ICommandHandler<DeleteClassCommand>
    {
        private readonly IClassesRepositry _classesRepositry;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteClassCommandHandler(IClassesRepositry classesRepositry, IUnitOfWork unitOfWork)
        {
            _classesRepositry = classesRepositry;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteClassCommand request, CancellationToken cancellationToken)
        {
            var classToDelete = await _classesRepositry.GetByIdAsync(request.Id, cancellationToken);
            if (classToDelete is null)
                return Result.Failure(ClassesErrors.NotFound);
            bool hasRelatedEntities = await _classesRepositry.HasRelatedDataAsync(request.Id, cancellationToken);
            if (hasRelatedEntities)
                return Result.Failure(ClassesErrors.HasRelatedData);
            await _classesRepositry.DeleteAsync(classToDelete.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
