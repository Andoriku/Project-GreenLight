namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class displayimage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "displayImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "displayImage");
        }
    }
}
