namespace SerPro.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Products");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
