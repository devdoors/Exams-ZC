namespace CreditScore.Common
{
    public static class Messages
    {
        public class ValuePointsRef 
        {         
            public const string E_CreditScoreRef_NotAvailable = "Credit score reference not available";

            public const string E_MissedPaymentsScoreRef_NotAvailable = "Missed payments score reference not available";

            public const string E_CompletedPaymentsScoreRef_NotAvailable = "Completed payments score reference not available";

            public const string E_AgeCapScoreRef_NotAvailable = "Age cap score reference not available";

            public const string E_AvailableCreditScoreRef_NotAvailable = "Available credit score reference not available";

            public const string E_ValuePointsRef_NotAvailable = "Value points reference not available";
        }

        public class Customer
        {
            public const string E_Customer_Null = "Customer cannot be null";            
        }


        public class CalculateCredit 
        {
            public const string E_CreditScore_NotAllowed = "Not allowed to use Zip";

            public const string E_CreditScore_OutOfRange = "Credit score is out of range";

            public const string E_AgeScore_BelowMinimum = "Age is below the allowed minimum";

            public const string E_MissedPayment_NegativeInput = "Cannot have negative value on missed payment";

            public const string E_CompletedPayment_NegativeInput = "Cannot have negative value on completed payment";
        }
    }
}
