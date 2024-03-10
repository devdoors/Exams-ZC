using CreditScore.Domains;

namespace CreditScore.Dtos
{
    public class CustomerDto
    {
        public int BureauScore { get; }

        public int MissedPaymentCount { get; }

        public int CompletedPaymentCount { get; }

        public int AgeInYears { get; }

        public CustomerDto(int bureauScore, int missedPaymentCount,
            int completedPaymentCount, int ageInYears)
        {
            BureauScore = bureauScore;
            MissedPaymentCount = missedPaymentCount;
            CompletedPaymentCount = completedPaymentCount;
            AgeInYears = ageInYears;
        }
    }

    public static class CustomerExtensions
    {
        public static List<CustomerDto> CustomerToDtos(this List<Customer> customers)
        {
            return customers.Select(CustomerToDto).ToList();
        }


        public static CustomerDto CustomerToDto(this Customer customer)
        {
            var dto = new CustomerDto(customer.BureauScore, customer.MissedPaymentCount, customer.CompletedPaymentCount, customer.AgeInYears);
            
            return dto;
        }
    }
}
