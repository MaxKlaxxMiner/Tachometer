// ReSharper disable UnusedMember.Global

namespace WindowsFormsApplication1
{
  /// <summary>
  /// simples Gaspedal, welches nur kein oder vollgas gibt
  /// </summary>
  public class TestGas : IGasgeber
  {
    public double GetGas(double istDrehzahl, double sollDrehzahl)
    {
      return istDrehzahl < sollDrehzahl ? 100 : 0;
    }
  }
}
