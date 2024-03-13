using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;

namespace CreditScore.Service.Implementations
{
    public class AgeCapScore : IAgeCapScore
    {
        public AgeCapScore(IReadRangeValuePointsRefData readRangeValuePointsRefData) 
        {
            _readRangeValuePointsRefData = readRangeValuePointsRefData;
        }


        private readonly IReadRangeValuePointsRefData _readRangeValuePointsRefData;


        public IEnumerable<RangeValuePointsRefDto> AgeCapScoreRef = new List<RangeValuePointsRefDto>();

        public async Task<int> GetAgeCapScores(int ageInYears)
        {
            await GetAgeCapScoreRef();

            if (!AgeCapScoreRef.Any())
            {
                throw new Exception(Messages.ValuePointsRef.E_AgeCapScoreRef_NotAvailable);
            }

            var ageCapScore = AgeCapScoreRef.FirstOrDefault(x => ageInYears >= x.ValueStart && ageInYears <= x.ValueEnd);

            if (ageCapScore == null)
            {
                var minAgeCap = AgeCapScoreRef.Min(x => x.ValueStart);

                if (ageInYears < minAgeCap)
                {
                    throw new Exception(Messages.CalculateCredit.E_AgeScore_BelowMinimum);
                }
                else
                {
                    var maxAgeCap = AgeCapScoreRef.Last();
                    return maxAgeCap.Points;
                }
            }

            return ageCapScore.Points;
        }

        private async Task GetAgeCapScoreRef()
        {
            var ageCapScoreRef = await _readRangeValuePointsRefData.GetAgeCapScoresAsync();
            AgeCapScoreRef = ageCapScoreRef.RangeValuePointsRefToDtos();
        }
    }
}
