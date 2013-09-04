namespace EF6WhatsNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TweakSP : DbMigration
    {
        public override void Up()
        {
            RenameStoredProcedure(name: "dbo.Post_Update", newName: "UpdatePost");
            AlterStoredProcedure(
                "dbo.UpdatePost",
                p => new
                    {
                        POST_PK_POST_ID = p.Int(),
                        POST_TITLE = p.String(maxLength: 200),
                        post_content = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Posts]
                      SET [POST_TITLE] = @POST_TITLE, [POST_CONTENT] = @post_content
                      WHERE ([POST_PK_POST_ID] = @POST_PK_POST_ID)"
            );
            
        }
        
        public override void Down()
        {
            RenameStoredProcedure(name: "dbo.UpdatePost", newName: "Post_Update");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
