using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Classes
{
    public class Classes : Entity
    {
        private Classes () : base (Guid.Empty)
        { }
        private Classes (Guid id, Name name) : base (id)=> Name = name;
           
        public Name Name { get; private set; }
      

        public static Classes Create(Name Name)
        {
            var @class = new Classes(Guid.NewGuid(),Name);
            @class.RaiseDomainEvent(new ClassesCreatedEvent(@class.Id, @class.Name.Value));
            return @class;
        }
    }
}
