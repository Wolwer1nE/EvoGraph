using EvoGraph.Random;

namespace EvoGraphTest.NNTest;

public class NeuralNetwork
{
    private double[] _weights;
    private double[] _neurons;
    private int[] _layers;
    private FuncAct[] _acts;

    public void SetRandom()
    {
        for (int i = 0; i < _weights.Length; i++)
            _weights[i] = Rnd.NextDouble() * 2 - 1;
    }
    
    public NeuralNetwork(int[] layers, string[] acts)
    {
        _layers = layers;
        int neuronCount = layers[0], weightCount = 0;
        for (int i = 1; i < layers.Length; i++)
        {
            neuronCount += layers[i];
            weightCount += layers[i] * layers[i - 1];
        }
        
        _weights = new double[weightCount];
        _neurons = new double[neuronCount];
        
        _acts = new FuncAct[acts.Length];
        for (int i = 0; i < acts.Length; i++) 
            _acts[i] = Activations.FromString(acts[i]);
    }

    public NeuralNetwork Clone()
    {
        var clone = new NeuralNetwork(_layers, new string[_layers.Length - 1]);
        Array.Copy(_weights, clone._weights, _weights.Length);
        clone._neurons = new double[_neurons.Length];
        Array.Copy(_layers, clone._layers, _layers.Length);
        Array.Copy(_acts, clone._acts, _acts.Length);
        return clone;
    }

    public void Clear()
    {
        for (int i = 0; i < _neurons.Length; i++)
            _neurons[i] = 0;
    }

    public string GetWeights()
    {
        return Encoder.Encoder.EncodeArray(_weights);
    }

    public void SetWeights(string str)
    {
        _weights = Encoder.Encoder.DecodeArray(str);
    }

    private void Forward()
    {
        int w = 0, offset = _layers[0], prev = 0;
        for (int i = 1; i < _layers.Length; i++)
        {
            for (int j = 0; j < _layers[i]; j++)
            for (int k = 0; k < _layers[i - 1]; k++)
                _neurons[offset + j] += _neurons[prev + k] * _weights[w++];
            
            _acts[i - 1](_neurons, offset, offset + _layers[i]);
            
            offset += _layers[i];
            prev += _layers[i - 1];
        }
    }

    public double[] Forward(double[] input)
    {
        if (input.Length != _layers[0]) 
            throw new ArgumentException("Incorrect input length");
        
        Clear();
        for (int i = 0; i < _layers[0]; i++)
            _neurons[i] = input[i];

        Forward();
        
        double[] answer = new double[_layers[^1]];
        for (int i = 1; i <= _layers[^1]; i++)
            answer[^i] = _neurons[^i];
        return answer;
    }

    [Test]
    public static void TestForward()
    {
        int[] layers = new int[] { 3, 3, 1 };
        string[] acts = new string[] { "relu", "sigmoid" };
        
        var network = new NeuralNetwork(layers, acts);
        network._weights = new double[] { 1, -2, 3, -4, 5, -6, 7, -8, 9, -10, 11, -12 };
        
        double[] input = new double[] { 1, 2, 3 };
        network.Forward(input);
        
        double[] answer = new double[] { 1, 2, 3, 6, 0, 18, 1 / (1 + Math.Exp(276)) };
        Assert.That(network._neurons, Is.EqualTo(answer));
    }
    
    public NeuralNetwork()
    {
        _layers = [];
        _weights = [];
        _neurons = [];
        _acts = [];
    }
}