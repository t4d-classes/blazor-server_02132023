using AutoMapper;
using Microsoft.EntityFrameworkCore;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;

namespace ToolsApp.Data.CarTool;

public class CarsEFCoreData : ICarsData
{
  private IMapper _mapper;
  private ToolsAppDbContext _toolsAppDbContext;

  public CarsEFCoreData(ToolsAppDbContext toolsAppDbContext)
  {
    _toolsAppDbContext = toolsAppDbContext;

    var mapperConfig = new MapperConfiguration(config => {
      config.CreateMap<INewCar, CarDataModel>();
      config.CreateMap<ICar, CarDataModel>();
      config.CreateMap<CarDataModel, CarModel>().ReverseMap();
    });

    _mapper = mapperConfig.CreateMapper();
  }

  public async Task<IEnumerable<ICar>> All()
  {
    if (_toolsAppDbContext.Cars is null) {
      throw new NullReferenceException("cars database set cannot be null");
    }

    return await _toolsAppDbContext
      .Cars
      .Select(carDataModel => _mapper.Map<CarDataModel, CarModel>(carDataModel))
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<ICar> Append(INewCar newCar)
  {
    var newCarDataModel = _mapper.Map<CarDataModel>(newCar);

    await _toolsAppDbContext.AddAsync(newCarDataModel);
    await _toolsAppDbContext.SaveChangesAsync();
    _toolsAppDbContext.ChangeTracker.Clear();

    return _mapper.Map<CarDataModel, CarModel>(newCarDataModel);
  }

  public async Task Replace(ICar car)
  {
    var carDataModel = _mapper.Map<CarDataModel>(car);
    _toolsAppDbContext.Update(carDataModel);
    await _toolsAppDbContext.SaveChangesAsync();
    _toolsAppDbContext.ChangeTracker.Clear();
  }

  public async Task Remove(int carId)
  {
    if (_toolsAppDbContext.Cars is null)
    {
      throw new NullReferenceException("cars database set cannot be null");
    }

    var carDataModel = await _toolsAppDbContext.Cars.FindAsync(carId);
    if (carDataModel is not null) {
      _toolsAppDbContext.Cars.Remove(carDataModel);
      await _toolsAppDbContext.SaveChangesAsync();
      _toolsAppDbContext.ChangeTracker.Clear();
    }
  }
}