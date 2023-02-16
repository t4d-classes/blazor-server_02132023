using Dapper;

using ToolsApp.Core.Interfaces.Data;
using ToolsApp.Core.Interfaces.Models;

using CarModel = ToolsApp.Models.Car;
using CarDataModel = ToolsApp.Data.Models.Car;


namespace ToolsApp.Data.CarTool
{

  public class CarsDapperData : BaseCarsData, ICarsData
  {
    private ToolsAppDapperContext _dataContext;

    public CarsDapperData(ToolsAppDapperContext dataContext): base()
    {
      _dataContext = dataContext;
    }

    public async Task<IEnumerable<ICar>> All()
    {
      using var con = _dataContext.CreateConnection();

      var sql = "select Id, Make, Model, Year, Color, Price from Car";
      var cars = await con.QueryAsync<CarDataModel>(sql);

      return cars
        .Select(car => mapper.Map<CarDataModel, CarModel>(car))
        .AsEnumerable<ICar>();
    }

    public async Task<ICar> Append(INewCar car)
    {
      using var con = _dataContext.CreateConnection();

      var carData = await con.QueryAsync<CarDataModel>(
        "[InsertCar]",
        car,
        commandType: System.Data.CommandType.StoredProcedure
      );

      return mapper.Map<CarDataModel, CarModel>(carData.Single()) as ICar;
    }

    public async Task<ICar> One(int carId)
    {
      using var con = _dataContext.CreateConnection();

      var parameters = new { CarId = carId };
      var sql = "select Id, Make, Model, Year, Color, Price from Car where Id = @CarId";
      var cars = await con.QueryAsync<CarDataModel>(sql, parameters);

      var car = cars
        .Select(car => mapper.Map<CarDataModel, CarModel>(car))
        .Cast<ICar>().SingleOrDefault();

      if (car is null) {
        throw new NullReferenceException($"car with id of {carId} not found");
      }

      return car;
    }

    public async Task Remove(int carId)
    {
      using var con = _dataContext.CreateConnection();

      await con.ExecuteAsync(
        "[DeleteCar]",
        new { Id = carId },
        commandType: System.Data.CommandType.StoredProcedure
      );
    }

    public async Task Replace(ICar car)
    {
      using var con = _dataContext.CreateConnection();

      await con.ExecuteAsync(
        "[UpdateCar]",
        car,
        commandType: System.Data.CommandType.StoredProcedure
      );
    }
  }

}
