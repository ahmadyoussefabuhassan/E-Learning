using E_Learning.Domain.Abstractions;
using E_Learning.Domain.RefreshTokens;
using E_Learning.Domain.Roles;
using E_Learning.Domain.Students;
using E_Learning.Domain.Teachers;
using E_Learning.Domain.User.Events;

namespace E_Learning.Domain.User
{
    public sealed class User : Entity
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
        public ImageUrl? ImageUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid RoleId { get; private set; }
        public Role Role { get; private set; } = null!;
        public Student Student { get; private set; } = null!;
        public Teacher Teacher { get; private set; } = null!;
        public ICollection<RefreshToken> RefreshTokens { get; private set; } = new List<RefreshToken>();
        public ICollection<Notification.Notification> Notification { get; private set; } = new List<Notification.Notification>();
        public ICollection<Courses.Course> Courses { get; private set; } = new List<Courses.Course>();
        public PasswordResetCode? PasswordResetCode { get; private set; }
        public DateTime? PasswordResetCodeExpiresAt { get; private set; }
        public static User Create(FullName fullName, Email email, Password password, PhoneNumber phoneNumber, Address address, ImageUrl imageUrl, Guid roleId)
        {
            var user = new User(Guid.NewGuid(), fullName, email, password, phoneNumber, address, imageUrl, roleId);
            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, user.FullName.Value, user.Email.Value, user.PhoneNumber.Value, user.Address.Value, user.ImageUrl?.Value));
            return user;
        }
        public void ChangePassword(Password newPassword)
            => Password = newPassword;
        public void UpdateProfile(FullName fullName, PhoneNumber phoneNumber, Email email, Address address, ImageUrl? imageUrl)
        {
            FullName = fullName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            ImageUrl = imageUrl;
        }
        public void GenerateResetCode()
        {
            PasswordResetCode = PasswordResetCode.Generate();
            PasswordResetCodeExpiresAt = DateTime.UtcNow.AddMinutes(15);
        }
        public void ClearResetCode()
        {
            PasswordResetCode = null;
            PasswordResetCodeExpiresAt = null;
        }


    }
}
