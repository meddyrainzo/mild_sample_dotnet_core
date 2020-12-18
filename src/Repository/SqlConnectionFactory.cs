using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace Repository
{
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private IDbConnection _connection;
        private readonly string _connectionString;
        public SqlConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        private void CreateNewConnection()
        {
            _connection = new NpgsqlConnection(_connectionString);
            _connection.Open();
        }

        public IDbConnection GetOpenConnection()
        {
            if (_connection is null || _connection.State != ConnectionState.Open)
                CreateNewConnection();
            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Dispose();
            }
        }
    }

}