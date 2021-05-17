using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TrackablePicture : DefaultTrackableEventHandler
{
    PieChart chart;
    WebRequest request;

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        chart.enabled = true;
        request = GetComponent<WebRequest>();
        chart = GameObject.Find("PieChartForImageTarget").GetComponent<PieChart>();
        chart.makeGraph(request.valuesOfBatiments, request.valuesOfBatiments.Count);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        chart = GameObject.Find("PieChartForImageTarget").GetComponent<PieChart>();
        chart.enabled = false;
    }

}