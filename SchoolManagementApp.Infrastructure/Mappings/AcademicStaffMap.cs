using SchoolManagementApp.Domain.AcademicStaffs;
using Shared.Infrastructure.Mappings;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class AcademicStaffMap:BaseMap<int,AcademicStaff>
    {
        public AcademicStaffMap()
        {
            Table("AcademicStaffs");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.LG_Of_Origin);
            Map(x => x.StateOfOrigin);
            Map(x => x.DateOfBirth);
            Map(x => x.Gender);
            Map(x => x.PhoneNumber);
            Map(x => x.Designation);
            Component(x => x.Address,
                member =>
                {
                    member.Map(x => x.House_Number);
                    member.Map(x => x.Street);
                    member.Map(x => x.City);
                });
            References(x => x.School);
            HasOne(x => x.SchoolClass);
            HasMany(x => x.Subjects).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
