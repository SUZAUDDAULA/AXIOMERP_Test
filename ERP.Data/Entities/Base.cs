using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities
{
    public class Base
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DefaultValue(0)]
        public int? isDelete { get; set; }

        public DateTime? createdAt { get; set; }

        public DateTime? updatedAt { get; set; }
        [StringLength(250)]
        public string createdBy { get; set; }
        [StringLength(250)]
        public string updatedBy { get; set; }
    }
}
