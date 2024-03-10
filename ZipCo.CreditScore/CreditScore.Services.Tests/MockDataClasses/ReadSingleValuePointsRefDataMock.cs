using CreditScore.Data.Contracts;
using CreditScore.Domains;

namespace CreditScore.Service.Tests.MockDataClasses
{
    /// <summary>
    /// Mock data service provider class
    /// </summary>
    public class ReadSingleValuePointsRefDataMock : IReadSingleValuePointsRefData
    {
        #region Public Methods

        //Mock data ref
        public Task<List<SingleValuePointsRef>> GetMissedPaymentsAsync()
        {
            var list = new List<SingleValuePointsRef>
                {
                    new SingleValuePointsRef() { Value = 0, Points = 0 },
                    new SingleValuePointsRef() { Value = 1, Points = -1 },
                    new SingleValuePointsRef() { Value = 2, Points = -3 },
                    new SingleValuePointsRef() { Value = 3, Points = -6 }
                };

            return Task.FromResult(list);
        }

        //Mock data ref
        public Task<List<SingleValuePointsRef>> GetCompletedPaymentsAsync()
        {
            var list = new List<SingleValuePointsRef>
                {
                    new SingleValuePointsRef() { Value = 0, Points = 0 },
                    new SingleValuePointsRef() { Value = 1, Points = 2 },
                    new SingleValuePointsRef() { Value = 2, Points = 3 },
                    new SingleValuePointsRef() { Value = 3, Points = 4 }
                };

            return Task.FromResult(list);
        }

        //Mock data ref
        public Task<List<SingleValuePointsRef>> GetAvaliableCreditScoresAsync()
        {
            var list = new List<SingleValuePointsRef>
                {
                    new SingleValuePointsRef() { Value = 0, Points = 0 },
                    new SingleValuePointsRef() { Value = 1, Points = 100 },
                    new SingleValuePointsRef() { Value = 2, Points = 200 },
                    new SingleValuePointsRef() { Value = 3, Points = 300 },
                    new SingleValuePointsRef() { Value = 4, Points = 400 },
                    new SingleValuePointsRef() { Value = 5, Points = 500 },
                    new SingleValuePointsRef() { Value = 6, Points = 600 }
                };

            return Task.FromResult(list);
        }

        #endregion
    }
}
