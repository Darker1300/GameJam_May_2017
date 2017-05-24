using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public GameObject Player;

    public float playerMoveSpeed;
    static float moveSpeedModifier; 

	// Use this for initialization
	void Start () {
        Player = this.gameObject;
        playerMoveSpeed = 10;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            Player.transform.Translate(-Vector3.right * playerMoveSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey(KeyCode.D))
        {
            Player.transform.Translate(Vector3.right * playerMoveSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey(KeyCode.W))
        {
            Player.transform.Translate(Vector3.forward * playerMoveSpeed * Time.deltaTime, Space.World);

        }
        if (Input.GetKey(KeyCode.S))
        {
            Player.transform.Translate(-Vector3.forward * playerMoveSpeed * Time.deltaTime, Space.World);

        }

    }
}
