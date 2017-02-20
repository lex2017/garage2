namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        MemberId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.MemberId);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegNumber = c.String(nullable: false, maxLength: 30),
                        Color = c.String(),
                        Manufacturer = c.String(),
                        Model = c.String(),
                        NumberOfWheels = c.Int(nullable: false),
                        ParkAt = c.DateTime(nullable: false),
                        VehicleTypeId = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeId, cascadeDelete: true)
                .Index(t => t.VehicleTypeId)
                .Index(t => t.MemberId);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        VehicleTypeId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.VehicleTypeId);
            
            CreateTable(
                "dbo.ReceiptViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkTime = c.Time(nullable: false, precision: 7),
                        ParkAt = c.DateTime(nullable: false),
                        ParkOut = c.DateTime(nullable: false),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StatisticsViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VehicleCount = c.Int(nullable: false),
                        WheelsTotal = c.Int(nullable: false),
                        CostTotal = c.Int(nullable: false),
                        Type_VehicleTypeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VehicleTypes", t => t.Type_VehicleTypeId)
                .Index(t => t.Type_VehicleTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StatisticsViewModels", "Type_VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "VehicleTypeId", "dbo.VehicleTypes");
            DropForeignKey("dbo.Vehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.StatisticsViewModels", new[] { "Type_VehicleTypeId" });
            DropIndex("dbo.Vehicles", new[] { "MemberId" });
            DropIndex("dbo.Vehicles", new[] { "VehicleTypeId" });
            DropTable("dbo.StatisticsViewModels");
            DropTable("dbo.ReceiptViewModels");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Vehicles");
            DropTable("dbo.Members");
        }
    }
}
