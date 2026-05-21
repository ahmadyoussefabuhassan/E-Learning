using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Classes.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Classes
{
    public class Classes : Entity
    {
        private Classes () : base (Guid.Empty)
        { }
        private Classes (Guid id, string name, string description, Guid teachersID, Guid  studentID) : base (id)
        {
            Name = name;
            Description = description;
            TeachersID = teachersID;
            StudentID = studentID;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid TeachersID { get; private set; }
        public Guid StudentID { get; private set; }

        public static Classes Create(string name, string description, Guid teachersID, Guid studentID)
        {
            var @class = new Classes(Guid.NewGuid(), name, description, teachersID, studentID);
            @class .RaiseDomainEvent (new ClassesCreatedEvent(@class.Id, @class.Name, @class.Description, @class.TeachersID, @class.StudentID));
            return @class;
        }
    }
}
