using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour
{
    public void ToggleValueChanged(Toggle toggle)
    {
        //Initialize the background of the application as GameObject and find is Name
        GameObject background = GameObject.Find("BackgroundMenu");
        //If toggle is on we get the component as Renderer and we disable is renderer
        //So the background is Hide
        if (toggle.isOn) background.GetComponent<Renderer>().enabled = false;
        //We activate the background
        else background.GetComponent<Renderer>().enabled = true;
    }
}