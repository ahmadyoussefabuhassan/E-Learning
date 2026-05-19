using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domin.Abstractions
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }
        protected Entity(Guid Id)
            => this.Id = Id;
    }
}
