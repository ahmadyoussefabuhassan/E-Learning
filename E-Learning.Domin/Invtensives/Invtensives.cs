using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Invtensives.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Invtensives
{
    public class Invtensives : Entity
    {
        public Invtensives(Guid Id, Title title, Description description, Price price, CourseID courseID) : base(Id)
        {
        }

        private Invtensives() : base(Guid.Empty)
        { }
        private Invtensives(Guid id, Title title, Description description, Price price, CourseID courseID) : base(id)
        {
            Title = title;
            Description = description;
            Price = price;
            CourseID = courseID ;
        }
        public Title Title { get; private set; }
        public Description Description { get; private set; }
        public Price Price { get; private set; }
        public CourseID CourseID { get; private set; }

        public static Invtensives Create(Title title, Description description, Price price, CourseID courseID)
        {
            var invtensive = new Invtensives(Guid.NewGuid(), title, description, price, courseID);
            invtensive.RaiseDomainEvent(new InvtensivesCreatedEvent(invtensive.Id, invtensive.Title.Value, invtensive.Description.Value, invtensive.Price.Value, invtensive.CourseID.Value));
            return invtensive;
        }
    }
}
