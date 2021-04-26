using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveForce : ForceController
{
    public Vector2 amplitude;
    public Vector2 wavelength;
    public float frequency;
    
    
    // Update is called once per frame
    public override void Modifier(PoolBallData pbd){
        float waveTime= Vector2.Dot(wavelength.normalized, (Vector2)pbd.transform.position)/wavelength.magnitude + Time.time * frequency;
        pbd.velocity+= Mathf.Sin(waveTime)*amplitude * pbd.deltaTime; 

        //Debug.Log("This was called:" +acc3);
    }
}
