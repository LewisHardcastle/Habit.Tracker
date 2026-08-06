using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Habit.Tracker
{
    internal class UserInputHandler
    {
        public void handleUserInput(string userInput, bool userIsActive, DataAccess db)
        {
            switch (userInput.ToUpper())
            {
                case "C":
                    CreateNewHabit(userInput, db);
                    break;
                case "R":
                    var habits = db.ListAllHabits();

                    foreach (HabitModel habit in habits)
                    {
                        Console.WriteLine($"{habit.Name} has been done {habit.Occurrence.Number} times");
                    }
                    break;
                case "U":
                    // Update occurrence
                    Console.WriteLine("Enter the habit name you want to update");
                    string name = Console.ReadLine();
                    Console.WriteLine("Enter the the number of times you have now completed the habit");
                    string newOccurence = Console.ReadLine();

                    while (!newOccurence.All(char.IsAsciiDigit))
                    {
                        Console.WriteLine("Please only insert a number");
                        newOccurence = Console.ReadLine();
                    }

                    int newOccurenceOut;
                    Int32.TryParse(newOccurence, out newOccurenceOut);

                    db.UpdateHabitOccurrence(name, newOccurenceOut);

                    break;
                case "D":
                    // Delete habit
                    Console.WriteLine("Which habit would you like to delete?");
                    string habitToDelete = Console.ReadLine();
                    db.DeleteHabit(habitToDelete);
                    break;
                default:
                    //
                    break;
            }

            Console.WriteLine("press Y to continue using the habit tracker or N to exit");
            string checkUserIsActive = Console.ReadLine().ToUpper();
            if (checkUserIsActive == "Y")
            {
                userIsActive = true;
            }
            else
            {
                System.Environment.Exit(0);
            }
        }

        private void CreateNewHabit(string userInput, DataAccess db)
        {
            Console.WriteLine("Enter the habit name");
            string habitName = Console.ReadLine();
            Console.WriteLine("Enter how many times you have completed the habit");
            string habitOccurrence = Console.ReadLine();

            while (!CheckStringIsANumber(habitOccurrence))
            {
                Console.WriteLine("Please only insert a number");
                habitOccurrence = Console.ReadLine();
            }

            int habitOccurrenceNum;
            Int32.TryParse(habitOccurrence, out habitOccurrenceNum);

            DateTime validatedHabitOccurrenceDate = GetDate();

            HabitModel newHabit = new HabitModel
            {
                Name = habitName,
                Occurrence = new Occurrence
                {
                    Date = validatedHabitOccurrenceDate,
                    Number = habitOccurrenceNum
                }
            };

            db.InsertHabit(newHabit);
        }

        internal static bool CheckStringIsANumber(string userInput)
        {
            int validatedUserInput;

            bool isANumber = int.TryParse(userInput, out validatedUserInput) ? true : false;

            return isANumber;
        }

        private DateTime GetDate()
        {
            Console.WriteLine("Enter a date (dd/mm/yyyy): ");
            string input = Console.ReadLine();

            DateTime? validatedHabitOccurrenceDate;

            while (true)
            {
                validatedHabitOccurrenceDate = CheckStringIsPastOrPresentDate(input);
                if (validatedHabitOccurrenceDate.HasValue)
                {
                    return validatedHabitOccurrenceDate.Value;
                }
                input = Console.ReadLine();
            }
        }

        internal static DateTime? CheckStringIsPastOrPresentDate(string userInputDate)
        {
            DateTime habitOccurrenceDate;

            if (DateTime.TryParseExact(userInputDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out habitOccurrenceDate))
            {
                if (habitOccurrenceDate <= DateTime.Today)
                {
                    Console.WriteLine($"Valid date: {habitOccurrenceDate:dd MMMM yyyy}");
                    return habitOccurrenceDate;
                }
                Console.WriteLine("Date cannot be in the future, please try again");
                return null;
            }
            Console.WriteLine("Invalid Date, please try again");
            return null;
        }
    }
}
