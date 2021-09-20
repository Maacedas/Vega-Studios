﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    
    [SerializeField] private float speed;
    
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed );
    }
}
