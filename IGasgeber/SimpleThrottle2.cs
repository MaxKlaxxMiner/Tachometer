// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// wie SimpleThrottle, jedoch wird zusätzlich die Drehzahl mit einbezogen (niedrige Drehzahlen benötigen weniger Gas um gehalten zu werden)
  /// </summary>
  public sealed class SimpleThrottle2 : IThrottleSimulate
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

      double th = diff * 0.025 + 47.17 + currentRpm * 0.0018;

      return th;
    }
  }
}
