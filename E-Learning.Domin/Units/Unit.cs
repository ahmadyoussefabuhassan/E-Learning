using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Shared;
using E_Learning.Domain.Units.Event;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Units
{
    public class Unit : Entity
    {
        private Unit(Guid Id) : base(Id)
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
        public static Unit Create(Guid id, Title title, Description description, Guid sectionId)
        {
            var unit = new Unit(id, title, description, sectionId);
            unit.RaiseDomainEvent(new UnitCreatedDomainEvent(id, title.Value, description.Value , sectionId));
            return unit;
        }
    }
}
