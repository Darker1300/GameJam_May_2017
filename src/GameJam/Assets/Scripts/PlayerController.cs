using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
   // bool playerIndexSet = false;
 

    public GameObject Player;
    public ParticleSystem PlayerVacuum;
    public bool playerMove = false;

    [HideInInspector]
    public int score;


    public float playerMoveSpeed;
    //  static float moveSpeedModifier;
    //  public float turnLag;
    public float modelForward;


    private RaycastHit hit;  // Used for finding if enemy is in line of sight
    private Vector3 rayDirection;

    public bool playerIsVacuuming = false;
    public float vacuumTimer;
    private float vacuumTime;

    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        
        for (int i = 1; i <= 4 ; i++)
        {
            if (gameObject.tag == ("Player" + i.ToString()))
            {
                Player = GameObject.FindGameObjectWithTag("Player" + i.ToString());
                PlayerVacuum = GameObject.FindGameObjectWithTag("Vacuum" + i.ToString()).GetComponent<ParticleSystem>();
                PlayerVacuum.Pause();
               
            }  
        }

        vacuumTimer = 1;
        playerMoveSpeed = 5;
        modelForward = 225;

        audioManager = AudioManager.instance;
        //  moveSpeedModifier = 1;

    }

    // Update is called once per frame
    void Update()
    {

        //for (int i = 1; i <= 4; ++i)
        //{
        //    PlayerIndex testPlayerIndex = (PlayerIndex)i;
        //    GamePadState testState = GamePad.GetState(testPlayerIndex);
        //    if (testState.IsConnected)
        //    {
        //        Debug.Log(string.Format("GamePad found {0}", testPlayerIndex));
        //        playerIndex = testPlayerIndex;
        //        playerIndexSet = true;
        //    }
        //}

        //prevState = state;
        //state = GamePad.GetState(playerIndex);



        for (int i = 1; i <= 4; i++)
        {
            
            if (Player.tag == "Player" + i.ToString())
            {
                vacuumTime += Time.deltaTime;
                if (vacuumTime >= vacuumTimer)
                {
                    playerIsVacuuming = false;
                    PlayerVacuum.GetComponent<BoxCollider>().enabled = false;
                    PlayerVacuum.Pause();
                    PlayerVacuum.Clear();
                }
                    

                if (Input.GetButton("A" + i.ToString()) && !playerIsVacuuming && playerMove )
                {

                    Debug.Log("Player" + i.ToString() + "Button A" + i.ToString());
                    if (!playerIsVacuuming && vacuumTime > vacuumTimer)
                    {
                        if (!playerIsVacuuming)
                        {
                            PlayerVacuum.Play();
                            playerIsVacuuming = true;
                            PlayerVacuum.GetComponent<BoxCollider>().enabled = true;
                           // audioManager.playHoveringSound();
                        }
                        vacuumTime = 0;
                    }
                }
                if (Input.GetButton("B" + i.ToString()) && !playerIsVacuuming && playerMove)
                {

                    Debug.Log("Player" + i.ToString() + "Button A" + i.ToString());
                    if (!playerIsVacuuming && vacuumTime > vacuumTimer)
                    {
                        if (!playerIsVacuuming)
                        {
                            PlayerVacuum.Play();
                            playerIsVacuuming = true;
                            PlayerVacuum.GetComponent<BoxCollider>().enabled = true;

                        }
                        vacuumTime = 0;
                    }
                }
                Vector3 newForward = new Vector3(Input.GetAxis("Horizontal" + i.ToString()), 0, Input.GetAxis("Vertical" + i.ToString())).normalized;
                if (newForward != Vector3.zero && !playerIsVacuuming && playerMove)
                {
                    

                    // set the forward vector of the transform to the newForward vector based on the usersinput
                    // Translate the vector towards the players forward amplified by movement speed
                    // then set the players rotation manually to offset the camera rotation on the y axis
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

                    Player.gameObject.transform.forward = newForward;
                    Player.gameObject.transform.Translate((Vector3.forward - Vector3.right).normalized * playerMoveSpeed * Time.deltaTime);
                    Player.gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + modelForward, transform.eulerAngles.z);


                }
            }

            //For implementing lag on turn 

            //Physics.Raycast(this.gameObject.transform.position, -this.gameObject.transform.TransformDirection(Vector3.right), out hit, 200);
            //Quaternion oldRot = transform.rotation;
            //transform.LookAt(new Vector3(this.gameObject.transform.position.x, transform.position.y, this.gameObject.transform.position.z));
            //Quaternion newRot = transform.rotation;
            //transform.rotation = Quaternion.Lerp(oldRot, newRot, 0);


       }
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
    
