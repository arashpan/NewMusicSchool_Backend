using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } // نقش (مدیر کل شعبه، مدیر شعبه، منشی و ...)
        public string Description { get; set; } // توضیحات مربوط به نقش
    }
}