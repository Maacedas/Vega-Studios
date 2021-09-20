using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelState { INIT, STARTED, WON, LOST }

public class LevelManager: ManagerBase<LevelManager, LevelState> {
    public int score { get; private set; }
    int nextScene = 0;
    private void Awake() {
        Reset();
    }

    public void Reset() {
        SetState(LevelState.INIT);
        SetScore(0);
    }

    // Reset Level Manager on every scene start
    protected override void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        base.OnSceneLoaded(scene, mode);
        Reset();
    }

    public void SetScore(int newScore) {
        score=newScore;
    }

    public static void LoadNextScene() {
        if(SceneManager.GetActiveScene().buildIndex==SceneManager.sceneCountInBuildSettings-1) {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void LoadNextSceneImmediate() {
        LoadNextScene();
    }

    public void LoadNextSceneAfter(float sec) {
        StartCoroutine(LoadNextScene(sec));
    }

    IEnumerator LoadNextScene(float sec) {
        yield return new WaitForSeconds(sec);
        LoadNextScene();
    }

    public static void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void m_ReloadScene() {
        ReloadScene();
    }
}
