// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// einfache Gaspedalsteuerung, welches den Abstand der Soll/Ist Drehzahl berücksichtigt
  /// </summary>
  public sealed class SimpleThrottle : IThrottleSimulate
  {
    /// <summary>
    /// Interface-Methode, welche das Gaspedal steuert
    /// </summary>
    /// <param name="currentRpm">aktuelle Drehzahl des Motors</param>
    /// <param name="targetRpm">gewünschte Drezahl des Motors, welche erreicht werden soll</param>
    /// <returns>vorgeschlagene Gaspedal-Stellung in Prozent (0 - 100)</returns>
    public double GetThrottle(double currentRpm, double targetRpm)
    {
      double diff = targetRpm - currentRpm;

      double th = diff * 0.025 + 55.81;

      return th;
    }
  }
}
