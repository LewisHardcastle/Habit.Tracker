using Habit.Tracker;
using System.Configuration;

namespace Habit.Tracker.Test
{
    public class HabitTrackerTest
    {
        [TestCase("abc")]
        [TestCase("!@#")]
        [TestCase("12a")]
        [TestCase("a12")]
        [TestCase("\n")]
        [TestCase(" ")]
        [TestCase("")]
        public void GivenInput_WhenIsNotANumber_ThenReturnFalse(string userInput)
        {
            //Arrange & Act
            var result = UserInputHandler.CheckStringIsANumber(userInput);

            //Assert
            Assert.That(result, Is.False);
        }

        [TestCase("1")]
        [TestCase("0")]
        [TestCase("99")]
        [TestCase("100")]
        public void GivenInput_WhenIsANumber_ThenReturnTrue(string userInput)
        {
            //Arrange & Act
            var result = UserInputHandler.CheckStringIsANumber(userInput);

            //Assert
            Assert.That(result, Is.True);
        }
        [TestCase("24/06/1997")]
        [TestCase("01/12/2024")]
        public void GivenString_WhenIsDate_ThenReturnDateTime(string date)
        {
            //Arrange & Act
            var result = UserInputHandler.CheckStringIsPastOrPresentDate(date);

            //Assert
            Assert.That(result, Is.InstanceOf<DateTime>());
        }

        [TestCase("01/13/1997")]
        [TestCase("01/25/2024")]
        [TestCase("abc")]
        [TestCase("")]
        [TestCase("\n")]
        public void GivenString_WhenIsNotDate_ThenReturnNull(string date)
        {
            //Arrange & Act
            var result = UserInputHandler.CheckStringIsPastOrPresentDate(date);

            //Assert
            Assert.That(result, Is.Null);
        }

        private static IEnumerable<string> FutureDates()
        {
            yield return DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
            yield return DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");
            yield return DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
        }

        [TestCaseSource(nameof(FutureDates))]
        public void GivenStringDate_WhenDateIsFuture_ThenReturnNull(string date)
        {
            // Arrange & Act
            var result = UserInputHandler.CheckStringIsPastOrPresentDate(date);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
