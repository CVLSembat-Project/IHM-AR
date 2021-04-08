using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TrackablePicture : MonoBehaviour
{
    public string[] imageToGetResult;
    WebRequest request;
    PieChart chart;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

    }

    // Update is called once per frame
    void Update()
    {
        request = GetComponent<WebRequest>();
        for(int i = 0; i < imageToGetResult.Length; i++)
        {
            switch (imageToGetResult[i])
            {
                case "ImageTargetElectrique" :
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = Constante.PERCENTAGE + "/electricite/7"; //Link for percentage electricity
                        chart.makeGraph(request.getPercentageOfBatiment(), request.getBatimentCount());

                    }
                    break;
                case "ImageTargetEau":
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = Constante.PERCENTAGE + "/eau/7"; //Link for percentage water
                        chart.makeGraph(request.getPercentageOfBatiment(), request.getBatimentCount());
                        
                    }
                    break;
                case "ImageTargetGaz":
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = Constante.PERCENTAGE + "/gaz/7"; //Link for percentage gaz
                        chart.makeGraph(request.getPercentageOfBatiment(), request.getBatimentCount());

                    }
                    break;
            }
        }

    }

    private bool isTrackingMarker(string imageTargetName)
    {
        var imageTarget = GameObject.Find(imageTargetName);
        var trackable = imageTarget.GetComponent<TrackableBehaviour>();
        var status = trackable.CurrentStatus;
        return status == TrackableBehaviour.Status.DETECTED;
    }


}
