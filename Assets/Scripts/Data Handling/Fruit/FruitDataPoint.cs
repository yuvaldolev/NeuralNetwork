using System;
public class FruitDataPoint : DataPoint
{
	private double[] _inputs;
	private double[] _expectedOutputs;

	public FruitDataPoint(Fruit fruit)
	{
		_inputs = new double[] { fruit.SpotSize, fruit.SpikeLength };
		_expectedOutputs = new double[] { fruit.Poisonous ? 0 : 1, fruit.Poisonous ? 1 : 0 };
	}

    public double[] Inputs()
    {
        return _inputs;
    }

    public double[] ExpectedOutputs()
    {
        return _expectedOutputs;
    }
}

