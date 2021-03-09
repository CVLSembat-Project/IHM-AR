using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JaugeVariance : MonoBehaviour
{
    /**
     * Description : Allow us to  vary the gauge with the percentage calculate with value of the JSON
     * @author : <b>Bureau Bastien</b>
     * @param jauge : Wait a gauge GameObject for changing its scale
     * @param value : Get the last value saved in the database
     * @param total : Get the total measure of one type in severals batiments or one batiment
     */
    public void SetVariance(GameObject jauge, float value, float total)
    {
        float percentage = 100 - calculatePercentage(value, total);
        Vector3 jaugeTransform = jauge.transform.localScale;

        //Condition to verify if the height of the gauge does not exceed 0
        if (jaugeTransform.y > 0)
        {
            //For calculate the height of the gauge 
            Debug.Log(percentage);
            jaugeTransform += new Vector3(0, 0, 0) * Time.deltaTime;
            Debug.Log(jaugeTransform);
        }
        else jaugeTransform += new Vector3(0, 0, 0);
    }


    /**
     * Description : Calulate a percentage
     * @author : <b>Bureau Bastien</b>
     * @param value : Get the last value saved in the database
     * @param total : Get the total measure of one type in severals batiments or one batiment
     * @return : a float value which is a percentage
    */
    float calculatePercentage(float value, float total)
    {
        //return a percentage
        return (value / total);
    }
}
