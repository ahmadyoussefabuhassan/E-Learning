using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Courses.Events;

namespace E_Learning.Domain.Courses
{
    public class Course : Entity
    {
        private Course() : base(Guid.Empty)
        {
        }

        private Course(Guid id, Name name, ImageUrl imageUrl, Description description, Price price, IsActive isActive, Guid classesId, Guid teacherId) : base(id)
        {
            Name = name;
            ImageUrl = imageUrl;
            Description = description;
            Price = price;
            IsActive = isActive;
            ClassesId = classesId;
            TeacherId = teacherId;
        }

        public Name Name { get; private set; }

        public ImageUrl ImageUrl { get; private set; }

        public Description Description { get; private set; }

        public Price Price { get; private set; }

        public IsActive IsActive { get; private set; }
        public Guid ClassesId { get; private set; }
        public Guid TeacherId { get; private set; }

        public static Course Create(Guid id, Name name, ImageUrl imageUrl, Description description, Price price, IsActive isActive, Guid classesId, Guid teacherId)
        {
            var course = new Course(id, name, imageUrl, description, price, isActive, classesId, teacherId);
            course.RaiseDomainEvent(new CourseCreatedDomainEvent(course.Id, course.Name.Value, course.Price.Value, course.TeacherId, course.ClassesId));
            return course;
        }
    }
}
