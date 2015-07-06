using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Budget.Data.Interfaces;
using Budget.Domain.Models;
using Budget.EF;
using System.Data.Entity;

namespace Budget.Data.Concrete
{
   public  class ItemRepository : IItemRepository
    {
       private BudgetContext db = new BudgetContext();
       private bool disposed = false;

       public BudgetItem GetItem(int id)
       {
           return db.BudgetItems.Where(p=>p.Id == id).SingleOrDefault();
       }
       
       public IEnumerable<BudgetItem> GetAll() 
       {
           return db.BudgetItems.ToList();
       }

        public BudgetItem UpdateItem(BudgetItem item) 
        {
           BudgetItem oldItem = db.BudgetItems.Find(item.Id);
           if (oldItem != null)
            {
                db.Entry(oldItem).CurrentValues.SetValues(item);           
            } 
         
            Save();
            return item;
        }

        public void DeleteItem(int id) 
        {
            BudgetItem item = db.BudgetItems.Find(id);
            db.BudgetItems.Remove(item);
            Save();

        }

        public void CreateItem(BudgetItem item) 
        {
            db.BudgetItems.Add(item);
            Save();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        protected virtual void Dispose(bool disposing) 
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
