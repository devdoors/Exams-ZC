

namespace CreditScore.Service.Contracts
{
    public interface IAvailableCredit
    {
        Task<int> GetAvaliableCreditScores(int totalScore);
    }
}
