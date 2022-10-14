using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaravanAndPetrols : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject relatedIcon;
    void Start()
    {
        Globals.randomHandler = this;
    }

   public void DisableIcon()
    {
        relatedIcon.SetActive(false);
    }
}
