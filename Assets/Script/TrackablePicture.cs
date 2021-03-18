using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TrackablePicture : MonoBehaviour
{
    public string[] imageToGetResult;
    WebRequest request;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < imageToGetResult.Length; i++)
        {
            switch (imageToGetResult[i])
            {
                case "ImageTargetElectrique" :
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = ""; //Link for percentage electricity
                        //Debug.Log(isTrackingMarker(imageToGetResult[i]));

                    }
                    break;
                case "ImageTargetEau":
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = ""; //Link for percentage water
                        //Debug.Log(isTrackingMarker(imageToGetResult[i]));

                    }
                    break;
                case "ImageTargetGaz":
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = ""; //Link for percentage gaz
                        //Debug.Log(isTrackingMarker(imageToGetResult[i]));

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
