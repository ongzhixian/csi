using System;
using System.Data;

namespace Csi.LookMeUp.Services
{
    public interface ISQLiteService
    {
        
    }

    public class SQLiteService : ISQLiteService
    {
        private System.Data.IDbConnection connection;
        private readonly string connectionString;

        //System.Data.SQLite.SQLiteConnection connection;
        // System.Data.SQLite.SQLiteConnectionStringBuilder connectionStringBuilder = 
        //     new System.Data.SQLite.SQLiteConnectionStringBuilder();

        public SQLiteService(string connectionString)
        {
            this.connectionString = connectionString;
            this.InitService();
        }

        private void InitService()
        {
            this.connection = new System.Data.SQLite.SQLiteConnection(connectionString);
        }

        public int Execute(IDbCommand cmd)
        {
            using (IDbConnection connection = new System.Data.SQLite.SQLiteConnection(this.connectionString))
            {
                connection.Open();
                cmd.Connection = connection;
                return cmd.ExecuteNonQuery();
            }
        }

        public IDbCommand GetCommand()
        {
            return this.connection.CreateCommand();
        }


        ////////////////////////////////////////////////////////////////////////////////
        // Creation SQL statements
        ////////////////////////////////////////////////////////////////////////////////

        public void CreateAppUserTableIfNotExists()
        {
            IDbCommand cmd = this.GetCommand();

            cmd.CommandText = 
@"CREATE TABLE IF NOT EXISTS ""app_user"" (
	""id""	            INTEGER NOT NULL PRIMARY KEY,
	""display_name""	TEXT NOT NULL,
	""provider_name""	TEXT NOT NULL,
	""name_identifier""	TEXT NOT NULL,
	""cre_date""	    INTEGER NOT NULL DEFAULT CURRENT_TIMESTAMP
);
";
            this.Execute(cmd);
        }

        public readonly string[] CreateTableSqlStatements = new string[] {
@"CREATE TABLE IF NOT EXISTS ""app_user"" (
	""id""	            INTEGER NOT NULL PRIMARY KEY,
	""display_name""	TEXT NOT NULL,
	""provider_name""	TEXT NOT NULL,
	""name_identifier""	TEXT NOT NULL,
	""cre_date""	    INTEGER NOT NULL DEFAULT CURRENT_TIMESTAMP
);"
        };
    }
}