

using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.Lessons.Commands.UpdateLesson
{
    internal sealed class UpdateLessonCommandValidator : AbstractValidator<UpdateLessonCommand>
    {
        public UpdateLessonCommandValidator() 
        {
            RuleFor(command => command.Title)
                    .NotEmpty().WithMessage("عنوان الدرس مطلوب.")
                    .MaximumLength(200).WithMessage("عنوان الدرس يجب أن لا يتجاوز 200 حرف.");
            RuleFor(command => command.TitleUrl)
               .NotEmpty().WithMessage("رابط عنوان الدرس مطلوب.");
            RuleFor(command => command.VidoUrl)
              .NotNull().WithMessage("يجب رفع ملف الفيديو الخاص بالدرس.")
              .Must(file => file.Length > 0).WithMessage("ملف الفيديو المرفق فارغ.")
              .Must(file => IsVideoFile(file)).WithMessage("صيغة الملف غير مدعومة. يرجى رفع فيديو بصيغة (mp4, avi, mov, mkv).");

        }
        private bool IsVideoFile(IFormFile file)
        {
            if (file == null) return false;

            var extensions = new[] { ".mp4", ".avi", ".mov", ".mkv", ".wmv" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            return extensions.Contains(fileExtension);
        }
    }
}
