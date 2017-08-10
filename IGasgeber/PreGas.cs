// ReSharper disable UnusedMember.Global

namespace WindowsFormsApplication1
{
  /// <summary>
  /// 
  /// </summary>
  public class PreGas : IGasgeber
  {
    double vorherIst;
    double g = 50.0;
    public double GetGas(double istDrehzahl, double sollDrehzahl)
    {
      double diff = (sollDrehzahl - istDrehzahl) * 0.1;
      double speed = (istDrehzahl - vorherIst) * 100.0;
      diff -= speed;


      vorherIst = istDrehzahl;
      g += diff * 0.0001;
      if (g < 0.0) g = 0.0;
      if (g > 100.0) g = 100.0;
      return g;
    }
  }
}
