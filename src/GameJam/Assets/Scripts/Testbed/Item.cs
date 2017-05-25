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
    private int scoreOwner;

    Vector3 targetLocation;
    private bool sucked = false;

    public enum ItemType
    {
        Standard
    }

    void Start()
    {
        moveToPlayerSpeed = 7f;

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
                HitTarget();
                return;
            }

            transform.Translate(dir.normalized * distanceThisFrame, Space.World);

            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x * 0.99f,
                gameObject.transform.localScale.y * 0.99f, gameObject.transform.localScale.z * 0.99f);
            

                //gameObject.transform.Translate(0, -100, 0);
                //gameObject.transform.localScale = scrapLocalScale;
                //scrapLifeTimer = scrapLifeTime;
              //  gameObject.SetActive(false);
            
        }


    }

    void OnTriggerEnter(Collider other)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (other.gameObject.tag == "Vacuum" + i.ToString() && other.gameObject.transform.parent.GetComponent<PlayerController>().PlayerVacuum)
            {
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                scoreOwner = i;
                targetLocation = other.gameObject.transform.parent.transform.position;
                Vector3 temp = transform.position; // world pos
                this.gameObject.transform.SetParent(null , true); // *should* not move, but you say...
              //  this.transform.position = temp; // restore world position
                sucked = true;
               // Debug.Log("sucked");
                //destroy 
            }
        }

    }
    void OnTriggerStay(Collider other)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (other.gameObject.tag == "Vacuum" + i.ToString() && !other.gameObject.transform.parent.GetComponent<PlayerController>().PlayerVacuum)
            {
                scoreOwner = i;
                targetLocation = other.gameObject.transform.parent.transform.position;
                Vector3 temp = transform.position; // world pos
                this.gameObject.transform.SetParent(null, true); // *should* not move, but you say...
                                                                 //  this.transform.position = temp; // restore world position
                sucked = true;
                // Debug.Log("sucked");
                //destroy 
            }
        }

    }

    void HitTarget()
    {
        //Debug.Log(scoreOwner);

        GameObject.FindGameObjectWithTag("Player" + scoreOwner).GetComponent<PlayerController>().score++;
        Destroy(this.gameObject);

    }
}
