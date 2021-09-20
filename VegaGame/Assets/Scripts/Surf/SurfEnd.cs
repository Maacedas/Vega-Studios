using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SurfEnd : MonoBehaviour
{
    public static Action surfEnd;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            surfEnd?.Invoke();
        }
    }
}
