namespace VelarisBackend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TodoItems", "DueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TodoItems", "DueDate");
        }
    }
}
