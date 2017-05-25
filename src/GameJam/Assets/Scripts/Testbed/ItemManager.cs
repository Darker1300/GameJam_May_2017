using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header("Spawnable Items")]
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
    }

    void Update()
    {
    }

    public GameObject GetItemModel()
    {
        return items[SampleItem()];
    }

    //	Choose an integer at random, according to the supplied distribution.
    int SampleItem()
    {
        float total = 0;
        foreach (GameObject go in items) { total += go.GetComponent<Item>().spawnWeight; }

        float randVal = total * Random.value;

        for (int i = 0; i < items.Count; i++)
        {
            Item item = items[i].GetComponent<Item>();

            if (randVal < item.spawnWeight)
            {
                return i;
            }

            randVal -= item.spawnWeight;
        }

        return items.Count - 1;
    }

}
