using Microsoft.AspNetCore.Components;

namespace ToolsApp.Components.Shared.TabDialog;

public interface ITab
{
  RenderFragment ChildContent { get;}
}
