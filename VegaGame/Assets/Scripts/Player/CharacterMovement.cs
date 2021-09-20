using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class CharacterMovement: MonoBehaviour {
    public static Action OnGameStart = delegate { };

    public Animator charAnimator;
    public float speed;
    public float sideSpeed;
    public ParticleSystem confettiGreen;
    public ParticleSystem confettiRed;
    private bool isGameFinished;
    private bool isGameStarted;
    private bool onFinishLine;
    [SerializeField]
    private float minX;
    [SerializeField]
    private float mouseStartPosX;
    private float charStartPosX;
    private bool surfing;
    private void OnEnable() {
        OnGameStart+=StartToPlay;
        FinishEvent.OnGameFinished+=GameFinished;
        FinishEvent.OnFinishLine+=ReachedFinishLine;
        SurfStart.surfStart+=Surfing;
        SurfEnd.surfEnd+=SurfEnded;
    }
    private void OnDisable() {
        OnGameStart-=StartToPlay;
        FinishEvent.OnGameFinished-=GameFinished;
        FinishEvent.OnFinishLine-=ReachedFinishLine;
        SurfStart.surfStart-=Surfing;
        SurfEnd.surfEnd-=SurfEnded;
    }

    private void Start() {
        isGameStarted=false;
    }

    void Update() {
        if(!isGameFinished&&!onFinishLine) {
            Movement();
        }

        if(isGameStarted&&!onFinishLine) {
            transform.DOMoveZ(transform.position.z+0.4f*speed, 0.3f);
        }
    }

    public void StartToPlay() {
        isGameStarted=true;
        charAnimator.SetBool("Walking", true);
        UIManager.I.SetState(UIState.STARTED);

    }
    void Movement() {
        if(Input.GetMouseButtonDown(0)) {
            OnGameStart?.Invoke();


            if(surfing) {
                charAnimator.SetBool("Walking", true);
            }

            mouseStartPosX=Input.mousePosition.x;
            charStartPosX=transform.position.x;
        } else if(Input.GetMouseButton(0)) {
            float spd = surfing ? speed*2f : speed;
            transform.Translate(Vector3.forward*spd*Time.deltaTime);
            float mousePosX = Input.mousePosition.x;
            float screenMiddle = Screen.width/2;
            float mousePos;

            if(mousePosX<screenMiddle) {
                mousePos=-(screenMiddle-Input.mousePosition.x);
            } else {
                mousePos=Input.mousePosition.x-screenMiddle;
            }

            float touchGap = mousePosX-mouseStartPosX;
            float dest = (8f*touchGap)/screenMiddle;
            float destination = charStartPosX+dest;

            float sideSpd = surfing ? sideSpeed/2f : sideSpeed;

            if(mousePos<0) {
                transform.position=new Vector3(Mathf.Lerp(transform.position.x, destination, sideSpd*Time.deltaTime), transform.position.y, transform.position.z);
            } else if(mousePos>0) {
                transform.position=new Vector3(Mathf.Lerp(transform.position.x, destination, sideSpd*Time.deltaTime), transform.position.y, transform.position.z);
            } else if(mousePos==0) {
                transform.position=new Vector3(Mathf.Lerp(transform.position.x, 0, sideSpd*Time.deltaTime), transform.position.y, transform.position.z);
            }

            if(transform.position.x>=minX) {
                transform.position=new Vector3(minX, transform.position.y, transform.position.z);
            }

            if(transform.position.x<=-minX) {
                transform.position=new Vector3(-minX, transform.position.y, transform.position.z);
            }

            //Surfing
            if(surfing) {
                Vector3 lookPos = new Vector3(destination, transform.position.y, transform.position.z+10f);
                Quaternion lookRot = Quaternion.LookRotation(lookPos-transform.position, Vector3.up);
                //transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, 100f * Time.deltaTime);
                transform.rotation=lookRot;
            }
        } else if(Input.GetMouseButtonUp(0)) {
            charAnimator.SetBool("Walking", false);
        }
    }

    void GameFinished() {
        isGameFinished=true;
    }

    void ReachedFinishLine() {
        onFinishLine=true;
        isGameStarted=false;
        charAnimator.SetBool("Walking", false);
        //charAnimator.SetBool("Idle",true);
        charAnimator.SetBool("Throw", true);
        UIManager.I.SetState(UIState.FINISHLINE);

        if(CardNumberManager.I.rival.rivalValue>CardNumberManager.I.currentNumber||CardNumberManager.I.currentNumber>21) {
            StartCoroutine(LevelFinisher(false));
            CardNumberManager.I.rival.anim.SetBool("Victory", true);
            StartCoroutine(CardNumberManager.I.MoveChips(false));
        } else {
            StartCoroutine(LevelFinisher(true));
            CardNumberManager.I.rival.anim.SetBool("Defeat", true);
            StartCoroutine(CardNumberManager.I.MoveChips(true));
        }
    }

    void Surfing() {
        surfing=true;
        charAnimator.SetBool("Surfing", true);
    }

    void SurfEnded() {
        surfing=false;
        transform.rotation=Quaternion.identity;
        charAnimator.SetBool("Surfing", false);
    }

    public void BlastParticle(bool positive) {
        if(positive)
            confettiGreen.Play();
        else {
            confettiRed.Play();
        }
    }

    IEnumerator LevelFinisher(bool isWon) {
        yield return new WaitForSeconds(1);

        if(isWon) {
            UIManager.I.SetState(UIState.WON);
        } else {
            UIManager.I.SetState(UIState.LOST);
        }
    }
}
