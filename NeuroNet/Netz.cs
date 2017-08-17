#region # using *.*
// ReSharper disable RedundantUsingDirective
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable CollectionNeverQueried.Local
#endregion

namespace NeuroNet
{
  public class Netz
  {
    readonly List<Neuron> input = new List<Neuron>();
    readonly List<Neuron> hidden = new List<Neuron>();
    readonly List<Neuron> output = new List<Neuron>();

    public Netz(int inputSize, int hiddenSize, int outputSize)
    {
      input.AddRange(Enumerable.Range(0, inputSize).Select(x => new Neuron(hidden)));
      hidden.AddRange(Enumerable.Range(0, hiddenSize).Select(x => new Neuron(output)));
      output.AddRange(Enumerable.Range(0, outputSize).Select(x => new Neuron(null)));
    }
  }
}
