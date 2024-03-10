using CreditScore.Service.Contracts;
using CreditScore.Common;
using CreditScore.Dtos;

namespace CreditScore.Service.Implementations
{
    public class CreditCalculator : ICreditCalculator
    {
        #region Constructor 

            public CreditCalculator(IValuePointsRef valuePointsRef) 
            { 
                _valuePointsRef = valuePointsRef;                     
            }


            /// <summary>
            /// This pattern temporarily handles IOC instead of setting up a full start up class.             
            /// Note: Once a start up is created, this constructor can be removed. 
            /// </summary>
            public CreditCalculator(): this(new ValuePointsRef()) { }

        #endregion


        #region Private Properties

            private IValuePointsRef _valuePointsRef;           

            private CreditCalculatorValuePointsRefDto _creditCalculatorValuePointsRef;

        #endregion


        #region Public Methods

            public async Task<decimal> CalculateCredit(CustomerDto customer)
            {                
                await GetValuePointsRef(); //Decided to make these ref a composition rather than aggregation by association to this parent class.
                return Calculate(customer);
            }

        #endregion
        

        #region Private Methods
            
            private async Task GetValuePointsRef()
            {
                _creditCalculatorValuePointsRef = await _valuePointsRef.GetCreditCalculatorValuePointsRefAsync();                
            }

            private decimal Calculate(CustomerDto customer)
            {          
                if (customer == null)
                {
                    //_logger.LogError($"Calculate | {Messages.Customer.E_Customer_Null}"
                    throw new ArgumentNullException(Messages.Customer.E_Customer_Null);                    
                }

                var creditScore = GetCreditScore(customer.BureauScore);
                var missedPaymentsScore = GetMissedPayments(customer.MissedPaymentCount);
                var completedPaymetsScore = GetCompletedPaymetsScore(customer.CompletedPaymentCount);                                
                var ageCapScore = GetAgeCapScores(customer.AgeInYears);

                var score = creditScore + missedPaymentsScore + completedPaymetsScore;
                var points = (score <= ageCapScore ? score : ageCapScore);

                return (points <= 0 ? 0 : GetAvaliableCreditScores(points));                
            }

            private int GetCreditScore(int bureauScore)
            {        
                if (!_creditCalculatorValuePointsRef.CreditScores.Any())
                {
                    throw new Exception(Messages.ValuePointsRef.E_CreditScoreRef_NotAvailable);
                }

                var creditScores = _creditCalculatorValuePointsRef.CreditScores.FirstOrDefault(x => bureauScore >= x.ValueStart && bureauScore <= x.ValueEnd);

                if (creditScores == null)
                { 
                    throw new Exception(Messages.CalculateCredit.E_CreditScore_OutOfRange);
                }                

                if (creditScores.Points == 0)
                { 
                    throw new Exception(Messages.CalculateCredit.E_CreditScore_NotAllowed);
                }

                return creditScores.Points;
            }

            private int GetMissedPayments(int missedPaymentCount)
            {
                int points = 0;

                if (missedPaymentCount < points)
                {
                    throw new Exception(Messages.CalculateCredit.E_MissedPayment_NegativeInput);
                }

                if (!_creditCalculatorValuePointsRef.MissedPayments.Any())
                {
                    throw new Exception(Messages.ValuePointsRef.E_MissedPaymentsScoreRef_NotAvailable);
                }

                var score = _creditCalculatorValuePointsRef.MissedPayments.FirstOrDefault(x => x.Value == missedPaymentCount);

                if (score == null)
                {                     
                    score = _creditCalculatorValuePointsRef.MissedPayments.MaxBy(x => x.Value);
                }
                
                return score.Points;                
            }

            private int GetCompletedPaymetsScore(int completedPaymentCount)
            { 
                int points = 0;

                if (completedPaymentCount < points)
                {
                    throw new Exception(Messages.CalculateCredit.E_CompletedPayment_NegativeInput);
                }

                if (!_creditCalculatorValuePointsRef.CompletedPayments.Any())
                {
                    throw new Exception(Messages.ValuePointsRef.E_CompletedPaymentsScoreRef_NotAvailable);
                }

                var completedPayment = _creditCalculatorValuePointsRef.CompletedPayments.FirstOrDefault(x => x.Value == completedPaymentCount);

                if (completedPayment == null)
                {
                    completedPayment = _creditCalculatorValuePointsRef.CompletedPayments.MaxBy(x => x.Value);
                }

                return completedPayment.Points;
            }

            private int GetAgeCapScores(int ageInYears)
            { 
                if (!_creditCalculatorValuePointsRef.AgeCapScores.Any())
                {
                    throw new Exception(Messages.ValuePointsRef.E_AgeCapScoreRef_NotAvailable);
                }                

                var ageCapScore = _creditCalculatorValuePointsRef.AgeCapScores.FirstOrDefault(x => ageInYears >= x.ValueStart && ageInYears <= x.ValueEnd);
                
                if (ageCapScore == null)
                { 
                    var minAgeCap = _creditCalculatorValuePointsRef.AgeCapScores.Min(x => x.ValueStart);

                    if (ageInYears < minAgeCap)
                    { 
                        throw new Exception(Messages.CalculateCredit.E_AgeScore_BelowMinimum);
                    }
                    else
                    { 
                        var maxAgeCap = _creditCalculatorValuePointsRef.AgeCapScores.Last();
                        return maxAgeCap.Points;
                    }
                }

                return ageCapScore.Points;
            }            

            private decimal GetAvaliableCreditScores(int totalScore)
            { 
                int points = 0;

                if (totalScore < points)
                {
                    return 0;
                }

                if (!_creditCalculatorValuePointsRef.AvaliableCreditScores.Any())
                {
                    throw new Exception(Messages.ValuePointsRef.E_AvailableCreditScoreRef_NotAvailable);
                }

                var availableCredit = _creditCalculatorValuePointsRef.AvaliableCreditScores.FirstOrDefault(x => x.Value == totalScore);                

                if (availableCredit == null)
                {
                    availableCredit = _creditCalculatorValuePointsRef.AvaliableCreditScores.MaxBy(x => x.Value);
                }

                return availableCredit.Points;
            }                    

        #endregion
    }
}
