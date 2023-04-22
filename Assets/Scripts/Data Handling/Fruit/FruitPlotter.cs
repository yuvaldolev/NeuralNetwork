using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPlotter : MonoBehaviour
{
    public TextAsset Data;
    public GameObject SafeFruitPrefab;
    public GameObject PoisonousFruitPrefab;

    private List<Fruit> fruit;

    // Start is called before the first frame update
    void Start()
    {
        fruit = CsvReader.Read(Data);

        foreach (var item in fruit)
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

    // Update is called once per frame
    void Update()
    {

    }
}
