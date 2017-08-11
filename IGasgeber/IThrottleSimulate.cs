
namespace TachometerApplication
{
  /// <summary>
  /// Interface für die Steuerung eines Gaspedals
  /// </summary>
  interface IThrottleSimulate
  {
    /// <summary>
    /// Interface-Methode, welche das Gaspedal steuert
    /// </summary>
    /// <param name="currentRpm">aktuelle Drehzahl des Motors</param>
    /// <param name="targetRpm">gewünschte Drezahl des Motors, welche erreicht werden soll</param>
    /// <returns>vorgeschlagene Gaspedal-Stellung in Prozent (0 - 100)</returns>
    double GetThrottle(double currentRpm, double targetRpm);
  }
}
