using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class HttpRequest : MonoBehaviour
{
    public Text text; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var task = Task.Run<string>(() =>
        {
            return DownloadDataAsync();
        });
        text.text = task.Result;
    }

    static async Task<string> DownloadDataAsync()
    {
        // notre cible
        string page = "http://172.19.127.23/API/hello/Bastien";

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
