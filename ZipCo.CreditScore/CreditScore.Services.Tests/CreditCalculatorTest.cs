using CreditScore.Common;
using CreditScore.Data.Contracts;
using CreditScore.Dtos;
using CreditScore.Service.Contracts;
using CreditScore.Service.Implementations;
using CreditScore.Service.Tests.MockDataClasses;
using Moq;

namespace CreditScore.Service.Tests
{
    [TestClass]
    public class CreditCalculatorTest
    {
        ICreditCalculator _creditCalculator;

        [TestInitialize]
        public async Task TestInitialize()
        {        
            var mockValues = await GetMockValuesAsync();
            var mockDependency = new Mock<IValuePointsRef>();
            mockDependency.Setup(d => d.GetCreditCalculatorValuePointsRefAsync()).Returns(Task.FromResult(mockValues));

            _creditCalculator = new CreditCalculator(mockDependency.Object);
        }


        [TestMethod]
        public async Task CalculateCredit_WithAllGivenValues_CorrectAvailableCreditsOf400()
        {
            //Arrange                         
            decimal expectedCredit = 400;

            var bureauScore = 750;
            var missedPaymentCount = 1;
            var completedPaymentCount = 4;
            var ageInYears = 29;
            var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);
            
            //Act
            var credit = await _creditCalculator.CalculateCredit(customer);

            //Assert
            Assert.AreEqual(expectedCredit, credit);
        }        


        #region Credit Scores

            [TestMethod]        
            public async Task CalculateCredit_WithBureauScoreInTheLowestRange_CatchAnExceptionWithCreditScoreMessage()
            {
                //Arrange             
                var bureauScore = 400;

                var missedPaymentCount = 1;
                var completedPaymentCount = 4;
                var ageInYears = 29;
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);            

