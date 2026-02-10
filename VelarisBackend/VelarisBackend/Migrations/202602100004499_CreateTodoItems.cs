namespace VelarisBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTodoItems : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TodoItems", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TodoItems", "Title", c => c.String());
        }
    }
}
