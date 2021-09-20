using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public enum LevelID {
    EasyShort,
    EasyMid,
    EasyLong,
    MediumShort,
    MediumMid,
    MediumLong,
    HardShort,
    HardMid,
    HardLong
}

public enum GateDifficulty {
    Easy,
    Medium,
    Hard
}

[Serializable]
public class LevelSettings {
    public LevelID id;
    public GameObject road;
    public GameObject obstacle;
    public GameObject gates;
}
public class LevelDebugManager: Singleton<LevelDebugManager> {

    public LevelSettings [] levelSet;
    GateDifficulty gateDifficulty;
    int activeLevelSetting = 1;
    [SerializeField] GameObject player;
    Vector3 playerPos;

    public GateDifficulty _GateDifficulty { get => gateDifficulty; set => gateDifficulty=value; }

    private void Start() {
        playerPos=player.transform.position;
        gateDifficulty=GateDifficulty.Medium;
    }
    public void ResetLevel(int levelNum) {
        //Deactivate old level
        for(int i = 0; i<levelSet.Length-1; i++) {
            levelSet [i].road.SetActive(false);
            levelSet [i].obstacle.SetActive(false);
            levelSet [i].gates.SetActive(false);
        }

        //increase level number
        activeLevelSetting=levelNum;

        //activate new level
        levelSet [activeLevelSetting].road.SetActive(true);
        levelSet [activeLevelSetting].obstacle.SetActive(true);
        levelSet [activeLevelSetting].gates.SetActive(true);

        //reset player card value
        CardNumberManager.I.Reset();

        //reset gates
        foreach(Transform child in levelSet [activeLevelSetting].gates.transform) {
            child.GetComponent<GateManager>().ResetGateHandlers();
        }

        player.transform.position=playerPos;
    }
}
