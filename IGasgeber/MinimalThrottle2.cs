// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// wie MinimalThrottle, jedoch wird die Änderung der letzten Drehzahl mit berücksichtigt (kleinere Sprünge)
  /// </summary>
  public sealed class MinimalThrottle2 : IThrottleSimulate
  {
    /// <summary>
    /// merkt sich die letzte Umdrehungzahl
    /// </summary>
    double lastRpm;

    /// <summary>
    /// Interface-Methode, welche das Gaspedal steuert
    /// </summary>
    /// <param name="currentRpm">aktuelle Drehzahl des Motors</param>
    /// <param name="targetRpm">gewünschte Drezahl des Motors, welche erreicht werden soll</param>
    /// <returns>vorgeschlagene Gaspedal-Stellung in Prozent (0 - 100)</returns>
    public double GetThrottle(double currentRpm, double targetRpm)
    {
      double nextRpm = currentRpm + (currentRpm - lastRpm) * 150.0;

      lastRpm = currentRpm;

      return nextRpm < targetRpm ? 100.0 : 0.0;
    }
  }
}
