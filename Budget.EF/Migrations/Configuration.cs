namespace Budget.EF.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations.Model;
    using System.Data.Entity.SqlServer;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Budget.EF.BudgetContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("System.Data.SqlClient", new CustomSqlServerMigrationSqlGenerator());

        }

        protected override void Seed(Budget.EF.BudgetContext context)
        {
            context.BudgetCategories.AddOrUpdate(x => x.Id,
                new Budget.Domain.Models.BudgetCategory() { Id = 1, Description = "Food" },
                new Budget.Domain.Models.BudgetCategory() { Id = 2, Description = "Transportation" },
                new Budget.Domain.Models.BudgetCategory() { Id = 3, Description = "Household" },
                new Budget.Domain.Models.BudgetCategory() { Id = 4, Description = "Entertaintment" },
                new Budget.Domain.Models.BudgetCategory() { Id = 5, Description = "Rent" },
                new Budget.Domain.Models.BudgetCategory() { Id = 6, Description = "Utilities" },
                new Budget.Domain.Models.BudgetCategory() { Id = 7, Description = "Travel" }
                );

            // SubCategories
            context.BudgetSubCategories.AddOrUpdate(x => x.Id,
            new Budget.Domain.Models.BudgetSubCategory() { Id = 1, Description = "Groceries", BudgetCategoryId = 1 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 2, Description = "Lunches", BudgetCategoryId = 1 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 3, Description = "Coffee, snacks", BudgetCategoryId = 1 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 4, Description = "Gas", BudgetCategoryId = 2 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 4, Description = "Car Registration", BudgetCategoryId = 2 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 5, Description = "Public Transit", BudgetCategoryId = 2 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 6, Description = "Parking", BudgetCategoryId = 2 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 7, Description = "PG&E", BudgetCategoryId = 6 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 8, Description = "DSL", BudgetCategoryId = 6 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 9, Description = "Netflix", BudgetCategoryId = 4 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 10, Description = "Books", BudgetCategoryId = 4 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 11, Description = "Clothes", BudgetCategoryId = 3 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 12, Description = "Gifts, Parties", BudgetCategoryId = 4 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 13, Description = "Rent", BudgetCategoryId = 5 },
            new Budget.Domain.Models.BudgetSubCategory() { Id = 14, Description = "DSL", BudgetCategoryId = 3 }


         );
            // locations
            context.Locations.AddOrUpdate(x => x.Id,
                new Budget.Domain.Models.BudgetLocation() { Id = 1, Description = "Whole Foods" },
                new Budget.Domain.Models.BudgetLocation() { Id = 2, Description = "Trader Joes" },
                new Budget.Domain.Models.BudgetLocation() { Id = 3, Description = "Safeway" },
                new Budget.Domain.Models.BudgetLocation() { Id = 4, Description = "Arguello Supermarket" },
                new Budget.Domain.Models.BudgetLocation() { Id = 5, Description = "Starbucks" },
                new Budget.Domain.Models.BudgetLocation() { Id = 7, Description = "Shell" },
                new Budget.Domain.Models.BudgetLocation() { Id = 8, Description = "Clipper card" }
                );
            // rent
            context.BudgetItems.AddOrUpdate(x => x.Id,
                new Budget.Domain.Models.BudgetItem() { Id = 1, Notes = "Rent", Amount = 1100, BudgetSubCategoryId = 13, IsFixed = true, IsExpense = true, DateOccured = System.DateTime.Now },
                new Budget.Domain.Models.BudgetItem() { Id = 2, Notes = "Phone", Amount = 45, BudgetSubCategoryId = 14, IsFixed = true, IsExpense = true, DateOccured = System.DateTime.Now }
                );
        }
    }

    internal class CustomSqlServerMigrationSqlGenerator : System.Data.Entity.SqlServer.SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedDateUtcColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetCreatedUtcColumn(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetCreatedDateUtcColumn(columnModel);
            }
        }

        private static void SetCreatedDateUtcColumn(PropertyModel column)
        {
            if (column.Name == "CreatedUtc")
            {
                column.DefaultValueSql = "GETUTCDATE()";
            }
        }
    }
}
