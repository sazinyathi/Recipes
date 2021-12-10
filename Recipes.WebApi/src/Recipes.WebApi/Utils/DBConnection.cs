using System.Data.SQLite;

namespace Recipes.WebApi.Utils
{
    public static class DBConnection
    {
        public static string ConnectionString { get; set; }

        public static SQLiteConnection GetOpenConnection(string connectionString, bool mars = false)
        {
            try
            {
                var connectionStringbuilder = new SQLiteConnectionStringBuilder();
                connectionStringbuilder.DataSource = @"./RecipesDB.db";
                var connection = new SQLiteConnection(connectionStringbuilder.ConnectionString);
                connection.Open();
                return connection;
            }
            catch (System.Exception ex)
            {

                throw;
            }
                
           
        }
    }
}


