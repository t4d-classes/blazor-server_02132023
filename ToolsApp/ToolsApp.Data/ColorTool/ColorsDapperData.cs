using AutoMapper;
using Dapper;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;

namespace ToolsApp.Data.ColorTool;

public class ColorsDapperData : IColorsData
{
  private IMapper _mapper;
  private ToolsAppDapperContext _toolsAppDapperContext;

  public ColorsDapperData(
    ColorsDataMapper mapper,
    ToolsAppDapperContext toolsAppDapperContext)
  {
    _toolsAppDapperContext = toolsAppDapperContext;
    _mapper = mapper.CreateMapper();
  }

  public async Task<IEnumerable<IColor>> All()
  {
    using var con = _toolsAppDapperContext.CreateConnection();

    var sql = "select Id, Name, Hexcode from Color";
    var colors = await con.QueryAsync<ColorDataModel>(sql);

    return colors
      .Select(color => _mapper.Map<ColorDataModel, ColorModel>(color))
      .AsEnumerable<IColor>();
  }

  public async Task<IColor> Append(INewColor color)
  {
    using var con = _toolsAppDapperContext.CreateConnection();

    var colorData = await con.QueryAsync<ColorDataModel>(
      "[InsertColor]",
      color,
      commandType: System.Data.CommandType.StoredProcedure);

    return _mapper.Map<ColorDataModel, ColorModel>(colorData.Single()) as IColor;
  }

  public async Task Remove(int colorId)
  {
    using var con = _toolsAppDapperContext.CreateConnection();

    await con.ExecuteAsync(
      "[DeleteColor]",
      new { Id = colorId },
      commandType: System.Data.CommandType.StoredProcedure
    );
  }
}
