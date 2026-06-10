using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;


namespace E_Learning.Application.Teachers.Commands.UpdateProfileTeacher
{
    public sealed class UpdateProfileTeacherCommandHandler : BaseService, ICommandHandler<UpdateProfileTeacherCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IFileService _fileService;
        public UpdateProfileTeacherCommandHandler(IUnitOfWork unitOfWork, 
            IUserRepository userRepository, 
            IFileService fileService,
            ITeacherRepository teacherRepository,
            IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _fileService = fileService;
            _teacherRepository = teacherRepository;
        }

        public async Task<Result<Guid>> Handle(UpdateProfileTeacherCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = UserId;
            var user = await _userRepository.GetByIdAsync(currentUserId, cancellationToken);
            if (user is null)
                return Result.Failure<Guid>(UserErorrs.NotFound);
            var teacher = await _teacherRepository.GetByIdAsync(user.Id, cancellationToken);
            if (teacher is null) 
                return Result.Failure<Guid>(TeacherErrors.NotFound);
           
            if(request.Email != user.Email.Value)
            {
               if(await _userRepository.IsEmailUniqueAsync(new Email(request.Email), cancellationToken) is not null)
                    return Result.Failure<Guid>(UserErorrs.EmailAlreadyExists);
            }
            string? image = user.ImageUrl?.Value;
            if (request.ImageUrl is not null)
            {
                if (!string.IsNullOrEmpty(user.ImageUrl?.Value))
                {
                    _fileService.DeleteImage(user.ImageUrl.Value);
                }
                image = await _fileService.UploadImageAsync(request.ImageUrl, "Users", cancellationToken);
            }

            user.UpdateProfile(
                new FullName(request.FullName),
                new PhoneNumber(request.PhoneNumber),
                new Email(request.Email),
                new Address(request.Address),
                image is null ? null : new ImageUrl(image)
            );
            teacher.UpdateProfile(
                new UrlShamCash(request.SahmCash),
                new SubjectTeacher(request.Education)
            );
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(user.Id);
        }
    }
}
