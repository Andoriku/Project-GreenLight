namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "Quote", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "Quote");
        }
    }
}
