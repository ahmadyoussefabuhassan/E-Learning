using E_Learning.Domin.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domin.Roles
{
    public class Role : Entity
    {
        private Role(Guid Id, Name name, NotType notType) : base(Id)
        {
            Name = name;
            this.notType = notType;
        }
        public Name Name { get; private set; }
        public NotType notType { get; private set; }
        public static Role Create(Name name, NotType notType)
        {
            var role = new Role(Guid.NewGuid(), name, notType);
            return role;
        }
    }
}
