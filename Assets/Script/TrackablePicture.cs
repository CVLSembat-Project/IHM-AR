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
        request = GetComponent<WebRequest>();
        chart = GameObject.Find("PieChartForImageTarget").GetComponent<PieChart>();
        chart.makeGraph(request.valuesOfBatiments, request.valuesOfBatiments.Count);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        if(chart.elementsOfChart.Count > 0) chart.ClearPieChart(chart.elementsOfChart);
    }

}