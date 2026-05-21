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

        private Section(Guid id, Title title, Price price, Guid courseId) : base(id)
        {
            Title = title;
            Price = price;
            CourseId = courseId;
        }

        [MaxLength(30)]
        public Title Title { get; private set; }

        public Price Price { get; private set; }
        public Guid CourseId { get; private set; }
        public static Section Create(Guid id, Title title, Price price, Guid courseId)
        {
            var section = new Section(id, title, price, courseId);
            section.RaiseDomainEvent(new SectionCreatedDomainEvent(section.Id, section.Title.Value, section.Price.Value, section.CourseId));
            return section;
        }
    }
}
