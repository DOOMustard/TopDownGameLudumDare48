using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : Activatable
{
    public GameObject cameraObject;
    public MouseTracker mt;
    public Camera camera;
    public GameObject levelReferance, nextLevelReferance;
    public bool success;
    public float slowdownTime, slowdown, time;
    public Activatable levelQueue;
    public Activatable successfulTransition, failedTransition;
    void Start(){
        
        if(cameraObject == null){
            cameraObject= GameObject.Find("Main Camera");
        }
        if(camera == null && cameraObject != null){

            camera=cameraObject.GetComponent<Camera>();
        }
        if(mt == null){
            mt= GameObject.Find("MouseTracker")?.GetComponent<MouseTracker>();
        }
        if(startActivated){
            Activate();
        }
    }
    public override void Activate()
    {
        if(levelQueue != null){
            levelQueue.DoneActivating+= CompleteLevel;
            levelQueue.Activate();
        }else{
            CompleteLevel();
        }
    }
    public event System.Action<Transition> SetUpTransition;
    void CompleteLevel(){
        
        if(levelQueue != null){
            levelQueue.DoneActivating-= CompleteLevel;
        }
        StartCoroutine(AnimateSlowdownDoChecks());
    }

    public void BeginAnimateLevelTransition(){
        if(success){
            if(successfulTransition != null){
                successfulTransition.DoneActivating-= BeginAnimateLevelTransition;
            }
        }else{
            if(failedTransition != null){
                failedTransition.DoneActivating-= BeginAnimateLevelTransition;
            }
        }
        StartCoroutine(AnimateTransition());
    }
    IEnumerator AnimateSlowdownDoChecks(){
        if(slowdownTime <= 0){
            slowdownTime= 1;
        }
        if(mt != null){
            for(float ft= 0; ft<= 1; ft+= Time.deltaTime/slowdownTime){
                mt.masterTime= Mathf.Lerp(1, slowdown, ft);
                yield return null;
            }
        }
        if(SetUpTransition != null){
            SetUpTransition(this);
        }
        if(success){
            if(successfulTransition != null){
                successfulTransition.DoneActivating+= BeginAnimateLevelTransition;
                successfulTransition.Activate();
            }else{
                BeginAnimateLevelTransition();
            }
        }else{
            if(failedTransition != null){
                failedTransition.DoneActivating+= BeginAnimateLevelTransition;
                failedTransition.Activate();
            }else{
                BeginAnimateLevelTransition();
            }
        }
    }

    IEnumerator AnimateTransition(){
        if(mt != null){
                mt.masterTime= slowdown;
                yield return null;
        }
        if(time <= 0){
            time= 1;
        }
        Vector3 startPos= (camera.transform.position);
        float startSize= camera.orthographicSize;
        if(levelReferance != null && nextLevelReferance != null){
            for(float ft= 0; ft<=1; ft+= Time.deltaTime/time){
                float endSize= startSize * levelReferance.transform.lossyScale.x/ nextLevelReferance.transform.lossyScale.y;
                Vector3 endPos= (startPos - (nextLevelReferance.transform.position))/startSize * endSize + (levelReferance.transform.position);
                endPos.z= startPos.z;
                float animationTiming= (ft * 2 - 1);
                
                if(ft < 0.5){
                    animationTiming= -Mathf.Pow(-animationTiming, 1f/1.25f);
                }else{
                    animationTiming= Mathf.Pow(animationTiming, 1f/1.25f);
                }
                animationTiming= (animationTiming + 1)/2;
                //Debug.Log(ft+","+animationTiming);
                camera.orthographicSize= Mathf.Lerp(startSize, endSize, animationTiming);
                Vector3 curPos= (Vector3) Vector3.Lerp(startPos, endPos, animationTiming);
                camera.transform.position= curPos;
                yield return null;
            }
            startPos.z= startPos.z- levelReferance.transform.position.z + nextLevelReferance.transform.position.z;
        }else{
            startPos.z+= 10;
        }
        camera.orthographicSize= startSize;
        camera.transform.position= startPos;
        mt.masterTime= 1;
        this.Finish();
    }
}
