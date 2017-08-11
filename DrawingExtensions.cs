using System.Drawing;

namespace TachometerApplication
{
  public static class DrawingExtensions
  {
    public static Brush ToBrush(this int wert)
    {
      return new SolidBrush(Color.FromArgb(wert | -16777216));
    }

    public static Pen ToPen(this int wert)
    {
      return new Pen(Color.FromArgb(wert | -16777216));
    }
  }
}
