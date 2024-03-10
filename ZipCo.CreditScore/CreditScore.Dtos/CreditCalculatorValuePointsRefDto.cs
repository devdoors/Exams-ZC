using CreditScore.Domains;

namespace CreditScore.Dtos
{
    public class CreditCalculatorValuePointsRefDto
    {
        public IEnumerable<RangeValuePointsRefDto> CreditScores = new List<RangeValuePointsRefDto>();

        public IEnumerable<SingleValuePointsRefDto> MissedPayments = new List<SingleValuePointsRefDto>();

        public IEnumerable<SingleValuePointsRefDto> CompletedPayments = new List<SingleValuePointsRefDto>();

        public IEnumerable<RangeValuePointsRefDto> AgeCapScores = new List<RangeValuePointsRefDto>();

        public IEnumerable<SingleValuePointsRefDto> AvaliableCreditScores = new List<SingleValuePointsRefDto>();
    }
}
