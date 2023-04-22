using UnityEngine;

public class NeuralNetwork : MonoBehaviour
{
	// Weight values connecting each input to the first output.
	public float Weight11;
	public float Weight21;

	// Weight values connecting each input to the second output.
	public float Weight12;
	public float Weight22;

	// Bias values;
	public float Bias1;
	public float Bias2;

	void Awake()
	{
		Weight11 = 0.0f;
		Weight21 = 0.0f;

		Weight12 = 0.0f;
		Weight22 = 0.0f ;

        Bias1 = 0;
        Bias2 = 0;
	}

	public int Classify(float input1, float input2)
	{
		double output1 = (Weight11 * input1) + (Weight21 * input2) + Bias1;
		double output2 = (Weight12 * input1) + (Weight22 * input2) + Bias2;

		return (output1 > output2) ? 0 : 1;
    }
}