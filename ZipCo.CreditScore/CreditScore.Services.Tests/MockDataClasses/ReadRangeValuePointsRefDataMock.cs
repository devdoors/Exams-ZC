using CreditScore.Data.Contracts;
using CreditScore.Domains;

namespace CreditScore.Service.Tests.MockDataClasses
{
    /// <summary>
    /// Mock data service provider class
    /// </summary>
    public class ReadRangeValuePointsRefDataMock : IReadRangeValuePointsRefData
    {
        #region Public Methods

        //Mock data ref
        public Task<List<RangeValuePointsRef>> GetCreditScoreAsync()
        {
            var list = new List<RangeValuePointsRef>
                {
                    new RangeValuePointsRef() { ValueStart = 0, ValueEnd = 450, Points = 0 },
                    new RangeValuePointsRef() { ValueStart = 451, ValueEnd = 700, Points = 1 },
                    new RangeValuePointsRef() { ValueStart = 701, ValueEnd = 850, Points = 2 },
                    new RangeValuePointsRef() { ValueStart = 851, ValueEnd = 1000, Points = 3 }                    
                };

            return Task.FromResult(list);
        }

        //Mock data ref
        public Task<List<RangeValuePointsRef>> GetAgeCapScoresAsync()
        {

            var list = new List<RangeValuePointsRef>
                {                    
                    new RangeValuePointsRef() { ValueStart = 18, ValueEnd = 25, Points = 3 },
                    new RangeValuePointsRef() { ValueStart = 26, ValueEnd = 35, Points = 4 },
                    new RangeValuePointsRef() { ValueStart = 36, ValueEnd = 50, Points = 5 },
                    new RangeValuePointsRef() { ValueStart = 51, ValueEnd = 51, Points = 6 }
                };

            return Task.FromResult(list);
        }

        #endregion
    }
}
