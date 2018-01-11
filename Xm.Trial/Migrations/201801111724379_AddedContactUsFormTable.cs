namespace Xm.Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedContactUsFormTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUsForms",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(nullable: false, maxLength: 150),
                        Message = c.String(nullable: false),
                        SentDate = c.DateTimeOffset(nullable: false, precision: 7),
                        ScreenshotsPath = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactUsForms");
        }
    }
}
