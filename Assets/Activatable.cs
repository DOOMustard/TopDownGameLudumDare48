using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour
{
    public bool startActivated;
    void Start(){
        if(startActivated){
            Activate();
        }
    }
    public abstract void Activate();
    public virtual void Deactivate(){
        //not all will need this but having this may be useful
        Finish();
    }
    public virtual void Finish(){
        if(DoneActivating != null)
            DoneActivating();
    }
    public event System.Action DoneActivating;
}
