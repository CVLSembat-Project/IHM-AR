﻿using System.Collections;
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
    public Text textOfElements;
    public Slider slider;
    private long time = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //To take time when the variable is init

    //Field where we get value of JSON
    static public int batimentCount;
    static public string nameOfBatiment;
    static public List<float> percentageOfBatiments;

    //Array to get lot of JSON object
    static List<Mesures> mesures = new List<Mesures>();

    void Awake()
    {
        StartCoroutine(GetRequest(URL + categorie));
    }

    // Update is called once per frame
    void Update()
    {
        long currentTime = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //Take the time of the system
        if (currentTime - time >= 10 * 1000) //For make a request every 15s
        {
            //After the condition we take the current time to reinit the time
            time = currentTime;
            //TODO search more information on Coroutine
            StartCoroutine(GetRequest(URL + categorie));
        }
        

    }

    /**
     * Description : <b>GetRequest</b> allow us to recover the value of JSON Object and show there values  
     * @author : <b>Bureau Bastien</b>
     * @param uri : The link for doing an http request
     * @param unite : Show if we want to have unite after the value
     * @return : A web Request
    */
    IEnumerator GetRequest(string uri)
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
                textOfElements.text = webRequest.error;
            }
            else
            {
                //Deserialize JSON Object
                mesures = JsonConvert.DeserializeObject<List<Mesures>>(webRequest.downloadHandler.text);
                percentageOfBatiments = new List<float>();
                foreach (Mesures mesure in mesures)
                {
                    switch (categorie)
                    {
                        case Constante.NAME_BATIMENT :
                            textOfElements.text = "Batiment : " + mesure.nomBatiment;
                            break;

                        case Constante.ELECTRICITY:
                        case Constante.WATER:
                        case Constante.GAZ:
                            slider.value = mesure.valeur;
                            textOfElements.text = mesure.valeur.ToString() + " " + mesure.unite;
                            break;
                        case Constante.NB_BATIMENTS:
                            batimentCount = mesure.nbBatiments;
                            break;
                        case Constante.PERCENTAGE + "/gaz/7":
                        //case Constante.PERCENTAGE + "/eau/7":
                        //case Constante.PERCENTAGE + "/electricite/7":
                            percentageOfBatiments.Add(Mathf.Round(mesure.pourcentage));
                            break;
                    }
                        
                }
   
            }
            
        }
    }

    public int getBatimentCount()
    {
        return batimentCount;
    }

    public string getNameOfBatiments()
    {
        return nameOfBatiment;
    }

    public List<float> getPercentageOfBatiment()
    {
        return percentageOfBatiments;
    }

    public int setBatimentCount(int value)
    {
        return batimentCount = value;
    }

    public string setNameOfBatiments(string text)
    {
        return nameOfBatiment = text;
    }

    public List<float> setPercentageOfBatiment(List<float> values)
    {
        return percentageOfBatiments = values;
    }
}
