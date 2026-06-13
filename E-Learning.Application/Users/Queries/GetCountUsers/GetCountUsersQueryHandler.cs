using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.User;

namespace E_Learning.Application.Users.Queries.GetCountUsers
{
    public sealed class GetCountUsersQueryHandler : IQueryHandler<GetCountUsersQuery, int>
    {
        private readonly IUserRepository _userRepository;

        public GetCountUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<int>> Handle(GetCountUsersQuery request, CancellationToken cancellationToken)
        {
           var count = await _userRepository.GetCountUserssAsync(cancellationToken);
            if(count == 0)
                return Result.Success(0);
            return Result.Success(count);
        }
    }
}
