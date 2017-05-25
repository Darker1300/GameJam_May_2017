using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPlateController : MonoBehaviour
{
    public int beltIndex = -1;
    public Transform itemAnchor = null;

    public float cooldown = 1;

    void Start()
    {
        itemAnchor = transform.Find("Plate/ItemAnchor");
        GetComponent<Animator>().SetBool("StartLower", true);
    }

    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if (cooldown < 0)
            {
                cooldown = 0;
                // Request Item
                GameObject go = GameObject.FindGameObjectWithTag("BeltManager");
                BeltManager bm = go.GetComponent<BeltManager>();
                bm.AddItemToBeltPlate(gameObject);
                GetComponent<Animator>().SetBool("StartLower", false);
                GetComponent<Animator>().SetBool("StartRaise", true);
            }
        }
    }

}
