using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;

public class WindowGraph : MonoBehaviour
{
    //Declaration Field
    [SerializeField] private Sprite circleSprite; 
    private RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform dashTemplateX;
    private RectTransform dashTemplateY;
    private List<GameObject> gameObjectList;
    private WebRequest request;

    private void Awake()
    {
        //Get container in unity and initialisation of graph
        Screen.orientation = ScreenOrientation.Landscape;
        graphContainer = GameObject.Find("GraphContainer").GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("labelTemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("labelTemplateY").GetComponent<RectTransform>();
        dashTemplateX = graphContainer.Find("dashTemplateX").GetComponent<RectTransform>();
        dashTemplateY = graphContainer.Find("dashTemplateY").GetComponent<RectTransform>();
        request = GameObject.Find("Window_Graph").GetComponent<WebRequest>();

        gameObjectList = new List<GameObject>();
        //List<int> valueList = new List<int>() { 1,98,55,32,12,34,48,69,89,7,45,55 ,87,47,52,14,29,92,78,47,41,15}; //A changer
        /*FunctionPeriodic.Create(() => {
            valueList.Clear();
            for (int i = 0; i < 15; i++) valueList.Add(UnityEngine.Random.Range(0, 500));
            showGraph(valueList, (int _i) => "Day " + (_i + 1), (float _f) => "$" + Mathf.RoundToInt(_f));
        }, .5f);*/
    }

    private void Start()
    {
        if (request.valuesOfBatiments.Count > 0)
        {
            Debug.Log(request.valuesOfBatiments[0]);
            showGraph(request.valuesOfBatiments, -1, (int _i) => "Day " + (_i + 1), (float _f) => Mathf.RoundToInt(_f) + request.unite);
        }
        else Debug.Log("Erreur il n'y a rien dans la liste car elle est égale a 0 ou inférieur");
    }

    /**
     * Description : Create each point of the line Chart
     * @author : <b>Bureau Bastien</b>
     * @parem Vector2 anchoredPosition : Get the position with the anchor for circle position
     * @return gameObject : return the circle with its feature
     */
    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    /**
     * Description : Show and create the entire chart, with values, label and dash
     * @author : <b>Bureau Bastien</b>
     * @param valueList : A list for the value in the chart
     * @param getAxisLabelX : get a value IN int and modify the type of the value and have a string in OUT of the function
     * @param getAxisLabelY : same but instead of an int it s a float
     */
    private void showGraph(List<float> valueList, int maxVisibleValueAmount = -1, Func<int,string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        //Delegate for convert the type and have a method for the result of the second type
        if (getAxisLabelX == null)getAxisLabelX = delegate (int _i) { return _i.ToString(); };
        if (getAxisLabelY == null) getAxisLabelY = delegate (float _f) { return Mathf.RoundToInt(_f).ToString(); };
        if (maxVisibleValueAmount <= 0) maxVisibleValueAmount = valueList.Count;
        

        foreach(GameObject gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        float graphWidth = graphContainer.sizeDelta.x;
        float graphHeight = graphContainer.sizeDelta.y;

        float yMaximum = valueList[0];
        float yMinimum = valueList[0];

        for (int i = Mathf.Max(valueList.Count - maxVisibleValueAmount ,0); i < valueList.Count; i++)
        {
            int value = Mathf.RoundToInt(valueList[i]);
            if (value > yMaximum) yMaximum = value;
            if (value < yMinimum) yMinimum = value;
        }

        float yDifference = yMaximum - yMinimum;
        if (yDifference <= 0) yDifference = 5f;

        yMaximum += yDifference * 0.2f;
        yMinimum -= yDifference * 0.2f;

        float xSize = graphWidth / (maxVisibleValueAmount + 1);
        GameObject lastCircleGameObject = null;

        int xIndex = 0;

        for(int i = Mathf.Max(valueList.Count - maxVisibleValueAmount, 0); i < valueList.Count; i++)
        {
            float xPosition = xSize + xIndex * xSize;
            float yPosition = ((valueList[i] - yMinimum) / (yMaximum - yMinimum)) * graphHeight;
            Debug.Log("la valeur de l'axe y est : " + yPosition);
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition,yPosition));
            gameObjectList.Add(circleGameObject);

            //Get the last point and replace the last point by the new point and made link beetwen us
            if(lastCircleGameObject != null)
            {
                GameObject dotConnection = CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnection);
            }
            lastCircleGameObject = circleGameObject;

            //For create each label in the X axis
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer,false);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPosition, -7f);
            labelX.GetComponent<Text>().text = getAxisLabelX(i); //A changer
            gameObjectList.Add(labelX.gameObject);

            //For create the dash 
            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer, false);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPosition, -3f);
            gameObjectList.Add(dashX.gameObject);
            xIndex++;
        }

        int separatorCount = 10; //Will change soon
        //Loop for create Y axis label and dash
        for(int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer,false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(-7f, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(normalizedValue * yMaximum);
            gameObjectList.Add(labelY.gameObject);

            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer, false);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(-4f, normalizedValue * graphHeight);
            gameObjectList.Add(dashY.gameObject);
        }
    }

    /**
     * Description : Create the link beetween each point of the chart
     * @author : <b>Bureau Bastien</b>
     * @param dotPositionA : get the first position of the point
     * @param dotPositionB : get the second position of the another point
     * @return : the link beetwen each point
     */
    private GameObject CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 direction = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + direction * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(direction));
        return gameObject;
    }
}
