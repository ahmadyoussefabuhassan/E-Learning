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
        private Lesson(Guid id, LessonTitle lessontitle, URL url, TitleUrl titleurl, Guid unitId) : base(id)
        {
            LessonTitle = lessontitle;
            URL = url;
            TitleUrl = titleurl;
            UnitId = unitId;
        }
        public LessonTitle LessonTitle { get; private set; }
        public URL URL { get; private set; }
        public TitleUrl TitleUrl { get; private set; }
        public Guid UnitId { get; private set; }
        public static Lesson Create(Guid id, LessonTitle lessontitle, URL url, TitleUrl titleurl, Guid unitId)
        {
            var lesson = new Lesson(id, lessontitle, url, titleurl, unitId);
            lesson.RaiseDomainEvent(new LessonCreatedDomainEvent(lesson.Id, lesson.LessonTitle.Value, lesson.URL.Value, lesson.TitleUrl.Value, lesson.UnitId));
            return lesson;
        }
    }
}
