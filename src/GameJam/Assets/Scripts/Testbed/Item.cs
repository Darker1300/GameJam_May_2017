﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private AudioManager audioManager;

    public float spawnWeight = 1.0f;
    public float pointValue = 1.0f;
    public ItemType itemType = ItemType.Standard;
    public float speedModifier = 0.0f;
    public float speedTime = 1.0f;

    public float respawnMin = 4;
    public float respawnMax = 4;

    public float moveToPlayerSpeed = 7f;
    private int scoreOwner = 0;

    Vector3 targetLocation;
    private bool sucked = false;

    public bool positiveItem;
    public bool negativeItem;

    public enum ItemType
    {
        Standard,
        Danger
    }

    void Start()
    {
        audioManager = AudioManager.instance;

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



        }


    }

    //void OnTriggerEnter(Collider other)
    //{
    //    for (int i = 1; i <= 4; i++)
    //    {
    //        if (other.gameObject.tag == "Vacuum" + i.ToString() && other.gameObject.transform.parent.GetComponent<PlayerController>().PlayerVacuum)
    //        {
    //            other.gameObject.GetComponent<BoxCollider>().enabled = false;
    //            scoreOwner = i;
    //            targetLocation = other.gameObject.transform.parent.transform.position;
    //            this.transform.parent.parent.parent.GetComponent<BeltPlateController>().Restock(Random.Range(2f, 8f));
    //            Vector3 temp = transform.position; // world pos
    //            this.gameObject.transform.SetParent(null , true); // *should* not move, but you say...
    //          //  this.transform.position = temp; // restore world position
    //            sucked = true;
    //           // Debug.Log("sucked");
    //            //destroy 
    //        }
    //    }

    //}
    void OnTriggerStay(Collider other)
    {
        for (int i = 1; i <= 4; i++)
        {
            if (other.gameObject.tag == "Vacuum" + i.ToString() && other.gameObject.transform.parent.GetComponent<PlayerController>().PlayerVacuum)
            {
                audioManager.playItemHitSound();
                other.gameObject.GetComponent<BoxCollider>().enabled = false;
                scoreOwner = i;
                targetLocation = other.gameObject.transform.parent.transform.position;
                if (transform.root != this.transform)
                {
                    Transform par = this.transform.parent.parent.parent;
                    if (par)
                    {
                        BeltPlateController bpc = par.GetComponent<BeltPlateController>();
                        if (bpc)
                            bpc.Restock(Random.Range(respawnMin, respawnMax));
                    }
                    Vector3 temp = transform.position; // world pos
                    this.gameObject.transform.SetParent(null, true); // *should* not move.
                                                                     //  this.transform.position = temp; // restore world position
                    sucked = true;
                    // Debug.Log("sucked");
                    //destroy 
                }
            }
        }

    }

    void HitTarget()
    {
        //Debug.Log(scoreOwner);
        if (positiveItem)
        {
            audioManager.playItemPositiveSound();
            audioManager.playPlayerVacuumSound();
            Destroy(this.gameObject);
        }
        else if (negativeItem)
        {
            audioManager.playItemNegativeSound();
            audioManager.playPlayerVacuumSound();
            Destroy(this.gameObject);
        }

        GameObject go = GameObject.FindGameObjectWithTag("Player" + scoreOwner);
        PlayerController pc = go.GetComponent<PlayerController>();
        if (itemType == ItemType.Danger)
            pc.SuckedDanger();
        pc.score += (int)pointValue;
        Destroy(this.gameObject);
    }
}
