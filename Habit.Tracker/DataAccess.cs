using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Data.Sqlite;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Habit.Tracker
{
    internal class DataAccess
    {
        string _connectionString;

        public DataAccess(string dbName)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[dbName].ConnectionString;
        }

        public void TestConnection()
        {
            try
            {
                var con = new SqliteConnection(_connectionString);
                con.Open();
                con.Close();

                Console.WriteLine("Connection successful.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
            }
        }

        public void CreateTable()
        {
            string sql = "CREATE TABLE IF NOT EXISTS Habits (" +
                         "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                         "Name TEXT NOT NULL, " +
                         "Occurrence INTEGER NOT NULL)";

            try
            {
                using (var con = new SqliteConnection(_connectionString))
                {
                    con.Open();
                    using (var cmd = new SqliteCommand(sql, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("Habit Tracker created.");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating table: " + ex.Message);
            }
        }

        public void InsertHabit(HabitModel habit)
        {
            string sql = "INSERT INTO Habits (Name, Occurrence) VALUES (@Name, @Occurrence)";
            try
            {
                using (var con = new SqliteConnection(_connectionString))
                {
                    con.Open();
                    using (var cmd = new SqliteCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", habit.Name);
                        cmd.Parameters.AddWithValue("@Occurrence", habit.Occurrence);
                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine($"{habit.Name} inserted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inserting habit: " + ex.Message);
            }
        }

        public List<HabitModel> ListAllHabits()
        {
            string sql = "SELECT * FROM Habits";
            var habits = new List<HabitModel>();
            try
            {
                using (var con = new SqliteConnection(_connectionString))
                {
                    con.Open();
                    using (var cmd = new SqliteCommand(sql, con))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var habit = new HabitModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Occurrence = reader.GetInt32(reader.GetOrdinal("Occurrence"))
                            };

                            habits.Add(habit);
                        }
                    }
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return habits;
        }
    }
}
