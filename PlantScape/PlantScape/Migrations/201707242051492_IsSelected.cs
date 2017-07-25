namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsSelected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plants", "isSelected", c => c.Boolean(nullable: false));
            DropColumn("dbo.Projects", "selected");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "selected", c => c.Boolean(nullable: false));
            DropColumn("dbo.Plants", "isSelected");
        }
    }
}
