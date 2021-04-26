using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deactivator : Activatable
{
    public Activatable activatable;

    public override void Activate(){
        if(activatable != null){
            activatable.DoneActivating+= Finish;
            activatable.Deactivate();
        }
    }
    public override void Finish(){
        if(activatable != null){
            activatable.DoneActivating-= Finish;
        }
        base.Finish();
    }
}
