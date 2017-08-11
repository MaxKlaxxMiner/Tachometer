using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
// ReSharper disable UnusedMember.Global
// ReSharper disable MemberCanBePrivate.Global

namespace TachometerApplication
{
  public sealed class TimerExact
  {
    readonly Control parent;

    #region # public static extern void timeBeginPeriod(int tick); // ermöglicht eine Veringerung der Wartephasen zwischen den Windows-Ticks (eine Erhöhung ist nicht möglich und wird ignoriert)
    /// <summary>
    /// ermöglicht eine Veringerung der Wartephasen zwischen den Windows-Ticks (eine Erhöhung ist nicht möglich und wird ignoriert)
    /// </summary>
    /// <param name="tick">Wartezeit in Millisekunden</param>
    [DllImport("winmm.dll", SetLastError = true)]
    static extern void timeBeginPeriod(int tick);
    #endregion

    public TimerExact(Control parent)
    {
      this.parent = parent;
      timeBeginPeriod(1);
    }

    public static double TickCount
    {
      get
      {
        return Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency * 1000.0;
      }
    }

    bool aktiv;

    Thread laufer;

    void Arbeite()
    {
      if (parent != null)
      {
        double soll = TickCount;
        while (aktiv)
        {
          try
          {
            if (parent.IsDisposed)
            {
              aktiv = false;
              return;
            }
            if (parent.IsHandleCreated)
            {
              parent.Invoke(new Action(() => Tick(null, EventArgs.Empty)));
              //Tick(null, EventArgs.Empty);
            }
          }
          catch { }

          soll += Interval;
          double ist = TickCount;
          while (soll > ist)
          {
            Thread.Sleep(0);
            ist = TickCount;
          }

          if (soll < ist - Interval * 3.0)
          {
            soll = ist + Interval * 3.0;
          }

        }
      }
    }

    [DefaultValue(false)]
    public bool Enabled
    {
      get
      {
        return aktiv;
      }
      set
      {
        if (value == aktiv) return;
        aktiv = value;
        if (value)
        {
          laufer = new Thread(Arbeite);
          laufer.Start();
        }
      }
    }

    [DefaultValue(16.666)]
    public double Interval { get; set; }

    public event EventHandler Tick;

    ~TimerExact()
    {
      aktiv = false;
    }

    public override string ToString()
    {
      return "Timer.";
    }
  }
}
