using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Learning.Domain.Sections.Events;
using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Sections
{
    public class Section : Entity
    {
        private Section() : base(Guid.Empty)
        {
        }

        private Section(Guid id, string title, decimal price, Guid courseId) : base(id)
        {
            Title = title;
            Price = price;
            CourseId = courseId;
        }

        [MaxLength(30)]
        public string Title { get; private set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; private set; }
        public Guid CourseId { get; private set; }
        public static Section Create(Guid id, string title, decimal price, Guid courseId)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("title must not be empty", nameof(title));

            title = title.Trim();

            if (title.Length > 30)
                throw new ArgumentException("title must be at most 30 characters", nameof(title));
            if (price < 0)
                throw new ArgumentException("price must be non-negative", nameof(price));
            if (courseId == Guid.Empty)
                throw new ArgumentException("courseId must be a valid Guid", nameof(courseId));

            if (id == Guid.Empty)
                id = Guid.NewGuid();

            var section = new Section(id, title, price, courseId);
            section.RaiseDomainEvent(new SectionCreatedDomainEvent(section.Id, section.Title, section.Price, section.CourseId));
            return section;
        }
    }
}
