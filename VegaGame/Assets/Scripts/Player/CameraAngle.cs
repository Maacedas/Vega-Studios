using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
    private bool isFinished;
    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer transposer;
    private CinemachineComposer composer;


    private void OnEnable()
    {
        DancingFinish.OnGameFinished += LevelFinished;
    }
    private void OnDisable()
    {
        DancingFinish.OnGameFinished -= LevelFinished;
    }

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        composer = vcam.GetCinemachineComponent<CinemachineComposer>();
    }

    void Update()
    {
        if (isFinished)
        {
            composer.m_TrackedObjectOffset = Vector3.Lerp(
                composer.m_TrackedObjectOffset, 
                new Vector3(2.2f, -0.31f, -4), 0.4f * Time.deltaTime);
            transposer.m_FollowOffset = Vector3.Lerp(
                transposer.m_FollowOffset,
                new Vector3(-16.4f, 17f, 29.3f), 0.4f * Time.deltaTime);
            
            
        }
    }

    void LevelFinished()
    {
        isFinished = true;
    }
}