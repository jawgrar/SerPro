namespace SerPro.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityRoles", newName: "Roles");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "UserRoles");
            RenameTable(name: "dbo.UserMasters", newName: "UserMaster");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "UserClaims");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "UserLogins");
            DropPrimaryKey("dbo.UserClaims");
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        Description = c.String(),
                        CreatedTimestamp = c.DateTime(nullable: false),
                        UpdatedTimestamp = c.DateTime(nullable: false),
                        ContentType = c.String(),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.UserClaims", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.UserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.UserClaims", "UserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserClaims");
            AlterColumn("dbo.UserClaims", "UserId", c => c.String());
            AlterColumn("dbo.UserClaims", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Products");
            AddPrimaryKey("dbo.UserClaims", "Id");
            RenameTable(name: "dbo.UserLogins", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.UserClaims", newName: "IdentityUserClaims");
            RenameTable(name: "dbo.UserMaster", newName: "UserMasters");
            RenameTable(name: "dbo.UserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.Roles", newName: "IdentityRoles");
        }
    }
}
