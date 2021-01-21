using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JaugeVariance : MonoBehaviour
{
    public float speedReduce;
    // Start is called before the first frame update
    void Start()
    {
        
        //TODO : Add request HTTP to get value of database
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.y > 0) transform.localScale += new Vector3(0, speedReduce, 0) * Time.deltaTime;
        else transform.localScale += new Vector3(0, 0, 0);
    }

    /*
     Exemple de récupération de variable : 
     public bool valuevariableA()
		{
			return variableA;
		}
         puis dans le script ou l'on souhaite importer la valeur :
         variableA=GameObject.Find("ObjectquicontientscriptA").GetComponent(lenomdetonscriptA).valuevariableA();
     */
}
