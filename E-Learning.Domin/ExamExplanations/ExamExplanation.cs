using E_Learning.Domain.Abstractions;
using E_Learning.Domain.ExamExplanations.Events;
using E_Learning.Domain.Shared;
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

        public static ExamExplanation Create(Guid id, Title title, Description description, Price price, Guid courseId)
        {
            
            var examExplanation = new ExamExplanation(id, title, description, price, courseId);
            examExplanation.RaiseDomainEvent(new ExamExplanationCreatedEvent(examExplanation.Id, examExplanation.Title.Value, examExplanation.Description.Value, examExplanation.Price.Value, examExplanation.CourseId));
            return examExplanation;
        }
    }
}
