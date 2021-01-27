using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class HttpRequest : MonoBehaviour
{
    public Text text;
    public string categorie;
    private IEnumerator coroutine;
    
    // Update is called once per frame
    void Update()
    {
        coroutine = WaitAndExecute(Time.deltaTime);
        StartCoroutine(coroutine);
    }

    private IEnumerator WaitAndExecute(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ShowResult();
    }

    void ShowResult()
    {
        var task = Task.Run<string>(() =>
        {
            return DownloadDataAsync(categorie);
        });
        text.text = task.Result;
    }

    
    static async Task<string> DownloadDataAsync(string parameter = "")
    {
        // notre cible
        string page = "http://172.19.127.23/" + parameter;

        using (HttpClient client = new HttpClient())
        {
            // autre possibilité
            //client.BaseAddress = new Uri(page);

            // on peut compléter le header
            //client.DefaultRequestHeaders.Add("X-TEST", "123");

            // la requête
            using (HttpResponseMessage response = await client.GetAsync(page))
            {

                using (HttpContent content = response.Content)
                {
                    // récupère la réponse, il ne resterai plus qu'à désérialiser
                    string result = await content.ReadAsStringAsync();
                    var Json = JSON.Parse(result);
                    Debug.Log(Json);
                    return Json;
                    
                }
            }
        }
    }
}
