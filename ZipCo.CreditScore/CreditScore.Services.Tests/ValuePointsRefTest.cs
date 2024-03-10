using CreditScore.Service.Implementations;
using CreditScore.Service.Tests.MockDataClasses;

namespace CreditScore.Service.Tests
{
    [TestClass]
    public class ValuePointsRefTest
    {
        [TestMethod]
        public async Task GetCreditCalculatorValuePointsRef_WithAllCollectionAvailable_ReturnCorrectCountOfCollections()
        {
            //Arrange 
            var valuePointsRef = new ValuePointsRef(new ReadRangeValuePointsRefDataMock(), new ReadSingleValuePointsRefDataMock());

            //Act
            var creditCalculatorValuePointsRefDto = await valuePointsRef.GetCreditCalculatorValuePointsRefAsync();

            //Arrange 
            Assert.AreEqual(4, creditCalculatorValuePointsRefDto.CreditScores.Count());
            Assert.AreEqual(4, creditCalculatorValuePointsRefDto.AgeCapScores.Count());
            Assert.AreEqual(4, creditCalculatorValuePointsRefDto.MissedPayments.Count());
            Assert.AreEqual(4, creditCalculatorValuePointsRefDto.CompletedPayments.Count());
            Assert.AreEqual(7, creditCalculatorValuePointsRefDto.AvaliableCreditScores.Count());
        }
    }
}
