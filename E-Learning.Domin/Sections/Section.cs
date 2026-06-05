using E_Learning.Domain.Sections.Events;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Shared;
using E_Learning.Domain.Courses;

namespace E_Learning.Domain.Sections
{
    public sealed class Section : Entity
    {
        private Section() : base(Guid.Empty)
        {
        }

        private Section(Guid id, SectionTitle title, Price price, Guid courseId) : base(id)
        {
            SectionTitle = title;
            Price = price;
            CourseId = courseId;
        }

        public SectionTitle SectionTitle { get; private set; }

        public Price Price { get; private set; }
        public Guid CourseId { get; private set; }
        public Course Course { get; private set; } = null!;
        public ICollection<Units.Unit> Units { get; private set; } = new List<Units.Unit>();
        public static Section Create(Guid id, SectionTitle title, Price price, Guid courseId)
        {
            var section = new Section(id, title, price, courseId);
            section.RaiseDomainEvent(new SectionCreatedDomainEvent(section.Id, section.SectionTitle.Value, section.Price.Value, section.CourseId));
            return section;
        }
    }
}
