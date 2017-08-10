
namespace WindowsFormsApplication1
{
  /// <summary>
  /// Interface für die Steuerung eines Gaspedals
  /// </summary>
  interface IGasgeber
  {
    /// <summary>
    /// Gas-Pedal
    /// </summary>
    /// <param name="istDrehzahl">aktuelle Drehzahl des Motors</param>
    /// <param name="sollDrehzahl">ziel-Drehzahl</param>
    /// <returns>Gaspedal in % (0.0 - 100.0)</returns>
    double GetGas(double istDrehzahl, double sollDrehzahl);
  }
}
