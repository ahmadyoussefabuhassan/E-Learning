using E_Learning.Application.Abstractions.Subscriptions;
using E_Learning.Domain.Sections;
using E_Learning.Domain.StudentSubscription;

namespace E_Learning.Application.Sections.Activator
{
    internal sealed class SectionActivator : ISubscriptionActivator
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionActivator(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        public bool CanHandle(string targetType)
            => targetType == TargetTypes.Section.ToArabicString() ||
               targetType == TargetTypes.Section.ToString();
        public async Task ActivateAsync(Guid targetId, CancellationToken ct)
        {
            var section = await _sectionRepository.GetByIdAsync(targetId);
            if (section is null) 
                return;
            if (section is not null && section.IsLocked)
            {
                section.ToggleLock();
                await _sectionRepository.UpdateAsync(section , ct);
            }
        }

 
    }
}
