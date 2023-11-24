using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Reflection;
using MySql.Data.MySqlClient;
using NHibernate.Loader.Custom;
using TestAvtobus.Models;

namespace TestAvtobus.Data;

public class DbInitializationService : IDbInitializationService
{
    private readonly string? _connectionString;
    private readonly string? _dbName;
    private readonly IConfiguration _configuration;

    public DbInitializationService(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("MySqlConn");
        _dbName = configuration.GetValue("dbName", "RecordUrlDb");
    }

    public void InitializeDatabase()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();
            
            string createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS {_dbName}";
            using (var command = new MySqlCommand(createDatabaseQuery, connection))
            {
                command.ExecuteNonQuery();
            }
            
            connection.ChangeDatabase($"{_dbName}");
            
            ApplyMigrations(connection);
            
            SeedData(connection);
        }
    }

    private void ApplyMigrations(MySqlConnection connection)
    {
        // Логика применения миграций (используйте библиотеку для миграций)
    }

    private void SeedData(MySqlConnection connection)
    {
        var recordUrl = new RecordURL()
        {
            Id = new long(),
            CreatedAt = DateTime.Now,
            OriginalUrl = "https://example.com",
            ShortUrl = "abc123"
        };
        
    }
}