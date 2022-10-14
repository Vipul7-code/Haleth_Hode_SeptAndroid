using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionNumbers : MonoBehaviour
{
    [SerializeField]
    GameObject[] contentChild;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Globals.noOfCompanions; i++)
        {
           
            contentChild[i].SetActive(true);
        }
    }
}
