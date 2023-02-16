using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ToolsApp.Data
{
  public class ToolsAppDapperContext
  {
    private readonly string _connectionString;

    public ToolsAppDapperContext(IConfiguration configuration) {
      _connectionString = configuration.GetConnectionString("ToolsApp");
    }

    public ToolsAppDapperContext() {
      _connectionString = "";
    }

    public virtual IDbConnection CreateConnection() => new SqlConnection(_connectionString);

  }
}