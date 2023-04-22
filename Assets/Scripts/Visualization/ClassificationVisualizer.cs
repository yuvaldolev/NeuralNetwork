using UnityEngine;
using UnityEngine.UI;

public class ClassificationVisualizer : MonoBehaviour
{
    public Slider Weight11Slider;
    public Slider Weight21Slider;

    public Slider Weight12Slider;
    public Slider Weight22Slider;

    public Slider Bias1Slider;
    public Slider Bias2Slider;

    private static readonly Color SAFE_COLOR = new Color(122.0f / 255.0f, 182.0f / 255.0f, 248.0f / 255.0f, 0.5f);
    private static readonly Color POISONOUS_COLOR = new Color(229.0f / 255.0f, 101.0f / 255.0f, 102.0f / 255.0f, 0.5f);
    private Texture2D graphTexture;
    private NeuralNetwork neuralNetwork;

    // Start is called before the first frame update
    void Start()
    {
        Weight11Slider.onValueChanged.AddListener((float value) => HandleWeight11SliderValueChanged(value));
        Weight21Slider.onValueChanged.AddListener((float value) => HandleWeight21SliderValueChanged(value));

        Weight12Slider.onValueChanged.AddListener((float value) => HandleWeight12SliderValueChanged(value));
        Weight22Slider.onValueChanged.AddListener((float value) => HandleWeight22SliderValueChanged(value));

        Bias1Slider.onValueChanged.AddListener((float value) => HandleBias1SliderValueChanged(value));
        Bias2Slider.onValueChanged.AddListener((float value) => HandleBias2SliderValueChanged(value));

        graphTexture = new Texture2D(1800, 1000);
        GetComponent<MeshRenderer>().material.mainTexture = graphTexture;

        neuralNetwork = FindObjectOfType<NeuralNetwork>();

        VisualizeGraph();
    }

    void HandleWeight11SliderValueChanged(float value)
    {
        neuralNetwork.Weight11 = value;
        Debug.Log("Weight 1 1: " + value);
        VisualizeGraph();
    }

    void HandleWeight21SliderValueChanged(float value)
    {
        neuralNetwork.Weight21 = value;
        Debug.Log("Weight 2 1: " + value);
        VisualizeGraph();
    }

    void HandleWeight12SliderValueChanged(float value)
    {
        neuralNetwork.Weight12 = value;
        Debug.Log("Weight 1 2: " + value);
        VisualizeGraph();
    }

    void HandleWeight22SliderValueChanged(float value)
    {
        neuralNetwork.Weight22 = value;
        Debug.Log("Weight 2 2: " + value);
        VisualizeGraph();
    }

    void HandleBias1SliderValueChanged(float value)
    {
        neuralNetwork.Bias1 = value;
        Debug.Log("Bias 1: " + value);
        VisualizeGraph();
    }

    void HandleBias2SliderValueChanged(float value)
    {
        neuralNetwork.Bias2 = value;
        Debug.Log("Bias 2: " + value);
        VisualizeGraph();
    }

    void VisualizeGraph()
    {
        for (int y = 0; y < graphTexture.height; y++)
        {
            for (int x = 0; x < graphTexture.width; x++)
            {
                VisualizePoint(x, y);
            }
        }

        graphTexture.Apply();
    }

    void VisualizePoint(int graphX, int graphY)
    {
        int predictedClass = neuralNetwork.Classify(graphX / 100.0f, graphY / 100.0f);

        Color color;
        if (0 == predictedClass)
        {
            color = SAFE_COLOR;
        } else
        {
            color = POISONOUS_COLOR;
        }

        graphTexture.SetPixel(graphX, graphY, color);
    }
}
