namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plantit : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlantViewModels", newName: "Plants");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Plants", newName: "PlantViewModels");
        }
    }
}
