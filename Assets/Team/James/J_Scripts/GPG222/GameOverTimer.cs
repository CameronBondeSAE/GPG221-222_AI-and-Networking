using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JamesKilpatrick
{
    public class GameOverTimer : MonoBehaviour
    {
        // Variables
        public float currentTimer;
        public float maxTimeLimit = 5f;
        // Use a bool here to keep track of the state of the timer. eg "bool timerActive"
        // For now, you will have to tick on the timer in the Unity inspector. Later we can add a coded starting sequence like "GET READY!!!"


        // Functions
        // Start
        public void StartTimer()
        {
            // Set the timerActive bool
        }
        // End
        public void EndTimer()
        {
            // Set the timerActive bool
        }


        // EndGame
        public void EndGame()
        {
            // ResetScene (replace the string with your scene file name)
            SceneManager.LoadScene("Main");
        }

        // Update is called once per frame
        void Update()
        {
            // deltaTime is Unity keeping track of time passed
            currentTimer = currentTimer + Time.deltaTime;


            // Add 'near the maxTimeLimit' warning

            // Check actual final limit
            if (currentTimer >= maxTimeLimit)
            {
                EndGame();
            }
        }
    }
}


