using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JaugeVariance : MonoBehaviour
{
    public void SetVariance(GameObject jauge, float value, float total)
    {
        float percentage = calculatePercentage(value, total);
        Vector3 jaugeTransform = jauge.transform.localScale;
        if (jaugeTransform.y > 0)
        {
            jaugeTransform += new Vector3(0, jaugeTransform.y * percentage, 0) * Time.deltaTime;
        }
        else jaugeTransform += new Vector3(0, 0, 0);
    }

    float calculatePercentage(float value, float total)
    {
        return (value / total) * 100;
    }
}
