namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTimeStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "ParkAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "ParkAt");
        }
    }
}
