using System.Data;
using Microsoft.Data.SqlClient;

namespace Repository
{
    public interface ISqlConnectionFactory
    {
        IDbConnection GetOpenConnection();
    }

}