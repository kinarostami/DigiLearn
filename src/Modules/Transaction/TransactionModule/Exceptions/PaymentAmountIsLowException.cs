using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionModule.Exceptions;

public class PaymentAmountIsLowException : Exception
{
    public PaymentAmountIsLowException()
    {
    }

    public PaymentAmountIsLowException(string message) : base(message)
    {
        
    }
}
