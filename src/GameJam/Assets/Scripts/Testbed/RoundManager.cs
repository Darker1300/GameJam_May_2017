using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoundManager : MonoBehaviour
{
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

    void Start()
    {
        // Events
        RoundStart.AddListener(OnRoundStart);
        RoundEnd.AddListener(OnRoundEnd);
        RoundTimerStart.AddListener(OnRoundTimerStart);
        RoundTimerChanged.AddListener(OnRoundTimerChanged);
        RoundTimerEnd.AddListener(OnRoundTimerEnd);

    }

    void Update()
    {

    }

    #region Events
    void OnRoundStart()
    {

    }

    void OnRoundEnd()
    {

    }

    void OnRoundTimerStart()
    {

    }

    void OnRoundTimerEnd()
    {

    }

    void OnRoundTimerChanged()
    {

    }
    #endregion
}
