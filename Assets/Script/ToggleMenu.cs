using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMenu : MonoBehaviour
{
    public Toggle toggle;
    public GameObject background;

    public void ToggleValueChanged()
    {
        toggle = GetComponent<Toggle>();
        if (toggle.isOn) background.GetComponentInParent<Renderer>().enabled = false ;
        else background.GetComponentInParent<Renderer>().enabled = true;
    }
}