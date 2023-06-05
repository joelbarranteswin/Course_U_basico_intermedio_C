namespace xUnitTestIntro
{
    public class MailTesting
    {
        [Fact]
        public void ValidateValidEmails()
        {
            //Arrange
            var mailValidator = new UnitTestingIntro.Mail();
            string emailAddres = "joelbarrantes@gmail.com";

            //Act
            bool isValid = mailValidator.IsValidEmail(emailAddres);

            //Assert
            Assert.True(isValid, "el email no es valido");
        }

        [Fact]
        public void InvalidateInvalidEmails()
        {
            //Arrange
            var mailValidator = new UnitTestingIntro.Mail();
            string emailAddres = "joelbarrantes@gmail";

            //Act
            bool isValid = mailValidator.IsValidEmail(emailAddres);

            //Assert
            Assert.False(isValid, "el email debe ser falso");
        }

        [Theory]
        [InlineData("invalid@invalid.invalid", false)]
        [InlineData("thesolutioner@gmail.com", true)]
        public void ValidateEmailWithTheory(string emailAddress, bool expected)
        {
            //Arrange
            var mailValidator = new UnitTestingIntro.Mail();

            //Act
            bool isValid = mailValidator.IsValidEmail(emailAddress);

            //Assert
            Assert.Equal(expected, isValid);
        }

        [Theory]
        [InlineData("spam@gmail.com", "OK")]
        [InlineData("spam@spam.com", "SPAM")]
        public void IdentifySpam(string emailAddress, string expected)
        {
            //Arrange
            var mailValidator = new UnitTestingIntro.Mail();

            //Act
            string isSpam = mailValidator.IsSpam(emailAddress);

            //Assert
            Assert.Equal(expected, isSpam);
        }

        [Fact]
        public void RaiseErrorWhenEmailisEmpty()
        {
            //Arrange
            var mailValidator = new UnitTestingIntro.Mail();

            //Act

            //Assert
            Assert.Throws<Exception>(() => mailValidator.IsValidEmail(""));
        }
    }
}