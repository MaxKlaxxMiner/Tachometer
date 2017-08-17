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
  public sealed class Netz
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

    public double[] Compute(double[] inputVektor)
    {
      var inputResult = input.Select(x => x.Fire(inputVektor)).ToArray();
      var hiddenResult = hidden.Select(x => x.Fire(inputResult)).ToArray();
      var outputResult = output.Select(x => x.Fire(hiddenResult)).ToArray();

      return outputResult;
    }
  }
}
