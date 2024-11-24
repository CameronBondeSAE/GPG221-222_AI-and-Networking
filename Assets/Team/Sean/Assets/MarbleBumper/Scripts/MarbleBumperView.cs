using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBumperView : MonoBehaviour
{
    public float bumpSize = 1.5f;
    Vector3 defScale;
    
    bool isActive = false;
    AudioSource sound;

    
    // Start is called before the first frame update
    void Start()
    {
        defScale = transform.localScale;
        sound = GetComponentInParent<AudioSource>();
    }

    public void PlayAnimation()
    {
        gameObject.transform.localScale = defScale * bumpSize;
        isActive = true;
        sound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, defScale, Time.deltaTime) ;
            if(gameObject.transform.localScale == defScale)
            {
                isActive = false;
            }
        }
    }
}
