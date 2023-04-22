public class Layer
{
	private int inNodesNumber;
	private int outNodesNumber;
	private double[,] weights;
	private double[] biases;

	public Layer(int inNodesNumber, int outNodesNumber)
	{
		this.inNodesNumber = inNodesNumber;
		this.outNodesNumber = outNodesNumber;

		weights = new double[this.inNodesNumber, this.outNodesNumber];
		biases = new double[this.outNodesNumber];
	}
}

