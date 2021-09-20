using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    public FloatingJoystick variableJoystick;
    [SerializeField] private float leftConstraint;
    [SerializeField] private float rightConstraint;
    

    

    public void Update()
    {
        
            MoveForward();
            ClampMovement();
    }

    

    void MoveForward()
    {
        Vector3 movement = Vector3.right * speed;
        float joystickInput = variableJoystick.Horizontal;
        transform.Translate(movement * joystickInput  * Time.deltaTime);
    }

    void ClampMovement()
    {
        float xPos = Mathf.Clamp(transform.position.x, leftConstraint, rightConstraint);
        transform.position = new Vector3(xPos,transform.position.y,transform.position.z);
    }
}