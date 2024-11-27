using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Timed_Button : MonoBehaviour
{
    //Used to change color for different states
    public Renderer buttonRenderer;

    private bool isButtonPressed = false;

    public float RefreshTimer = 5f;

    public enum buttonStates
    {
        Default,
        Pressed
    }

    public buttonStates state;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == buttonStates.Default)
        {
            TimerButtonDefault();
        }
        if (state == buttonStates.Pressed)
        {
            TimerButtonPressed();
        }
    }

    //When hitting button Change state to Pressed.
    public void OnTriggerEnter(Collider other)
    {
        //If object with Player Tag and the Button is in Default state allow for button press to happen
        if (other.CompareTag("Player") && state == buttonStates.Default)
        {
            state = buttonStates.Pressed;
        }
    }

    //Used To Change the Timer Button Material Color to show changes in states.
    public void ChangeTimerButtonColors(Color color)
    {
        buttonRenderer.material.color = color;
    }

    //Change Button Color to Blue to show change in State and allow for button to be pressed again.
    public void TimerButtonDefault()
    {
        ChangeTimerButtonColors(Color.blue);
    }

    //Change Button Color to Red to show change in State and start cooldown timer.
    public void TimerButtonPressed()
    {
        //Change to red color to display button being pressed
        ChangeTimerButtonColors(Color.red);
        isButtonPressed = true;

        //If button has been pressed start 5 second cooldown on pressing button.
        if (isButtonPressed == true)
        {
            RefreshTimer -= Time.deltaTime;

            //Once 5 seconds are over reset button back to default state.
            if (RefreshTimer <= 0)
            {
                state = buttonStates.Default;
            }

        }
    }
}
