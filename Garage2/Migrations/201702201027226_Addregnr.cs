namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addregnr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceiptViewModels", "RegNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceiptViewModels", "RegNumber");
        }
    }
}
