using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;

namespace CreditScore.Service.Implementations
{
    public class MissedPayments : IMissedPayments
    {
        public MissedPayments(IReadSingleValuePointsRefData readSingleValuePointsRefData) 
        {
            _readSingleValuePointsRefData = readSingleValuePointsRefData;
        }


        private readonly IReadSingleValuePointsRefData _readSingleValuePointsRefData;


        public IEnumerable<SingleValuePointsRefDto> MissedPaymentsRef = new List<SingleValuePointsRefDto>();

        public async Task<int> GetMissedPayments(int missedPaymentCount)
        {
            await GetMissedPaymentRef();

            int points = 0;

            if (missedPaymentCount < points)
            {
                throw new Exception(Messages.CalculateCredit.E_MissedPayment_NegativeInput);
            }

            if (!MissedPaymentsRef.Any())
            {
                throw new Exception(Messages.ValuePointsRef.E_MissedPaymentsScoreRef_NotAvailable);
            }

            var score = MissedPaymentsRef.FirstOrDefault(x => x.Value == missedPaymentCount);

            if (score == null)
            {
                score = MissedPaymentsRef.MaxBy(x => x.Value);
            }

            return score.Points;
        }

        private async Task GetMissedPaymentRef()
        {
            var missedPaymentScores = await _readSingleValuePointsRefData.GetMissedPaymentsAsync();
            MissedPaymentsRef = missedPaymentScores.SingleValuePointsRefToDtos();
        }
    }
}
