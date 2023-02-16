using AutoMapper;
using ToolsApp.Core.Interfaces.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;


namespace ToolsApp.Data.CarTool;

public abstract class BaseCarsData
{
  protected IMapper mapper;

  public BaseCarsData() {
    var mapperConfig = new MapperConfiguration(config => {
      config.CreateMap<INewColor, CarDataModel>();
      config.CreateMap<CarDataModel, CarModel>();
    });

    mapper = mapperConfig.CreateMapper();
  }

}
