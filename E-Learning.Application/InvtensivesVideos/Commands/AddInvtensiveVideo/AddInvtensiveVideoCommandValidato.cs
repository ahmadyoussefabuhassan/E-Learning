using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace E_Learning.Application.InvtensivesVideos.Commands.AddInvtensiveVideo
{
    internal sealed class AddInvtensiveVideoCommandValidato : AbstractValidator<AddInvtensiveVideoCommand>
    {
        public AddInvtensiveVideoCommandValidato()
        {
            RuleFor(command => command.VidoeUrl)
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
