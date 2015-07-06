using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Budget.Domain.Models
{
    public class BudgetLocation : EntityBase
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        
    }
}