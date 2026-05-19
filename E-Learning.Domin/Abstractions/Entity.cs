using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        protected Entity()
        {
                
        }
        protected Entity(Guid Id)
            => this.Id = Id;
    }
}
