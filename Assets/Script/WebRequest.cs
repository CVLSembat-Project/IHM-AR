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
    static ChangeIP IPAdress;
    public string URL;
    public string categorie;
    public Text textOfElements;
    public Slider slider;
    private long time = DateTimeOffset.Now.ToUnixTimeMilliseconds(); //To take time when the variable is init

    //Field where we get value of JSON
    static public int batimentCount;
    static public string nameOfBatiment;
    public string unite;
    public List<float> valuesOfBatiments;
    public List<string> types;

    //Array to get lot of JSON object
    static List<Mesures> mesures = new List<Mesures>();

    void Awake()
    {
        URL = "http://" + PlayerPrefs.GetString("adresse") + "/API/";
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
            //Start of an execution of the script
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
            while (!webRequest.isDone) yield return null;
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                textOfElements.text = webRequest.error;
            }
            else
            {
                //Deserialize JSON Object
                mesures = JsonConvert.DeserializeObject<List<Mesures>>(webRequest.downloadHandler.text);
                //Initialization
                valuesOfBatiments = new List<float>();
                types = new List<string>();
                slider = gameObject.GetComponent<Slider>();

                foreach (Mesures mesure in mesures)
                {
                    if (webRequest.responseCode == 200) //If the http response is 200, we get the JSON data from API-REST
                    {
                        switch (categorie) //For sorts the differents path
                        {
                            case Constante.ELECTRICITY:
                            case Constante.WATER:
                            case Constante.GAZ:
                                slider.value = mesure.valeur;
                                textOfElements.text = mesure.valeur.ToString() + " " + mesure.unite;
                                GameObject.Find("BatimentText").GetComponent<Text>().text = "Batiment : " + mesure.nomBatiment;
                                break;
                            case Constante.NB_BATIMENTS:
                                batimentCount = mesure.nbBatiments;
                                break;
                            case Constante.PERCENTAGE + "/gaz/7":
                            case Constante.PERCENTAGE + "/eau/7":
                            case Constante.PERCENTAGE + "/electricite/7":
                                valuesOfBatiments.Add(Mathf.Round(mesure.pourcentage));
                                break;
                            case Constante.TYPE_PERCENTAGE:
                                valuesOfBatiments.Add(Mathf.Round(mesure.pourcentage));
                                types.Add(mesure.nomType);
                                break;
                            case Constante.GRAPH_GAZ + "/A/7":
                            case Constante.GRAPH_EAU + "/A/7":
                            case Constante.GRAPH_ELEC + "/A/7":
                                valuesOfBatiments.Add(mesure.valeur);
                                unite = mesure.unite;
                                break;
                        }
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

    public List<float> getValuesOfBatiments()
    {
        return valuesOfBatiments;
    }

}
