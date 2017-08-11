// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// einfachtes Gaspedal, welches nur vollgas oder gar kein gas gibt (große Sprünge)
  /// </summary>
  public sealed class MinimalThrottle : IThrottleSimulate
  {
    /// <summary>
    /// Interface-Methode, welche das Gaspedal steuert
    /// </summary>
    /// <param name="currentRpm">aktuelle Drehzahl des Motors</param>
    /// <param name="targetRpm">gewünschte Drezahl des Motors, welche erreicht werden soll</param>
    /// <returns>vorgeschlagene Gaspedal-Stellung in Prozent (0 - 100)</returns>
    public double GetThrottle(double currentRpm, double targetRpm)
    {
      return currentRpm < targetRpm ? 100 : 0;
    }
  }
}
