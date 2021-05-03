using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{
    //PieChart's variable
    public Image wedgePrefab;
    public Text textPrefab;
    public Transform parent;
    private Text textValue;
    private bool stopUpdate = false;
    

    private void Update()
    {
        if (!stopUpdate) //For create one time a graph
        {
            WebRequest request = this.GetComponent<WebRequest>();
            if (request.types.Count > 0)
            {
                makeGraph(request.getPercentageOfBatiment(), request.types.Count, true, request.types);
                stopUpdate = true;
            }
        }
    }

    /** 
     * Description : <b>makeGraph</b> need a List of values for the data in the graph and the number of batiments
     * and have two optional parameters. It make a graph at the top right of the screen
     * @author : <b>Bureau Bastien</b>
     * @param values : A list of values for the data in the graph
     * @param numberBatiment : for the number of each portion of the graph
     * @param isShowPieChart : to display the pie chart
     * @param textType : for make a graph with the name of the type of data we get for exemple (electricite, gaz, eau)
     */
    public void makeGraph(List<float> values, int numberBatiment, bool isShowPieChart = true, List<string> textType = null)
    {
        float angle;
        float pSize;
        float calc = 0f;
        float zRotation = 0f;

        for (int i = 0; i < numberBatiment; i++)
        {
            Image newWedge = Instantiate(wedgePrefab) as Image;
            newWedge.name = "PieChart" + (char)(65 + i); //set name of the pie chart
            newWedge.rectTransform.anchoredPosition = new Vector2(1f, -14f); //for setting the position at the top right corner of the screen
            newWedge.transform.SetParent(parent, false);
            //We start the range at 50 to 240 for don't have black and white color
            newWedge.color = new Color32((byte) Random.Range(50,240),(byte) Random.Range(50,240),(byte) Random.Range(50,240),255);
            newWedge.fillAmount = values[i] / values.Sum(); //For making each portion of the pie chart
            newWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation)); // The rotation for a circular shape
            zRotation -= newWedge.fillAmount * 360f;

            //Same for the text
            textValue = Instantiate(textPrefab) as Text;
            textValue.text = textValue.text == null ? (char)(65 + i) + " : " + values[i].ToString() + " %" : textType[i] + " : " + values[i].ToString() + " %";
            textValue.rectTransform.anchoredPosition = newWedge.rectTransform.anchoredPosition;
            textValue.transform.SetParent(transform, false);
            //For have an opposite color of the pie chart for a better reading of the data
            textValue.color = new Color(255 - newWedge.color.r , 255 - newWedge.color.g , 255 - newWedge.color.b); 


            pSize = values[i] / values.Sum() * 360;
            angle = pSize / 2f + calc;
            calc += pSize;

            float x = 1f + (100 * Mathf.Sin(angle * Mathf.PI / 180));
            float y = -14f + (100 * Mathf.Cos(angle * Mathf.PI / 180));

            textValue.rectTransform.anchoredPosition = new Vector2(x, y);

            //For showing the pie chart
            if (isShowPieChart) gameObject.GetComponent<PieChart>().enabled = true;
            else gameObject.GetComponent<PieChart>().enabled = false;

        }
    }
}
