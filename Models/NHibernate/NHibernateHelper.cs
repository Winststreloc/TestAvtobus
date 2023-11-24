using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MySql.Data.MySqlClient;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace TestAvtobus.Models.NHibernate;

public class NHibernateHelper
{
    private readonly IConfiguration _configuration;
    private static string? _connectionString;

    public NHibernateHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MySqlConn");
    }

    public static global::NHibernate.ISession OpenSession()
    {
        ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MySQLConfiguration.Standard.ConnectionString("Server=localhost;Database=RecordUrlDb;Uid=winststreloc;Pwd=Scarlxrd_25;")
                .ShowSql()
            )
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<RecordURL>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
        return sessionFactory.OpenSession();
    }
}