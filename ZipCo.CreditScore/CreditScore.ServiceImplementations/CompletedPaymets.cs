using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;

namespace CreditScore.Service.Implementations
{
    public class CompletedPaymets : ICompletedPaymets
    {
        public CompletedPaymets(IReadSingleValuePointsRefData readSingleValuePointsRefData) 
        {
            _readSingleValuePointsRefData = readSingleValuePointsRefData;
        }


        private readonly IReadSingleValuePointsRefData _readSingleValuePointsRefData;


        public IEnumerable<SingleValuePointsRefDto> CompletedPaymetsScoreRef = new List<SingleValuePointsRefDto>();

        public async Task<int> GetCompletedPaymetsScore(int completedPaymentCount)
        {
            await GetCompletedPaymetsRef();

            int points = 0;

            if (completedPaymentCount < points)
            {
                throw new Exception(Messages.CalculateCredit.E_CompletedPayment_NegativeInput);
            }

            if (!CompletedPaymetsScoreRef.Any())
            {
                throw new Exception(Messages.ValuePointsRef.E_CompletedPaymentsScoreRef_NotAvailable);
            }

            var completedPayment = CompletedPaymetsScoreRef.FirstOrDefault(x => x.Value == completedPaymentCount);

            if (completedPayment == null)
            {
                completedPayment = CompletedPaymetsScoreRef.MaxBy(x => x.Value);
            }

            return completedPayment.Points;
        }

        private async Task GetCompletedPaymetsRef()
        {
            var completedPaymetsScore = await _readSingleValuePointsRefData.GetCompletedPaymentsAsync();
            CompletedPaymetsScoreRef = completedPaymetsScore.SingleValuePointsRefToDtos();
        }
    }
}
