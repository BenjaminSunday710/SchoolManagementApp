using FluentNHibernate.Mapping;
using SchoolManagementApp.Domain.Schools;
using Shared.Infrastructure.Mappings;
using System;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class SchoolMap:BaseMap<Guid, School>
    {
        public SchoolMap()
        {
            Table("Schools");
            Map(x => x.Name);
            Map(x => x.StaffIdFormat);
            Map(x => x.StudentIdFormat);
            Map(x => x.LastStaffIdIndex);
            Map(x => x.LastStudentIdIndex);
            Component(x => x.Location,
                member =>
                {
                    member.Map(x => x.House_Number);
                    member.Map(x => x.Street);
                    member.Map(x => x.City);
                });
            HasMany(x => x.NonAcademicStaffs).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.AcademicStaffs).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.SchoolClasses).Inverse().Cascade.AllDeleteOrphan();
            HasMany(x => x.Students).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
