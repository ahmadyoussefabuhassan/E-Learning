using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.User
{
    public class User : Entity
    {
        private User() { }
        private User(Guid Id, FullName fullName, Email email, Password password, PhoneNumber phoneNumber, Address address, ImageUrl imageUrl, DateTime createdAt, Guid roleId) : base(Id)
        {
            FullName = fullName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Address = address;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
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
    }
}
