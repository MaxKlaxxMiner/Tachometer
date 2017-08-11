using System.Linq;

namespace TachometerApplication
{
  public sealed class Latenz
  {
    int pos;
    readonly double[] merker;
    public Latenz(int schritte)
    {
      merker = new double[schritte];
    }
    public double Rechne(double neuWert)
    {
      pos++;
      if (pos == merker.Length) pos = 0;
      double ausgabe = merker[pos];
      merker[pos] = neuWert;
      return ausgabe;
    }
    public Latenz(Latenz latenz)
    {
      pos = latenz.pos;
      merker = latenz.merker.ToArray();
    }
  }
}
