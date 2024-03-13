using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;

namespace CreditScore.Service.Implementations
{
    public class CreditScore : ICreditScore
    {
        public CreditScore(IReadRangeValuePointsRefData readRangeValuePointsRefData) 
        {
            _readRangeValuePointsRefData = readRangeValuePointsRefData;
        }


        private readonly IReadRangeValuePointsRefData _readRangeValuePointsRefData;


        public IEnumerable<RangeValuePointsRefDto> CreditScores = new List<RangeValuePointsRefDto>();

        public async Task<int> GetCreditScore(int bureauScore)
        {
            await GetCreditScoreRef();

            if (!CreditScores.Any())
            {
                throw new Exception(Messages.ValuePointsRef.E_CreditScoreRef_NotAvailable);
            }

            var creditScores = CreditScores.FirstOrDefault(x => bureauScore >= x.ValueStart && bureauScore <= x.ValueEnd);

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

        private async Task GetCreditScoreRef()
        {
            var creditScores = await _readRangeValuePointsRefData.GetCreditScoreAsync();
            CreditScores = creditScores.RangeValuePointsRefToDtos();
        }
    }
}
