using E_Learning.Application.Abstractions.Subscriptions;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.StudentSubscription;

namespace E_Learning.Application.ExamExplanations.Activator
{
    internal sealed class ExamExplanationActivator : ISubscriptionActivator
    {
        private readonly IExamExplanationRepository _examExplanationRepository;

        public ExamExplanationActivator(IExamExplanationRepository examExplanationRepository)
        {
            _examExplanationRepository = examExplanationRepository;
        }

        public bool CanHandle(string targetType)
              => targetType == TargetTypes.ExamExplanation.ToArabicString() ||
               targetType == TargetTypes.ExamExplanation.ToString();
        public async Task ActivateAsync(Guid targetId, CancellationToken ct)
        {
            var exam = await _examExplanationRepository.GetByIdAsync(targetId , ct);
            if (exam is null)
                return;
            if(exam is not null && exam.IsLocked)
            {
                exam.ToggleLock();
                await _examExplanationRepository.UpdateAsync(exam , ct);
            }

        }

  
    }
}
