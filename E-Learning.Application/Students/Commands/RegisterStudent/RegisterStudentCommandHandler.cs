using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Students;
using E_Learning.Domain.User;

namespace E_Learning.Application.Students.Commands.RegisterStudent
{
    public sealed class RegisterStudentCommandHandler : ICommandHandler<RegisterStudentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IRoleRepository _roleRepository;

        public RegisterStudentCommandHandler(IUnitOfWork unitOfWork,
            IStudentRepository studentRepository,
            IUserRepository userRepository, 
            IFileService fileService,
            IRoleRepository roleRepository)
        {
            _unitOfWork = unitOfWork;
            _studentRepository = studentRepository;
            _userRepository = userRepository;
            _fileService = fileService;
            _roleRepository = roleRepository;
        }

        public async Task<Result<Guid>> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetByNameAsync(Name.Student, NotType.Student);
            if (role is null)
                return Result.Failure<Guid>(RoleErrors.NotFound);
            var existingUser = await _userRepository.GetByEmailAsync(new Email(request.Email), cancellationToken);
            if (existingUser != null)
                return Result.Failure<Guid>(UserErrors.EmailAlreadyExists);
     
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
            var student = Student.Create(
                user.Id,
                new  SubjectStudent(request.Education)
            );
            await _studentRepository.AddAsync(student, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(student.Id);
        }
    }
}
