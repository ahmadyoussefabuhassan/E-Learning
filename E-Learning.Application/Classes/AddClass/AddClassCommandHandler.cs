using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes;

namespace E_Learning.Application.Classes.AddClass
{
    public sealed class AddClassCommandHandler : ICommandHandler<AddClassCommand, Guid>
    {
        private readonly IClassesRepositry _classesRepositry;
        private readonly IUnitOfWork _unitOfWork;

        public AddClassCommandHandler(IClassesRepositry classesRepositry, IUnitOfWork unitOfWork)
        {
            _classesRepositry = classesRepositry;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(AddClassCommand request, CancellationToken cancellationToken)
        {
            var classes = await _classesRepositry.IsClassesUniqueAsync(new ClassesName(request.Name) , cancellationToken);
            if (classes is not null)
                return Result.Failure<Guid>(ClassesErrors.AlreadyExists);
            var newClass = Domain.Classes.Classes.Create(new ClassesName(request.Name));
            await _classesRepositry.AddAsync(newClass, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(newClass.Id);
        }
    }
}
