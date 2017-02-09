namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotationsMig : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "RegNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "RegNumber", c => c.String());
        }
    }
}
