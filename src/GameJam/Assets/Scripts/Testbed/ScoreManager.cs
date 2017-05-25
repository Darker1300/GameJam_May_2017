using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour {

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

    void Start () {
        ResetScores();
	}

    void Update()
    {
     
        p1score = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>().score;
        p2score = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>().score;
        p3score = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerController>().score;
        p4score = GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerController>().score;

        scoreText1.text = "Score" + " \n" + p1score;
        scoreText2.text = "Score" + " \n" + p2score;
        scoreText3.text = "Score" + " \n" + p3score;
        scoreText4.text = "Score" + " \n" + p4score;

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
