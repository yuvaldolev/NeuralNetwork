using System;
using System.Linq;
using UnityEngine;

public class NeuralNetwork
{
    private Layer[] _layers;

    public NeuralNetwork(params int[] layerSizes)
    {
        _layers = new Layer[layerSizes.Length - 1];
        for (int layer_index = 0; layer_index < _layers.Length; ++layer_index)
        {
            _layers[layer_index] = new Layer(layerSizes[layer_index], layerSizes[layer_index + 1]);
        }
    }

    public void SetWeight(int layer, int inputNode, int outputNode, double value) => _layers[layer].SetWeight(inputNode, outputNode, value);

    public void SetBias(int layer, int outputNode, double value) => _layers[layer].SetBias(outputNode, value);

    public int Classify(double[] inputs)
    {
        double[] outputs = CalculateOutputs(inputs);
        return Array.IndexOf(outputs, outputs.Max());
    }

    private double[] CalculateOutputs(double[] inputs)
    {
        double[] outputs = inputs;
        foreach (var layer in _layers)
        {
            outputs = layer.CalculateOutputs(outputs);
        }

        return outputs;
    }
}