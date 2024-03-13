using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditScore.Service.Contracts
{
    public interface IMissedPayments
    {
        Task<int> GetMissedPayments(int missedPaymentCount);
    }
}
