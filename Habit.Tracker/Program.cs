using Habit.Tracker;
using System.Configuration;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

var db = new DataAccess("Sample");

db.TestConnection();

db.CreateTable();

bool userIsActive = true;

UserInputHandler userInputHandler = new UserInputHandler();

while(userIsActive)
{
    Console.WriteLine("Welcome to Habit Tracker!");
    Console.WriteLine("Choose from the options below");
    Console.WriteLine("C - Add a record of a habit");
    Console.WriteLine("R - List All habits");
    Console.WriteLine("U - Update a habits occurrences");
    Console.WriteLine("D - Delete a habit");

    string userInput = Console.ReadLine();

    userInputHandler.handleUserInput(userInput, userIsActive, db);
}
