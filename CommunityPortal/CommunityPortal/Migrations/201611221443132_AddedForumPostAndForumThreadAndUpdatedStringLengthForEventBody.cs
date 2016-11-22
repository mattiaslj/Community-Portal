namespace CommunityPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForumPostAndForumThreadAndUpdatedStringLengthForEventBody : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(nullable: false),
                        PostTime = c.DateTime(nullable: false),
                        Thread_Id = c.Int(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ForumThreads", t => t.Thread_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Thread_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ForumThreads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        PostTime = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            AlterColumn("dbo.Events", "Body", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumPosts", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumPosts", "Thread_Id", "dbo.ForumThreads");
            DropForeignKey("dbo.ForumThreads", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ForumThreads", new[] { "User_Id" });
            DropIndex("dbo.ForumPosts", new[] { "User_Id" });
            DropIndex("dbo.ForumPosts", new[] { "Thread_Id" });
            AlterColumn("dbo.Events", "Body", c => c.String(nullable: false, maxLength: 2000));
            DropTable("dbo.ForumThreads");
            DropTable("dbo.ForumPosts");
        }
    }
}
