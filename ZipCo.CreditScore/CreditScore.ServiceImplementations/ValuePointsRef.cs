using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Data.Implementations;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;

namespace CreditScore.Service.Implementations
{
    //Service to get data from DB, file or another external service.
    public class ValuePointsRef : IValuePointsRef
    {
        #region Constructors

            public ValuePointsRef(IReadRangeValuePointsRefData readRangeValuePointsRefData, IReadSingleValuePointsRefData readSingleValuePointsRefData) 
            { 
                _readRangeValuePointsRefData = readRangeValuePointsRefData;
                _readSingleValuePointsRefData = readSingleValuePointsRefData;
            }
            
            /// <summary>
            /// This pattern temporarily handles IOC instead of setting up a full start up class.             
            /// Note: Once a start up is created, this constructor can be removed. 
            /// </summary>
            public ValuePointsRef(): this (new ReadRangeValuePointsRefData(), new ReadSingleValuePointsRefData()) {}

        #endregion


        #region Private Properties

            private readonly IReadRangeValuePointsRefData _readRangeValuePointsRefData;

            private readonly IReadSingleValuePointsRefData _readSingleValuePointsRefData;

        #endregion 


        #region Public Methods            

            public async Task<CreditCalculatorValuePointsRefDto> GetCreditCalculatorValuePointsRefAsync()
            {
                try 
                {
                    var creditScores = await _readRangeValuePointsRefData.GetCreditScoreAsync();
                    var missedPayments = await _readSingleValuePointsRefData.GetMissedPaymentsAsync();
                    var completedPayments = await _readSingleValuePointsRefData.GetCompletedPaymentsAsync();
                    var ageCapScores = await _readRangeValuePointsRefData.GetAgeCapScoresAsync();
                    var avaliableCreditScores = await _readSingleValuePointsRefData.GetAvaliableCreditScoresAsync();

                    return new CreditCalculatorValuePointsRefDto()
                    {
                        CreditScores = creditScores.RangeValuePointsRefToDtos(),
                        MissedPayments = missedPayments.SingleValuePointsRefToDtos(),
                        CompletedPayments = completedPayments.SingleValuePointsRefToDtos(),
                        AgeCapScores = ageCapScores.RangeValuePointsRefToDtos(),
                        AvaliableCreditScores = avaliableCreditScores.SingleValuePointsRefToDtos()
                    };
                }
                catch (Exception ex) 
                {
                    //Log ex.Message
                    throw new Exception(Messages.ValuePointsRef.E_ValuePointsRef_NotAvailable);
                }
            }

        #endregion
    }
}
