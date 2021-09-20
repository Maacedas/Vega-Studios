using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGameFinish : MonoBehaviour
{
    public Rival rival;
    public Rigidbody [] chips;
    void Start()
    {
        CardNumberManager.I.rival=rival;
        CardNumberManager.I.chips=chips;
    }
}
