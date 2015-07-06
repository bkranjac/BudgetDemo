using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budget.EF
{
        public class BudgetContext : DbContext
        {
            public BudgetContext()
                : base("name=BudgetConnection")
            {
                Configuration.ProxyCreationEnabled = false;
            }

            public System.Data.Entity.DbSet<Budget.Domain.Models.BudgetItem> BudgetItems { get; set; }

            public System.Data.Entity.DbSet<Budget.Domain.Models.BudgetCategory> BudgetCategories { get; set; }
            public System.Data.Entity.DbSet<Budget.Domain.Models.BudgetSubCategory> BudgetSubCategories { get; set; }

            public System.Data.Entity.DbSet<Budget.Domain.Models.BudgetLocation> Locations { get; set; }

        }
    }

