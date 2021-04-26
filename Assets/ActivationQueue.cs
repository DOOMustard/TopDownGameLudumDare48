using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationQueue : Activatable
{
    public List<Activatable> activationQueue;
    public int i= -1;
    
    [SerializeField]
    private Activatable current;
    // Start is called before the first frame update
    public Guide guide;
    public override void Activate(){
        ActivateMyNext();
    }
    public void ActivateMyNext(){
        if(current != null){
            current.DoneActivating-= this.ActivateMyNext;
        }
        i++;
        if(i < activationQueue.Count){
            current= activationQueue[i];
        }else{
            current= null;
            i= -1;
        }
        if(current == null){
            Finish();
        }else{
            if((guide != null) && (current is PoolBall pb)){
                guide.poolBall= pb;
            }
            if((guide != null) && (current is ActivationQueue aq)){
                if(aq.guide == null){
                    aq.guide= this.guide;
                }
            }
            current.DoneActivating+= this.ActivateMyNext;
            current.Activate();
        }


    }
}
