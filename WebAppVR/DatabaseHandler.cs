//  DatabaseHandler.css
using Microsoft.Data.SqlClient;
using System;

namespace WhatsAppBot
{
    public class DatabaseHandler
    {
        // Update the connection string to point to your SQL Server instance
        private const string ConnectionString = "Server=YOUR_SERVER_NAME;Database=YOUR_DATABASE_NAME;Trusted_Connection=True;";

        public DatabaseHandler()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Messages' AND xtype='U')
                CREATE TABLE Messages (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Sender NVARCHAR(100) NOT NULL,
                    MessageText NVARCHAR(MAX) NOT NULL,
                    Timestamp DATETIME DEFAULT GETDATE(),
                    IsDeleted BIT DEFAULT 0
                )";
            command.ExecuteNonQuery();
        }

        public void SaveMessage(string sender, string messageText)
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO Messages (Sender, MessageText)
                VALUES (@sender, @messageText)";
            command.Parameters.AddWithValue("@sender", sender);
            command.Parameters.AddWithValue("@messageText", messageText);

            command.ExecuteNonQuery();
        }
    }
}
