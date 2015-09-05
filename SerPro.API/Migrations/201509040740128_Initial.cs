namespace SerPro.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                        UserMaster_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.Roles", t => t.IdentityRole_Id)
                .ForeignKey("dbo.UserMaster", t => t.UserMaster_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.UserMaster_Id);
            
            CreateTable(
                "dbo.UserMaster",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        UserMaster_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserMaster", t => t.UserMaster_Id)
                .Index(t => t.UserMaster_Id);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        UserMaster_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.UserMaster", t => t.UserMaster_Id)
                .Index(t => t.UserMaster_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "UserMaster_Id", "dbo.UserMaster");
            DropForeignKey("dbo.UserLogins", "UserMaster_Id", "dbo.UserMaster");
            DropForeignKey("dbo.UserClaims", "UserMaster_Id", "dbo.UserMaster");
            DropForeignKey("dbo.UserRoles", "IdentityRole_Id", "dbo.Roles");
            DropIndex("dbo.UserLogins", new[] { "UserMaster_Id" });
            DropIndex("dbo.UserClaims", new[] { "UserMaster_Id" });
            DropIndex("dbo.UserRoles", new[] { "UserMaster_Id" });
            DropIndex("dbo.UserRoles", new[] { "IdentityRole_Id" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.UserMaster");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
        }
    }
}
