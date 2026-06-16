using E_Learning.Domain.Abstractions;
using E_Learning.Domain.Lessons.Event;
using E_Learning.Domain.Units;

namespace E_Learning.Domain.Lessons
{
    public sealed class Lesson : Entity
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
        public Unit Unit { get; private set; } = null!;
        public static Lesson Create(LessonTitle lessontitle, URL url, TitleUrl titleurl, Guid unitId)
        {
            var lesson = new Lesson(Guid.NewGuid(), lessontitle, url, titleurl, unitId);
            lesson.RaiseDomainEvent(new LessonCreatedDomainEvent(lesson.Id, lesson.LessonTitle.Value, lesson.URL.Value, lesson.TitleUrl.Value, lesson.UnitId));
            return lesson;
        }
        public void UpdateLesson(LessonTitle title , URL uRL , TitleUrl titleUrl)
        {
            LessonTitle = title;
            URL = uRL;
            TitleUrl = titleUrl;
        }
    }
}
