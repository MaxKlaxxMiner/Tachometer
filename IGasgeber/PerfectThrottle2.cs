// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// simuliert einen eigenen Motor um das Verhalten exakt vorhersagen zu können
  /// </summary>
  public sealed class PerfectThrottle2 : IThrottleSimulate
  {
    /// <summary>
    /// Basis-Motor, welcher verwendet wird
    /// </summary>
    readonly EngineSimulator engine = new EngineSimulator(8600.0);

    double NextCalc(double currentRpm, double targetRpm)
    {
      var temp = new EngineSimulator(engine);

      for (int i = 0; i < 120; i++)
      {
        temp.Rechne(currentRpm < targetRpm ? 1.0 : 0.0);
      }

      double nextIstDrehzahl = temp.upmIst;

      return nextIstDrehzahl < targetRpm ? 100 : 0;
    }

    /// <summary>
    /// Interface-Methode, welche das Gaspedal steuert
    /// </summary>
    /// <param name="currentRpm">aktuelle Drehzahl des Motors</param>
    /// <param name="targetRpm">gewünschte Drezahl des Motors, welche erreicht werden soll</param>
    /// <returns>vorgeschlagene Gaspedal-Stellung in Prozent (0 - 100)</returns>
    public double GetThrottle(double currentRpm, double targetRpm)
    {
      double nextThrottle = NextCalc(currentRpm, targetRpm);

      engine.Rechne(nextThrottle * 0.01);

      return nextThrottle;
    }
  }
}
