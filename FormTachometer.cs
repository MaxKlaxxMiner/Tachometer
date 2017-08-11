using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace TachometerApplication
{
  public sealed partial class FormTachometer : Form
  {
    //readonly IThrottleSimulate throttle = null; // manuelle Steuerung zum ausprobieren
    //readonly IThrottleSimulate throttle = new MinimalThrottle(); // einfachtes Gaspedal, welches nur vollgas oder gar kein gas gibt (große Sprünge)
    //readonly IThrottleSimulate throttle = new MinimalThrottle2(); // wie MinimalThrottle, jedoch wird die Änderung der letzten Drehzahl mit berücksichtigt (kleinere Sprünge)
    //readonly IThrottleSimulate throttle = new SimpleThrottle(); // einfache Gaspedalsteuerung, welches den Abstand der Soll/Ist Drehzahl berücksichtigt
    //readonly IThrottleSimulate throttle = new SimpleThrottle2(); // wie SimpleThrottle, jedoch wird zusätzlich die Drehzahl mit einbezogen (niedrige Drehzahlen benötigen weniger Gas um gehalten zu werden)
    //readonly IThrottleSimulate throttle = new PredictedThrottle(); // benutzt die Änderungsgeschwindigkeit des Drehzahlmessers um die passende Gaspedal-Stellung zu ermitteln
    //readonly IThrottleSimulate throttle = new TabledThrottle(); // für die Ermittlung der passenden Gaspedalstellung, ein Tabelle mit bekannten Werten benutzt
    //readonly IThrottleSimulate throttle = new PerfectThrottle(); // simuliert einen eigenen Motor um das Verhalten exakt vorhersagen zu können
    readonly IThrottleSimulate throttle = new PerfectThrottle2(); // wie PerfectThrottle, jedoch zusätzlich mit Feinjustierung

    /// <summary>
    /// Multiplikator, wie die Berechnung pro Tick durchgeführt wird
    /// </summary>
    const int MultiRechne = 1;

    /// <summary>
    /// Konstruktor
    /// </summary>
    public FormTachometer()
    {
      CheckForIllegalCrossThreadCalls = false;
      InitializeComponent();
    }

    TimerExact timer1;

    void Form1_Load(object sender, EventArgs e)
    {
      timer1 = new TimerExact(this);
      timer1.Tick += timer1_Tick;
      timer1.Interval = 1000.0 / 60.0;
      timer1.Enabled = true;
    }

    readonly EngineSimulator motor = new EngineSimulator(8600.0);

    void Rechne()
    {
      if (throttle != null)
      {
        gasSoll = throttle.GetThrottle(motor.upmIst, motor.upmSoll) * 0.01;
      }

      motor.Rechne(gasSoll);
    }

    #region # // --- Konstanten / Einstellungen ---
    float kreisX = 300.0f;
    float kreisY = 300.0f;
    float kreisR = 280.0f;

    double radVon = Math.PI * 1.3;
    double radBis = Math.PI * 2.6;

    double gasSoll = 0.0;
    #endregion

    #region # // --- Variablen ---
    Bitmap bild;
    int breite;
    int höhe;
    Graphics g;
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
      Text = "UPM: " + motor.upmIst.ToString("#,##0.00") + ", Gas: " + (gasSoll * 100.0).ToString("0.000") + " %";

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

      double multi = (radBis - radVon) / motor.upmMax;

      int umdrehung = 0;
      Font zahlenFont = new Font(this.Font.FontFamily, 24.0f);

      while (umdrehung <= (int)motor.upmMax / 500 * 500 + 1000)
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

      double uRad = motor.upmIst * multi + radVon;

      g.FillPolygon(0xffffff.ToBrush(), new[] { Kp(uRad, 0.99), Kp(uRad - 1.57, 0.1), Kp(uRad + Math.PI, 0.3), Kp(uRad + 1.57, 0.1) });
      g.FillPolygon(0xff8080.ToBrush(), new[] { Kp(uRad, 0.99 * 0.8), Kp(uRad - 1.57, 0.1 * 0.8), Kp(uRad + Math.PI, 0.3 * 0.8), Kp(uRad + 1.57, 0.1 * 0.8) });

      g.DrawPolygon(0x000000.ToPen(), new[] { Kp(uRad, 0.99), Kp(uRad - 1.57, 0.1), Kp(uRad + Math.PI, 0.3), Kp(uRad + 1.57, 0.1) });
      g.DrawPolygon(0xffe0e0.ToPen(), new[] { Kp(uRad, 0.99 * 0.8), Kp(uRad - 1.57, 0.1 * 0.8), Kp(uRad + Math.PI, 0.3 * 0.8), Kp(uRad + 1.57, 0.1 * 0.8) });

      uRad = motor.upmSoll * multi + radVon;

      g.FillPolygon(0x800000.ToBrush(), new[] { Kp(uRad, 1.05), Kp(uRad + 0.05, 1.08), Kp(uRad, 1.00), Kp(uRad - 0.05, 1.08) });

    }

    long arbeitsTick = (long)TimerExact.TickCount;

    void timer1_Tick(object sender, EventArgs e)
    {
      long neuTick = (long)TimerExact.TickCount;

      while (neuTick > arbeitsTick)
      {
        arbeitsTick++;
        for (int i = 0; i < MultiRechne; i++) Rechne();
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
      motor.upmSoll = double.Parse(listBox1.SelectedItem.ToString());
    }
  }
}
