using CreditScore.Domains;

namespace CreditScore.Data.Contracts
{
    public interface IReadSingleValuePointsRefData
    {
        #region Public Methods

            public Task<List<SingleValuePointsRef>> GetMissedPaymentsAsync();

            public Task<List<SingleValuePointsRef>> GetCompletedPaymentsAsync();            

            public Task<List<SingleValuePointsRef>> GetAvaliableCreditScoresAsync();

        #endregion         
    }
}
