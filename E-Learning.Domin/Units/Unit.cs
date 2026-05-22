using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons;
using E_Learning.Domain.Shared;
using E_Learning.Domain.Units.Event;
using E_Learning.Domain.Sections;
namespace E_Learning.Domain.Units
{
    public sealed class Unit : Entity
    {
        private Unit() : base(Guid.Empty)
        {

        }
        private Unit(Guid id, Title title, Description description, Guid sectionId) : base(id)
        {
            Title = title;
            Description = description;
            SectionId = sectionId;
        }
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public Guid SectionId { get; private set; }
        public Section? Section { get; private set; }
        public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();  
        public static Unit Create(Guid id, Title title, Description description, Guid sectionId)
        {
            var unit = new Unit(id, title, description, sectionId);
            unit.RaiseDomainEvent(new UnitCreatedDomainEvent(id, title.Value, description.Value , sectionId));
            return unit;
        }
    }
}
