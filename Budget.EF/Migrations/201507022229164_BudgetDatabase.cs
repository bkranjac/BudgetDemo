namespace Budget.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BudgetDatabase : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BudgetCategories", "CreatedUtc", c => c.DateTime());
            AlterColumn("dbo.BudgetItems", "CreatedUtc", c => c.DateTime());
            AlterColumn("dbo.BudgetLocations", "CreatedUtc", c => c.DateTime());
            AlterColumn("dbo.BudgetSubCategories", "CreatedUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BudgetSubCategories", "CreatedUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BudgetLocations", "CreatedUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BudgetItems", "CreatedUtc", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BudgetCategories", "CreatedUtc", c => c.DateTime(nullable: false));
        }
    }
}
