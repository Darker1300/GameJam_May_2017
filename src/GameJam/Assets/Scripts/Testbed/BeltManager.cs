using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeltManager : MonoBehaviour
{
    [Header("Transform")]
    public Transform BeltTopTransform = null;
    public float BeltRadius = 8.5f;
    public int BeltCapacity = 10;
    public float BeltOffset = 0.0f;

    void Start()
    {
        if (!BeltTopTransform) BeltTopTransform = this.transform;
    }

    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {
        if (!BeltTopTransform) BeltTopTransform = this.transform;
        Gizmos.DrawWireSphere(BeltTopTransform.position, BeltRadius);
        float interval = 360.0f / BeltCapacity;
        for (int i = 0; i < BeltCapacity; i++)
            Gizmos.DrawWireSphere(DegreesToPosition(IndexToDegrees(i)), 0.5f);
    }

    float IndexToDegrees(int i)
    {
        return (360.0f / BeltCapacity * i) + BeltOffset;
    }

    Vector3 DegreesToPosition(float _degrees)
    {
        Vector3 pos = BeltTopTransform.position;
        pos.x += BeltRadius * Mathf.Sin(_degrees * Mathf.Deg2Rad);
        pos.z += BeltRadius * Mathf.Cos(_degrees * Mathf.Deg2Rad);
        return pos;
    }
}
