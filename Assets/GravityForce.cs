using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce : ForceController
{
    public float gravity;
    // Start is called before the first frame update
 
    // Update is called once per frame
    public override void Modifier(PoolBallData pbd){
        float dist= Vector3.Distance(pbd.transform.position,transform.position);
        float Acc= gravity/dist/dist;
        Vector3 acc3= Acc * Vector3.Normalize((transform.position - pbd.transform.position));
        pbd.velocity+= (Vector2)acc3 * pbd.deltaTime;
        //Debug.Log("This was called:" +acc3);
    }
}
