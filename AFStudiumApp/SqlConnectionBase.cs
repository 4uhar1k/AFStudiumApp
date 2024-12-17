using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
//using AFStudiumApp.Models;
using System.Collections.ObjectModel;
using SQLite;


namespace AFStudiumApp
{
    public class SqliteConnectionBase
    {
        string DataBasePath = "curuserdatabase.db3";
        public ISQLiteAsyncConnection CreateConnection()
        {
            return new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DataBasePath),
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
        }
        /* private readonly string _connectionString;

         public SqlConnectionBase()
         {
             _connectionString = "Server=localhost;Port=3306;Database=AFStudiumDB;Uid=root;Pwd=root;";
         }

         public async Task<ObservableCollection<string>> GetUsers()
         {

             ObservableCollection<string> users = new ObservableCollection<string>();
             MySqlConnection connection = new MySqlConnection(_connectionString);
             //connection.Close();
             try
             {
                 connection.Open();
             }
             catch (AggregateException ex)
             {
                 foreach (var innerException in ex.InnerExceptions)
                 {
                     Console.WriteLine(innerException.Message);
                 }
             }

             string query = "Select * From UsersTable";
             using var command = new MySqlCommand(query, connection);
             using var reader = await command.ExecuteReaderAsync();
             while (await reader.ReadAsync())
             {
                 try
                 {
                     users.Add(reader.GetString(0));
                 }
                 catch { }

             }
             await connection.CloseAsync();
             return users;
         }

         public async Task InsertUser(User user)
         {
             using var connection = new MySqlConnection(_connectionString);
             await connection.OpenAsync();
             string query = $"Insert Into UserTable Values ({user.MatrikelNum}, {user.Email}, {user.Password}, {user.Name}, {user.Surname}, {user.Course}, {user.Semester})";
             using var command = new MySqlCommand(query, connection);
             await command.ExecuteNonQueryAsync();
             await connection.CloseAsync();

         }*/
    }
}
