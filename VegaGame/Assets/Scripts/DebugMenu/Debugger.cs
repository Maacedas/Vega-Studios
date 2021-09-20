using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using System;
using DG.Tweening;

[Serializable]
public enum DebuggerGameDifficulty {
    Easy,
    Medium,
    Hard
}

[Serializable]
public enum DebuggerGateDifficulty {
    Easy,
    Medium,
    Hard
}

[Serializable]
public enum DebuggerRoadLenght {
    Short,
    Mid,
    Long
}
public class Debugger: MonoBehaviour {
    [Header("Debug Mode")]
    [SerializeField] GameObject DebugMenu;
    [SerializeField] GameObject [] panels;
    //[SerializeField] LevelDebugManager levelSettings;
    int currentPanel = 0;
    bool debugModeEnabled = false;

    [Header("Player Settings")]
    [SerializeField] CharacterMovement player;
    [SerializeField] Slider playerSpeed;

    [Header("Camera Settings")]
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] Slider camFov;

    [Header("Difficulty Settings")]
    DebuggerGameDifficulty gameDifficulty;
    DebuggerGateDifficulty gateDifficulty;
    DebuggerRoadLenght roadLenght;

    [Header("Theme Settings")]
    [SerializeField] GameObject verticalFog;
    [SerializeField] Color [] fogColors;
    [SerializeField] Material [] skyboxMaterials;
    int activeFogSet = 0;

    void Start() {
        camFov.maxValue=90;
        camFov.minValue=30;
        camFov.value=cam.m_Lens.FieldOfView;

        playerSpeed.minValue=0;
        playerSpeed.maxValue=4;
        playerSpeed.value=2;
    }

    void Update() {

        if(!debugModeEnabled)
            return;

        player.speed=playerSpeed.value;
        cam.m_Lens.FieldOfView=camFov.value;
    }

    public void PauseGame() {
        Time.timeScale=0;
    }

    public void PlayGame() {
        Time.timeScale=1;
        player.enabled=true;
    }

    public void CameraAngleUp() {
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y+=1;
    }
    public void CameraAngleDown() {
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y-=1;
    }
    public void CameraScreenUp() {
        cam.GetCinemachineComponent<CinemachineComposer>().m_ScreenY+=0.1f;
    }
    public void CameraScreenDown() {
        cam.GetCinemachineComponent<CinemachineComposer>().m_ScreenY-=0.1f;
    }
    void DebugModeEnabled() {
        debugModeEnabled=true;
        DebugMenu.SetActive(true);
        Time.timeScale=0;
    }
    void DebugModeDisable() {
        debugModeEnabled=false;
        DebugMenu.SetActive(false);
        Time.timeScale=1;
    }

    public void DebugMenuButtonDown() {
        if(!debugModeEnabled) {
            DebugModeEnabled();
        } else {
            DebugModeDisable();
        }
    }

    public void NextPanel() {

        if(currentPanel==panels.Length-1)
            return;

        panels [currentPanel].SetActive(false);
        currentPanel++;
        panels [currentPanel].SetActive(true);
    }
    public void PreviousPanel() {

        if(currentPanel==0)
            return;

        panels [currentPanel].SetActive(false);
        currentPanel--;
        panels [currentPanel].SetActive(true);
    }

    public void ResetLevel() {
        int levelSet = 1;
        switch(gameDifficulty) {
            case DebuggerGameDifficulty.Easy:
                switch(roadLenght) {
                    case DebuggerRoadLenght.Short:
                        levelSet=0;
                        break;
                    case DebuggerRoadLenght.Mid:
                        levelSet=1;
                        break;
                    case DebuggerRoadLenght.Long:
                        levelSet=2;
                        break;
                    default:
                        break;
                }
                break;
            case DebuggerGameDifficulty.Medium:
                switch(roadLenght) {
                    case DebuggerRoadLenght.Short:
                        levelSet=3;
                        break;
                    case DebuggerRoadLenght.Mid:
                        levelSet=4;
                        break;
                    case DebuggerRoadLenght.Long:
                        levelSet=5;
                        break;
                    default:
                        break;
                }
                break;
            case DebuggerGameDifficulty.Hard:
                switch(roadLenght) {
                    case DebuggerRoadLenght.Short:
                        levelSet=6;
                        break;
                    case DebuggerRoadLenght.Mid:
                        levelSet=7;
                        break;
                    case DebuggerRoadLenght.Long:
                        levelSet=8;
                        break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        Debug.Log(levelSet);
        LevelDebugManager.I.ResetLevel(levelSet);
        player.transform.DOKill();
    }

    public void ChangeLevelDifficulty(int _gameDif) {
        switch(_gameDif) {
            case 0:
                gameDifficulty=DebuggerGameDifficulty.Easy;
                break;
            case 1:
                gameDifficulty=DebuggerGameDifficulty.Medium;
                break;
            case 2:
                gameDifficulty=DebuggerGameDifficulty.Hard;
                break;
            default:
                break;
        }
    }
    public void ChangeGateDifficulty(int _gateDif) {
        switch(_gateDif) {
            case 0:
                gateDifficulty=DebuggerGateDifficulty.Easy;
                LevelDebugManager.I._GateDifficulty=GateDifficulty.Easy;
                break;
            case 1:
                gateDifficulty=DebuggerGateDifficulty.Medium;
                LevelDebugManager.I._GateDifficulty=GateDifficulty.Medium;
                break;
            case 2:
                gateDifficulty=DebuggerGateDifficulty.Hard;
                LevelDebugManager.I._GateDifficulty=GateDifficulty.Hard;
                break;
            default:
                break;
        }
    }
    public void ChangeRoadLength(int _roadLengt) {
        switch(_roadLengt) {
            case 0:
                roadLenght=DebuggerRoadLenght.Short;
                break;
            case 1:
                roadLenght=DebuggerRoadLenght.Mid;
                break;
            case 2:
                roadLenght=DebuggerRoadLenght.Long;
                break;
            default:
                break;
        }
    }

    public void NextFogSet() {

        if(activeFogSet==fogColors.Length-1)
            return;

        activeFogSet++;
        RenderSettings.fogColor=fogColors [activeFogSet];
        RenderSettings.skybox=skyboxMaterials [activeFogSet];
        verticalFog.GetComponent<Renderer>().material.color=fogColors [activeFogSet];
    }

    public void PreviousFogSet() {

        if(activeFogSet==0)
            return;

        activeFogSet--;
        RenderSettings.fogColor=fogColors [activeFogSet];
        RenderSettings.skybox=skyboxMaterials [activeFogSet];
        verticalFog.GetComponent<Renderer>().material.color=fogColors [activeFogSet];
    }
}
