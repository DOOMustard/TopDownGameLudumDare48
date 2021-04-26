using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MouseTracker : MonoBehaviour
{
    public float masterTime= 1;
    public Vector2 initialMousePosition;

    public event System.Action<MouseTracker> Ready;
    public event System.Action<Vector2> Aim;
    public event System.Action<Vector2> Fire;
    public float velocityScalingFactor;

    // Start is called before the first frame update
    void OnMouseDown(){
        initialMousePosition= Input.mousePosition;
        if(Ready != null)
            Ready(this);
    }
    Vector2 GetVelocityFromMouse(Vector2 initial, Vector2 current){
        return (initial - current) * velocityScalingFactor;
    }
    void OnMouseDrag(){
        if(Aim != null)
            Aim(GetVelocityFromMouse(initialMousePosition, Input.mousePosition));
    }
    void OnMouseUp(){
        if(Fire != null)
            Fire(GetVelocityFromMouse(initialMousePosition, Input.mousePosition));
    }

    
}
