using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses.Events;
using E_Learning.Domain.ExamExplanations;
using E_Learning.Domain.Sections;
using E_Learning.Domain.Shared;
using E_Learning.Domain.Teachers;

namespace E_Learning.Domain.Courses
{
    public sealed class Course : Entity
    {
        private Course() : base(Guid.Empty)
        {
        }

        private Course(Guid id, CourseName name, ImageUrl imageUrl, Description description, Price price, bool isActive, Guid classesId, Guid teacherId) : base(id)
        {
            CourseName = name;
            ImageUrl = imageUrl;
            Description = description;
            Price = price;
            IsActive = isActive;
            ClassesId = classesId;
            TeacherId = teacherId;
        }

        public CourseName CourseName { get; private set; }

        public ImageUrl ImageUrl { get; private set; }

        public Description Description { get; private set; }

        public Price Price { get; private set; }

        public bool IsActive { get; private set; }
        public Guid ClassesId { get; private set; }
        public Guid TeacherId { get; private set; }
        public Teacher? Teachers { get; private set; }
        public ICollection<Section> Sections { get; private set; } = new List<Section>();
        public ICollection<ExamExplanation> ExamExplanations { get; private set; } = new List<ExamExplanation>();
        public ICollection<Invtensives.Invtensives> Invtensives { get; private set; } = new List<Invtensives.Invtensives>();

        public static Course Create(Guid id, CourseName name, ImageUrl imageUrl, Description description, Price price, Guid classesId, Guid teacherId)
        {

            var course = new Course(id, name, imageUrl, description, price, true, classesId, teacherId);
            course.RaiseDomainEvent(new CourseCreatedDomainEvent(course.Id, course.CourseName.Value, course.Price.Value, course.TeacherId, course.ClassesId));
            return course;
        }
        public void ToggleStatus() => IsActive = !IsActive;
    }
}
