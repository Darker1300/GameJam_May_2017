﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public BeltManager beltManager = null;

    [HideInInspector]
    public UnityEvent RoundStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundDurationChanged = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundEnd = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundTimerStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundTimerChanged = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundTimerEnd = new UnityEvent();

    public bool startTimerStarted = false;
    public float startTimerStart = 5;
    public float startTimerCurrent = 0;

    public bool durationTimerStarted = false;
    public float durationTimerStart = 45;
    public float durationTimerCurrent = 0;

    private AudioManager audioManager;
    public AudioClip endBGM;
    [Range(0.0f, 1.0f)]
    public float endBGMVol;
    private bool startAudioPlayed = false;
    private bool EndAudioPlayed = false;
    private bool startBGMPlayed = false;
    private bool EndBGMPlayed = false;

    public int slowDownTimer = 6;

    public GameObject LeaderBoard;

    public Text TimeText = null;
    public Color Countdown_Start_Color = Color.yellow;
    public Color Countdown_End_Color = Color.red;

    void Start()
    {
        // Events
        RoundStart.AddListener(OnRoundStart);
        RoundDurationChanged.AddListener(OnRoundDurationChanged);
        RoundEnd.AddListener(OnRoundEnd);
        RoundTimerStart.AddListener(OnRoundTimerStart);
        RoundTimerChanged.AddListener(OnRoundTimerChanged);
        RoundTimerEnd.AddListener(OnRoundTimerEnd);

        // Assume Timer starts as soon as scene loads
        startTimerStarted = true;
        RoundTimerStart.Invoke();

        //audio instance
        audioManager = AudioManager.instance;

        durationTimerCurrent = durationTimerStart;

        LeaderBoard.SetActive(false);

    }

    void Update()
    {
        if (startTimerStarted)
        {
            startTimerCurrent -= Time.deltaTime;
            RoundTimerChanged.Invoke();
        }
        if (durationTimerStarted)
        {
            durationTimerCurrent -= Time.deltaTime;
            RoundDurationChanged.Invoke();

        }

        // Audio
        if (startTimerCurrent <= 4 && !startAudioPlayed)
        {
            audioManager.playLevelIntroSound();
            startAudioPlayed = true;
        }
        if (startTimerCurrent <= 0 && !startBGMPlayed)
        {

            for (int i = 1; i <= 4; i++)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Player" + i.ToString());
                if (go) go.GetComponent<PlayerController>().playerMove = true;
            }
            // GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>().enabled = true;

            GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>().Play();
            startBGMPlayed = true;
        }

        if (durationTimerCurrent <= slowDownTimer)
        {
            GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>().pitch = (durationTimerCurrent / slowDownTimer + 0.01f);
            for (int i = 1; i <= 4; i++)
            {
                // GameObject.FindGameObjectWithTag("Player" + i.ToString()).GetComponent<PlayerController>().playerMoveSpeed = GameObject.FindGameObjectWithTag("Player" + i.ToString()).GetComponent<PlayerController>().playerMoveSpeed * (durationTimerCurrent / 6 + 0.01f);
                GameObject first = GameObject.FindGameObjectWithTag("Player" + i.ToString());
                GameObject second = GameObject.FindGameObjectWithTag("Player" + i.ToString());
                if (first && second)
                    first.GetComponent<PlayerController>().playerMoveSpeed -= second.GetComponent<PlayerController>().playerMoveSpeed / durationTimerCurrent / (slowDownTimer * 4);
            }
        }

        if (durationTimerCurrent <= -0.01f && !EndAudioPlayed)
        {
            for (int i = 1; i <= 4; i++)
            {
                GameObject.FindGameObjectWithTag("Player" + i.ToString()).GetComponent<PlayerController>().playerMove = false;
            }
            GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>().pitch = 1;
            GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>().Stop();
            audioManager.playLevelEndSound();
            EndAudioPlayed = true;
            // GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>().;
        }
        if (durationTimerCurrent <= -4.01f && !EndBGMPlayed)
        {
            gameEnd();
            GameObject.FindGameObjectWithTag("BGMManager").GetComponent<AudioSource>().Pause();
            GameObject.FindGameObjectWithTag("SBGM").GetComponent<AudioSource>().PlayOneShot(endBGM, endBGMVol);
            EndBGMPlayed = true;

        }
    }
    void gameEnd()
    {
        gameObject.GetComponent<ScoreManager>().leaderText1.text = "Player 1 SCORE : " + GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerController>().score;
        gameObject.GetComponent<ScoreManager>().leaderText2.text = "Player 2 SCORE : " + GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerController>().score;
        gameObject.GetComponent<ScoreManager>().leaderText3.text = "Player 3 SCORE : " + GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerController>().score;
        gameObject.GetComponent<ScoreManager>().leaderText4.text = "Player 4 SCORE : " + GameObject.FindGameObjectWithTag("Player4").GetComponent<PlayerController>().score;

        LeaderBoard.SetActive(true);


    }

    public void gameReset()
    {
        gameObject.GetComponent<ScoreManager>().ResetScores();
        LeaderBoard.SetActive(false);
        SceneManager.LoadScene(1);

    }

    public void exitToMenu()
    {
        gameObject.GetComponent<ScoreManager>().ResetScores();
        LeaderBoard.SetActive(false);
		SceneManager.LoadScene(0);

    }

    #region Events
    void OnRoundStart()
    {
        // Debug.Log("Round Start!");
        durationTimerStarted = true;
        durationTimerCurrent = durationTimerStart;

    }

    void OnRoundDurationChanged()
    {
        if (durationTimerCurrent <= 0.0f)
        {
            RoundEnd.Invoke();
            TimeText.text = "STOP!";
        }
        else
        {
            TimeText.text = Mathf.CeilToInt(durationTimerCurrent).ToString();
            TimeText.color = Color.Lerp(Countdown_Start_Color, Countdown_End_Color, 1 - (durationTimerCurrent / durationTimerStart));
        }
    }

    void OnRoundEnd()
    {
        // Debug.Log("Round End!");


    }

    void OnRoundTimerStart()
    {
        // Debug.Log("Round Timer Start!");
        startTimerStarted = true;
        startTimerCurrent = startTimerStart;

        beltManager.FillBelt();
        // Show UI


    }
    void OnRoundTimerChanged()
    {
        if (startTimerCurrent <= 0.0f) RoundTimerEnd.Invoke();

        TimeText.text = Mathf.CeilToInt(startTimerCurrent).ToString();
        TimeText.color = Color.Lerp(Countdown_Start_Color, Countdown_End_Color, 1 - (startTimerCurrent / startTimerStart));
        // if Timer<=0 Invoke END
        // else Change UI Text to Mathf.Round(TimerValue)
    }

    void OnRoundTimerEnd()
    {
        startTimerStarted = false;
        RoundStart.Invoke();
        //for (int i = 1; i <= 4; i++)
        //{
        //    GameObject go = GameObject.FindGameObjectWithTag("Player" + i.ToString());
        //    if (go) Debug.Log(go.GetComponent<PlayerController>().score);
        //}
        // Debug.Log("Round Timer End!");
    }




    #endregion
}
