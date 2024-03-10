using CreditScore.Domains;

namespace CreditScore.Dtos
{
    public class RangeValuePointsRefDto
    {
        public int ValueStart { get; }

        public int ValueEnd { get; }

        public int Points { get; }

        public RangeValuePointsRefDto(int valueStart, int valueEnd, int points)
        {
            ValueStart = valueStart;
            ValueEnd = valueEnd;
            Points = points;
        }
    }


    public static class RangeValuePointsRefExtensions
    {
        public static List<RangeValuePointsRefDto> RangeValuePointsRefToDtos(this List<RangeValuePointsRef> rangeValuePointsRef)
        {
            return rangeValuePointsRef.Select(RangeValuePointsRefToDto).ToList();
        }


        public static RangeValuePointsRefDto RangeValuePointsRefToDto(this RangeValuePointsRef rangeValuePointsRef)
        {
            var dto = new RangeValuePointsRefDto(rangeValuePointsRef.ValueStart, rangeValuePointsRef.ValueEnd, rangeValuePointsRef.Points);

            return dto;
        }
    }
}
