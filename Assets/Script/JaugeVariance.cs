using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaugeVariance : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
        //TODO : Add request HTTP to get value of database
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(0,-5,0) * Time.deltaTime;
    }
}
