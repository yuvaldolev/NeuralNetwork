using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class ClassificationVisualizer : MonoBehaviour
{
    public int TextureWidth;
    public int TextureHeight;

    //  Weights 1.
    public Slider Weights1Slider11;
    public Slider Weights1Slider21;

    public Slider Weights1Slider12;
    public Slider Weights1Slider22;

    public Slider Weights1Slider13;
    public Slider Weights1Slider23;

    // Biases 1.
    public Slider Biases1Slider1;
    public Slider Biases1Slider2;
    public Slider Biases1Slider3;

    // Weights 2.
    public Slider Weights2Slider11;
    public Slider Weights2Slider21;
    public Slider Weights2Slider31;

    public Slider Weights2Slider12;
    public Slider Weights2Slider22;
    public Slider Weights2Slider32;

    // Biases 2.
    public Slider Biases2Slider1;
    public Slider Biases2Slider2;

    public GameObject CostMarker;
    public GameObject CorrectMaker;

    private static readonly Color SAFE_COLOR = new Color(122.0f / 255.0f, 182.0f / 255.0f, 248.0f / 255.0f, 0.5f);
    private static readonly Color POISONOUS_COLOR = new Color(229.0f / 255.0f, 101.0f / 255.0f, 102.0f / 255.0f, 0.5f);

    private NeuralNetwork _neuralNetwork;
    private Texture2D _graphTexture;
    private FruitLoader _fruitLoader;

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the Neural Network.
        _neuralNetwork = new NeuralNetwork(2, 3, 2);

        // Create a new texture for visualizing the Neural Network's classifications
        // on the graph.
        _graphTexture = new Texture2D(TextureWidth, TextureHeight);
        GetComponent<MeshRenderer>().material.mainTexture = _graphTexture;

        // Find the FruitLoader.
        _fruitLoader = FindObjectOfType<FruitLoader>();

        // Initialize the Slider listeners.
        InitializeSliderListeners();

        // Visualize the starting point.
        UpdateVisualization();
    }

    private void InitializeSliderListeners()
    {
        // Weights 1.
        Weights1Slider11.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(0, 0, 0, value));
        Weights1Slider21.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(0, 1, 0, value));

        Weights1Slider12.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(0, 0, 1, value));
        Weights1Slider22.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(0, 1, 1, value));

        Weights1Slider13.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(0, 0, 2, value));
        Weights1Slider23.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(0, 1, 2, value));

        // Biases 1.
        Biases1Slider1.onValueChanged.AddListener((float value) => HandleBiasSliderValueChanged(0, 0, value));
        Biases1Slider2.onValueChanged.AddListener((float value) => HandleBiasSliderValueChanged(0, 1, value));
        Biases1Slider3.onValueChanged.AddListener((float value) => HandleBiasSliderValueChanged(0, 2, value));

        // Weights 2.
        Weights2Slider11.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(1, 0, 0, value));
        Weights2Slider21.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(1, 1, 0, value));
        Weights2Slider31.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(1, 2, 0, value));

        Weights2Slider12.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(1, 0, 1, value));
        Weights2Slider22.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(1, 1, 1, value));
        Weights2Slider32.onValueChanged.AddListener((float value) => HandleWeightSliderValueChanged(1, 2, 1, value));

        // Biases 2.
        Biases2Slider1.onValueChanged.AddListener((float value) => HandleBiasSliderValueChanged(1, 0, value));
        Biases2Slider2.onValueChanged.AddListener((float value) => HandleBiasSliderValueChanged(1, 1, value));
    }

    private void UpdateVisualization()
    {
        UpdateGraph();
        UpdateCostMarker();
        UpdateCorrectMarker();
    }

    private void HandleWeightSliderValueChanged(int layer, int inputNode, int outputNode, float value)
    {
        _neuralNetwork.SetWeight(layer, inputNode, outputNode, value);
        Debug.Log("On Weight Slider Value Changed: layer=" + layer + ", inputNode=" + inputNode + ", outputNode=" + outputNode + ", value=" + value);
        UpdateVisualization();
    }

    private void HandleBiasSliderValueChanged(int layer, int outputNode, float value)
    {
        _neuralNetwork.SetBias(layer, outputNode, value);
        Debug.Log("On Bias Slider Value Changed: layer=" + layer + ", outputNode=" + outputNode + ", value=" + value);
        UpdateVisualization();
    }

    private void UpdateGraph()
    {
        for (int y = 0; y < _graphTexture.height; y++)
        {
            for (int x = 0; x < _graphTexture.width; x++)
            {
                VisualizePoint(x, y);
            }
        }

        _graphTexture.Apply();
    }

    private void UpdateCostMarker()
    {
        var cost = _neuralNetwork.Cost(_fruitLoader.FruitDataPoints);
        CostMarker.GetComponent<TextMeshPro>().SetText("Cost: " + cost.ToString("G4"));
    }

    private void UpdateCorrectMarker()
    {
        var correctAmount = 0;
        foreach (var dataPoint in _fruitLoader.FruitDataPoints)
        {
            var predictedClass = _neuralNetwork.Classify(dataPoint.Inputs());
            var classificationOutput = new double[] { (0 == predictedClass) ? 1 : 0, (0 == predictedClass) ? 0 : 1 };

            if (Enumerable.SequenceEqual(classificationOutput, dataPoint.ExpectedOutputs()))
            {
                correctAmount += 1;
            }
        }

        CorrectMaker.GetComponent<TextMeshPro>().SetText("Correct: " + correctAmount + " / " + _fruitLoader.FruitDataPoints.Length);
    }

    private void VisualizePoint(int graphX, int graphY)
    {
        var predictedClass = _neuralNetwork.Classify(new double[] { graphX / 10.0f, graphY / 10.0f });

        Color color;
        if (0 == predictedClass)
        {
            color = SAFE_COLOR;
        }
        else
        {
            color = POISONOUS_COLOR;
        }

        _graphTexture.SetPixel(graphX, graphY, color);
    }
}
