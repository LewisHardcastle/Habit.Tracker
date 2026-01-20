using Habit.Tracker;

var db = new DataAccess("Sample");

db.TestConnection();

db.CreateTable();

Console.ReadKey();