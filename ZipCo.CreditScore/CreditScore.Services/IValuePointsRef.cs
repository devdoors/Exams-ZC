using CreditScore.Dtos;

namespace CreditScore.Service.Contracts
{
    public interface IValuePointsRef
    {
        /// <summary>
        /// Return an object that contain properties of data references for RangeValuePointsRefDto and SingleValuePointsRefDto collection types
        /// </summary>
        /// <returns></returns>
        public Task<CreditCalculatorValuePointsRefDto> GetCreditCalculatorValuePointsRefAsync();
    }
}
