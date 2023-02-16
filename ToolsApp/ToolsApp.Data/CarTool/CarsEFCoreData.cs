using Microsoft.EntityFrameworkCore;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;

namespace ToolsApp.Data.CarTool;

public class CarsEFCoreData : BaseCarsData, ICarsData
{
  private ToolsAppDbContext _toolsAppDbContext;

  public CarsEFCoreData(ToolsAppDbContext toolsAppDbContext): base()
  {
    _toolsAppDbContext = toolsAppDbContext;
  }

  public async Task<IEnumerable<ICar>> All()
  {
    if (_toolsAppDbContext.Cars is null) {
      throw new NullReferenceException("cars database set cannot be null");
    }

    return await _toolsAppDbContext
      .Cars
      .Select(carDataModel => mapper.Map<CarDataModel, CarModel>(carDataModel))
      .AsNoTracking()
      .ToListAsync();
  }

  public async Task<ICar> Append(INewCar newCar)
  {
    var newCarDataModel = mapper.Map<CarDataModel>(newCar);

    await _toolsAppDbContext.AddAsync(newCarDataModel);
    await _toolsAppDbContext.SaveChangesAsync();
    _toolsAppDbContext.ChangeTracker.Clear();

    return mapper.Map<CarDataModel, CarModel>(newCarDataModel);
  }

  public async Task Replace(ICar car)
  {
    var carDataModel = mapper.Map<CarDataModel>(car);
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