// ReSharper disable UnusedMember.Global

namespace WindowsFormsApplication1
{
  /// <summary>
  /// 
  /// </summary>
  public class TestGas2 : IGasgeber
  {
    double vorher;
    public double GetGas(double istDrehzahl, double sollDrehzahl)
    {
      double nextDrehzahl = istDrehzahl + (istDrehzahl - vorher) * 80.0;
      vorher = istDrehzahl;
      return nextDrehzahl < sollDrehzahl ? 100.0 : 0.0;
    }
  }
}
