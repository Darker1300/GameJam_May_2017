using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float spawnWeight = 1.0f;
    public float pointValue = 1.0f;
    public ItemType itemType = ItemType.Standard;
    public float speedModifier = 0.0f;
    public float speedTime = 1.0f;

    public float moveToPlayerSpeed;

    Vector3 targetLocation;
    static bool sucked = false;

    public enum ItemType
    {
        Standard
    }

    void Start()
    {
        moveToPlayerSpeed = 0.3f;

    }


    void Update()
    {
        if (sucked)
        {
            //this.transform.position = Vector3.MoveTowards(transform.position, targetLocation, moveToPlayerSpeed);

            Vector3 dir = targetLocation - transform.position;
            float distanceThisFrame = moveToPlayerSpeed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame)
            {
                //HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        }


    }

    void OnTriggerStay(Collider other)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (other.gameObject.tag == "Vacuum" + i.ToString())
            {
                targetLocation = other.gameObject.transform.parent.transform.position;
                Vector3 temp = transform.position; // world pos
                this.gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("Player1").transform , true); // *should* not move, but you say...
              //  this.transform.position = temp; // restore world position
                sucked = true;
                Debug.Log("sucked");
                //destroy 
            }
        }

    }
}
