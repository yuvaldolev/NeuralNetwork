using System.Collections.Generic;
using UnityEngine;

public class FruitLoader : MonoBehaviour
{
    public TextAsset Data;

    public Fruit[] Fruit;
    public FruitDataPoint[] FruitDataPoints;

    private void Awake()
    {
        Fruit = FruitCsvReader.Read(Data);

        FruitDataPoints = new FruitDataPoint[Fruit.Length];
        makeFruitDataPoints();
    }

    private void makeFruitDataPoints()
    {
        for (int fruitIndex = 0; fruitIndex < Fruit.Length; ++fruitIndex)
        {
            FruitDataPoints[fruitIndex] = new FruitDataPoint(Fruit[fruitIndex]);
        }
    }
}