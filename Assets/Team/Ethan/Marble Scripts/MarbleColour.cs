using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarbleColour : MonoBehaviour
{
    public Button redColour;
    public Button greenColour;
    public Button blueColour;
    public Button yellowColour;

    private void Start()
    {
        redColour.onClick.AddListener(() => SetColor("Red"));
        greenColour.onClick.AddListener(() => SetColor("Green"));
        blueColour.onClick.AddListener(() => SetColor("Blue"));
        yellowColour.onClick.AddListener(() => SetColor("Yellow"));
    }

    public void SetColor(string color)
    {
        PlayerPrefs.SetString("SelectedColor", color);
        PlayerPrefs.Save();
        Debug.Log("Colour selected " + color);
    }
}
