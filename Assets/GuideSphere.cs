using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideSphere : MonoBehaviour
{
    public SpriteRenderer sr;
    public Color color;
    public Guide master;
    public GuideSphere previous, next;
    public Vector2 velocity;
    void Start(){
        sr= GetComponent<SpriteRenderer>();
    }
    public void OnReady(){
        //setVisible;
        sr.color= color;
        next?.OnReady();
    }
    public void OnAim(){
        if(previous != null){
            assign(previous.velocity, previous.transform);
        }else if(master!=null){
            assign(master.poolBall.initialVelocity, master.poolBall.transform);
        }
        next?.OnAim();
    }
    public void assign(Vector2 v, Transform t){
        velocity= new Vector2(v.x, v.y);
        this.transform.position= t.position;
        for(int i=0; i<master.calcPer; i++){
            transform.position+= (Vector3)velocity * master.deltaTime;
            velocity= master.poolBall.calculateNextVelocity(velocity, transform, master.deltaTime, true);
        }
    }
    public void OnFire(){
        //setInvisible
        
        sr.color= new Color(0,0,0,0);
        next?.OnFire();
    
    }

    
}
