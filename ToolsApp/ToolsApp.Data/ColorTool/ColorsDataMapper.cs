using AutoMapper;
using ToolsApp.Core.Interfaces.Models;

using ColorModel = ToolsApp.Models.Color;
using ColorDataModel = ToolsApp.Data.Models.Color;


namespace ToolsApp.Data.ColorTool;

public class CarsDataMapper
{

  public IMapper CreateMapper()
  {
    var mapperConfig = new MapperConfiguration(config => {
      config.CreateMap<INewColor, ColorDataModel>();
      config.CreateMap<ColorDataModel, ColorModel>();
    });

    return mapperConfig.CreateMapper();
  }

}
