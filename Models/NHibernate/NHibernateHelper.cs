using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MySql.Data.MySqlClient;
using NHibernate.Tool.hbm2ddl;
using ISession = NHibernate.ISession;

namespace TestAvtobus.Models.NHibernate;

public class NHibernateHelper
{
    private const string ConnectionString = "Server=localhost;Uid=winststreloc;Pwd=1488;"; //тут нужно изменить ConnectionString
    public static ISession OpenSession()
    {
        
        var config = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString(ConnectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RecordURL>())
            .BuildConfiguration();
        
        using (var tempSessionFactory = config.BuildSessionFactory())
        using (var tempSession = tempSessionFactory.OpenSession())
        {
            var databaseExists = CheckIfDatabaseExists(tempSession, "RecordUrlDb"); 

            if (!databaseExists)
            {
                CreateDatabase(ConnectionString, "RecordUrlDb");
            }
        }
        
        var sessionFactory = Fluently.Configure()
            .Database(MySQLConfiguration.Standard
                .ConnectionString(ConnectionString + "Database=RecordUrlDb;")
                .ShowSql()
            )
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RecordURL>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();

        return sessionFactory.OpenSession();
    }
    
    private static bool CheckIfDatabaseExists(ISession session, string databaseName)
    {
        var sqlQuery = $"SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{databaseName}'";
        var result = session.CreateSQLQuery(sqlQuery).UniqueResult<string>();
        return !string.IsNullOrEmpty(result);
    }
    
    private static void CreateDatabase(string connectionString, string databaseName)
    {
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = $"CREATE DATABASE IF NOT EXISTS `{databaseName}`";
                                  
            command.ExecuteNonQuery();
        }
    }
    
}