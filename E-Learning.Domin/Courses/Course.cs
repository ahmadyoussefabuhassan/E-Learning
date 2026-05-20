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

        private Course(Guid id, string name, string imageUrl, string description, decimal price, bool isActive, Guid classesId, Guid teacherId) : base(id)
        {
            Name = name;
            ImageUrl = imageUrl;
            Description = description;
            Price = price;
            IsActive = isActive;
            ClassesId = classesId;
            TeacherId = teacherId;
        }

        [MaxLength(30)]
        public string Name { get; private set; }

        [MaxLength(255)]
        public string ImageUrl { get; private set; }

        [MaxLength(255)]
        public string Description { get; private set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; private set; }

        public bool IsActive { get; private set; }
        public Guid ClassesId { get; private set; }
        public Guid TeacherId { get; private set; }

        public static Course Create(Guid id, string name, string imageUrl, string description, decimal price, bool isActive, Guid classesId, Guid teacherId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Course name cannot be null or empty.", nameof(name));

            name = name.Trim();

            if (name.Length > 30)
                throw new ArgumentException("Course name must be at most 30 characters.", nameof(name));

            if (price < 0)
                throw new ArgumentException("price must be non-negative", nameof(price));

            if (classesId == Guid.Empty)
                throw new ArgumentException("ClassesId cannot be empty.", nameof(classesId));

            if (teacherId == Guid.Empty)
                throw new ArgumentException("TeacherId cannot be empty.", nameof(teacherId));

            if (id == Guid.Empty)
                id = Guid.NewGuid();

            var course = new Course(id, name, imageUrl, description, price, isActive, classesId, teacherId);
            course.RaiseDomainEvent(new CourseCreatedDomainEvent(course.Id, course.Name, course.Price, course.TeacherId, course.ClassesId));
            return course;
        }
    }
}
