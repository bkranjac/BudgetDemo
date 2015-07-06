using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Budget.Domain.Models
{
        public abstract class EntityBase
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            public DateTime? CreatedUtc { get; set; }
            //[Required, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            //public DateTime ChangedAt { get; set; }
        }
  
}