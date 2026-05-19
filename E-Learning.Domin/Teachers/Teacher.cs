using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Teachers.Events;

namespace E_Learning.Domain.Teachers
{
    public class Teacher : Entity
    {
        private Teacher() : base(Guid.Empty)
        {
        }

        private Teacher(Guid Id, UrlShamCash urlShamCash, SubjectTeacher subjectTeacher) : base(Id)
        {
            UrlShamCash = urlShamCash;
            SubjectTeacher = subjectTeacher;
        }
        public UrlShamCash UrlShamCash { get; private set; }
        public SubjectTeacher SubjectTeacher { get; private set; }
        public static Teacher Create(Guid userId, UrlShamCash urlShamCash , SubjectTeacher subjectTeacher)
        {
            var teacher = new Teacher(userId, urlShamCash ,subjectTeacher);
            teacher.RaiseDomainEvent(new TeacherCreatedDomainEvent(teacher.Id, teacher.UrlShamCash.Value, teacher.SubjectTeacher.Value));
            return teacher;
        }
    }
}
