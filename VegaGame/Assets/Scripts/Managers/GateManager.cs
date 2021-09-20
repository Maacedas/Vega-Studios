using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager: MonoBehaviour {
    [Header("Refs")]
    public GameObject leftGate;
    public GameObject rightGate;

    [Header("Settings")]
    [SerializeField]
    private int leftGateValue;
    [SerializeField]
    private int rightGateValue;

    void Start() {
        InitGateSettings();
    }

    void InitGateSettings() {
        leftGate.GetComponent<GateHandler>().GateValue=leftGateValue;
        rightGate.GetComponent<GateHandler>().GateValue=rightGateValue;
        leftGate.GetComponent<GateHandler>().InitGateSettings();
        rightGate.GetComponent<GateHandler>().InitGateSettings();

    }
    void Update() {
        if(leftGate.GetComponent<GateHandler>().IsCollided||rightGate.GetComponent<GateHandler>().IsCollided) {
            leftGate.GetComponent<BoxCollider>().enabled=false;
            rightGate.GetComponent<BoxCollider>().enabled=false;
        }
    }

    public void ResetGateHandlers() {
        foreach(Transform child in transform) {
            child.GetComponent<GateHandler>().IsCollided=false;
            child.GetComponent<BoxCollider>().enabled=true;
        }
    }
}
