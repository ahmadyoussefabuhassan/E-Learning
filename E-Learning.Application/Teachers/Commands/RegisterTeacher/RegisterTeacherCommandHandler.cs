using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;

namespace E_Learning.Application.Teachers.Commands.RegisterTeacher
{
    public sealed class RegisterTeacherCommandHandler : ICommandHandler<RegisterTeacherCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public RegisterTeacherCommandHandler(IUnitOfWork unitOfWork,
            ITeacherRepository teacherRepository, 
            IUserRepository userRepository, 
            IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _teacherRepository = teacherRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public async Task<Result<Guid>> Handle(RegisterTeacherCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByNameAsync(Name.Teacher, NotType.Teacher);
            if(role is null)
                return Result.Failure<Guid>(RoleErrors.NotFound);
            var existingUser = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
            if (existingUser != null)
                return Result.Failure<Guid>(UserErorrs.EmailAlreadyExists);
           
            var user = User.Create(
                new FullName(request.FullName),
                new Email(request.Email),
                new Password(request.Password),
                new PhoneNumber(request.PhoneNumber),
                new Address(request.Address),
                new ImageUrl("/uploads/users/default-profile.png"),
                role.Id
            );
            await _userRepository.AddAsync(user, cancellationToken);
            var teacher = Teacher.Create(
                user.Id,
                new UrlShamCash(request.SahmCash),
                new SubjectTeacher(request.Education)

            );
            await _teacherRepository.AddAsync(teacher , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(teacher.Id);
        }
    }
}
