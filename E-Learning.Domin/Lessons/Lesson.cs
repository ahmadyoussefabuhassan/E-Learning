using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons.Event;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Domain.Lessons
{
    public class Lesson : Entity
    {
        private Lesson() : base(Guid.Empty)
        {

        }
        private Lesson(Guid id, string lessontitle, string url, string titleurl, Guid unitId) : base(id)
        {
            LessonTitle = lessontitle;
            URL = url;
            TitleUrl = titleurl;
            UnitId = unitId;
        }
        [MaxLength(30)]
        public string LessonTitle { get; private set; }
        [MaxLength(255)]
        public string URL { get; private set; }
        [MaxLength(50)]
        public string TitleUrl { get; private set; }
        public Guid UnitId { get; private set; }
        public static Lesson Create(Guid id, string lessontitle, string url, string titleurl, Guid unitId)
        {
            if (string.IsNullOrWhiteSpace(lessontitle))
                throw new ArgumentException("Lesson title cannot be null or empty.", nameof(lessontitle));
            lessontitle = lessontitle.Trim();
            if (lessontitle.Length > 30)
                throw new ArgumentException("Lesson title must be at most 30 characters.", nameof(lessontitle));
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            url = url.Trim();
            if (url.Length > 255)
                throw new ArgumentException("URL must be at most 255 characters.", nameof(url));
            if (string.IsNullOrWhiteSpace(titleurl))
                throw new ArgumentException("Title URL cannot be null or empty.", nameof(titleurl));
            titleurl = titleurl.Trim();
            if (titleurl.Length > 255)
                throw new ArgumentException("Title URL must be at most 255 characters.", nameof(titleurl));
            if (unitId == Guid.Empty)
                throw new ArgumentException("UnitId cannot be empty.", nameof(unitId));
            if (id == Guid.Empty)
                id = Guid.NewGuid();
            var lesson = new Lesson(id, lessontitle, url, titleurl, unitId);
            lesson.RaiseDomainEvent(new LessonCreatedDomainEvent(lesson.Id, lesson.LessonTitle, lesson.URL, lesson.TitleUrl, lesson.UnitId));
            return lesson;
        }
    }
}
