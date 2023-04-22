public class Layer
{
	private int inputNodesNumber;
	private int outputNodesNumber;
	private double[,] weights;
	private double[] biases;

	public Layer(int inputNodesNumber, int outputNodesNumber)
	{
		this.inputNodesNumber = inputNodesNumber;
		this.outputNodesNumber = outputNodesNumber;

		weights = new double[this.inputNodesNumber, this.outputNodesNumber];
		biases = new double[this.outputNodesNumber];
	}

	public double[] CalculateOutputs(double[] inputs)
	{
		var weightedInputs = new double[outputNodesNumber];

		for (int outputNode = 0; outputNode < outputNodesNumber; ++outputNode)
		{
			double weightedInput = biases[outputNode];

			for (int inputNode = 0; inputNode < inputNodesNumber; inputNode++)
			{
				weightedInput += inputs[inputNode] * weights[inputNode, outputNode];
			}

			weightedInputs[outputNode] = weightedInput;
		}

		return weightedInputs;
	}
}

