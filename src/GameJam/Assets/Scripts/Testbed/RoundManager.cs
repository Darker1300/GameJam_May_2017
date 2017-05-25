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
    public UnityEvent RoundEnd = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundTimerStart = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundTimerChanged = new UnityEvent();
    [HideInInspector]
    public UnityEvent RoundTimerEnd = new UnityEvent();

    public bool roundTimerStarted = false;
    public float roundTimerStart = 5;
    public float roundTimerCurrent = 0;

    void Start()
    {
        // Events
        RoundStart.AddListener(OnRoundStart);
        RoundEnd.AddListener(OnRoundEnd);
        RoundTimerStart.AddListener(OnRoundTimerStart);
        RoundTimerChanged.AddListener(OnRoundTimerChanged);
        RoundTimerEnd.AddListener(OnRoundTimerEnd);

        // Trigger Belt to fill itself upon scene load
        //beltManager.FillBelt();

        // Assume Timer starts as soon as scene loads
        roundTimerStarted = true;
        RoundTimerStart.Invoke();
    }

    void Update()
    {
        if (roundTimerStarted)
        {
            roundTimerCurrent -= Time.deltaTime;
            RoundTimerChanged.Invoke();
        }

    }

    #region Events
    void OnRoundStart()
    {
        // Debug.Log("Round Start!");
    }

    void OnRoundEnd()
    {
        // Debug.Log("Round End!");

     
    }

    void OnRoundTimerStart()
    {
        // Debug.Log("Round Timer Start!");
        roundTimerStarted = true;
        roundTimerCurrent = roundTimerStart;
        beltManager.FillBelt();
        // Show UI
    }

    void OnRoundTimerEnd()
    {
        roundTimerStarted = false;
        RoundStart.Invoke();
        for (int i = 1; i <= 4; i++)
            Debug.Log(GameObject.FindGameObjectWithTag("Player" + i.ToString()).GetComponent<PlayerController>().score);
        // Debug.Log("Round Timer End!");
    }

    void OnRoundTimerChanged()
    {
        if (roundTimerCurrent <= 0.0f) RoundTimerEnd.Invoke();
        // if Timer<=0 Invoke END
        // else Change UI Text to Mathf.Round(TimerValue)
    }
    #endregion
}
