using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
            startTimerCurrent -= Time.deltaTime;
            RoundDurationChanged.Invoke();
        }
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
        if (durationTimerCurrent <= 0.0f) RoundEnd.Invoke();
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
