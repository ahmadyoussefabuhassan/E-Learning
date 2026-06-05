using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.ExamExplanations.Events;
using E_Learning.Domain.ExamVideos;
using E_Learning.Domain.Shared;

namespace E_Learning.Domain.ExamExplanations
{
    public sealed class ExamExplanation : Entity
    {
        private ExamExplanation() : base(Guid.Empty)
        {
        }
        private ExamExplanation(Guid id, Title title, Description description, Price price, Guid courseId) : base(id)
        {
            Title = title;
            Description = description;
            Price = price;
            CourseId = courseId;
        }

        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public Price Price { get; private set; }
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; } = null!; 
        public ICollection<ExamVideo> ExamExplanationVideos { get; private set; } = new List<ExamVideo>();

        public static ExamExplanation Create(Title title, Description description, Price price, Guid courseId)
        {
            
            var examExplanation = new ExamExplanation(Guid.NewGuid(), title, description, price, courseId);
            examExplanation.RaiseDomainEvent(new ExamExplanationCreatedEvent(examExplanation.Id, examExplanation.Title.Value, examExplanation.Description.Value, examExplanation.Price.Value, examExplanation.CourseId));
            return examExplanation;
        }
    }
}
