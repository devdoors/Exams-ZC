using CreditScore.Dtos;
using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;

namespace CreditScore.Service.Contracts
{
    public interface IAvailableCredit
    {
        Task<int> GetAvaliableCreditScores(int totalScore);
    }
}
