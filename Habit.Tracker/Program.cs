using Habit.Tracker;

var db = new DataAccess("Sample");

db.TestConnection();

db.CreateTable();

Console.WriteLine("Welcome to Habit Tracker!");
Console.WriteLine("Choose from the options below");
Console.WriteLine("C - Add a record of a habit");
Console.WriteLine("R - List All habits");
Console.WriteLine("U - Update a habits occurrences");
Console.WriteLine("D - Delete a habit");

string userInput = Console.ReadLine();

switch(userInput.ToUpper())
{
    case "C":
        Console.WriteLine("Enter the habit name");
        string habitName = Console.ReadLine();
        Console.WriteLine("Enter how many times you have completed the habit");
        string habitOccurrence = Console.ReadLine();
        
        while(!habitOccurrence.All(char.IsAsciiDigit))
        {
            Console.WriteLine("Please only insert a number");
            habitOccurrence = Console.ReadLine();
        }

        int habitOccurrenceNum;
        Int32.TryParse(habitOccurrence, out habitOccurrenceNum);
        

        HabitModel newHabit = new HabitModel
        {
            Name = habitName,
            Occurrence = habitOccurrenceNum
        };

        db.InsertHabit(newHabit);

        break;
    case "R":
        // List all habits
        break;
    case "U":
        // Update occurrence
        break;
    case "D":
        // Delete habit
        break;
    default:
        //
        break;
}


Console.ReadKey();