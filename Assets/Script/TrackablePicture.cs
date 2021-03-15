using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class TrackablePicture : MonoBehaviour
{
    public string[] imageToGetResult;
    public UnityEngine.UI.Image wedgePrefab;
    WebRequest request;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        makeGraph();

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
                        Debug.Log(isTrackingMarker(imageToGetResult[i]));

                    }
                    break;
                case "ImageTargetEau":
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = ""; //Link for percentage water
                        Debug.Log(isTrackingMarker(imageToGetResult[i]));

                    }
                    break;
                case "ImageTargetGaz":
                    if (isTrackingMarker(imageToGetResult[i]))
                    {
                        request.categorie = ""; //Link for percentage gaz
                        Debug.Log(isTrackingMarker(imageToGetResult[i]));

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

    void makeGraph()
    {
        byte[] red = {100, 100, 100, 0, 0, 0,100 };
        byte[] green = { 0, 40, 75, 100, 100, 0, 100 };
        byte[] blue = {0,0,0,0,100,100,100 };

        for (int i = 1; i < request.batimentCount + 1; i++)
        {
            UnityEngine.UI.Image newWedge = Instantiate(wedgePrefab) as UnityEngine.UI.Image;
            newWedge.name = "PieChart" + (char)64 + i;
            newWedge.transform.SetParent(transform, false);
            newWedge.color = new Color32(red[i], green[i], blue[i], 0);
            

        }
    }
}
