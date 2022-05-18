using FluentMigrator;

namespace UserManagement.Infrastructure.Migrations
{
    [Migration(202205140357)]
    public class Initial_UserManagement_Table : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt64().NotNullable().Unique()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("Created").AsDateTimeOffset().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTimeOffset().Nullable();

            Create.Table("Roles")
                .WithColumn("Id").AsInt64().NotNullable().Unique()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Created").AsDateTimeOffset().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTimeOffset().Nullable();

            Create.Table("UserRoles")
                .WithColumn("Id").AsInt64().NotNullable().Unique()
                .WithColumn("UserId").AsInt64().NotNullable()
                    .ForeignKey("Fk_UserRoles_User_Id", "Users", "Id")
                .WithColumn("RoleId").AsInt64().NotNullable()
                    .ForeignKey("Fk_UserRoles_Role_Id", "Roles", "Id")
                .WithColumn("Created").AsDateTimeOffset().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTimeOffset().Nullable();

            var userRoleCompositeKey = new string[] { "UserId", "RoleId" };
            Create.PrimaryKey("Pk_User_Role_Id").OnTable("UserRoles").Columns(userRoleCompositeKey);

            Create.Table("Permissions")
                .WithColumn("Id").AsInt64().NotNullable().Unique()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Created").AsDateTimeOffset().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTimeOffset().Nullable();

            Create.Table("RolePermissions")
               .WithColumn("Id").AsInt64().NotNullable().Unique()
               .WithColumn("Created").AsDateTimeOffset().NotNullable()
               .WithColumn("CreatedBy").AsString().NotNullable()
               .WithColumn("LastModified").AsString().Nullable()
               .WithColumn("LastModifiedBy").AsDateTimeOffset().Nullable()
               .WithColumn("RoleId").AsInt64().NotNullable()
               .ForeignKey("Fk_RolePermissions_Role_Id", "Roles", "Id")
               .WithColumn("PermissionId").AsInt64().NotNullable()
               .ForeignKey("Fk_RolePermissions_Permission_Id", "Permissions", "Id");

            var rolePermissionCompositeKey = new string[] { "RoleId", "PermissionId" };
            Create.PrimaryKey("Pk_Role_Permission_Id").OnTable("RolePermissions").Columns(rolePermissionCompositeKey);
        }
    }
}
