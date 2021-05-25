using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mesures
{
    //Class which allow to get JSON Object
    [SerializeField]
    public string nomBatiment { get; set; }
    public float valeur { get; set; }
    public string unite { get; set; }
    public DateTime date { get; set; }
    public float MesureTotal { get; set; }
    public int nbBatiments { get; set; }
    public float pourcentage { get; set; }
    public string nomType { get; set; }

    public float ValeurParjour { get; set; }

    public float valeurSeuil { get; set; }
}
