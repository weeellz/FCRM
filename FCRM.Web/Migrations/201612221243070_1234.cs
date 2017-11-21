namespace FCRM.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1234 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Stage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Stage");
        }
    }
}
