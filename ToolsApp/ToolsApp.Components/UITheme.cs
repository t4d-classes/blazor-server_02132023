namespace ToolsApp.Components;

public class ButtonStyle {
  public string BackgroundColor {  get; set; } = "initial";
  public string TextColor { get; set; } = "initial";
}

public class UITheme
{
  public ButtonStyle? PrimaryButton { get; set; }
  public ButtonStyle? SecondaryButton { get; set; }
  public ButtonStyle? DangerButton { get; set; }
}
