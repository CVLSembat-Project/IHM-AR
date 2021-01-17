using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    public void LoadingScene(string SceneName)
    {
        //We get in parameter the Scene name to load it and show it.
        SceneManager.LoadScene(SceneName);
    }
}
