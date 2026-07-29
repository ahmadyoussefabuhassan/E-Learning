using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Invtensives;

namespace E_Learning.Application.Invtensives.Queries.GetInvtensivesByIdForStudent
{
    public sealed class GetInvtensivesByIdForStudentQueryHandler : IQueryHandler<GetInvtensivesByIdForStudentQuery, InvtensiveResponse>
    {
        private readonly IInvtensivesRepositry _invtensivesRepositry;

        public GetInvtensivesByIdForStudentQueryHandler(IInvtensivesRepositry invtensivesRepositry)
        {
            _invtensivesRepositry = invtensivesRepositry;
        }

        public async Task<Result<InvtensiveResponse>> Handle(GetInvtensivesByIdForStudentQuery request, CancellationToken cancellationToken)
        {
            var invtensive = await _invtensivesRepositry.GetByIdAsync(request.Id, cancellationToken);
            if (invtensive is null)
                return Result.Failure<InvtensiveResponse>(InvtensivesErrors.NotFound);
            var response = new InvtensiveResponse(
                invtensive.Id,
                invtensive.Title.Value,
                invtensive.Description.Value,
                invtensive.Price.Value,
                invtensive.IsLocked
            );
            return Result.Success(response);
        }
    }
}
