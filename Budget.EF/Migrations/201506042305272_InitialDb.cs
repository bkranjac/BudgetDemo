namespace Budget.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BudgetCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        CreatedUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BudgetItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsFixed = c.Boolean(nullable: false),
                        IsExpense = c.Boolean(nullable: false),
                        Notes = c.String(),
                        DateOccured = c.DateTime(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        BudgetSubCategoryId = c.Int(),
                        BudgetLocationId = c.Int(),
                        CreatedUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BudgetLocations", t => t.BudgetLocationId)
                .ForeignKey("dbo.BudgetSubCategories", t => t.BudgetSubCategoryId)
                .Index(t => t.BudgetSubCategoryId)
                .Index(t => t.BudgetLocationId);
            
            CreateTable(
                "dbo.BudgetLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false, maxLength: 500),
                        CreatedUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BudgetSubCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BudgetCategoryId = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 500),
                        CreatedUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BudgetItems", "BudgetSubCategoryId", "dbo.BudgetSubCategories");
            DropForeignKey("dbo.BudgetItems", "BudgetLocationId", "dbo.BudgetLocations");
            DropIndex("dbo.BudgetItems", new[] { "BudgetLocationId" });
            DropIndex("dbo.BudgetItems", new[] { "BudgetSubCategoryId" });
            DropTable("dbo.BudgetSubCategories");
            DropTable("dbo.BudgetLocations");
            DropTable("dbo.BudgetItems");
            DropTable("dbo.BudgetCategories");
        }
    }
}
