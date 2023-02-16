using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;


namespace ToolsApp.Data.CarTool;

public class CarsInMemoryData : BaseCarsData, ICarsData
{
  private List<CarDataModel> _cars { get; set; } = new() {
    new() { Id = 1, Make="Ford", Model="Fusion Hybrid", Year=2020, Color="Blue", Price=45000 },
    new() { Id = 2, Make="Tesla", Model="S", Year=2022, Color="Red", Price=115000 },
  };

  public CarsInMemoryData(): base() { }

  public Task<IEnumerable<ICar>> All()
  {
    return Task.FromResult(_cars.Select(
        carDataModel => mapper.Map<CarDataModel, CarModel>(carDataModel)
      ).AsEnumerable<ICar>());
  }

  public Task<ICar> Append(INewCar newCar)
  {
    var newCarDataModel = mapper.Map<CarDataModel>(newCar);
    newCarDataModel.Id = _cars.Any() ? _cars.Max(c => c.Id) + 1 : 1;

    _cars.Add(newCarDataModel);

    return Task.FromResult(
      mapper.Map<CarDataModel, CarModel>(newCarDataModel) as ICar);
  }

  public Task Replace(ICar car)
  {
    var carIndex = _cars.FindIndex(c => c.Id == car.Id);
    if (carIndex == -1)
    {
      throw new IndexOutOfRangeException("Car not found");
    }
    _cars[carIndex] = mapper.Map<CarDataModel>(car);
    return Task.CompletedTask;
  }

  public Task Remove(int carId)
  {
    var carIndex = _cars.FindIndex(car => car.Id == carId);
    if (carIndex == -1)
    {
      throw new IndexOutOfRangeException("Car not found");
    }

    _cars.RemoveAt(carIndex);
    return Task.CompletedTask;
  }
}