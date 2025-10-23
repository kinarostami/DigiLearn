using CoreModule.Query._Data.Entities;
using CoreModule.Query.Order._DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Query.Order;

public class OrdreMapper
{
    public static OrderDto? MapOrder(OrderQueryModel? order)
    {
        if(order == null)
            return null;
        return new OrderDto()
        {
            Id = order.Id,
            CreationDate = order.CreationDate,
            UserId = order.UserId,
            IsPay = order.IsPay,
            Discount = order.Discount,
            DiscountCode = order.DiscountCode,
            PaymentDate = order.PaymentDate,
            OrderItems = order.OrderItems.Select(x => new OrderItemDto()
            {
                Id = x.Id,
                CourseId = x.CourseId,
                OrderId = x.OrderId,
                CourseTitle = x.Course.Title,
                Price = x.Price,
                TeacherFullName = x.Course.Teacher.User.FullName,
                CreationDate = x.CreationDate
            }).ToList(),
            User = new Query._DTOs.CoreModuleUserDto()
            {
                Id = order.User.Id,
                CreationDate = order.User.CreationDate,
                Avatar = order.User.Avatar,
                Email = order.User.Email,
                Name = order.User.Name,
                Family = order.User.Family,
                PhoneNumber = order.User.PhoneNumber,
            }
        };
    }
}
