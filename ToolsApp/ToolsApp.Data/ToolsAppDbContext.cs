using Microsoft.EntityFrameworkCore;

using ToolsApp.Data.Models;

namespace ToolsApp.Data;

public class ToolsAppDbContext: DbContext
{
  public ToolsAppDbContext(DbContextOptions<ToolsAppDbContext> options): base(options) {  }


  public DbSet<Color> Colors { get; set; }
}
