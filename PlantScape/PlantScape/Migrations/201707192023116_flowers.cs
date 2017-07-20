namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flowers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plants", "flowers", c => c.String());
            AddColumn("dbo.Plants", "imageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plants", "imageUrl");
            DropColumn("dbo.Plants", "flowers");
        }
    }
}
