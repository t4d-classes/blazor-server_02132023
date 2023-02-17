using System.ComponentModel.DataAnnotations;

namespace ToolsApp.Components.CarTool.Models;

public class CarForm
{
  [Required]
  [MinLength(3)]
  public string Make { get; set; }
  
  [Required]
  public string Model { get; set; }

  [Required] 
  public int Year { get; set; }

  [Required] 
  public string Color { get; set; }

  [Required] 
  public decimal Price { get; set; }
}
