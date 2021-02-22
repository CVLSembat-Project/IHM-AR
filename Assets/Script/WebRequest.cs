using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class WebRequest : MonoBehaviour
{
    public Text text;
    // Update is called once per frame
    void Update()
    {
        print("début de séance de requête");
        StartCoroutine(GetRequest("http://172.19.127.23/Batiment/A/gaz"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error" + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived : " + webRequest.downloadHandler.text);
                text.text = webRequest.downloadHandler.text;
            }
        }
    }
}
