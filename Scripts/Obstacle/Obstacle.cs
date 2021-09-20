using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private static readonly int Stumble = Animator.StringToHash("Stumble");
    [NonSerialized]public bool isTriggered;
    
    public static Action OnHitObstacle = delegate {  };

    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.gameObject.GetComponentInParent<Animator>().SetTrigger(Stumble);
            OnHitObstacle.Invoke();
        }
    }
    
}
