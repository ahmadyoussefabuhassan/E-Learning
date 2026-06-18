using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Invtensives;

namespace E_Learning.Application.Invtensives.Commands.DeleteInvtensive
{
    public sealed class DeleteInvtensiveCommandHandler : ICommandHandler<DeleteInvtensiveCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvtensivesRepositry _invtensivesRepositry;

        public DeleteInvtensiveCommandHandler(IUnitOfWork unitOfWork, IInvtensivesRepositry invtensivesRepositry)
        {
            _unitOfWork = unitOfWork;
            _invtensivesRepositry = invtensivesRepositry;
        }

        public async Task<Result> Handle(DeleteInvtensiveCommand request, CancellationToken cancellationToken)
        {
            var invtensive = await _invtensivesRepositry.GetByIdAsync(request.Id, cancellationToken);
            if (invtensive is null)
                return Result.Failure(InvtensivesErrors.NotFound);
            await _invtensivesRepositry.DeleteAsync(request.Id, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
