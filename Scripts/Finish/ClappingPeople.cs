using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClappingPeople : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        FinishEvent.OnClappingPeople += StartClapping;
    }
    private void OnDisable()
    {
        FinishEvent.OnClappingPeople -= StartClapping;
    }

    void StartClapping()
    {
        anim.SetBool("Clapping",true);
    }
}
