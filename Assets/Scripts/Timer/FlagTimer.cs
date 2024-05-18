using System;
using UnityEngine.AI;

[Serializable]
public class FlagTimer {
    public bool IsStopped => isStopped;
    public bool HasFinishedCounting => finishedCounting;
    public float Time;

    [UnityEngine.SerializeField]
    private float timeLeft;

    public delegate void TimeoutEventHandler();

    private bool isStopped;
    private bool finishedCounting = false;

    public FlagTimer(float time, bool stopped = false) {
        this.Time = time;
        this.timeLeft = time;
        this.isStopped = stopped;
    }

    public void ResetFlag() {
        finishedCounting = false;
    }

    public void Start() {
        isStopped = false;
        finishedCounting = false;
        timeLeft = Time;
    }

    public void Stop() {
        isStopped = true;
    }

    public void Update() {
        if (isStopped) {
            return;
        }

        timeLeft -= UnityEngine.Time.deltaTime;

        if (timeLeft <= 0f) {
            timeLeft = Time;
            isStopped = true;
            finishedCounting = true;
        }
    }
}