using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionModule.Domain;

namespace TransactionModule.Services.DTOs.Commands;

public class CreateTransactions
{
    public Guid UserId { get; set; }
    public int PaymentAmount { get; set; }
    public Guid LinkId { get; set; }
    public PaymentGateway PaymentGateway { get; set; }
    public TransactionFor TransactionFor { get; set; }
}
