using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
 public partial class Form1 : Form
 {
  //IGasgeber basis = null;
  //IGasgeber basis = new TestGas();
  //IGasgeber basis = new TestGas2();
  //IGasgeber basis = new PreGas();
  //IGasgeber basis = new TabGas();
  IGasgeber basis = new PerfektGas();
  //IGasgeber basis = new PerfektGas2();


  public class TestGas : IGasgeber
  {
   public double GetGas(double istDrehzahl, double sollDrehzahl)
   {
    return istDrehzahl < sollDrehzahl ? 100 : 0;
   }
  }

  #region # public class TestGas2 : IGasgeber
  public class TestGas2 : IGasgeber
  {
   double vorher = 0.0;
   public double GetGas(double istDrehzahl, double sollDrehzahl)
   {
    double nextDrehzahl = istDrehzahl + (istDrehzahl - vorher) * 80.0;
    vorher = istDrehzahl;
    if (nextDrehzahl < sollDrehzahl) return 100.0; else return 0.0;
   }
  }
  #endregion

  #region # public class PreGas : IGasgeber
  public class PreGas : IGasgeber
  {
   double vorherIst = 0;
   double g = 50.0;
   public double GetGas(double istDrehzahl, double sollDrehzahl)
   {
    double diff = (sollDrehzahl - istDrehzahl) * 0.1;
    double speed = (istDrehzahl - vorherIst) * 100.0;
    diff -= speed;


    vorherIst = istDrehzahl;
    g += diff * 0.0001;
    if (g < 0.0) g = 0.0;
    if (g > 100.0) g = 100.0;
    return g;
   }
  }
  #endregion

  #region # public class TabGas : IGasgeber
  public class TabGas : IGasgeber
  {
   double vorherDrehzahl = 0.0;

   struct GasFürDrehzahl
   {
    public double drehzahl;
    public double gas;
   }

   #region # GasFürDrehzahl[] gasWerte = new GasFürDrehzahl[]
   GasFürDrehzahl[] gasWerte = new GasFürDrehzahl[]
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
    new GasFürDrehzahl { drehzahl = 8617.0, gas = 60.15927 },
   };
   #endregion

   public double GetGas(double istDrehzahl, double sollDrehzahl)
   {
    double optiGas = 0.0;
    foreach (var satz in gasWerte) if (satz.drehzahl >= sollDrehzahl - 1.0) { optiGas = satz.gas; break; }

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
  #endregion

  public class PerfektGas : IGasgeber
  {
   MotorSteuerung jetztMotor = new MotorSteuerung(8600.0);

   double NextGas(double istDrehzahl, double sollDrehzahl)
   {
    MotorSteuerung tempMotor = new MotorSteuerung(jetztMotor);

    for (int i = 0; i < 120; i++)
    {
     tempMotor.Rechne(istDrehzahl < sollDrehzahl ? 1.0 : 0.0);
    }

    double nextIstDrehzahl = tempMotor.UpmIst;

    return nextIstDrehzahl < sollDrehzahl ? 100 : 0;
   }

   public double GetGas(double istDrehzahl, double sollDrehzahl)
   {
    double nextGas = NextGas(istDrehzahl, sollDrehzahl);
    jetztMotor.Rechne(nextGas);
    return nextGas;
   }
  }

  public class PerfektGas2 : IGasgeber
  {
   MotorSteuerung jetztMotor = new MotorSteuerung(8600.0);

   double NextGas(double istDrehzahl, double sollDrehzahl)
   {
    MotorSteuerung tempMotor = new MotorSteuerung(jetztMotor);

    List<MotorSteuerung> merkerMotor = new List<MotorSteuerung>();

    if (istDrehzahl < sollDrehzahl)
    {
     if (istDrehzahl < sollDrehzahl - 2000.0) return 100;
     for (int i = 0; i < 1000; i++)
     {
      merkerMotor.Add(tempMotor);
      tempMotor.Rechne(1.0);
      if (tempMotor.UpmIst > sollDrehzahl)
      {

       return 0;
       //int stop = 0;
      }
     }
    }
    else
    {
     if (istDrehzahl > sollDrehzahl + 2000.0) return 0;
    }


    return istDrehzahl < sollDrehzahl ? 100 : 0;
   }

   public double GetGas(double istDrehzahl, double sollDrehzahl)
   {
    double nextGas = NextGas(istDrehzahl, sollDrehzahl);
    jetztMotor.Rechne(nextGas);
    return nextGas;
   }
  }

  int multiRechne = 1;

  public Form1()
  {
   CheckForIllegalCrossThreadCalls = false;
   InitializeComponent();
  }

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

  TimerExact timer1;

  void Form1_Load(object sender, EventArgs e)
  {
   timer1 = new WindowsFormsApplication1.TimerExact(this);
   timer1.Tick += new System.EventHandler(this.timer1_Tick);
   timer1.Interval = 1000.0 / 60.0;
   timer1.Enabled = true;
  }

  public class Latenz
  {
   int pos = 0;
   double[] merker;
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
    this.pos = latenz.pos;
    this.merker = latenz.merker.ToArray();
   }
  }

  public class MotorSteuerung
  {
   #region # // --- Variablen und Properties ---
   Latenz latenzMotor = new Latenz(20);
   Latenz latenzSteuerung = new Latenz(80);

   double upmMax = 8600.0;
   public double UpmMax
   {
    get
    {
     return upmMax;
    }
    set
    {
     upmMax = value;
    }
   }
   double upmIst = 8000.0;
   public double UpmIst
   {
    get
    {
     return upmIst;
    }
    set
    {
     upmIst = value;
    }
   }
   double upmSoll = 4550.0;
   public double UpmSoll
   {
    get
    {
     return upmSoll;
    }
    set
    {
     upmSoll = value;
    }
   }

   double gas = 0.0;
   double gasMuss = 0.0;
   #endregion

   #region # // --- Konstruktor ---
   /// <summary>
   /// Konstruktor
   /// </summary>
   /// <param name="upmMax">Maximal erlaubte Drehzahl des Motors</param>
   public MotorSteuerung(double upmMax)
   {
    this.upmMax = upmMax;
    this.upmIst = 1000.0;
    this.upmSoll = (upmMax - upmIst) * 0.5 + upmIst;
   }

   /// <summary>
   /// erstellt eine Kopie einer vorhandenen Motor-Steuerung
   /// </summary>
   /// <param name="motor">ursprünglicher Motor</param>
   public MotorSteuerung(MotorSteuerung motor)
   {
    this.gas = motor.gas;
    this.gasMuss = motor.gasMuss;
    this.latenzMotor = new Latenz(motor.latenzMotor);
    this.latenzSteuerung = new Latenz(motor.latenzSteuerung);
    this.upmIst = motor.upmIst;
    this.upmMax = motor.upmMax;
    this.upmSoll = motor.upmSoll;
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

  MotorSteuerung motor = new MotorSteuerung(8600.0);

  void Rechne()
  {
   if (basis != null)
   {
    gasSoll = basis.GetGas(motor.UpmIst, motor.UpmSoll) * 0.01;
   }

   motor.Rechne(gasSoll);
  }

  #region # // --- Konstanten / Einstellungen ---
  float kreisX = 150.0f;
  float kreisY = 150.0f;
  float kreisR = 140.0f;

  double radVon = Math.PI * 1.3;
  double radBis = Math.PI * 2.6;

  double gasSoll = 0.0;
  #endregion

  #region # // --- Variablen ---
  Bitmap bild = null;
  int breite = 0;
  int höhe = 0;
  Graphics g = null;

  #endregion

  #region # // --- Methoden für die einfachere Bedienung ---
  PointF Kp(double rad, double abstand)
  {
   return new PointF(
               (float)Math.Sin(rad) * kreisR * (float)abstand + kreisX, 
               (float)-Math.Cos(rad) * kreisR * (float)abstand + kreisY
              );
  }

  PointF Kp(double rad, double abstand, double offsetX, double offsetY)
  {
   return new PointF(
               (float)Math.Sin(rad) * kreisR * (float)abstand + kreisX + (float)offsetX,
               (float)-Math.Cos(rad) * kreisR * (float)abstand + kreisY + (float)offsetY
              );
  }
  #endregion

  void Zeichne()
  {
   Text = "UPM: " + motor.UpmIst.ToString("#,##0.00") + ", Gas: " + (gasSoll * 100.0).ToString("0.000") + " %";

   if (bild == null)
   {
    bild = new Bitmap(pictureBox1.Width, pictureBox1.Height, PixelFormat.Format32bppRgb);
    pictureBox1.Image = bild;
    breite = bild.Width;
    höhe = bild.Height;
    g = Graphics.FromImage(bild);
    g.SmoothingMode = SmoothingMode.AntiAlias;
   }
   else pictureBox1.Refresh();

   g.FillRectangle(0xa0c0e0.ToBrush(), 0, 0, breite, höhe);

   g.FillEllipse(0xf0e0d0.ToBrush(), kreisX - kreisR, kreisY - kreisR, kreisR * 2.0f, kreisR * 2.0f);
   g.DrawEllipse(0x000000.ToPen(), kreisX - kreisR, kreisY - kreisR, kreisR * 2.0f, kreisR * 2.0f);

   double multi = (radBis - radVon) / motor.UpmMax;

   int umdrehung = 0;
   Font zahlenFont = new Font(this.Font.FontFamily, 16.0f);

   while (umdrehung <= (int)motor.UpmMax / 500 * 500 + 1000)
   {
    double rad = (double)umdrehung * multi + radVon;
    g.DrawLine(0x000000.ToPen(), Kp(rad, 0.98 - (umdrehung % 1000 == 0 ? 0.03 : 0.0) - (umdrehung % 500 == 0 ? 0.02 : 0.0)), Kp(rad, 1.0));
    if (umdrehung % 1000 == 0)
    {
     SizeF zahlenSize = g.MeasureString((umdrehung / 1000).ToString(), zahlenFont);
     g.DrawString((umdrehung / 1000).ToString(), zahlenFont, 0x000000.ToBrush(), Kp(rad, 0.85, -zahlenSize.Width / 1.8, -zahlenSize.Height / 2.5));
    }
    umdrehung += 250;
   }

   double uRad = motor.UpmIst * multi + radVon;

   g.FillPolygon(0xffffff.ToBrush(), new[] { Kp(uRad, 0.99), Kp(uRad - 1.57, 0.1), Kp(uRad + Math.PI, 0.3), Kp(uRad + 1.57, 0.1) });
   g.FillPolygon(0xff8080.ToBrush(), new[] { Kp(uRad, 0.99 * 0.8), Kp(uRad - 1.57, 0.1 * 0.8), Kp(uRad + Math.PI, 0.3 * 0.8), Kp(uRad + 1.57, 0.1 * 0.8) });

   g.DrawPolygon(0x000000.ToPen(), new[] { Kp(uRad, 0.99), Kp(uRad - 1.57, 0.1), Kp(uRad + Math.PI, 0.3), Kp(uRad + 1.57, 0.1) });
   g.DrawPolygon(0xffe0e0.ToPen(), new[] { Kp(uRad, 0.99 * 0.8), Kp(uRad - 1.57, 0.1 * 0.8), Kp(uRad + Math.PI, 0.3 * 0.8), Kp(uRad + 1.57, 0.1 * 0.8) });

   uRad = motor.UpmSoll * multi + radVon;

   g.FillPolygon(0x800000.ToBrush(), new[] { Kp(uRad, 1.05), Kp(uRad + 0.05, 1.08), Kp(uRad, 1.00),Kp(uRad - 0.05, 1.08) });

  }

  long arbeitsTick = (long)TimerExact.TickCount;

  void timer1_Tick(object sender, EventArgs e)
  {
   long neuTick = (long)TimerExact.TickCount;

   while (neuTick > arbeitsTick)
   {
    arbeitsTick++;
    for (int i = 0; i < multiRechne; i++) Rechne();
   }

   Zeichne();
  }

  private void button1_MouseDown(object sender, MouseEventArgs e)
  {
   gasSoll = 1.0;
  }

  private void button1_MouseUp(object sender, MouseEventArgs e)
  {
   gasSoll = 0.0;
  }

  private void trackBar1_Scroll(object sender, EventArgs e)
  {
   gasSoll = trackBar1.Value * 0.01;
  }

  private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
  {
   motor.UpmSoll = double.Parse(listBox1.SelectedItem.ToString());
  }
 }
}
