using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;

namespace E_Learning.Application.Classes.Commands.UpdateClass
{
    public sealed class UpdateClassCommandHandler : ICommandHandler<UpdateClassCommand, Guid>
    {
        private readonly IClassesRepositry _classesRepositry;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateClassCommandHandler(IClassesRepositry classesRepositry, IUnitOfWork unitOfWork)
        {
            _classesRepositry = classesRepositry;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(UpdateClassCommand request, CancellationToken cancellationToken)
        {
            var classToUpdate = await _classesRepositry.GetByIdAsync(request.Id, cancellationToken);
            if (classToUpdate is null)
                return Result.Failure<Guid>(ClassesErrors.NotFound);
            var duplicateNameClass = await _classesRepositry.IsClassesUniqueAsync(new ClassesName(request.Name), cancellationToken);
            if (duplicateNameClass is not null && duplicateNameClass.Id != request.Id)
                return Result.Failure<Guid>(ClassesErrors.AlreadyExists);

            classToUpdate.UpdateName(new ClassesName(request.Name));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(classToUpdate.Id);
        }
    }
}
