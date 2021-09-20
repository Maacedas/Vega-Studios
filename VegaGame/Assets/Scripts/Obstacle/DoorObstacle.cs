using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DoorObstacle : MonoBehaviour
{
    [SerializeField]
    private GameObject movingPart;
    [SerializeField]
    private float maxScale;
    [SerializeField]
    private float minScale;
    [SerializeField]
    private float speed;
    public static Action<GameObject> onHitDoor;
    // Start is called before the first frame update
    void Start()
    {
        Expand();
    }

    private void Expand()
    {
        movingPart.transform.DOScaleZ(maxScale, speed).OnComplete(Shrink);
    }

    private void Shrink()
    {
        movingPart.transform.DOScaleZ(minScale, speed).OnComplete(Expand);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent.GetComponent<Animator>().SetTrigger("Stumble");
            onHitDoor(other.gameObject);
        }
    }

}
