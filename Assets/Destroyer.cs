using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : Activatable
{
    public List<GameObject> objectsToDestroy; 
    public override void Activate()
    {
        if(objectsToDestroy != null){
            foreach(GameObject go in objectsToDestroy){
                if(go != null)
                    GameObject.Destroy(go);
            }
        }
        Finish();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
