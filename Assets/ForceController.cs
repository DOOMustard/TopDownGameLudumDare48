using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ForceController : Activatable
{
    public bool isActivated;
    public List<PoolBall> affected;
    
    public override void Activate()
    {
        if(!isActivated){
            isActivated= true;
            foreach(PoolBall pb in affected){
                if(pb!= null)
                    pb.ModifyNextVector+= Modifier;
            }
        }
        Finish();
    }
    public void ApplyTo(PoolBall toAdd){
        if(toAdd == null)
            return;
        affected.Add(toAdd);
        if(isActivated){
            toAdd.ModifyNextVector+= Modifier;
        }
    }
    public override void Deactivate(){
        if(isActivated){
            isActivated= false;
            foreach(PoolBall pb in affected){
                if(pb != null)
                    pb.ModifyNextVector-= Modifier;
            }
        }
        Finish();

    }
    public void OnDestroy(){
        if(isActivated){
            foreach(PoolBall pb in affected){
                if(pb != null)
                    pb.ModifyNextVector-= Modifier;
            }
        }
    }
    // Update is called once per frame
    public abstract void Modifier(PoolBallData pbd);
}
