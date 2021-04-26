using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityActivator : Activatable
{
    public float time;
    public SpriteRenderer sr;
    public bool hasActivated;
    // Start is called before the first frame update
    void Start()
    {
        sr= GetComponent<SpriteRenderer>();
        if(!startActivated){
            sr.color= new Color(sr.color.r, sr.color.g, sr.color.b, 0);
        }
        hasActivated= startActivated;
    }
    IEnumerator FadeIn(){
        if(!hasActivated){
            hasActivated= true;
            yield return null;
            if(time <= 0){
                time= 1;
            }
            if(sr == null){
                Debug.Log(this.name);
            }
            for(float ft= 0; ft<=1; ft+= Time.deltaTime/time){
                sr.color= new Color(sr.color.r, sr.color.g, sr.color.b, ft);
                yield return null;
            }
        }
        sr.color= new Color(sr.color.r, sr.color.g, sr.color.b, 1);
    }
    
    IEnumerator FadeOut(){
        if(hasActivated){
            hasActivated= false;
            yield return null;
            if(time <= 0){
                time= 1;
            }
            if(sr == null){
                Debug.Log(this.name);
            }
            for(float ft= 1; ft >= 0; ft-= Time.deltaTime/time){
                sr.color= new Color(sr.color.r, sr.color.g, sr.color.b, ft);
                yield return null;
            }
        }
        sr.color= new Color(sr.color.r, sr.color.g, sr.color.b, 0);
    }
    public override void Activate(){
        StartCoroutine(FadeIn());
        Finish();
    }
    public override void Deactivate()
    {
        StartCoroutine(FadeOut());
        Finish();
    }
}
