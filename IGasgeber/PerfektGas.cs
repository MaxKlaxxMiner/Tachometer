// ReSharper disable UnusedMember.Global

namespace WindowsFormsApplication1
{
  /// <summary>
  /// 
  /// </summary>
  public sealed class PerfektGas : IGasgeber
  {
    readonly MotorSteuerung jetztMotor = new MotorSteuerung(8600.0);

    double NextGas(double istDrehzahl, double sollDrehzahl)
    {
      var tempMotor = new MotorSteuerung(jetztMotor);

      for (int i = 0; i < 120; i++)
      {
        tempMotor.Rechne(istDrehzahl < sollDrehzahl ? 1.0 : 0.0);
      }

      double nextIstDrehzahl = tempMotor.upmIst;

      return nextIstDrehzahl < sollDrehzahl ? 100 : 0;
    }

    public double GetGas(double istDrehzahl, double sollDrehzahl)
    {
      double nextGas = NextGas(istDrehzahl, sollDrehzahl);
      jetztMotor.Rechne(nextGas);
      return nextGas;
    }
  }
}
