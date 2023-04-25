using System;

public class Layer
{
	private int _inputNodesNumber;
	private int _outputNodesNumber;
	private double[,] _weights;
	private double[] _biases;

	public Layer(int inputNodesNumber, int outputNodesNumber)
	{
		_inputNodesNumber = inputNodesNumber;
		_outputNodesNumber = outputNodesNumber;

		_weights = new double[_inputNodesNumber, _outputNodesNumber];
		_biases = new double[_outputNodesNumber];
	}

	public double[] CalculateOutputs(double[] inputs)
	{
		var activations = new double[_outputNodesNumber];

		for (int outputNode = 0; outputNode < _outputNodesNumber; ++outputNode)
		{
			double weightedInput = _biases[outputNode];

			for (int inputNode = 0; inputNode < _inputNodesNumber; inputNode++)
			{
				weightedInput += inputs[inputNode] * _weights[inputNode, outputNode];
			}

			activations[outputNode] = Sigmoid(weightedInput);
		}

		return activations;
	}

    public void SetWeight(int inputNode, int outputNode, double value) => _weights[inputNode, outputNode] = value;

	public void SetBias(int outputNode, double value) => _biases[outputNode] = value;

    private double Sigmoid(double weightedInput)
	{
		return 1 / (1 + Math.Exp(-weightedInput));
	}
}

