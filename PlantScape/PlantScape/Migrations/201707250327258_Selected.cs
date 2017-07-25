namespace PlantScape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Selected : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "userComments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Projects", "userComments");
        }
    }
}
