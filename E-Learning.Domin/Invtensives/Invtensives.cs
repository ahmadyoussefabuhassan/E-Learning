using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses;
using E_Learning.Domain.Invtensives.Events;
using E_Learning.Domain.Shared;

namespace E_Learning.Domain.Invtensives
{
    public sealed class Invtensives : Entity
    {
        private Invtensives() : base(Guid.Empty)
        { }
        private Invtensives(Guid id, InvtensivesTitle title, Description description, Price price, Guid courseID) : base(id)
        {
            Title = title;
            Description = description;
            Price = price;
            CourseID = courseID ;
        }
        public InvtensivesTitle Title { get; private set; }
        public Description Description { get; private set; }
        public Price Price { get; private set; }
        public Guid CourseID { get; private set; }
        public Course Course { get; private set; } = null!;
        public ICollection<InvtensivesVideos.InvtensivesVideos> InvtensivesVideos { get; private set; } = new List<InvtensivesVideos.InvtensivesVideos>();

        public static Invtensives Create(InvtensivesTitle title, Description description, Price price, Guid courseID)
        {
            var invtensive = new Invtensives(Guid.NewGuid(), title, description, price, courseID);
            invtensive.RaiseDomainEvent(new InvtensivesCreatedEvent(invtensive.Id, invtensive.Title.Value, invtensive.Description.Value, invtensive.Price.Value, invtensive.CourseID));
            return invtensive;
        }
        public void UpdateInvtensives(InvtensivesTitle title, Description description, Price price)
        {
            Title = title;
            Description = description;
            Price = price;
        }
    }
}
