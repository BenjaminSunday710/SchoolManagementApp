using SchoolManagementApp.Domain.NonAcademicStaffs;
using SchoolManagementApp.Domain.SharedKernel.Persons;
using Shared.Infrastructure.Mappings;
using System;

namespace SchoolManagementApp.Infrastructure.Mappings
{
    public class NonAcademicStaffMap:BaseMap<Guid, NonAcademicStaff>
    {
        public NonAcademicStaffMap()
        {
            Table("NonAcademicStaffs");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.LG_Of_Origin);
            Map(x => x.StateOfOrigin);
            Map(x => x.DateOfBirth);
            Map(x => x.Gender).CustomType<GenericEnumMapper<Gender>>().Not.Nullable();
            Map(x => x.PhoneNumber);
            Map(x => x.Unit).CustomType<GenericEnumMapper<Unit>>().Not.Nullable();
            Map(x => x.Designation);
            Map(x => x.EmploymentId);
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
