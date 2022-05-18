using SchoolManagementApp.Domain.Students;
using Shared.Infrastructure.Mappings;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class StudentMap: BaseMap<int, Student>
    {
        public StudentMap()
        {
            Table("Students");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.LG_Of_Origin);
            Map(x => x.StateOfOrigin);
            Map(x => x.DateOfBirth);
            Map(x => x.Gender);
            Map(x => x.PhoneNumber);
            Component(x => x.Address,
                member =>
                {
                    member.Map(x => x.House_Number);
                    member.Map(x => x.Street);
                    member.Map(x => x.City);
                });
            References(x => x.School);
            References(x => x.SchoolClass);
            HasMany(x => x.Subjects).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.Results).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
