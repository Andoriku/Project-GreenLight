namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Favorite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationUserPlants",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Plants_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Plants_id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Plants", t => t.Plants_id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Plants_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserPlants", "Plants_id", "dbo.Plants");
            DropForeignKey("dbo.ApplicationUserPlants", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserPlants", new[] { "Plants_id" });
            DropIndex("dbo.ApplicationUserPlants", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserPlants");
        }
    }
}
