namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projects : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        projectName = c.String(),
                        image = c.Binary(),
                        developer_Id = c.String(maxLength: 128),
                        requester_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.AspNetUsers", t => t.developer_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.requester_Id)
                .Index(t => t.developer_Id)
                .Index(t => t.requester_Id);
            
            CreateTable(
                "dbo.ProjectsPlants",
                c => new
                    {
                        Projects_id = c.Int(nullable: false),
                        Plants_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Projects_id, t.Plants_id })
                .ForeignKey("dbo.Projects", t => t.Projects_id, cascadeDelete: true)
                .ForeignKey("dbo.Plants", t => t.Plants_id, cascadeDelete: true)
                .Index(t => t.Projects_id)
                .Index(t => t.Plants_id);
            
            AddColumn("dbo.AspNetUsers", "project_id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "requestedProject_id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "project_id");
            CreateIndex("dbo.AspNetUsers", "requestedProject_id");
            AddForeignKey("dbo.AspNetUsers", "project_id", "dbo.Projects", "id");
            AddForeignKey("dbo.AspNetUsers", "requestedProject_id", "dbo.Projects", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "requester_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProjectsPlants", "Plants_id", "dbo.Plants");
            DropForeignKey("dbo.ProjectsPlants", "Projects_id", "dbo.Projects");
            DropForeignKey("dbo.Projects", "developer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "requestedProject_id", "dbo.Projects");
            DropForeignKey("dbo.AspNetUsers", "project_id", "dbo.Projects");
            DropIndex("dbo.ProjectsPlants", new[] { "Plants_id" });
            DropIndex("dbo.ProjectsPlants", new[] { "Projects_id" });
            DropIndex("dbo.AspNetUsers", new[] { "requestedProject_id" });
            DropIndex("dbo.AspNetUsers", new[] { "project_id" });
            DropIndex("dbo.Projects", new[] { "requester_Id" });
            DropIndex("dbo.Projects", new[] { "developer_Id" });
            DropColumn("dbo.AspNetUsers", "requestedProject_id");
            DropColumn("dbo.AspNetUsers", "project_id");
            DropTable("dbo.ProjectsPlants");
            DropTable("dbo.Projects");
        }
    }
}
