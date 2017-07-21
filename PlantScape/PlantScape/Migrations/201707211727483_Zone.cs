namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Zones",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        zipcode = c.Int(nullable: false),
                        zone = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            
        }
        
        public override void Down()
        {
          
           DropTable("dbo.Zones");
        }
    }
}
