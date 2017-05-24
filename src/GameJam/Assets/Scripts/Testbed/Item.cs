using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public float spawnRatio = 0.1f;
    public float pointValue = 1.0f;
    public ItemType itemType = ItemType.Standard;
    public float speedModifier = 0.0f;
    public float speedTime = 1.0f;

    public enum ItemType
    {
        Standard
    }
}
