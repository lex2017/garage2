namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatisticsViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleCount = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        WheelsTotal = c.Int(nullable: false),
                        CostTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.StatisticsViewModels");
        }
    }
}
