using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using Widgets.Data;
using Widgets.Data.Migrations;

namespace Widgets.Tests
{
    public static class TestSetup
    {
        public static void CreateDatabase()
        {
            // Create database
            ExecuteSqlCommand(Master, $@"
                CREATE DATABASE [Widgets]
                ON (NAME = 'Widgets',
                FILENAME = '{Filename}')");

            // Run the database migration
            var migration = new MigrateDatabaseToLatestVersion<WidgetsContext, WidgetsConfiguration>();
            migration.InitializeDatabase(new WidgetsContext());
        }

        public static void DestroyDatabase()
        {
            var fileNames = ExecuteSqlQuery(Master, @"
                SELECT [physical_name] FROM [sys].[master_files]
                WHERE [database_id] = DB_ID('Widgets')",
                row => (string)row["physical_name"]);

            if (fileNames.Any())
            {
                ExecuteSqlCommand(Master, @"
                    ALTER DATABASE [Widgets] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                    EXEC sp_detach_db 'Widgets'");

                fileNames.ForEach(File.Delete);
            }
        }

        private static void ExecuteSqlCommand(SqlConnectionStringBuilder connectionStringBuilder, string commandText)
        {
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.ExecuteNonQuery();
                }
            }
        }

        private static List<T> ExecuteSqlQuery<T>(SqlConnectionStringBuilder connectionStringBuilder, string queryText, Func<SqlDataReader, T> read)
        {
            var result = new List<T>();
            using (var connection = new SqlConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = queryText;
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(read(reader));
                        }
                    }
                }
            }
            return result;
        }

        private static SqlConnectionStringBuilder Master => new SqlConnectionStringBuilder
        {
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            InitialCatalog = "master",
            IntegratedSecurity = true
        };

        private static string Filename => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Widgets.mdf");
    }
}
