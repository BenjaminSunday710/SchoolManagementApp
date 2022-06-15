using FluentMigrator;

namespace UserManagement.Infrastructure.Migrations
{
    [Migration(202205200957)]
    public class Initial_UserManagement_Table : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsGuid().NotNullable().Unique()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("PasswordHash").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable()
                .WithColumn("RefreshToken").AsString().NotNullable()
                .WithColumn("RefreshTokenExpiryTime").AsDateTime().NotNullable();

            Create.Table("Roles")
                .WithColumn("Id").AsGuid().NotNullable().Unique()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("UserRoles")
                .WithColumn("User_id").AsGuid().NotNullable()
                    .ForeignKey("Fk_UserRoles_User_Id", "Users", "Id")
                .WithColumn("Role_id").AsGuid().NotNullable()
                    .ForeignKey("Fk_UserRoles_Role_Id", "Roles", "Id");

            var userRoleCompositeKey = new string[] { "User_id", "Role_id" };
            Create.PrimaryKey("Pk_User_Role_Id").OnTable("UserRoles").Columns(userRoleCompositeKey);

            Create.Table("Permissions")
                .WithColumn("Id").AsGuid().NotNullable().Unique()
                .WithColumn("Name").AsString().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable()
                .WithColumn("LastModified").AsString().Nullable()
                .WithColumn("LastModifiedBy").AsDateTime().Nullable();

            Create.Table("RolePermissions")
               .WithColumn("Role_id").AsGuid().NotNullable()
                    .ForeignKey("Fk_RolePermissions_Role_Id", "Roles", "Id")
               .WithColumn("Permission_id").AsGuid().NotNullable()
                    .ForeignKey("Fk_RolePermissions_Permission_Id", "Permissions", "Id");

            var rolePermissionCompositeKey = new string[] { "Role_id", "Permission_id" };
            Create.PrimaryKey("Pk_Role_Permission_Id").OnTable("RolePermissions").Columns(rolePermissionCompositeKey);
        }
    }
}
