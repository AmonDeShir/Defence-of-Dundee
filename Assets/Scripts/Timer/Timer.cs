using System;
using UnityEngine.AI;

[Serializable]
public class Timer {
    public event TimeoutEventHandler OnTimeout;

    public bool IsStopped;
    public readonly bool IsInterval;
    public readonly bool IsOneShoot;

    public float Time;

    [UnityEngine.SerializeField]
    private float timeLeft;

    public delegate void TimeoutEventHandler();

    public Timer(float time, bool autostart = true, bool interval = false, bool oneShoot = false) {
        this.IsStopped = !autostart;
        this.IsInterval = interval;
        this.IsOneShoot = oneShoot;
        this.Time = time;
        this.timeLeft = time;
    }

    public Timer(float time, TimeoutEventHandler handler, bool autostart = true, bool interval = false, bool oneShoot = false): this(time, autostart, interval, oneShoot) {
        OnTimeout += handler;  
    }

    public void Start() {
        IsStopped = false;
        timeLeft = Time;
    }

    public void Update() {
        if (IsStopped) {
            return;
        }

        timeLeft -= UnityEngine.Time.deltaTime;

        if (timeLeft <= 0f) {
            timeLeft = Time;

            OnTimeout.Invoke();
        
            if (!IsInterval) {
                IsStopped = true;
            }

            if (IsOneShoot) {
                OnTimeout = null;
            }
        }
    }
}