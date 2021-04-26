using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    public Color color;
    public GameObject guideSpherePrefab;
    public PoolBall _poolBall_;
    public PoolBall poolBall{
        set{
            if(_poolBall_ == value){
                return;
            }
            if(_poolBall_ != null){
                _poolBall_.OnFire-= this.OnFire;
                _poolBall_.OnAim-= this.OnAim;
                _poolBall_.OnReady-= this.OnReady;
            }
            _poolBall_= value;
            if(_poolBall_ != null){
                _poolBall_.OnFire+= this.OnFire;
                _poolBall_.OnAim+= this.OnAim;
                _poolBall_.OnReady+= this.OnReady;
            }
        }
        get{
            return _poolBall_;
        }
    }
    public GuideSphere first;
    public int calcPer, sphereCount;

    public float deltaTime;
    public MouseTracker mouseTracker;
    void Start(){
     
        if(mouseTracker == null){
            mouseTracker= GameObject.Find("MouseTracker")?.GetComponent<MouseTracker>();
        }
        
        //Reload();
        GameObject go= Instantiate(guideSpherePrefab);
        this.first= go?.GetComponent<GuideSphere>();
        GuideSphere prev= this.first;
        prev.master= this;
        prev.color= this.color;
        prev.transform.parent= this.transform;
        for(int i= 1; i<sphereCount; i++){

            go= Instantiate(guideSpherePrefab);
            prev.next= go?.GetComponent<GuideSphere>();
            prev.next.previous= prev;
            prev= prev.next;
            prev.master= this;
            prev.color= new Color(color.r, color.g, color.b, color.a*(sphereCount-i)/sphereCount);
            prev.transform.parent= this.transform;
        }
        //poolBall= transform.parent?.GetComponent<PoolBall>();
        //create Guide Spheres

    }
    void FixedUpdate(){
        if(mouseTracker != null){
            this.deltaTime= Time.deltaTime * mouseTracker.masterTime;
        }else{
            this.deltaTime= Time.deltaTime;
        }
    }
    void OnFire(){
        if(first != null)
            first.OnFire();
    }
    void OnAim(){
        if(first != null)
            first.OnAim();
    }
    void OnReady(){
        if(first != null)
            first.OnReady();
    }
}
