using CreditScore.Service.Contracts;
using CreditScore.Common;
using CreditScore.Dtos;

namespace CreditScore.Service.Implementations
{
    public class CreditCalculator : ICreditCalculator
    {
        #region Constructor 

            public CreditCalculator(IValuePointsRef valuePointsRef, ICreditScore creditScore, IMissedPayments missedPayments, ICompletedPaymets completedPaymets, IAgeCapScore ageCapScore, IAvailableCredit availableCredit) 
            { 
                _valuePointsRef = valuePointsRef;      
                _creditScore = creditScore;
                _missedPayments = missedPayments;
                _completedPaymets = completedPaymets;
                _ageCapScore = ageCapScore;
                _availableCredit = availableCredit; 
            }

        #endregion


        #region Private Properties

            private readonly IValuePointsRef _valuePointsRef;           

            private readonly ICreditScore _creditScore;

            private readonly IMissedPayments _missedPayments;

            private readonly ICompletedPaymets _completedPaymets;

            private readonly IAgeCapScore _ageCapScore;

            private readonly IAvailableCredit _availableCredit;

            private CreditCalculatorValuePointsRefDto _creditCalculatorValuePointsRef;

        #endregion


        #region Public Methods

            public async Task<decimal> CalculateCredit(CustomerDto customer)
            {                
                await GetValuePointsRef(); //Decided to make these ref a composition rather than aggregation by association to this parent class.
                return await Calculate(customer);
            }

        #endregion
        

        #region Private Methods
            
            private async Task GetValuePointsRef()
            {
                _creditCalculatorValuePointsRef = await _valuePointsRef.GetCreditCalculatorValuePointsRefAsync();                
            }

            private async Task<decimal> Calculate(CustomerDto customer)
            {          
                if (customer == null)
                {
                    //_logger.LogError($"Calculate | {Messages.Customer.E_Customer_Null}"
                    throw new ArgumentNullException(Messages.Customer.E_Customer_Null);                    
                }

                var creditScore = await _creditScore.GetCreditScore(customer.BureauScore);
                var missedPaymentsScore = await _missedPayments.GetMissedPayments(customer.MissedPaymentCount);
                var completedPaymetsScore = await _completedPaymets.GetCompletedPaymetsScore(customer.CompletedPaymentCount);                                
                var ageCapScore = await _ageCapScore.GetAgeCapScores(customer.AgeInYears);

                var score = creditScore + missedPaymentsScore + completedPaymetsScore;
                var points = (score <= ageCapScore ? score : ageCapScore);

                return (points <= 0 ? 0 : await _availableCredit.GetAvaliableCreditScores(points));                
            }                                      

        #endregion
    }
}
