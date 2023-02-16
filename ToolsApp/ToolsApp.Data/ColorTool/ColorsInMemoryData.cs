using AutoMapper;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;

namespace ToolsApp.Data.ColorTool;

public class ColorsInMemoryData : IColorsData
{
  private IMapper _mapper;

  private List<ColorDataModel> _colors = new()
  {
    new() { Id = 1, Name = "red", HexCode="ff0000"},
    new() { Id = 2, Name = "green", HexCode="00ff00"},
    new() { Id = 3, Name = "blue", HexCode="0000ff"},
  };

  public ColorsInMemoryData(CarsDataMapper mapper) {
    _mapper = mapper.CreateMapper();
  }

  public Task<IEnumerable<IColor>> All()
  {
    return Task.FromResult(_colors
      .Select(colorDataModel =>
        _mapper.Map<ColorDataModel, ColorModel>(colorDataModel))
      .AsEnumerable<IColor>());
  }

  public Task<IColor> Append(INewColor color)
  {
    var colorDataModel = _mapper.Map<ColorDataModel>(color);
    colorDataModel.Id = _colors.Any() ? _colors.Max(c => c.Id) + 1 : 1;
     
    _colors.Add(colorDataModel);

    return Task.FromResult(
      _mapper.Map<ColorDataModel, ColorModel>(colorDataModel) as IColor
     );
  }

  public Task Remove(int colorId)
  {
    _colors.RemoveAt(_colors.FindIndex(c => c.Id == colorId));
    return Task.CompletedTask;
  }
}
