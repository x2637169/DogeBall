using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class GameEventController : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private BallManager ballManager;
    [SerializeField] private TrapManager trapManager;
    [SerializeField] public List<TimeEvent> timeEvents;
    [SerializeField] public List<TimeEvent> everyTimeEvents;
    private List<IEnumerator> everyTimeIE = new List<IEnumerator>();
    private IEnumerator checkGameEvent;
    public bool isChecking = false;

    void Awake()
    {
        checkGameEvent = CheckGameEvent();
    }

    public void StartCheckGameEvent()
    {
        StopCheckGameEvent();
        StartCoroutine(checkGameEvent);

        for (int i = 0; i < everyTimeEvents.Count; i++)
        {
            everyTimeIE.Add(RunEveryTimeEvent(everyTimeEvents[i]));
            if (everyTimeIE[i] != null) StartCoroutine(everyTimeIE[i]);
        }

    }

    public void StopCheckGameEvent()
    {
        StopCoroutine(checkGameEvent);

        for (int i = 0; i < everyTimeIE.Count; i++)
        {
            if (everyTimeIE[i] != null) StopCoroutine(everyTimeIE[i]);
        }

        isChecking = false;
    }

    private IEnumerator RunEveryTimeEvent(TimeEvent timeEvent)
    {
        int time;

        while (true)
        {
            time = timer.GetTime() + timeEvent.time;
            yield return new WaitUntil(() => time <= timer.GetTime());
            RunGameEvent(timeEvent);
        }
    }

    private IEnumerator CheckGameEvent()
    {
        int time = 0;

        while (true)
        {
            isChecking = true;

            if (timeEvents.Count >= 1)
            {
                timeEvents.Sort((x, y) => x.time.CompareTo(y.time));
                time = timeEvents[0].time;
                yield return new WaitUntil(() => time <= timer.GetTime());
                RunGameEvent(timeEvents[0]);
                timeEvents.RemoveAt(0);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void RunGameEvent(TimeEvent _timeEvent)
    {
        switch (_timeEvent.gameEvent)
        {
            case GameEvent.AddBall:
                ballManager.AddBall(_timeEvent.addBall);
                break;
            case GameEvent.AddBallSpeed:
                ballManager.AddBallsSpeed(_timeEvent.addBallSpeed);
                break;
            case GameEvent.TrapActive:
                trapManager.TrapActive();
                break;
            case GameEvent.AddTrapAcitveCount:
                trapManager.AddTrapActiveCount(_timeEvent.addTrapActiveCount);
                break;
        }
    }

    public Vector3 test = Vector3.zero;

    [System.Serializable]
    public class TimeEvent
    {
        public int time;
        public GameEvent gameEvent;
        public int addBall;
        public float addBallSpeed;
        public int addTrapActiveCount;
    }

    public enum GameEvent
    {
        AddBall,
        AddBallSpeed,
        TrapActive,
        AddTrapAcitveCount
    }
}
