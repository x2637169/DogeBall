using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text textTimer;
    [SerializeField] private int time = 0;
    private WaitForSeconds waitTime = new WaitForSeconds(1f);
    private IEnumerator coutingTime;
    private bool isTimeRuuing = false;

    private void Awake()
    {
        coutingTime = CountingTime();
    }

    public int GetTime()
    {
        return time;
    }

    public void StartTimer()
    {
        if (!isTimeRuuing)
        {
            isTimeRuuing = true;
            StartCoroutine(coutingTime);
        }
    }

    private IEnumerator CountingTime()
    {
        while (true)
        {
            yield return waitTime;
            time++;
            SetTimer();
        }
    }

    private void SetTimer()
    {
        int hour = (int)(time / 3600);
        int minute = (int)(time / 60) % 60;
        int second = (int)(time % 60);
        textTimer.text = string.Format("{0}{1}{2}", TimeToSting(0, hour)
                                               , TimeToSting(hour, minute)
                                               , second.ToString("D2"));
    }

    private string TimeToSting(int _front, int _behind)
    {
        string i = _behind.ToString("D2") + ":";

        if (_front <= 0)
        {
            if (_behind <= 0) return "";
            if (_behind < 10) return _behind.ToString("D1") + ":";
        }

        return i;
    }

    public void ResetTimer()
    {
        time = 0;
        SetTimer();
    }

    public void StopCounting()
    {
        if (isTimeRuuing)
        {
            StopCoroutine(coutingTime);
            isTimeRuuing = false;
        }
    }

}
