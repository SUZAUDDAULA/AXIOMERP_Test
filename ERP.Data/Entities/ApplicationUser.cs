using ERP.Data.Entities.MasterData;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities
{
    public class ApplicationUser: IdentityUser
    {
        [Column(TypeName = "nvarchar(50)")]
        public string EmpCode { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }

        public decimal? MaxAmount { get; set; }

        public int? isActive { get; set; }
        [StringLength(120)]
        public string org { get; set; }

        public DateTime? createdAt { get; set; }
        [StringLength(120)]
        public string createdBy { get; set; }

        public DateTime? updatedAt { get; set; }
        [StringLength(120)]
        public string updatedBy { get; set; }
    }
}
