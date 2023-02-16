using AutoMapper;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;
using Microsoft.EntityFrameworkCore;

namespace ToolsApp.Data.ColorTool;

public class ColorsEFCoreData : IColorsData
{
  private IMapper _mapper;
  private ToolsAppDbContext _toolsAppDbContext;

  public ColorsEFCoreData(
    CarsDataMapper mapper,
    ToolsAppDbContext toolsAppDbContext)
  {
    _toolsAppDbContext = toolsAppDbContext;
    _mapper = mapper.CreateMapper();
  }

  public async Task<IEnumerable<IColor>> All()
  {
    if (_toolsAppDbContext.Colors is null)
    {
      throw new NullReferenceException("colors database set cannot be null");
    }

    return await _toolsAppDbContext
      .Colors
      .Select(colorDataModel =>_mapper.Map<ColorDataModel, ColorModel>(colorDataModel))
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<IColor> Append(INewColor color)
  {
    var colorDataModel = _mapper.Map<ColorDataModel>(color);

    await _toolsAppDbContext.AddAsync(colorDataModel);
    await _toolsAppDbContext.SaveChangesAsync();
    _toolsAppDbContext.ChangeTracker.Clear();

    return _mapper.Map<ColorDataModel, ColorModel>(colorDataModel);
  }

  public async Task Remove(int colorId)
  {
    if (_toolsAppDbContext.Colors is null)
    {
      throw new NullReferenceException("colors database set cannot be null");
    }

    var colorDataModel = await _toolsAppDbContext.Colors.FindAsync(colorId);
    if (colorDataModel is not null) {
      _toolsAppDbContext.Colors.Remove(colorDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();
    }

  }
}
