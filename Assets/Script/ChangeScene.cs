using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    /**
     * Description : Allow to change scene
     * @author : <b>Bureau Bastien</b>
     * @param SceneName : Enter the name of the scene
     */
    public void LoadingScene(string SceneName)
    {
        //We get in parameter the Scene name to load it and show it.
        SceneManager.LoadScene(SceneName);
        
    }
}
