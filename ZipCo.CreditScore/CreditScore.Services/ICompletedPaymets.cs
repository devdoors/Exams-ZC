

namespace CreditScore.Service.Contracts
{
    public interface ICompletedPaymets
    {
        Task<int> GetCompletedPaymetsScore(int completedPaymentCount);
    }
}