                //Act
                try 
                {
                    var credit = await _creditCalculator.CalculateCredit(customer);
                }
                catch (Exception ex) 
                {
                    //Assert
                    Assert.AreEqual(Messages.CalculateCredit.E_CreditScore_NotAllowed, ex.Message);
                }
            }


            [TestMethod]
            public async Task CalculateCredit_WithBureauScoreAboveMaximumRange_CatchAnExceptionWithCreditScoreMessage()
            {
                //Arrange             
                var bureauScore = 1001;

                var missedPaymentCount = 1;
                var completedPaymentCount = 4;
                var ageInYears = 29;
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                try
                {
                    var credit = await _creditCalculator.CalculateCredit(customer);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.AreEqual(Messages.CalculateCredit.E_CreditScore_OutOfRange, ex.Message);
                }
            }


            [TestMethod]
            public async Task CalculateCredit_WithBureauScoreBelowMinimumRange_CatchAnExceptionWithCreditScoreMessage()
            {
                //Arrange             
                var bureauScore = -1;

                var missedPaymentCount = 1;
                var completedPaymentCount = 4;
                var ageInYears = 29;
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                try
                {
                    var credit = await _creditCalculator.CalculateCredit(customer);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.AreEqual(Messages.CalculateCredit.E_CreditScore_OutOfRange, ex.Message);
                }
            }

        #endregion


        #region Missed Payments

            [TestMethod]
            public async Task CalculateCredit_WithMissedPaymentSetAboveMaximum_CorrectAvailableCreditsOf0()
            {
                //Arrange 
                var missedPaymentCount = 4;
                decimal expectedCredit = 0;

                var bureauScore = 750;                
                var completedPaymentCount = 4;
                var ageInYears = 55;
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                var credit = await _creditCalculator.CalculateCredit(customer);

                //Assert
                Assert.AreEqual(expectedCredit, credit);
            }


            [TestMethod]
            public async Task CalculateCredit_WithMissedPaymentSetNegativeValue_CatchAnExceptionWithCreditScoreMessage()
            {
                //Arrange             
                var missedPaymentCount = -1;

                var bureauScore = 750;                
                var completedPaymentCount = 4;
                var ageInYears = 55;
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                try
                {
                    var credit = await _creditCalculator.CalculateCredit(customer);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.AreEqual(Messages.CalculateCredit.E_MissedPayment_NegativeInput, ex.Message);
                }
            }

        #endregion


        #region Completed Payments

            [TestMethod]
            public async Task CalculateCredit_WithCompletedPaymentSetAboveMaximum_CorrectAvailableCreditsOf600()
            {
                //Arrange                 
                var completedPaymentCount = 4;
                decimal expectedCredit = 600;

                var bureauScore = 750;
                var missedPaymentCount = 0;                
                var ageInYears = 55;
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                var credit = await _creditCalculator.CalculateCredit(customer);

                //Assert
                Assert.AreEqual(expectedCredit, credit);
            }


            [TestMethod]            
            public async Task CalculateCredit_WithCompletedPaymentSetNegativeValue_CatchAnExceptionWithCreditScoreMessage()
            {
                //Arrange             
                var completedPaymentCount = -1;
                
                var bureauScore = 750;
                var missedPaymentCount = 0;                
                var ageInYears = 55;

                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                try
                {
                    var credit = await _creditCalculator.CalculateCredit(customer);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.AreEqual(Messages.CalculateCredit.E_CompletedPayment_NegativeInput, ex.Message);
                }
            }

        #endregion


        #region AgeInYears

            [TestMethod]
            public async Task CalculateCredit_WithAgeInYearsIsBelowTheMinimum_CatchAnExceptionWithAgeInYearsMessage()
            {
                //Arrange             
                var ageInYears = 16;

                var bureauScore = 750;
                var missedPaymentCount = 1;
                var completedPaymentCount = 4;                
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                try
                {
                    var credit = await _creditCalculator.CalculateCredit(customer);
                }
                catch (Exception ex)
                {
                    //Assert
                    Assert.AreEqual(Messages.CalculateCredit.E_AgeScore_BelowMinimum, ex.Message);
                }
            }

            [TestMethod]
            public async Task CalculateCredit_WithAgeInYearsIsAboveTheMaximum_CorrectAvailableCreditsOf600()
            {
                //Arrange                 
                var ageInYears = 55;
                decimal expectedCredit = 600;

                var bureauScore = 750;
                var missedPaymentCount = 0;
                var completedPaymentCount = 4;                
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                var credit = await _creditCalculator.CalculateCredit(customer);

                //Assert
                Assert.AreEqual(expectedCredit, credit);
            }

        #endregion


        #region Completed Payments

            [TestMethod]
            public async Task CalculateCredit_WithAllGivenValuesSetToMaximum_CorrectAvailableCreditsOf100()
            {
                //Arrange
                decimal expectedCredit = 100;

                var bureauScore = 1000;
                var missedPaymentCount = 6;
                var completedPaymentCount = 4;                
                                
                var ageInYears = 55;
                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act
                var credit = await _creditCalculator.CalculateCredit(customer);

                //Assert
                Assert.AreEqual(expectedCredit, credit);
            }


            [TestMethod]
            public async Task CalculateCredit_WithAllGivenValuesSetToMaximumAndMissedPaymentToMinimum_CorrectAvailableCreditsOf600()
            {
                //Arrange             
                decimal expectedCredit = 600;
                var missedPaymentCount = 0;
                var completedPaymentCount = 4;
                var bureauScore = 1000;                
                var ageInYears = 55;

                var customer = new CustomerDto(bureauScore, missedPaymentCount, completedPaymentCount, ageInYears);

                //Act                
                var credit = await _creditCalculator.CalculateCredit(customer);
               
                //Assert
                Assert.AreEqual(expectedCredit, credit);              
            }

        #endregion


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task CalculateCredit_WithNullParameter_ThrowsArgumentNullException()
        {                        
            //Act
            await _creditCalculator.CalculateCredit(null);            
        }


        #region Private Methods

            private async Task<CreditCalculatorValuePointsRefDto> GetMockValuesAsync()
            { 
                IReadRangeValuePointsRefData readRangeValuePointsRefData = new ReadRangeValuePointsRefDataMock();
                IReadSingleValuePointsRefData readSingleValuePointsRefData = new ReadSingleValuePointsRefDataMock();

                var creditScores = await readRangeValuePointsRefData.GetCreditScoreAsync();
                var missedPayments = await readSingleValuePointsRefData.GetMissedPaymentsAsync();
                var completedPayments = await readSingleValuePointsRefData.GetCompletedPaymentsAsync();
                var ageCapScores = await readRangeValuePointsRefData.GetAgeCapScoresAsync();
                var avaliableCreditScores = await readSingleValuePointsRefData.GetAvaliableCreditScoresAsync();

                var mockValues = new CreditCalculatorValuePointsRefDto()
                {
                    CreditScores = creditScores.RangeValuePointsRefToDtos(),
                    MissedPayments = missedPayments.SingleValuePointsRefToDtos(),
                    CompletedPayments = completedPayments.SingleValuePointsRefToDtos(),
                    AgeCapScores = ageCapScores.RangeValuePointsRefToDtos(),
                    AvaliableCreditScores = avaliableCreditScores.SingleValuePointsRefToDtos()
                };

                return mockValues;
            }

        #endregion
    }
}