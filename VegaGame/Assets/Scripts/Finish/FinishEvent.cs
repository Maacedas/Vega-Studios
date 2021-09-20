using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Cinemachine;

public class FinishEvent: MonoBehaviour {
    public static Action OnClappingPeople = delegate { };
    public static Action OnFinishLine = delegate { };
    public static Action OnGameFinished = delegate { };

    [SerializeField] private GameObject closeUpCamera;

    public GameObject playerAnimator;
    [SerializeField]
    private float winUIDelay=1;
    [SerializeField]

    private GameObject playerMesh;
    private void Start() {
        playerAnimator=GameObject.FindGameObjectWithTag("PlayerAnimator");
        playerMesh=GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Player")) {
            OnClappingPeople.Invoke();
            OnFinishLine.Invoke();
            other.GetComponent<Rigidbody>().isKinematic=false;
            closeUpCamera.SetActive(true);
        }
    }

    private void LevelEnd() {
        playerAnimator.GetComponent<Animator>().SetBool("Dance", true);
        StartCoroutine(ActiveWinUI());
    }

    IEnumerator ActiveWinUI() {
        yield return new WaitForSeconds(winUIDelay);
        UIManager.I.SetState(UIState.WON);
    }
}
