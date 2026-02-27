using MusicSchool.Domain.Common;

namespace MusicSchool.Domain.Entities
{
    public class Teacher : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Specialization { get; set; }  // تخصص استاد
        public int BranchId { get; set; }  // ارتباط با شعبه
        public Branch Branch { get; set; }  // ارجاع به شعبه
    }
}