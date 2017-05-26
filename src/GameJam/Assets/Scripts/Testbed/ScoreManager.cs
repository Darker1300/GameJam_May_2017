using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    static int p1score;
    static int p2score;
    static int p3score;
    static int p4score;

    public Text scoreText1;
    public Text scoreText2;
    public Text scoreText3;
    public Text scoreText4;

    public Text leaderText1;
    public Text leaderText2;
    public Text leaderText3;
    public Text leaderText4;

    public GameObject LeaderBoard;

    void Start()
    {
        p1score = -1;
        p2score = -1;
        p3score = -1;
        p4score = -1;
        ResetScores();
    }

    void Update()
    {
        GameObject go = null;

        go = GameObject.FindGameObjectWithTag("Player1");
        if (go)
        {
            p1score = go.GetComponent<PlayerController>().score;
            scoreText1.text = "Score" + " \n" + p1score;
        }

        go = GameObject.FindGameObjectWithTag("Player2");
        if (go)
        {
            p2score = go.GetComponent<PlayerController>().score;
            scoreText2.text = "Score" + " \n" + p2score;
        }

        go = GameObject.FindGameObjectWithTag("Player3");
        if (go)
        {
            p3score = go.GetComponent<PlayerController>().score;
            scoreText3.text = "Score" + " \n" + p3score;
        }

        go = GameObject.FindGameObjectWithTag("Player4");
        if (go)
        {
            p4score = go.GetComponent<PlayerController>().score;
            scoreText4.text = "Score" + " \n" + p4score;
        }
    }

    public void AddItemToScore(PlayerController _player, Item _item)
    {

    }

    public void ResetScores()
    {
        p1score = 0;
        p2score = 0;
        p3score = 0;
        p4score = 0;

    }
}
