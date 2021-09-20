using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightRotation : MonoBehaviour
{
    private float index;
    [SerializeField] private float amplitudeX;
    [SerializeField] private float frequencyX;
    [SerializeField] private float positionOffset;
    private void Update()
    {
        index += Time.deltaTime;
        float x = amplitudeX*Mathf.Sin (frequencyX*index) - positionOffset;
            transform.localRotation= Quaternion.Euler(new Vector3(x, transform.localRotation.y, transform.localRotation.z));  
        
        
        
    }
}

