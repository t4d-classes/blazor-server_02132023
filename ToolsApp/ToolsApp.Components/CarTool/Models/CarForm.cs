using System.ComponentModel.DataAnnotations;
using ToolsApp.Components.Shared.Validators;

namespace ToolsApp.Components.CarTool.Models;

public class CarForm
{
  [Required]
  [MinLength(3)]
  public string Make { get; set; }
  
  [Required]
  public string Model { get; set; }

  [Required]
  [MinNum(1886)]
  public int Year { get; set; }

  [Required] 
  public string Color { get; set; }

  [Required]
  [MinNum(0)]
  public decimal Price { get; set; }
}
