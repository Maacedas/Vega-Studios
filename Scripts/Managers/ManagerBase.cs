using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ManagerBase<T, StateType> : Singleton<T> where T: Component
{
    public StateType state { get; protected set; }

    public event OnStateChangeHandler OnStateChange;
    public delegate void OnStateChangeHandler();

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
    }

    public virtual void SetState(StateType newState) {
        state = newState;
        OnStateChange?.Invoke();
    }
}
