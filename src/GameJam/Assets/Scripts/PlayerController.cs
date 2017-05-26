using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controller;


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

    public Animator animator = null;
    public ParticleSystem SteamParticle1;
    public ParticleSystem SteamParticle2;

    // Use this for initialization
    void Start()
    {
        switch (gameObject.tag)
        {
            case "Player1": if (!ControllerStatus.One) gameObject.SetActive(false); break;
            case "Player2": if (!ControllerStatus.Two) gameObject.SetActive(false); break;
            case "Player3": if (!ControllerStatus.Three) gameObject.SetActive(false); break;
            case "Player4": if (!ControllerStatus.Four) gameObject.SetActive(false); break;
        }

        for (int i = 1; i <= 4; i++)
        {
            if (gameObject.tag == ("Player" + i.ToString()))
            {
                Player = GameObject.FindGameObjectWithTag("Player" + i.ToString());
                GameObject go = GameObject.FindGameObjectWithTag("Vacuum" + i.ToString());
                PlayerVacuum = go ? go.GetComponent<ParticleSystem>() : null;
                if (PlayerVacuum) PlayerVacuum.Pause();

            }
        }

        //vacuumTimer = 1;
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


                if (Input.GetButton("A" + i.ToString()) && !playerIsVacuuming && playerMove)
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

    public void SuckedDanger()
    {
        animator.SetTrigger("ActivateMilk");

        if (playerMoveSpeed >= 0.6f)
            playerMoveSpeed = playerMoveSpeed - 0.5f;

        SteamParticle1.Play(false);
        SteamParticle2.Play(false);
    }
    public void EndSteamEffect()
    {
        // playerMoveSpeed = 5f;
        SteamParticle1.Stop();
        SteamParticle2.Stop();
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

