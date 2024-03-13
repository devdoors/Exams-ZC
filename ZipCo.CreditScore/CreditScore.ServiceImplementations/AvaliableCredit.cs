using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;

namespace CreditScore.Service.Implementations
{
    public class AvaliableCredit : IAvailableCredit
    {
        public AvaliableCredit(IReadSingleValuePointsRefData readSingleValuePointsRefData) 
        {
            _readSingleValuePointsRefData = readSingleValuePointsRefData;
        }


        private readonly IReadSingleValuePointsRefData _readSingleValuePointsRefData;


        public IEnumerable<SingleValuePointsRefDto> AvaliableCreditRef = new List<SingleValuePointsRefDto>();

        public async Task<int> GetAvaliableCreditScores(int totalScore)
        {
            await GetAvaliableCreditRef();

            int points = 0;

            if (totalScore < points)
            {
                return 0;
            }

            if (!AvaliableCreditRef.Any())
            {
                throw new Exception(Messages.ValuePointsRef.E_AvailableCreditScoreRef_NotAvailable);
            }

            var availableCredit = AvaliableCreditRef.FirstOrDefault(x => x.Value == totalScore);

            if (availableCredit == null)
            {
                availableCredit = AvaliableCreditRef.MaxBy(x => x.Value);
            }

            return availableCredit.Points;
        }

        private async Task GetAvaliableCreditRef()
        {
            var avaliableCreditRef = await _readSingleValuePointsRefData.GetAvaliableCreditScoresAsync();
            AvaliableCreditRef = avaliableCreditRef.SingleValuePointsRefToDtos();
        }
    }
}
