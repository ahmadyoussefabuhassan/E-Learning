using E_Learning.Domain.Abstractions;
using E_Learning.Domain.User;

namespace E_Learning.Domain.Roles
{
    public sealed class Role : Entity
    {
        private Role() : base(Guid.Empty)
        {
        }
        private Role(Guid Id, Name name, NotType notType) : base(Id)
        {
            Name = name;
            this.notType = notType;
        }
        public Name Name { get; private set; }
        public NotType notType { get; private set; }
        public ICollection<User.User> Users { get; private set; } = new List<User.User>();
        public static Role Create(Name name, NotType notType)
        {
            var role = new Role(Guid.NewGuid(), name, notType);
            return role;
        }
    }
}
