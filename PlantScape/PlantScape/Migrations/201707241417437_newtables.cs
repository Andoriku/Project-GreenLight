namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "reqId", c => c.String());
            AddColumn("dbo.Projects", "devId", c => c.String());
            DropColumn("dbo.Projects", "requesterId");
            DropColumn("dbo.Projects", "developerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "developerId", c => c.Int(nullable: false));
            AddColumn("dbo.Projects", "requesterId", c => c.Int(nullable: false));
            DropColumn("dbo.Projects", "devId");
            DropColumn("dbo.Projects", "reqId");
        }
    }
}
