using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SurfStart : MonoBehaviour
{
    public static Action surfStart;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            surfStart?.Invoke();
        }
    }
}
