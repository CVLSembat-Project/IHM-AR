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
    //Initialize field
    //Most of variables are in public for get access in Unity
    private const string URL = "http://172.19.6.102/API/";
    public string categorie;
    public bool unite;
    public Text txt;
    public Slider slider;
    private long time = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //To take time when the variable is init

    //Field where we get value of JSON
    public int batimentCount { get; set; }
    public string nameOfBatiment { get; set; }
    public float[] percentageOfBatiments {get; set;}

    //Array to get lot of JSON object
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
            //TODO search more information on Coroutine
            StartCoroutine(GetRequest(URL + categorie,unite));
        }
        

    }

    /**
     * Description : <b>GetRequest</b> allow us to recover the value of JSON Object and show there values  
     * @author : <b>Bureau Bastien</b>
     * @param uri : The link for doing an http request
     * @param unite : Show if we want to have unite after the value
     * @return : A web Request
    */
    IEnumerator GetRequest(string uri, bool unite)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            //We set an request Header to the website for get the content of the web page
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            //Show the concernated web page
            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(pages[page] + ": Error" + webRequest.error);
                txt.text = webRequest.error;
            }
            else
            {
                //Deserialize JSON Object
                mesures = JsonConvert.DeserializeObject<List<Mesures>>(webRequest.downloadHandler.text);
                foreach(Mesures mesure in mesures)
                {
                    if (!unite)
                    {
                        Debug.Log(pages[page] + ":\nReceived : " + mesure.nomBatiment);
                        //We get the name of the batiment
                        txt.text = "Batiment : " + mesure.nomBatiment;
                    }
                    else
                    {
                        //Allow to varying the slider with the value of JSON
                        slider.value = mesure.valeur;
                        txt.text = mesure.valeur.ToString() + " " + mesure.unite;
                        Debug.Log(pages[page] + ":\nReceived : " + mesure.valeur.ToString());
                        batimentCount = mesure.nbBatiments;
                        nameOfBatiment = mesure.nomBatiment;
                        percentageOfBatiments = mesure.pourcentage;
                    }
                        
                }
   
            }
            
        }
    }
}
