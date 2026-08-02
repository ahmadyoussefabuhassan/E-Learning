using E_Learning.Application.Abstractions.Subscriptions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.StudentSubscription;

namespace E_Learning.Application.Courses.Activator
{
    internal sealed class CourseActivator : ISubscriptionActivator
    {
        private readonly ICourseRepository _courseRepository;

        public CourseActivator(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public bool CanHandle(string targetType)
             => targetType == TargetTypes.Course.ToArabicString() ||
                targetType == TargetTypes.Course.ToString();
        public async Task ActivateAsync(Guid targetId, CancellationToken ct)
        {
            var course = await _courseRepository.GetByIdAsync(targetId);
            if (course == null)
                return;
            if(course is not null && course.IsLocked)
            {
                course.ToggleLock();
                await _courseRepository.UpdateLoukedSectionAsync(course.Id , ct);
                await _courseRepository.UpdateAsync(course , ct);
            }
        }


    }
}
