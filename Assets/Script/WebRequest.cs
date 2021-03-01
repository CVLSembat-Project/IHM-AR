using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using static Mesures;
using System.Collections.Generic;



public class WebRequest : MonoBehaviour
{
    const string URL = "http://172.19.6.102/API/";
    public string categorie;
    public bool unite;
    public Text txt ;
    public GameObject gameJauge;
    private long time = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //To take time when the variable is init
    private JaugeVariance jauge = new JaugeVariance();

    static List<Mesures> mesures = new List<Mesures>();

    // Update is called once per frame
    void Update()
    {
        long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //Take the time of the system
        if (currentTime - time >= 10 * 1000) //For make a request every 15s
        {
            //After the condition we take the current time to reinit the time
            time = currentTime;
            print("début de séance de requête");
            StartCoroutine(GetRequest(URL + categorie,unite));
        }
        

    }

    IEnumerator GetRequest(string uri, bool unite)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(pages[page] + ": Error" + webRequest.error);
                txt.text = webRequest.error;
            }
            else
            {
                mesures = JsonConvert.DeserializeObject<List<Mesures>>(webRequest.downloadHandler.text);
                foreach(Mesures mesure in mesures)
                {
                    if (!unite)
                    {
                        Debug.Log(pages[page] + ":\nReceived : " + mesure.nomBatiment);
                        txt.text = "Batiment : " + mesure.nomBatiment;
                    }
                    else
                    {
                        jauge.SetVariance(gameJauge, mesure.valeur, mesure.MesureTotal);
                        txt.text = mesure.valeur.ToString() + " " + mesure.unite;
                        Debug.Log(pages[page] + ":\nReceived : " + mesure.MesureTotal.ToString());
                    }
                        
                }
   
            }
            
        }
    }
}
