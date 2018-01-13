namespace Xm.Trial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPostViewTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        Views = c.String(),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostViews", "PostId", "dbo.Posts");
            DropIndex("dbo.PostViews", new[] { "PostId" });
            DropTable("dbo.PostViews");
        }
    }
}
