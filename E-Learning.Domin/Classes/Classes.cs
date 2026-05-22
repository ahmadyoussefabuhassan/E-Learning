using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes.Events;
using E_Learning.Domain.Courses;


namespace E_Learning.Domain.Classes
{
    public sealed class Classes : Entity
    {
        private Classes () : base (Guid.Empty)
        { }
        private Classes (Guid id, ClassesName name) : base (id)=> Name = name;
           
        public ClassesName Name { get; private set; }
      
        public ICollection<Course> Courses { get; private set; } = new List<Course>();
        public static Classes Create(ClassesName Name)
        {
            var classes = new Classes(Guid.NewGuid(),Name);
            classes.RaiseDomainEvent(new ClassesCreatedEvent(classes.Id, classes.Name.Value));
            return classes;
        }
    }
}
