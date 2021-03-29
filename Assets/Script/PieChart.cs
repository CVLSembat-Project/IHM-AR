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
        makeGraph(request.getPercentageOfBatiment(), request.getPercentageOfBatiment().Count);
    }

    //TODO : Quand on retourne a l'acceuil puis on réappui le diagramme disparaît 
    public void makeGraph(List<float> values, int numberBatiment)
    {
        float angle;
        float pSize;
        float calc = 0f;
        float zRotation = 0f;

        //for (int i = 0; i < values.Count; i++) total += values[i];

        for (int i = 0; i < numberBatiment; i++)
        {
            Image newWedge = Instantiate(wedgePrefab) as Image;
            newWedge.name = "PieChart" + (char)(65 + i);
            newWedge.rectTransform.anchoredPosition = new Vector2(1f, -14f);
            newWedge.transform.SetParent(parent, false);
            newWedge.color = new Color32((byte) Random.Range(0,255),(byte) Random.Range(0,255),(byte) Random.Range(0,255),255);
            newWedge.fillAmount = values[i] / values.Sum();
            newWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= newWedge.fillAmount * 360f;

            textValue = Instantiate(textPrefab) as Text;
            textValue.text = (char)(65 + i) + " : " + values[i].ToString();  
            textValue.rectTransform.anchoredPosition = newWedge.rectTransform.anchoredPosition;
            textValue.transform.SetParent(transform, false);
            textValue.color = new Color(0, 0, 0);

            pSize = values[i] / values.Sum() * 360;
            angle = pSize / 2f + calc;
            calc += pSize;

            float x = 1f + (100 * Mathf.Sin(angle * Mathf.PI / 180));
            float y = -14f + (100 * Mathf.Cos(angle * Mathf.PI / 180));

            textValue.rectTransform.anchoredPosition = new Vector2(x, y);

        }
    }
}
