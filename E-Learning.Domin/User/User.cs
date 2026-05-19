using E_Learning.Domain.Abstractions;
using E_Learning.Domain.User.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.User
{
    public class User : Entity
    {
        private User() : base(Guid.Empty)
        {
        }
        private User(Guid Id, FullName fullName, Email email, Password password, PhoneNumber phoneNumber, Address address, ImageUrl imageUrl, Guid roleId) : base(Id)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
            ImageUrl = imageUrl;
            CreatedAt = DateTime.UtcNow;
            RoleId = roleId;
        }
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public Password Password { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Address Address { get; private set; }
        public ImageUrl ImageUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid RoleId { get; private set; }
        public static User Create(Guid userId, FullName fullName, Email email, Password password, PhoneNumber phoneNumber, Address address, ImageUrl imageUrl, Guid roleId)
        {
            var user = new User(userId, fullName, email, password, phoneNumber, address, imageUrl, roleId);
            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.FullName.Value, user.Email.Value, user.PhoneNumber.Value, user.Address.Value, user.ImageUrl.Value));
            return user;
        }
    }
}
