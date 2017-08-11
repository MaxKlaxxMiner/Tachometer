// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// wie PerfectThrottle, jedoch zusätzlich mit Feinjustierung
  /// </summary>
  public sealed class PerfectThrottle2 : IThrottleSimulate
  {
    /// <summary>
    /// Basis-Motor, welcher verwendet wird
    /// </summary>
    readonly EngineSimulator engine = new EngineSimulator(8600.0);

    static double RpmLast(double throttle, int count, EngineSimulator engine)
    {
      throttle *= 0.01;
      var temp = new EngineSimulator(engine);
      for (int i = 0; i < count; i++)
      {
        temp.Rechne(throttle);
      }
      return temp.upmIst;
    }

    double NextCalc(double currentRpm, double targetRpm)
    {
      const int Depth = 150;

      double minThr = 0.0;
      double maxThr = 100.0;

      if (RpmLast(minThr, Depth, engine) > targetRpm) return minThr;
      if (RpmLast(maxThr, Depth, engine) < targetRpm) return maxThr;

      for (int r = 0; r < 32; r++)
      {
        double chkThr = (minThr + maxThr) * 0.5;
        if (RpmLast(chkThr, Depth, engine) > targetRpm)
        {
          maxThr = chkThr;
        }
        else
        {
          minThr = chkThr;
        }
      }

      return (minThr + maxThr) * 0.5;
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
