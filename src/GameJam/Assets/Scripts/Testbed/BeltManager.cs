using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeltManager : MonoBehaviour
{
    public ItemManager itemManager = null;
    public List<GameObject> currentItems = new List<GameObject>();

    [Header("Transform")]
    public Transform BeltTopTransform = null;
    public float BeltRadius = 8.5f;
    public int BeltCapacity = 10;
    public float BeltOffset = 0.0f;

    [Header("Speed Control")]
    public float BeltSpeedTarget = 0.5f;
    public float BeltAccelerationTime = 1.0f;
    public float BeltSpeedCurrentOffset = 0.0f;
    float BeltSpeedVelocity = 0.0f;

    #region Events
    void Start()
    {
        if (!BeltTopTransform) BeltTopTransform = this.transform;

       // FillBelt();
    }

    void Update()
    {
        BeltSpeedCurrentOffset = Mathf.SmoothDamp(BeltSpeedCurrentOffset, BeltSpeedTarget, ref BeltSpeedVelocity, BeltAccelerationTime);
    }

    void FixedUpdate()
    {
        BeltOffset += BeltSpeedCurrentOffset;

        for (int i = 0; i < BeltCapacity; i++)
        {
            if (currentItems[i] != null)
            {
                currentItems[i].transform.localPosition = IndexToPosition(i);
                currentItems[i].transform.LookAt(BeltTopTransform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!BeltTopTransform) BeltTopTransform = this.transform;

        Gizmos.DrawWireSphere(BeltTopTransform.position, BeltRadius);

        for (int i = 0; i < BeltCapacity; i++)
            Gizmos.DrawWireSphere(DegreesToPosition(IndexToDegrees(i)), 0.5f);

      //  ClearBelt();
    }
    #endregion

    public void FillBelt()
    {
        for (int i = 0; i < BeltCapacity; i++)
        {
            GameObject model = itemManager.GetItemModel();
            Vector3 pos = IndexToPosition(i);
            GameObject go = GameObject.Instantiate(model, pos, Quaternion.identity, BeltTopTransform);
            go.transform.LookAt(BeltTopTransform);
            currentItems.Add(go);
        }
    }

    public void ClearBelt()
    {
        //TODO
        for (int i = 0; i < currentItems.Count; i++)
        {
            if (currentItems[i] != null)
            {
                GameObject.Destroy(currentItems[i]);
                currentItems[i] = null;
            }
        }
    }

    void AddItem()
    {

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

    Vector3 IndexToPosition(int i)
    {
        return DegreesToPosition(IndexToDegrees(i));
    }
}
