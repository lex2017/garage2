namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReceiptViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParkTime = c.DateTime(nullable: false),
                        ParkAt = c.DateTime(nullable: false),
                        ParkOut = c.Time(nullable: false, precision: 7),
                        Cost = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReceiptViewModels");
        }
    }
}
