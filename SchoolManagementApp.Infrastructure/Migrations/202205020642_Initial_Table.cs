using FluentMigrator;

namespace SchoolManagementApp.Infrastructure.Migrations
{
    [Migration(202205020642)]
    public class Initial_Table : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Schools")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Website").AsString().Nullable()
                .WithColumn("House_Number").AsInt64().NotNullable()
                .WithColumn("Street").AsString().NotNullable()
                .WithColumn("City").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("AcademicStaffs")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("FirstName").AsString().NotNullable()
               .WithColumn("LastName").AsString().NotNullable()
               .WithColumn("DateofBirth").AsDate().NotNullable()
               .WithColumn("LG_Of_Origin").AsString().NotNullable()
               .WithColumn("StateOfOrigin").AsString().NotNullable()
               .WithColumn("Gender").AsString().NotNullable()
               .WithColumn("PhoneNumber").AsString().NotNullable()
               .WithColumn("Designation").AsString().Nullable()
               .WithColumn("House_Number").AsInt64().NotNullable()
               .WithColumn("Street").AsString().NotNullable()
               .WithColumn("City").AsString().NotNullable()
               .WithColumn("School_id").AsGuid().NotNullable().ForeignKey("Fk_AcademicStaffs_School_id", "Schools", "Id")
               .WithColumn("Created").AsDateTime().NotNullable()
               .WithColumn("CreatedBy").AsString().NotNullable()
               .WithColumn("LastModified").AsString().Nullable()
               .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("SchoolClasses")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("School_id").AsGuid().NotNullable().ForeignKey("Fk_SchoolClasses_School_id", "Schools", "Id")
                .WithColumn("ClassTeacher_id").AsGuid().Nullable().ForeignKey("Fk_SchoolClasses_AcademicStaff_id", "AcademicStaffs", "Id")
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("Subjects")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("SchoolClass_id").AsGuid().NotNullable().ForeignKey("Fk_Subjects_SchoolClasses_id", "SchoolClasses", "Id")
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("NonAcademicStaffs")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("FirstName").AsString().NotNullable()
               .WithColumn("LastName").AsString().NotNullable()
               .WithColumn("DateofBirth").AsDate().NotNullable()
               .WithColumn("LG_Of_Origin").AsString().NotNullable()
               .WithColumn("StateOfOrigin").AsString().NotNullable()
               .WithColumn("Gender").AsString().NotNullable()
               .WithColumn("PhoneNumber").AsString().NotNullable()
               .WithColumn("Designation").AsString().Nullable()
               .WithColumn("Unit").AsString().NotNullable()
               .WithColumn("House_Number").AsInt64().NotNullable()
               .WithColumn("Street").AsString().NotNullable()
               .WithColumn("City").AsString().NotNullable()
               .WithColumn("School_id").AsGuid().NotNullable().ForeignKey("Fk_NonAcademicStaffs_School_id", "Schools", "Id")
               .WithColumn("Created").AsDateTime().NotNullable()
               .WithColumn("CreatedBy").AsString().NotNullable()
               .WithColumn("LastModified").AsString().Nullable()
               .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("Students")
              .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
              .WithColumn("FirstName").AsString().NotNullable()
              .WithColumn("LastName").AsString().NotNullable()
              .WithColumn("DateofBirth").AsDate().NotNullable()
              .WithColumn("LG_Of_Origin").AsString().NotNullable()
              .WithColumn("StateOfOrigin").AsString().NotNullable()
              .WithColumn("Gender").AsString().NotNullable()
              .WithColumn("PhoneNumber").AsString().NotNullable()
              .WithColumn("House_Number").AsInt64().NotNullable()
              .WithColumn("Street").AsString().NotNullable()
              .WithColumn("City").AsString().NotNullable()
              .WithColumn("School_id").AsGuid().NotNullable().ForeignKey("Fk_Students_School_id", "Schools", "Id")
              .WithColumn("SchoolClass_id").AsGuid().NotNullable().ForeignKey("Fk_Students_SchoolClass_id", "SchoolClasses", "Id")
              .WithColumn("Created").AsDateTime().NotNullable()
              .WithColumn("CreatedBy").AsString().NotNullable()
              .WithColumn("LastModified").AsString().Nullable()
              .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("AcademicStaffSubjects")
                .WithColumn("Subject_id").AsGuid().NotNullable().ForeignKey("Fk_AcademicStaffSubjects_Subject_id", "Subjects", "Id")
                .WithColumn("AcademicStaff_id").AsGuid().NotNullable().ForeignKey("Fk_AcademicStaffSubjects_AcademicStaff_id", "AcademicStaffs", "Id");

            var staffSubjectCompositeKey = new string[] { "AcademicStaff_id", "Subject_id" };
            Create.PrimaryKey("Pk_AcademicStaff_Subjects_id").OnTable("AcademicStaffSubjects").Columns(staffSubjectCompositeKey);

            Create.Table("StudentSubjects")
                 .WithColumn("Subject_id").AsGuid().NotNullable().ForeignKey("Fk_StudentSubjects_Subject_id", "Subjects", "Id")
                 .WithColumn("Student_id").AsGuid().NotNullable().ForeignKey("Fk_StudentSubjects_Student_id", "Students", "Id");

            var studentSubjectCompositeKey = new string[] { "Student_id", "Subject_id" };
            Create.PrimaryKey("Pk_Student_Subjects_id").OnTable("StudentSubjects").Columns(studentSubjectCompositeKey);

            Create.Table("ResultVariantManagers")
               .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
               .WithColumn("Session").AsString().NotNullable()
               .WithColumn("Term").AsString().NotNullable()
               .WithColumn("Created").AsDateTime().NotNullable()
               .WithColumn("CreatedBy").AsString().NotNullable()
               .WithColumn("LastModified").AsString().Nullable()
               .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("Results")
                 .WithColumn("Id").AsGuid().NotNullable()
                 .WithColumn("Created").AsDateTime().NotNullable()
                 .WithColumn("CreatedBy").AsString().NotNullable()
                 .WithColumn("LastModified").AsString().Nullable()
                 .WithColumn("LastModifiedBy").AsDateTime().Nullable()
                 .WithColumn("ContinuousAssessment").AsString().NotNullable()
                 .WithColumn("Examination").AsString().NotNullable()
                 .WithColumn("Total").AsString().NotNullable()
                 .WithColumn("Grade").AsString().NotNullable()
                 .WithColumn("Remark").AsString().NotNullable()
                 .WithColumn("SchoolClass_id").AsGuid().NotNullable().ForeignKey("Fk_Results_SchoolClass_id", "SchoolClasses", "Id")
                 .WithColumn("Subject_id").AsGuid().NotNullable().ForeignKey("Fk_Results_Subject_id", "Subjects", "Id")
                 .WithColumn("Student_id").AsGuid().NotNullable().ForeignKey("Fk_Results_Student_id", "Students", "Id")
                 .WithColumn("ResultVariantManager_id").AsGuid().NotNullable().ForeignKey("Fk_Results_ResultVariantManager_Id", "ResultVariantManagers", "Id");

            var resultCompositeKey = new string[] { "Student_id", "Subject_id", "SchoolClass_id","ResultVariantManager_id" };
            Create.PrimaryKey("Pk_Result_Student_Subject_Id").OnTable("Results").Columns(resultCompositeKey);
        }
    }
}
