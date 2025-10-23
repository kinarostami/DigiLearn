using Common.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query._Data.Entities;

public class OrderQueryModel : BaseEntity
{
    public Guid UserId { get; set; }
    public bool IsPay { get; set; }
    public int Discount { get; set; }
    public string? DiscountCode { get; set; }
    public DateTime? PaymentDate { get; set; }

    public List<OrderItemQueryModel> OrderItems { get; set; }

    [ForeignKey("UserId")]
    public UserQueryModel User { get; set; }
}
public class OrderItemQueryModel : BaseEntity
{
    public Guid CourseId { get; set; }
    public Guid OrderId { get; set; }
    public int Price { get; set; }

    [ForeignKey("OrderId")]
    public OrderQueryModel Order { get; set; }
    public CourseQueryModel Course { get; set; }
}
