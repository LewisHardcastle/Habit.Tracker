using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.Data.Sqlite;
using System.Linq.Expressions;

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

    }
}
