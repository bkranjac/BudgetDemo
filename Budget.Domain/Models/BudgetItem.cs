using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.Domain.Models
{
    public class BudgetItem : EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public bool IsFixed { get; set; }
        public bool IsExpense { get; set; }
        public string Notes { get; set; }
        public DateTime? DateOccured { get; set; }
       
        [Timestamp]
        public byte[] RowVersion { get; set; }
        // Category
        public int? BudgetSubCategoryId { get; set; }
        public virtual BudgetSubCategory SubCategory { get; set; }
        // Place
        public int? BudgetLocationId { get; set; }
        public virtual BudgetLocation Place { get; set; }
    }
}