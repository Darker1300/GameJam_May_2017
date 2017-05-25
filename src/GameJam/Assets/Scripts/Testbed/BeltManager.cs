using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeltManager : MonoBehaviour
{
    public ItemManager itemManager = null;
    public List<GameObject> beltPlates = new List<GameObject>();

    public GameObject BeltPlatePrefab = null;

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

        CreateBelt();
    }

    void FixedUpdate()
    {
        BeltSpeedCurrentOffset = Mathf.SmoothDamp(BeltSpeedCurrentOffset, BeltSpeedTarget, ref BeltSpeedVelocity, BeltAccelerationTime);
        BeltOffset += BeltSpeedCurrentOffset;

        for (int i = 0; i < BeltCapacity; i++)
        {
            if (beltPlates[i] != null)
            {
                Vector3 pos = IndexToPosition(i);
                pos.y = beltPlates[i].transform.localPosition.y;
                beltPlates[i].transform.localPosition = pos;

                Vector3 lookPoint = BeltTopTransform.position;
                lookPoint.y = pos.y;

                beltPlates[i].transform.LookAt(lookPoint);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!BeltTopTransform) BeltTopTransform = this.transform;

        Gizmos.DrawWireSphere(BeltTopTransform.position, BeltRadius);

        for (int i = 0; i < BeltCapacity; i++)
            Gizmos.DrawWireSphere(DegreesToPosition(IndexToDegrees(i)), 0.5f);
    }
    #endregion

    public void CreateBelt()
    {
        for (int i = 0; i < BeltCapacity; i++)
        {
            Vector3 pos = IndexToPosition(i);
            GameObject go = GameObject.Instantiate(BeltPlatePrefab, pos, Quaternion.identity, BeltTopTransform);
            beltPlates.Add(go);
            BeltPlateController bpc = go.GetComponent<BeltPlateController>();
            bpc.beltIndex = i;
        }
    }

    public void DestroyBelt()
    {
        for (int i = 0; i < beltPlates.Count; i++)
        {
            if (beltPlates[i] != null)
            {
                GameObject.Destroy(beltPlates[i]);
                beltPlates[i] = null;
            }
        }
    }

    public void FillBelt()
    {
        for (int i = 0; i < beltPlates.Count; i++)
        {
            beltPlates[i].GetComponent<BeltPlateController>().Restock(1.0f);
        }
    }

    public void ClearBelt()
    {

    }


    public void AddItemToBeltPlate(GameObject _beltPlateObj)
    {
        GameObject model = itemManager.GetItemModel();
        BeltPlateController bpc = _beltPlateObj.GetComponent<BeltPlateController>();

        Vector3 pos = bpc.itemAnchor.position;
        GameObject.Instantiate(model, pos, Quaternion.identity, bpc.itemAnchor);

        //go.transform.LookAt(BeltTopTransform);
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

    public void QueuePlateRestock(GameObject _plateChild)
    {
        BeltPlateController bPlate = _plateChild.GetComponentInParent<BeltPlateController>();
        Animator anim = bPlate.GetComponent<Animator>();
        anim.SetBool("StartLower", true);
    }
}
