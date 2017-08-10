using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
  public class TimerExact
  {
    Control _parent;

    #region # public static extern void timeBeginPeriod(int tick); // ermöglicht eine Veringerung der Wartephasen zwischen den Windows-Ticks (eine Erhöhung ist nicht möglich und wird ignoriert)
    /// <summary>
    /// ermöglicht eine Veringerung der Wartephasen zwischen den Windows-Ticks (eine Erhöhung ist nicht möglich und wird ignoriert)
    /// </summary>
    /// <param name="tick">Wartezeit in Millisekunden</param>
    [DllImport("winmm.dll", SetLastError = true)]
    public static extern void timeBeginPeriod(int tick);
    #endregion

    public TimerExact(Control parent)
    {
      _parent = parent;
      timeBeginPeriod(1);
    }

    public static double TickCount
    {
      get
      {
        return (double)Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency * 1000.0;
      }
    }

    bool aktiv = false;

    Thread laufer = null;

    void Arbeite()
    {
      if (_parent != null)
      {
        double soll = TickCount;
        while (aktiv)
        {
          try
          {
            if (_parent.IsDisposed)
            {
              aktiv = false;
              return;
            }
            if (_parent.IsHandleCreated)
            {
              _parent.Invoke(new Action(() => Tick(null, EventArgs.Empty)));
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
          laufer = new Thread(() => { Arbeite(); });
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

    public void Start()
    {
      Enabled = true;
    }

    public void Stop()
    {
      Enabled = false;
    }

    public override string ToString()
    {
      return "Timer.";
    }


  }

}
