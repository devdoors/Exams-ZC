using CreditScore.Domains;

namespace CreditScore.Data.Contracts
{
    public interface IReadRangeValuePointsRefData
    {
        #region Public Methods

            public Task<List<RangeValuePointsRef>> GetCreditScoreAsync();

            public Task<List<RangeValuePointsRef>> GetAgeCapScoresAsync();

        #endregion    
    }
}
