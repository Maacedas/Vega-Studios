using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurfBoardFollow : MonoBehaviour
{
    private GameObject player;
    private CharacterMovement charMov;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerParent");
        charMov = FindObjectOfType<CharacterMovement>();
    }

    private void OnEnable()
    {
        SurfStart.surfStart += SurfStarted;
        SurfEnd.surfEnd += SurfEnded;
    }

    private void OnDisable()
    {
        SurfStart.surfStart -= SurfStarted;
        SurfEnd.surfEnd -= SurfEnded;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.transform.rotation;
    }

    void SurfStarted()
    {
        Vector3 playerPos = player.transform.position;
        transform.position = new Vector3(playerPos.x, transform.position.y, playerPos.z);
        transform.SetParent(player.transform);
    }

    void SurfEnded()
    {
        transform.parent = null;
    }
}
