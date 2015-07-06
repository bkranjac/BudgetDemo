using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Domain.Models;

namespace Budget.Data.Interfaces
{
    public interface IItemRepository : IDisposable
    {
        BudgetItem GetItem(int id);
        IEnumerable<BudgetItem> GetAll();
        BudgetItem UpdateItem(BudgetItem item);
        void DeleteItem(int id);
        void CreateItem(BudgetItem item);
        void Save();

    }
}
    
