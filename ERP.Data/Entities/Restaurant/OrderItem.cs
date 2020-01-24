using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Restaurant
{
    public partial class OrderItem:Base
    {
        public int OrderID { get; set; }
        public virtual Order Order { get; set; }
        public int ItemID { get; set; }
        public virtual Item Item { get; set; }
        public decimal Quantity { get; set; }
    }
}
