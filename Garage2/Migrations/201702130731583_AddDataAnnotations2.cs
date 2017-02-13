namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotations2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReceiptViewModels", "ParkTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.ReceiptViewModels", "ParkOut", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReceiptViewModels", "ParkOut", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.ReceiptViewModels", "ParkTime", c => c.DateTime(nullable: false));
        }
    }
}
