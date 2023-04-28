using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPlotter : MonoBehaviour
{
    public GameObject SafeFruitPrefab;
    public GameObject PoisonousFruitPrefab;

    private void Start()
    {
        var fruitLoader = FindObjectOfType<FruitLoader>();

        foreach (var item in fruitLoader.Fruit)
        {
            GameObject prefab;
            if (item.Poisonous)
            {
                prefab = PoisonousFruitPrefab;

            }
            else
            {
                prefab = SafeFruitPrefab;
            }

            Instantiate(prefab, new Vector3(item.SpotSize, item.SpikeLength, 0), Quaternion.identity);
        }
    }
}
