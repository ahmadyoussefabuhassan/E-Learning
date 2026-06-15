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
        private Unit(Guid id, UnitTitle unitTitle, Description description, Guid sectionId) : base(id)
        {
            UnitTitle = unitTitle;
            Description = description;
            SectionId = sectionId;
        }
        public UnitTitle UnitTitle { get; private set; }
        public Description Description { get; private set; }
        public Guid SectionId { get; private set; }
        public Section Section { get; private set; } = null!;
        public ICollection<Lesson> Lessons { get; private set; } = new List<Lesson>();  
        public static Unit Create( UnitTitle UnitTitle, Description description, Guid sectionId)
        {
            var unit = new Unit(Guid.NewGuid(), UnitTitle, description, sectionId);
            unit.RaiseDomainEvent(new UnitCreatedDomainEvent(unit.Id, UnitTitle.Value, description.Value , sectionId));
            return unit;
        }
        public void UpdateUnit(UnitTitle title, Description description)
            => (UnitTitle,Description) = (title,description);
    }
}
