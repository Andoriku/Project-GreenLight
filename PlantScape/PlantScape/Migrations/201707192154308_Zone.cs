namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HardinessZones",
                c => new
                    {
                        zipcode = c.Int(nullable: false, identity: true),
                        zone = c.String(),
                    })
                .PrimaryKey(t => t.zipcode);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HardinessZones");
        }
    }
}
