using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingFinish : MonoBehaviour
{
    public static Action OnGameFinished = delegate {  };
    public ParticleSystem finishParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            finishParticle.gameObject.SetActive(true);
            finishParticle.Play();
            OnGameFinished.Invoke();
            other.transform.parent.GetComponent<Animator>().SetBool("Dance",true);
            UIManager.I.SetState(UIState.WON);
        }
    }
}
