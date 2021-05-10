using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIP : MonoBehaviour
{
    public string IP = "0.0.0.0";
    public GameObject inputFieldObj;

    public void setIpAdresse()
    {
        InputField inputField = inputFieldObj.GetComponent<InputField>();
        string text = inputField.text;
        if (string.IsNullOrEmpty(text))
        {
            Debug.Log("Error input is EMPTY");
            return;
        }
        Debug.Log(text);
        IP = text;
        PlayerPrefs.SetString("adresse", IP);
    }

    public string getIP()
    {

        return IP;
    }
}
