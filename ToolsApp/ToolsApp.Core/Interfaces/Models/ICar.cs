﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToolsApp.Core.Interfaces.Models
{
  public interface ICar
  {
    int Id {  get; set; }
    string Make { get; set; }
    string Model { get; set; }
    int Year { get; set; }
    string Color { get; set; }
    decimal Price { get; set; }
  }
}
