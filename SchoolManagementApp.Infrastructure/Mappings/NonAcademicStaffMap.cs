using SchoolManagementApp.Domain.NonAcademicStaffs;
using Shared.Infrastructure.Mappings;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class NonAcademicStaffMap:BaseMap<int,NonAcademicStaff>
    {
        public NonAcademicStaffMap()
        {
            Table("NonAcademicStaffs");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.LG_Of_Origin);
            Map(x => x.StateOfOrigin);
            Map(x => x.DateOfBirth);
            Map(x => x.Gender);
            Map(x => x.PhoneNumber);
            Map(x => x.Unit);
            Map(x => x.Designation);
            Component(x => x.Address,
                member =>
                {
                    member.Map(x => x.House_Number);
                    member.Map(x => x.Street);
                    member.Map(x => x.City);
                });
            References(x => x.School);
        }
    }
}
