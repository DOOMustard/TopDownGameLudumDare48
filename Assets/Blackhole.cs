using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : GravityForce
{
    public Activatable activateOnCollision;
    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision){
        if(isActivated){
            GameObject other= collision.gameObject;
            PoolBall pb=other.GetComponent<PoolBall>();
            if(affected.Contains(pb)){
                GameObject.Destroy(other);
                if(activateOnCollision != null){
                    activateOnCollision.Activate();
                }
            }
        }
        
        
    }
}
