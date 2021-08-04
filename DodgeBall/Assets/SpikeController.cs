using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    [SerializeField] private Animator m_Animaotr;

    public void SpikeUp()
    {
        SetAnimation(SpikeAnimaton.SpikeUp);
        StartCoroutine(SpikeDown());
    }

    private IEnumerator SpikeDown()
    {
        yield return new WaitForSeconds(2f);
        SetAnimation(SpikeAnimaton.SpikeDown);
    }

    public void SetAnimation(SpikeAnimaton _animtion)
    {
        m_Animaotr.SetTrigger(_animtion.ToString());
    }

    public enum SpikeAnimaton
    {
        SpikeUp,
        SpikeDown
    }
}
