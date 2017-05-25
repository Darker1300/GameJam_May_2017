using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPlateController : MonoBehaviour
{
    public int beltIndex = -1;
    public Transform itemAnchor = null;

    public float cooldown = -1.0f;

    public bool HasItem { get { return itemAnchor.childCount != 0; } }
    public bool isLowered = false;

    BeltManager beltManager = null;
    Animator animator = null;

    void Start()
    {
        itemAnchor = transform.Find("Plate/ItemAnchor");
        beltManager = GameObject.FindGameObjectWithTag("BeltManager").GetComponent<BeltManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isLowered)
        {
            if (cooldown <= 0)
            {
                cooldown = -1;
                isLowered = false;
                // Request Item
                if (!HasItem)
                    beltManager.AddItemToBeltPlate(gameObject);
                animator.SetBool("StartLower", false);
                animator.SetBool("StartRaise", true);
            }
            else
            {
                cooldown -= Time.deltaTime;
            }
        }
    }

    public void Restock(float _delay = 0.0f)
    {
        (animator == null ? GetComponent<Animator>() : animator).SetBool("StartLower", true);
        cooldown = _delay;
    }

}
