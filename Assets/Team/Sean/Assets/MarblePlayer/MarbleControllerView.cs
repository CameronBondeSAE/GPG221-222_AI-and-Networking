using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleControllerView : MonoBehaviour
{
    [SerializeField] GameObject noisePrefab;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void JumpEffects()
    {
        //Debug.Log("JumpEffect");
        Instantiate(noisePrefab, transform.position, Quaternion.identity);
    }
}
