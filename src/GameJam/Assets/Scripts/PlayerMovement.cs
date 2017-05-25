using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public GameObject Player;

    public float playerMoveSpeed;
  //  static float moveSpeedModifier;
    public float turnLag;

    private RaycastHit hit;  // Used for finding if enemy is in line of sight
    private Vector3 rayDirection;

    // Use this for initialization
    void Start()
    {
        Player = this.gameObject;
        playerMoveSpeed = 5;
      //  moveSpeedModifier = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("A"))
        {

            Debug.Log("TTT");
        }


        Vector3 newForward = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        if (newForward != Vector3.zero)
        {


            // If the player is not attacking set the forward vector of the transform to the newForward vector based on the usersinput
            // Translate the vector towards the players forward amplified by movement speed
            // then set the players rotation manually to offset the camera rotation on the y axis

            transform.forward = newForward;
            transform.Translate((Vector3.forward - Vector3.right ).normalized * playerMoveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 225 , transform.eulerAngles.z);


        }

        Physics.Raycast(this.gameObject.transform.position, -this.gameObject.transform.TransformDirection(Vector3.right), out hit, 200);
        Quaternion oldRot = transform.rotation;
        transform.LookAt(new Vector3(this.gameObject.transform.position.x, transform.position.y, this.gameObject.transform.position.z));
        Quaternion newRot = transform.rotation;
        transform.rotation = Quaternion.Lerp(oldRot, newRot, turnLag);


    }

    //if (Input.GetKey(KeyCode.A))
    //{
    //    Player.transform.Translate(-Vector3.right * playerMoveSpeed * Time.deltaTime, Space.World);

    //}
    //if (Input.GetKey(KeyCode.D))
    //{
    //    Player.transform.Translate(Vector3.right * playerMoveSpeed * Time.deltaTime, Space.World);

    //}
    //if (Input.GetKey(KeyCode.W))
    //{
    //    Player.transform.Translate(Vector3.forward * playerMoveSpeed * Time.deltaTime, Space.World);

    //}
    //if (Input.GetKey(KeyCode.S))
    //{
    //    Player.transform.Translate(-Vector3.forward * playerMoveSpeed * Time.deltaTime, Space.World);

    //}
}
    
