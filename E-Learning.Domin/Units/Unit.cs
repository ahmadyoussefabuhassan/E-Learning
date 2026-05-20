using E_Learning.Domain.Abstractions;
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
        private Unit(Guid id, string title, string description, Guid sectionId) : base(id)
        {
            Title = title;
            Description = description;
            SectionId = sectionId;
        }
        [MaxLength(30)]
        public string Title { get; private set; }
        [MaxLength(255)]
        public string Description { get; private set; }
        public Guid SectionId { get; private set; }
        public static Unit Create(Guid id, string title, string description, Guid sectionId)
        {
            var unit = new Unit(id, title, description, sectionId);
            unit.RaiseDomainEvent(new UnitCreatedDomainEvent(id, title, description, sectionId));
            return unit;
        }
    }
}
