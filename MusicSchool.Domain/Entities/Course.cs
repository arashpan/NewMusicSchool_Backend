using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public int DurationInWeeks { get; set; }  // مدت زمان دوره به هفته
        public int TeacherId { get; set; }  // ارتباط با استاد
        public Teacher Teacher { get; set; }  // ارجاع به استاد
    }
}