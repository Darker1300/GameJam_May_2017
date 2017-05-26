using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Controller;

public class MenuInputManager : MonoBehaviour
{
    [Header("Ready Status:")]

    public Image P1 = null;
    public Image P2 = null;
    public Image P3 = null;
    public Image P4 = null;

    public Sprite available = null;
    public Sprite accepted = null;
    void Start()
    {
    }

    void OnDisable()
    {
        ControllerStatus.One = P1.sprite == accepted;
        ControllerStatus.Two = P2.sprite == accepted;
        ControllerStatus.Three = P3.sprite == accepted;
        ControllerStatus.Four = P4.sprite == accepted;

        P1.sprite = available;
        P2.sprite = available;
        P3.sprite = available;
        P4.sprite = available;

    }

    void Update()
    {
        Process("A1", P1);
        Process("A2", P2);
        Process("A3", P3);
        Process("A4", P4);
    }

    void Process(string _inputID, Image _status)
    {
        if (Input.GetButton(_inputID))
            _status.sprite = accepted;
    }

    public void StartGame()
    {
        if (ControllerStatus.One || ControllerStatus.Two || ControllerStatus.Three || ControllerStatus.Four)
        {
            ControllerStatus.One = P1.sprite == accepted;
            ControllerStatus.Two = P2.sprite == accepted;
            ControllerStatus.Three = P3.sprite == accepted;
            ControllerStatus.Four = P4.sprite == accepted;
            SceneManager.LoadScene(1);
        }
    }

    public void GoToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}


public class GameControllerManager : MonoBehaviour
{
    void Start()
    {
    }
}

