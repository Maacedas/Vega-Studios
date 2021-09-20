using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CardNumberManager: Singleton<CardNumberManager> {
    [Header("Refs")]
    public int currentNumber = 0;
    public Rival rival;
    public Rigidbody [] chips;

    public static Action OnGateCollision = delegate { };
    public static Action OnIncreaseNumber = delegate { };
    public static Action OnDecreaseNumber = delegate { };
    public static Action OnNumberChange = delegate { };
    private void OnEnable() {
        Obstacle.OnHitObstacle+=DecreaseNumber;
    }

    private void OnDisable() {
        Obstacle.OnHitObstacle-=DecreaseNumber;
    }

    public void SetCurrentNumber(int number) {
        currentNumber+=number;
        if(currentNumber<0) {
            currentNumber=0;
        }
        SetNumberColor();
        UIManager.I.UpdatePlayerHand(currentNumber);
    }

    private void DecreaseNumber() {
        currentNumber/=2;
        SetNumberColor();
        UIManager.I.UpdatePlayerHand(currentNumber);
    }

    private void SetNumberColor() {
        int closeness = Mathf.Abs(21-currentNumber);

        if(currentNumber>21) {
            UIManager.I.playerHandText.color=Color.red;
            return;
        }

        if(closeness<3) {
            UIManager.I.playerHandText.color=Color.green;
        }

        if(closeness>=3&&closeness<6) {
            UIManager.I.playerHandText.color=Color.yellow;
        }

        if(closeness>=6) {
            UIManager.I.playerHandText.color=Color.red;
        }
    }
    public void Reset() {
        currentNumber=0;
        UIManager.I.UpdatePlayerHand(currentNumber);
    }

    public IEnumerator MoveChips(bool isWon) {
        yield return new WaitForSeconds(0.5f);
        if(isWon) {
            foreach(var item in chips) {
                item.AddForce(Vector3.forward*-300);
            }
        } else {
            foreach(var item in chips) {
                item.AddForce(Vector3.forward*300);
            }
        }
    }
}
