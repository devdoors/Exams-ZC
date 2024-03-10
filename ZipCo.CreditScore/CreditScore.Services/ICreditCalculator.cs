using CreditScore.Dtos;

namespace CreditScore.Service.Contracts
{
    public interface ICreditCalculator
    {
        /// <summary>
        /// Calculates the available credit (in $) for a given customer
        /// </summary>
        /// <param name="customer">The customer for whom we are calculating credit</param>
        /// <returns>Available credit amount in $</returns>
        public Task<decimal> CalculateCredit(CustomerDto customer);
    }
}
