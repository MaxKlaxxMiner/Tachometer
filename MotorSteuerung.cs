
namespace WindowsFormsApplication1
{
  public sealed class MotorSteuerung
  {
    #region # // --- Variablen und Properties ---
    readonly Latenz latenzMotor = new Latenz(20);
    readonly Latenz latenzSteuerung = new Latenz(80);

    public readonly double upmMax;
    public double upmIst;
    public double upmSoll;

    double gas;
    double gasMuss;
    #endregion

    #region # // --- Konstruktor ---
    /// <summary>
    /// Konstruktor
    /// </summary>
    /// <param name="upmMax">Maximal erlaubte Drehzahl des Motors</param>
    public MotorSteuerung(double upmMax)
    {
      this.upmMax = upmMax;
      upmIst = 1000.0;
      upmSoll = (upmMax - upmIst) * 0.5 + upmIst;
    }

    /// <summary>
    /// erstellt eine Kopie einer vorhandenen Motor-Steuerung
    /// </summary>
    /// <param name="motor">ursprünglicher Motor</param>
    public MotorSteuerung(MotorSteuerung motor)
    {
      gas = motor.gas;
      gasMuss = motor.gasMuss;
      latenzMotor = new Latenz(motor.latenzMotor);
      latenzSteuerung = new Latenz(motor.latenzSteuerung);
      upmIst = motor.upmIst;
      upmMax = motor.upmMax;
      upmSoll = motor.upmSoll;
    }
    #endregion

    #region # public void Rechne(double gasSoll) // Hauptmethode zum berechnen eines neuen Schrittes
    /// <summary>
    /// Hauptmethode zum berechnen eines neuen Schrittes
    /// </summary>
    /// <param name="gasSoll">Gas-Wert, welcher erwünscht ist (0.0 bis 1.0)</param>
    public void Rechne(double gasSoll)
    {
      upmIst *= 0.999;
      upmIst -= 0.1;
      upmIst -= upmIst * 0.001;
      if (upmIst < 0.0) upmIst = 0.0;

      if (gasSoll > 1.0) gasSoll = 1.0;
      if (gasSoll < 0.0) gasSoll = 0.0;

      gasMuss = (gasMuss * 49.0 + gasSoll) * 0.02;

      double gasJetzt = gasMuss;

      if (upmIst < 1000.0 && gasJetzt < (1000.0 - upmIst) * 0.001) gasJetzt = (1000.0 - upmIst) * 0.001;

      gasJetzt = latenzSteuerung.Rechne(gasJetzt);

      if (upmIst > upmMax) gasJetzt = 0.0;

      gas = (gas * 9.0 + latenzMotor.Rechne(gasJetzt)) * 0.1;

      upmIst *= 1.0 + 0.003 * gas;
      upmIst += 3.0 * gas;
    }
    #endregion

    #region # public override string ToString() // gibt den Motor-Status als lesbaren String aus
    /// <summary>
    /// gibt den Motor-Status als lesbaren String aus
    /// </summary>
    /// <returns>lesbare Zeichenkette</returns>
    public override string ToString()
    {
      return upmIst.ToString("#,##0.00") + " " + gasMuss.ToString("0.00") + " / " + gas.ToString("0.00");
    }
    #endregion
  }
}
