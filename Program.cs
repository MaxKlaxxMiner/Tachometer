using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WindowsFormsApplication1
{
 public static class Erweiterungen
 {
  #region # // --- überladene Werte für einfachere Bedienung ---
  public static Color ToColor(this int wert)
  {
   return Color.FromArgb(wert | -16777216);
  }

  public static Brush ToBrush(this int wert)
  {
   return new SolidBrush(Color.FromArgb(wert | -16777216));
  }

  public static Pen ToPen(this int wert)
  {
   return new Pen(Color.FromArgb(wert | -16777216));
  }
  #endregion
 }

 static class Program
 {
  /// <summary>
  /// Der Haupteinstiegspunkt für die Anwendung.
  /// </summary>
  [STAThread]
  static void Main()
  {
   Application.EnableVisualStyles();
   Application.SetCompatibleTextRenderingDefault(false);
   Application.Run(new Form1());
  }
 }
}
