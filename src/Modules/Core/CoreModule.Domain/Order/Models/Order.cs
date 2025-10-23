using Common.Domain;
using Common.Domain.Exceptions;
using CoreModule.Domain.Order.DomainServices;
using CoreModule.Domain.Order.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Domain.Order.Models;

public class Order : AggregateRoot
{
    private Order()
    {
    }

    public Order(Guid userId)
    {
        UserId = userId;
        IsPay = false;
        PaymaentDate = null;
        Discount = 0;
        OrderItems = new();
        DiscountCode = null;
    }

    public Guid UserId { get; set; }
    public bool IsPay { get; set; }
    public int Discount { get; set; }
    public string? DiscountCode { get; set; }
    public DateTime? PaymaentDate { get; set; }
    public List<OrderItem> OrderItems { get; set; } = new();
    public int TotalPrice
    {
        get
        {
            return OrderItems.Sum(x => x.Price) - Discount;
        }
    }

    public async Task AddItem(Guid courseId, IOrderDomainService orderDomainService)
    {
        var price = await orderDomainService.GetCoursePriceById(courseId);
        if (price <= 0)
        {
            throw new InvalidDomainDataException("امکان اضافه کردن این دوره به سبد خرید وجود ندارد");
        }
        if (OrderItems.Any(x => x.CourseId == courseId))
        {
            return;
        }
        OrderItems.Add(new OrderItem()
        {
            CourseId = courseId,
            Price = price,
            OrderId = Id
        });
    }

    public void FinallyOrder()
    {
        IsPay = true;
        PaymaentDate = DateTime.Now;
        AddDomainEvent(new OrderFinallyEvent()
        {
            OrderId = Id,
            UserId = UserId
        });
    }

    public void RemoveItem(Guid id)
    {
        var item = OrderItems.FirstOrDefault(x => x.Id == id);
        if (item != null)
            OrderItems.Remove(item);
    }
}
public class OrderItem : BaseEntity
{
    public Guid CourseId { get; set; }
    public Guid OrderId { get; set; }
    public int Price { get; set; }
}
