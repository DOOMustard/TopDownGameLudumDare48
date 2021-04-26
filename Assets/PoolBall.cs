using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBall : Activatable
{
    public float speedMultiplier= 1;
    public MouseTracker mouseTracker;
    public float threshold= 1;
    public bool hasFired, isLoaded, isReadied= false;
    public Vector2 initialVelocity;
    public Vector2 currentVelocity;
    public bool forceReload= false; //Useful for debuging
    void Start()
    {
        if(mouseTracker == null){
            mouseTracker= GameObject.Find("MouseTracker")?.GetComponent<MouseTracker>();
        }
        
        //Reload();
    }
    void FixedUpdate(){
        if(hasFired){
            float dt= Time.deltaTime;
            if(mouseTracker != null){
                dt*= mouseTracker.masterTime;
            }
            transform.position+= (Vector3)currentVelocity * dt;
            currentVelocity= calculateNextVelocity(currentVelocity, transform, dt, true);
            if(forceReload){
                forceReload= false;
                hasFired= false;
                Reload();
                transform.position= Vector3.zero;
                transform.Translate(Vector3.zero);
            }
            //otherUpdates;
        }
    }
    public virtual Vector2 calculateNextVelocity(Vector2 currentVelocity, Transform transform, float timefactor, bool fired= false){
        if(ModifyNextVector != null){
            PoolBallData pbd= new PoolBallData(currentVelocity, transform, timefactor, fired);
            ModifyNextVector(pbd);
            return pbd.velocity;
        }
        return currentVelocity;
    }
    public event System.Action<PoolBallData> ModifyNextVector;
    public void Reload(){
        hasFired= false;
        forceReload= false;
        if(mouseTracker == null){
            mouseTracker= GameObject.Find("MouseTracker")?.GetComponent<MouseTracker>();
        }
        if(mouseTracker != null){
            mouseTracker.Ready+= this.Ready;
            isLoaded= true;
        }
    }
    public void Ready(MouseTracker mt){
        if(mouseTracker == null){
            mouseTracker= GameObject.Find("MouseTracker")?.GetComponent<MouseTracker>();
        }
        mouseTracker.Ready-= this.Ready;
        mouseTracker.Aim+= this.Aim;
        mouseTracker.Fire+= this.Fire;
        isReadied= true;
        if(OnReady != null)
            OnReady();
    }
    public void Aim(Vector2 v2){
        initialVelocity= v2 * speedMultiplier;
        if(OnAim != null)
            OnAim();
    }
    public void Fire(Vector2 v2){
        if(mouseTracker != null){
            mouseTracker.Fire-= this.Fire;
            mouseTracker.Aim-= this.Aim;
        }
        if(v2.magnitude < threshold){
            isLoaded= true;
            isReadied= false;
            if(mouseTracker != null){
                mouseTracker.Fire-= this.Fire;
                mouseTracker.Aim-= this.Aim;
                mouseTracker.Ready+= this.Ready;
            }
            return;
        }
        Debug.Log("Fire"+v2);
        hasFired= isLoaded && isReadied;
        isLoaded= false;
        isReadied= false;
        initialVelocity= v2 * speedMultiplier;
        currentVelocity= v2 * speedMultiplier;
        if(hasFired && OnFire!= null){
            OnFire();
        }
        if(hasFired){
            Finish();
        }
    }

    public override void Activate(){
        Reload();
    }
    public event System.Action OnReady, OnAim, OnFire; //make noise, score points  probably
}
public class PoolBallData{
    public PoolBallData(Vector2 vel, Transform trans, float dt, bool fired){
        velocity= vel;
        transform= trans;
        deltaTime= dt;
        hasFired= fired;
    }
    public Vector2 velocity;
    public Transform transform;
    public float deltaTime;
    public bool hasFired;
    void OnCollisionEnter2D(Collision2D collide){}
}
