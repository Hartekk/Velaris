namespace VelarisBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToTodoItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TodoItems", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TodoItems", "UserId", c => c.Int(nullable: false));
        }
    }
}
