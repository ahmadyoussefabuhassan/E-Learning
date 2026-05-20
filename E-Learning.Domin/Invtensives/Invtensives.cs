using E_Learning.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Invtensives
{
    public class Invtensives : Entity
    {
        private Invtensives() : base(Guid.Empty)
        { }
        private Invtensives(Guid id, string title, string description, decimal  price, Guid courseID) : base(id)
        {
            Title = title;
            Description = description;
            Price = price;
            CourseID = courseID ;
        }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid CourseID { get; private set; }

        public static Invtensives Create(string title, string description, decimal price, Guid courseID)
        {
            var invtensive = new Invtensives(Guid.NewGuid(), title, description, price, courseID);
            invtensive.RaiseDomainEvent(new InvtensivesCreatedEvent(invtensive.Id, invtensive.Title, invtensive.Description, invtensive.Price, invtensive.CourseID));
            return invtensive;
        }
    }
}
