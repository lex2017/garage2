namespace Garage2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addnamecheck : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "Name", c => c.String());
        }
    }
}
