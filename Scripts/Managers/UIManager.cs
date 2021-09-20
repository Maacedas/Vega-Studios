using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UIState { INIT, WON, LOST, STARTED , FINISHLINE}

public class UIManager : ManagerBase<UIManager, UIState>
{
   
    [Header("Refs")] 
    public GameObject winUI;
    public GameObject lostUI;
    public GameObject dragGesture;
    public TextMeshProUGUI playerHandText;
    
    private void Awake() {
        OnStateChange += onStateChange;
    }

    private void OnDestroy() {
        OnStateChange -= onStateChange;
    }

    private void onStateChange() {
        switch (state) {
            case UIState.INIT:
                winUI.SetActive(false);
                lostUI.SetActive(false);
                break;
                
            case UIState.WON:
                winUI.SetActive(true);
                lostUI.SetActive(false);
                break;
            
            case UIState.LOST:
                winUI.SetActive(false);
                lostUI.SetActive(true);
                break;
            
            case UIState.STARTED:
                dragGesture.SetActive(false);
                break;
            
            case UIState.FINISHLINE:
                break;
            
            default:
                winUI.SetActive(false);
                lostUI.SetActive(false);
                break;
        }
    }

   
    public void UpdatePlayerHand(int value)
    {
        playerHandText.SetText(value.ToString());
    }
    
    
}
