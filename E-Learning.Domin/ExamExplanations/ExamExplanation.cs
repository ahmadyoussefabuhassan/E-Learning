using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.ExamExplanations
{
    public class ExamExplanation : Entity
    {
        private ExamExplanation(Guid Id) : base(Id)
        {
        }
        private ExamExplanation(Guid id, string title, string description, decimal price, Guid courseId) : base(id)
        {
            Title = title;
            Description = description;
            Price = price;
            CourseId = courseId;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid CourseId { get; private set; }

        public static ExamExplanation Create(Guid id, string title, string description, decimal price, Guid courseId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Exam explanation title cannot be null or empty.", nameof(title));
            title = title.Trim();
            if (title.Length > 30)
                throw new ArgumentException("Exam explanation title must be at most 30 characters.", nameof(title));
            if (price < 0)
                throw new ArgumentException("Price must be non-negative", nameof(price));
            if (courseId == Guid.Empty)
                throw new ArgumentException("CourseId cannot be empty.", nameof(courseId));
            if (id == Guid.Empty)
                id = Guid.NewGuid();
            var examExplanation = new ExamExplanation(id, title, description, price, courseId);
            examExplanation.RaiseDomainEvent(new ExamExplanationCreatedEvent(examExplanation.Id, examExplanation.Title, examExplanation.Description, examExplanation.Price, examExplanation.CourseId));
            return examExplanation;
        }
    }
}
