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
    private long time = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //To take time when the variable is init

    public class Root
    {
        public string IDBatiment { get; set; }
        public string nomBatiment { get; set; }
    }

    // Update is called once per frame
    void Update()
    {
        long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //Take the time of the system
        if (currentTime - time >= 10*1000) //For make a request every 15s
        {
            //After the condition we take the current time to reinit the time
            time = currentTime;
            ShowResult();
        }
        
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
                    return result;
                    
                }
            }
        }
    }
}
