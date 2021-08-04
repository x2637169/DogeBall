using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    [SerializeField] private SpikeController[] spikes;
    [SerializeField] private int trapActiveCount = 1;
    [SerializeField] private int maxTrapActiveCount = 1;
    public void AddTrapActiveCount(int _addCount)
    {
        trapActiveCount += _addCount;
        if (trapActiveCount >= maxTrapActiveCount) trapActiveCount = maxTrapActiveCount;
    }

    public void TrapActive()
    {
        if (trapActiveCount > spikes.Length) trapActiveCount = spikes.Length;
        int[] activeNums = GetRandomNumber(spikes.Length, trapActiveCount);

        for (int i = 0; i < activeNums.Length; i++)
        {
            spikes[activeNums[i]].SpikeUp();
        }
    }

    private int[] GetRandomNumber(int _range, int getLen)
    {
        if (_range < getLen)
        {
            Debug.Log("Range can't small than getLen");
            return null;
        }

        List<int> randomNumber = new List<int>();
        while (randomNumber.Count < getLen)
        {
            int num = Random.Range(0, _range);
            bool isEqual = randomNumber.Contains(num);
            if (!isEqual) randomNumber.Add(num);
        }

        return randomNumber.ToArray();
    }

}
