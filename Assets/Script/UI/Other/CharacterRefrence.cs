using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterRefrence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Globals.imageUse = this.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
