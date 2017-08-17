#region # using *.*
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace NeuroNet
{
  public sealed class Neuron
  {
    double[] gewicht;
    double schwellwert;
    List<Neuron> sendTo;

    public Neuron(List<Neuron> sendTo)
    {
      this.sendTo = sendTo;
    }

    public bool Fire(double[] input)
    {
      return false;
    }
  }
}
