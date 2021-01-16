using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    private AssetBundle loadedScene;
    

    // Start is called before the first frame update
    void Start()
    {
        loadedScene = AssetBundle.LoadFromFile("Assets/Scenes");
    }

    public void LoadingScene(string SceneName)
    {
        SceneName = "Visualisation";
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
