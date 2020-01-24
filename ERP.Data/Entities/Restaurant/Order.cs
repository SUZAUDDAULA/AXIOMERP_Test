using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Data.Entities.Restaurant
{
    public partial class Order:Base
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }
        
        public string OrderNo { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public string PMethod { get; set; }
        public Nullable<decimal> GTotal { get; set; }
        [NotMapped]
        public string DeletedOrderItemsIds { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
