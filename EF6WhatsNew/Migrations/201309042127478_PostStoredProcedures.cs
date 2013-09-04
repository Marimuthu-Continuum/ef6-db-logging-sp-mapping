namespace EF6WhatsNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostStoredProcedures : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure(
                "dbo.Post_Insert",
                p => new
                    {
                        POST_TITLE = p.String(maxLength: 200),
                        POST_CONTENT = p.String(),
                    },
                body:
                    @"INSERT [dbo].[Posts]([POST_TITLE], [POST_CONTENT])
                      VALUES (@POST_TITLE, @POST_CONTENT)
                      
                      DECLARE @POST_PK_POST_ID int
                      SELECT @POST_PK_POST_ID = [POST_PK_POST_ID]
                      FROM [dbo].[Posts]
                      WHERE @@ROWCOUNT > 0 AND [POST_PK_POST_ID] = scope_identity()
                      
                      SELECT t0.[POST_PK_POST_ID]
                      FROM [dbo].[Posts] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[POST_PK_POST_ID] = @POST_PK_POST_ID"
            );
            
            CreateStoredProcedure(
                "dbo.Post_Update",
                p => new
                    {
                        POST_PK_POST_ID = p.Int(),
                        POST_TITLE = p.String(maxLength: 200),
                        POST_CONTENT = p.String(),
                    },
                body:
                    @"UPDATE [dbo].[Posts]
                      SET [POST_TITLE] = @POST_TITLE, [POST_CONTENT] = @POST_CONTENT
                      WHERE ([POST_PK_POST_ID] = @POST_PK_POST_ID)"
            );
            
            CreateStoredProcedure(
                "dbo.Post_Delete",
                p => new
                    {
                        POST_PK_POST_ID = p.Int(),
                    },
                body:
                    @"DELETE [dbo].[Posts]
                      WHERE ([POST_PK_POST_ID] = @POST_PK_POST_ID)"
            );
            
        }
        
        public override void Down()
        {
            DropStoredProcedure("dbo.Post_Delete");
            DropStoredProcedure("dbo.Post_Update");
            DropStoredProcedure("dbo.Post_Insert");
        }
    }
}
