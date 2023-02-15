using System.Threading.Tasks;

using ToolsApp.Core.Interfaces.Models;

namespace ToolsApp.Core.Interfaces.Data;

public interface IColorsData
{
  Task<IEnumerable<IColor>> All();
  Task<IColor> Append(INewColor color);

  Task Remove(int colorId);
}


// three implementations of IColorsData service

// - ColorsInMemoryData
// - ColorsDapperData
// - ColorsEFCoreData

// I should be able to swap the different services
// without having to modify the application

// the chosen service will be determined by configuration