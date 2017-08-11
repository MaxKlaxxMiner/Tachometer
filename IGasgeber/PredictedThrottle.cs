// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// benutzt die Änderungsgeschwindigkeit des Drehzahlmessers um die passende Gaspedal-Stellung zu ermitteln
  /// </summary>
  public sealed class PredictedThrottle : IThrottleSimulate
  {
    /// <summary>
    /// merkt sich die letzte Umdrehungzahl
    /// </summary>
    double lastRpm;

    /// <summary>
    /// aktuelle Gaspedal-Stellung
    /// </summary>
    double currentThrottle = 50;

    /// <summary>
    /// Interface-Methode, welche das Gaspedal steuert
    /// </summary>
    /// <param name="currentRpm">aktuelle Drehzahl des Motors</param>
    /// <param name="targetRpm">gewünschte Drezahl des Motors, welche erreicht werden soll</param>
    /// <returns>vorgeschlagene Gaspedal-Stellung in Prozent (0 - 100)</returns>
    public double GetThrottle(double currentRpm, double targetRpm)
    {
      double diff = (targetRpm - currentRpm) * 0.2;
      double speed = (currentRpm - lastRpm) * 150.0;
      diff -= speed;

      lastRpm = currentRpm;
      currentThrottle += diff * 0.0001;
      if (currentThrottle < 0.0) currentThrottle = 0.0;
      if (currentThrottle > 100.0) currentThrottle = 100.0;

      return currentThrottle;
    }
  }
}
