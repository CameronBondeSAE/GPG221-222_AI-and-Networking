using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsingPlatform_View : MonoBehaviour
{
    [SerializeField] GameObject platforMmesh;
    [SerializeField] GameObject brokenplatformMesh;
    AudioSource sound;

    public void ChangeMesh()
    {
        platforMmesh.SetActive(false);
        brokenplatformMesh.SetActive(true);
        sound.Play();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        platforMmesh.SetActive(true);
        brokenplatformMesh.SetActive(false);
        sound = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
