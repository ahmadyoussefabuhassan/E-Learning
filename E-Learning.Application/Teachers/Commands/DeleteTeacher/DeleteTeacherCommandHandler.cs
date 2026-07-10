using E_Learning.Application.Abstractions.Files;
using E_Learning.Application.Abstractions.Messaging;
using E_Learning.Application.Abstractions.Services;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Teachers.Commands.DeleteTeacher
{
    public sealed class DeleteTeacherCommandHandler : ICommandHandler<DeleteTeacherCommand, bool>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        public DeleteTeacherCommandHandler(
            ITeacherRepository teacherRepository, 
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IFileService fileService) 
        {
            _teacherRepository = teacherRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _fileService = fileService;
        }

        public async Task<Result<bool>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.TeacherId, cancellationToken);
            if(user is null)
                return Result.Failure<bool>(UserErrors.NotFound);
            var teacher = await _teacherRepository.GetByIdAsync(user.Id, cancellationToken);
            if(teacher is null)
                return Result.Failure<bool>(TeacherErrors.NotFound);
            if (!string.IsNullOrEmpty(user.ImageUrl?.Value) && user.ImageUrl.Value != "/uploads/users/default-profile.png")
                _fileService.DeleteImage(user.ImageUrl.Value);
            await _userRepository.DeleteAsync(user.Id, cancellationToken);
            await _teacherRepository.DeleteAsync(teacher.Id, cancellationToken);
            return Result.Success(true);
        }
    }
}
