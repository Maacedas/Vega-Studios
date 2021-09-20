using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Rival: MonoBehaviour {
    public int rivalValue;
    public Animator anim;
    public TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start() {
        rivalValue=Random.Range(13, 19);
        valueText.text=rivalValue.ToString();
    }

    // Update is called once per frame
    void Update() {

    }
}
