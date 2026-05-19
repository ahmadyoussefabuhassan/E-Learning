using E_Learning.Domain.Abstractions;

namespace E_Learning.Domain.Teachers
{
    public class Teacher : Entity
    {
        public Teacher() { }
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
            return teacher;
        }
    }
}
