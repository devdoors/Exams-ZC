using CreditScore.Domains;

namespace CreditScore.Dtos
{
    public class SingleValuePointsRefDto
    {
        public int Value { get; set; }

        public int Points { get; set; }

        public SingleValuePointsRefDto(int value, int points)
        {
            Value = value;         
            Points = points;
        }
    }

    public static class SingleValuePointsRefExtensions
    {
        public static List<SingleValuePointsRefDto> SingleValuePointsRefToDtos(this List<SingleValuePointsRef> singleValuePointsRef)
        {
            return singleValuePointsRef.Select(SingleValuePointsRefToDto).ToList();
        }


        public static SingleValuePointsRefDto SingleValuePointsRefToDto(this SingleValuePointsRef singleValuePointsRef)
        {
            var dto = new SingleValuePointsRefDto(singleValuePointsRef.Value, singleValuePointsRef.Points);

            return dto;
        }
    }
}
