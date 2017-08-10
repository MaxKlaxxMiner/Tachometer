// ReSharper disable UnusedMember.Global

namespace WindowsFormsApplication1
{
  /// <summary>
  /// 
  /// </summary>
  public class TabGas : IGasgeber
  {
    double vorherDrehzahl;

    struct GasFürDrehzahl
    {
      public double drehzahl;
      public double gas;
    }

    #region # GasFürDrehzahl[] gasWerte = new GasFürDrehzahl[]

    static readonly GasFürDrehzahl[] GasWerte =
    {
      new GasFürDrehzahl { drehzahl = 706.0, gas = 29.55330 },
      new GasFürDrehzahl { drehzahl = 750.0, gas = 30.48780 },
      new GasFürDrehzahl { drehzahl = 1000.0, gas = 35.01840 },
      new GasFürDrehzahl { drehzahl = 1250.0, gas = 38.54300 },
      new GasFürDrehzahl { drehzahl = 1500.0, gas = 41.36330 },
      new GasFürDrehzahl { drehzahl = 1750.0, gas = 43.67110 },
      new GasFürDrehzahl { drehzahl = 2000.0, gas = 45.59450 },
      new GasFürDrehzahl { drehzahl = 2250.0, gas = 47.22220 },
      new GasFürDrehzahl { drehzahl = 2500.0, gas = 48.61745 },
      new GasFürDrehzahl { drehzahl = 2750.0, gas = 49.82680 },
      new GasFürDrehzahl { drehzahl = 3000.0, gas = 50.88505 },
      new GasFürDrehzahl { drehzahl = 3250.0, gas = 51.81885 },
      new GasFürDrehzahl { drehzahl = 3500.0, gas = 52.64895 },
      new GasFürDrehzahl { drehzahl = 3750.0, gas = 53.39170 },
      new GasFürDrehzahl { drehzahl = 4000.0, gas = 54.06020 },
      new GasFürDrehzahl { drehzahl = 4250.0, gas = 54.66505 },
      new GasFürDrehzahl { drehzahl = 4500.0, gas = 55.21495 },
      new GasFürDrehzahl { drehzahl = 4750.0, gas = 55.71704 },
      new GasFürDrehzahl { drehzahl = 5000.0, gas = 56.17730 },
      new GasFürDrehzahl { drehzahl = 5250.0, gas = 56.60075 },
      new GasFürDrehzahl { drehzahl = 5500.0, gas = 56.99164 },
      new GasFürDrehzahl { drehzahl = 5750.0, gas = 57.35357 },
      new GasFürDrehzahl { drehzahl = 6000.0, gas = 57.68967 },
      new GasFürDrehzahl { drehzahl = 6250.0, gas = 58.00259 },
      new GasFürDrehzahl { drehzahl = 6500.0, gas = 58.29466 },
      new GasFürDrehzahl { drehzahl = 6750.0, gas = 58.56789 },
      new GasFürDrehzahl { drehzahl = 7000.0, gas = 58.82404 },
      new GasFürDrehzahl { drehzahl = 7250.0, gas = 59.06468 },
      new GasFürDrehzahl { drehzahl = 7500.0, gas = 59.29116 },
      new GasFürDrehzahl { drehzahl = 7750.0, gas = 59.50470 },
      new GasFürDrehzahl { drehzahl = 8000.0, gas = 59.70638 },
      new GasFürDrehzahl { drehzahl = 8250.0, gas = 59.89717 },
      new GasFürDrehzahl { drehzahl = 8500.0, gas = 60.07791 },
      new GasFürDrehzahl { drehzahl = 8617.0, gas = 60.15927 }
    };
    #endregion

    public double GetGas(double istDrehzahl, double sollDrehzahl)
    {
      double optiGas = 0.0;
      foreach (var satz in GasWerte) if (satz.drehzahl >= sollDrehzahl - 1.0) { optiGas = satz.gas; break; }

      double bew = istDrehzahl - vorherDrehzahl;
      vorherDrehzahl = istDrehzahl;

      double bonus = 0.0;
      if (istDrehzahl < 6000.0) bonus += 0.003;
      if (istDrehzahl < 5000.0) bonus += 0.003;
      if (istDrehzahl < 4000.0) bonus += 0.003;
      if (istDrehzahl < 3000.0) bonus += 0.003;
      if (istDrehzahl < 2000.0) bonus += 0.010;
      if (istDrehzahl < 1500.0) bonus += 0.010;

      istDrehzahl += bew * 25.0;

      double diff = sollDrehzahl - istDrehzahl;

      return optiGas + diff * (0.011 + bonus);
    }
  }
}
