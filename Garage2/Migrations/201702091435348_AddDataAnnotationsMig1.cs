namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataAnnotationsMig1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Vehicles", "RegNumber", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Vehicles", "RegNumber", c => c.String(nullable: false));
        }
    }
}
