using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{

    public Image wedgePrefab;
    public Text textPrefab;
    public Transform parent;
    Text percentage;
    int numberBatiment;
    private Text textValue;
    

    // Start is called before the first frame update
    void Start()
    {
        WebRequest request = GetComponent<WebRequest>();
        makeGraph(request.getPercentageOfBatiment(), request.types.Count, true, request.types);
        Debug.Log("Il y a : " + request.types.Count);
    }

    //TODO : Quand on retourne a l'acceuil puis on réappui le diagramme disparaît 
    public void makeGraph(List<float> values, int numberBatiment, bool isShowPieChart = true, List<string> textType = null)
    {
        float angle;
        float pSize;
        float calc = 0f;
        float zRotation = 0f;

        for (int i = 0; i < numberBatiment; i++)
        {
            Image newWedge = Instantiate(wedgePrefab) as Image;
            newWedge.name = "PieChart" + (char)(65 + i);
            newWedge.rectTransform.anchoredPosition = new Vector2(1f, -14f);
            newWedge.transform.SetParent(parent, false);
            //We start the range at 50 to 240 for don't have black and white color
            newWedge.color = new Color32((byte) Random.Range(50,240),(byte) Random.Range(50,240),(byte) Random.Range(50,240),255);
            newWedge.fillAmount = values[i] / values.Sum();
            newWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= newWedge.fillAmount * 360f;

            textValue = Instantiate(textPrefab) as Text;
            textValue.text = textValue.text == null ? (char)(65 + i) + " : " + values[i].ToString() + " %" : textType[i] + " : " + values[i].ToString() + " %";
            textValue.rectTransform.anchoredPosition = newWedge.rectTransform.anchoredPosition;
            textValue.transform.SetParent(transform, false);
            textValue.color = new Color(255 - newWedge.color.r , 255 - newWedge.color.g , 255 - newWedge.color.b);


            pSize = values[i] / values.Sum() * 360;
            angle = pSize / 2f + calc;
            calc += pSize;

            float x = 1f + (100 * Mathf.Sin(angle * Mathf.PI / 180));
            float y = -14f + (100 * Mathf.Cos(angle * Mathf.PI / 180));

            textValue.rectTransform.anchoredPosition = new Vector2(x, y);

            if (isShowPieChart) gameObject.GetComponent<PieChart>().enabled = true;
            else gameObject.GetComponent<PieChart>().enabled = false;

        }
    }
}
