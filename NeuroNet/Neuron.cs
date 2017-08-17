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

    public double Fire(double[] input)
    {
      double sum = 0;
      double len = Math.Min(input.Length, gewicht.Length);

      for (int i = 0; i < len; i++)
      {
        sum += gewicht[i] * input[i];
      }

      return sum >= schwellwert ? 1 : 0;
    }
  }
}
