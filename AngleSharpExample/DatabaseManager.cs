using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

public class DatabaseManager
{
    private SqliteConnection connection;

    public DatabaseManager(string connectionString)
    {
        connection = new SqliteConnection(connectionString);
        connection.Open();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        using var command = connection.CreateCommand();
        command.CommandText =
        @"
            CREATE TABLE IF NOT EXISTS Articles (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT,
                Url TEXT
            );
        ";
        command.ExecuteNonQuery();
    }

    public async Task SaveArticleAsync(Article article)
    {
        using var command = connection.CreateCommand();
        command.CommandText = "INSERT INTO Articles (Title, Url) VALUES (@Title, @Url)";
        command.Parameters.AddWithValue("@Title", article.Title);
        command.Parameters.AddWithValue("@Url", article.Url);
        await command.ExecuteNonQueryAsync();
    }
}
