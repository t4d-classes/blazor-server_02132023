using AutoMapper;
using ToolsApp.Models;
using ToolsApp.Core.Interfaces.Models;

namespace ToolsApp.Components.CarTool.Models;

public class CarToolMapper
{
  public static MapperConfiguration GetConfig() {
    return new MapperConfiguration(config =>
    {
      config.CreateMap<CarForm, NewCar>();
      config.CreateMap<ICar, CarForm>();
      config.CreateMap<CarForm, Car>();
    });
  }

  public static IMapper GetMapper() {
    return GetConfig().CreateMapper();
  }
}
