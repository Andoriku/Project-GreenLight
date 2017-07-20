namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlantViewModels",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        botanicalName = c.String(),
                        commonName = c.String(),
                        type = c.String(),
                        fColorSpring = c.String(),
                        fColorFall = c.String(),
                        leafType = c.String(),
                        hardinessZone = c.String(),
                        soilType = c.Int(nullable: false),
                        lightReq = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PlantViewModels");
        }
    }
}
