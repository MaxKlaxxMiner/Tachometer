// ReSharper disable UnusedMember.Global

namespace TachometerApplication
{
  /// <summary>
  /// für die Ermittlung der passenden Gaspedalstellung, ein Tabelle mit bekannten Werten benutzt
  /// </summary>
  public sealed class TabledThrottle : IThrottleSimulate
  {
    /// <summary>
    /// merkt sich die letzte Umdrehungzahl
    /// </summary>
    double lastRpm;

    /// <summary>
    /// Struktur eines Drehzahl/Gaspedal-Paares
    /// </summary>
    struct RpmPair
    {
      public double rpm;
      public double throttle;
    }

    /// <summary>
    /// liste der bekannten Werte
    /// </summary>
    static readonly RpmPair[] RpmPairs =
    {
      new RpmPair { rpm = 706.0, throttle = 29.55330 },
      new RpmPair { rpm = 750.0, throttle = 30.48780 },
      new RpmPair { rpm = 1000.0, throttle = 35.01840 },
      new RpmPair { rpm = 1250.0, throttle = 38.54300 },
      new RpmPair { rpm = 1500.0, throttle = 41.36330 },
      new RpmPair { rpm = 1750.0, throttle = 43.67110 },
      new RpmPair { rpm = 2000.0, throttle = 45.59450 },
      new RpmPair { rpm = 2250.0, throttle = 47.22220 },
      new RpmPair { rpm = 2500.0, throttle = 48.61745 },
      new RpmPair { rpm = 2750.0, throttle = 49.82680 },
      new RpmPair { rpm = 3000.0, throttle = 50.88505 },
      new RpmPair { rpm = 3250.0, throttle = 51.81885 },
      new RpmPair { rpm = 3500.0, throttle = 52.64895 },
      new RpmPair { rpm = 3750.0, throttle = 53.39170 },
      new RpmPair { rpm = 4000.0, throttle = 54.06020 },
      new RpmPair { rpm = 4250.0, throttle = 54.66505 },
      new RpmPair { rpm = 4500.0, throttle = 55.21495 },
      new RpmPair { rpm = 4750.0, throttle = 55.71704 },
      new RpmPair { rpm = 5000.0, throttle = 56.17730 },
      new RpmPair { rpm = 5250.0, throttle = 56.60075 },
      new RpmPair { rpm = 5500.0, throttle = 56.99164 },
      new RpmPair { rpm = 5750.0, throttle = 57.35357 },
      new RpmPair { rpm = 6000.0, throttle = 57.68967 },
      new RpmPair { rpm = 6250.0, throttle = 58.00259 },
      new RpmPair { rpm = 6500.0, throttle = 58.29466 },
      new RpmPair { rpm = 6750.0, throttle = 58.56789 },
      new RpmPair { rpm = 7000.0, throttle = 58.82404 },
      new RpmPair { rpm = 7250.0, throttle = 59.06468 },
      new RpmPair { rpm = 7500.0, throttle = 59.29116 },
      new RpmPair { rpm = 7750.0, throttle = 59.50470 },
      new RpmPair { rpm = 8000.0, throttle = 59.70638 },
      new RpmPair { rpm = 8250.0, throttle = 59.89717 },
      new RpmPair { rpm = 8500.0, throttle = 60.07791 },
      new RpmPair { rpm = 8617.0, throttle = 60.15927 }
    };

    /// <summary>
    /// Interface-Methode, welche das Gaspedal steuert
    /// </summary>
    /// <param name="currentRpm">aktuelle Drehzahl des Motors</param>
    /// <param name="targetRpm">gewünschte Drezahl des Motors, welche erreicht werden soll</param>
    /// <returns>vorgeschlagene Gaspedal-Stellung in Prozent (0 - 100)</returns>
    public double GetThrottle(double currentRpm, double targetRpm)
    {
      double optiGas = 0.0;
      foreach (var pair in RpmPairs) if (pair.rpm >= targetRpm - 1.0) { optiGas = pair.throttle; break; }

      double bew = currentRpm - lastRpm;
      lastRpm = currentRpm;

      double bonus = 0.0;
      if (currentRpm < 6000.0) bonus += 0.003;
      if (currentRpm < 5000.0) bonus += 0.003;
      if (currentRpm < 4000.0) bonus += 0.003;
      if (currentRpm < 3000.0) bonus += 0.003;
      if (currentRpm < 2000.0) bonus += 0.010;
      if (currentRpm < 1500.0) bonus += 0.010;

      currentRpm += bew * 25.0;

      double diff = targetRpm - currentRpm;

      return optiGas + diff * (0.011 + bonus);
    }
  }
}
