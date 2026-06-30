using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int? SalerId { get; set; }

    public int? DeliveryWorkerId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public virtual User Customer { get; set; } = null!;

    public virtual User? DeliveryWorker { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>(); // 1 order có nhiều orderdetal 

    public virtual User? Saler { get; set; }
}
