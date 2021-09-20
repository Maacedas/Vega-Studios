using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateHandler: MonoBehaviour {
    [Header("Refs")]
    public TextMeshProUGUI gateValueText;
    public GameObject gateMat;
    public GameObject gateTransparent;
    public Material gateGreenMaterial;
    public Material gateRedMaterial;

    private Material gateMaterial;
    private Material transMat;
    private Color matColor;

    private int gateValue;
    private bool isCollided = false;
    public int GateValue {
        get => gateValue;
        set => gateValue=value;

    }

    public bool IsCollided {
        get => isCollided;
        set => isCollided=value;
    }

    void Start() {
        gateMaterial=gateMat.GetComponent<MeshRenderer>().material;
        transMat=gateTransparent.GetComponent<MeshRenderer>().material;
        InitGateSettings();
    }

    public void InitGateSettings() {
        gateValueText.SetText(GateValue.ToString());

        if(!gateMaterial)
            return;

        if(GateValue>0) {
            gateMaterial.color=Color.green;
            gateTransparent.GetComponent<MeshRenderer>().material=gateGreenMaterial;
        } else {
            gateMaterial.color=Color.red;
            gateTransparent.GetComponent<MeshRenderer>().material=gateRedMaterial;
        }
    }
    private void OnTriggerEnter(Collider col) {
        if(col.gameObject.CompareTag("Player")) {
            IsCollided=true;
            CardNumberManager.I.SetCurrentNumber(GateValue);
            if(GateValue>0)
                col.transform.root.gameObject.GetComponent<CharacterMovement>().BlastParticle(true);
            else
                col.transform.root.gameObject.GetComponent<CharacterMovement>().BlastParticle(false);
        }
    }
}
