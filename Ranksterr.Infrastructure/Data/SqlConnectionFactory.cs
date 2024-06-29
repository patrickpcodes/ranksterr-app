using System.Data;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using Ranksterr.Application.Abstractions.Data;

namespace Ranksterr.Infrastructure.Data;

internal sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory( string connectionString )
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection( _connectionString );
        connection.Open();

        return connection;
    }
}