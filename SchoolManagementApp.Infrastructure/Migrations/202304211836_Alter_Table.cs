using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementApp.Infrastructure.Migrations
{
    [Migration(202304211836, "Alter_Table")]
    public class Alter_Table : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table("Schools")
                .AddColumn("StaffIdFormat").AsString().NotNullable().WithDefaultValue("Default")
                .AddColumn("StudentIdFormat").AsString().NotNullable().WithDefaultValue("Default")
                .AddColumn("LastStaffIdIndex").AsInt64().NotNullable().WithDefaultValue(0)
                .AddColumn("LastStudentIdIndex").AsInt64().NotNullable().WithDefaultValue(0);

            Alter.Table("AcademicStaffs")
                .AddColumn("EmploymentId").AsString().NotNullable().WithDefaultValue("Default");

            Alter.Table("NonAcademicStaffs")
                .AddColumn("EmploymentId").AsString().NotNullable().WithDefaultValue("Default");

            Alter.Table("Students")
                .AddColumn("RegistrationId").AsString().NotNullable().WithDefaultValue("Default");
        }
    }
}
