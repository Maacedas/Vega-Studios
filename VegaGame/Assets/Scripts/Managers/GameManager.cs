using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { INIT }

public class GameManager : ManagerBase<GameManager, GameState>
{

    private void Awake() {
        Reset();
    }

    public void Reset() {
        SetState(GameState.INIT);
    }
}
