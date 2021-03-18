using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieChart : MonoBehaviour
{

    public Image wedgePrefab;
    int numberBatiment;
    float[] values = { 1.1f, 2.2f, 3.7f, 1.4f, 0.7f , 2.3f,2.4f};
    // Start is called before the first frame update
    void Start()
    {
        
        numberBatiment = GetComponent<WebRequest>().batimentCount;
        Debug.Log("Le nombre de batiments est de " + numberBatiment);
        makeGraph();
    }

    //TODO : Quand on retourne a l'acceuil puis on réappui le diagramme disparaît 
    void makeGraph()
    {
        float total = 0f;
        float zRotation = 0f;

        for (int i = 0; i < values.Length; i++) total += values[i];

        for (int i = 0; i < numberBatiment; i++)
        {
            Image newWedge = Instantiate(wedgePrefab) as Image;
            newWedge.name = "PieChart" + (char)(65 + i);
            newWedge.transform.SetParent(transform, false);
            newWedge.color = new Color32((byte) Random.Range(0,255),(byte) Random.Range(0,255),(byte) Random.Range(0,255),255);
            newWedge.fillAmount = values[i] / total;
            newWedge.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, zRotation));
            zRotation -= newWedge.fillAmount * 360f;

        }
    }
}
