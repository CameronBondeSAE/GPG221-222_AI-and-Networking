using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewScene : MonoBehaviour
{
    public float timer = 10f;

    // Start is called before the first frame update
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            SceneManager.LoadScene(8);
        }
    }

    
}
