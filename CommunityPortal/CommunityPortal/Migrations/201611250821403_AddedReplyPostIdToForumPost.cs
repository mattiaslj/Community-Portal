namespace CommunityPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedReplyPostIdToForumPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ForumPosts", "ReplyPostId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ForumPosts", "ReplyPostId");
        }
    }
}
