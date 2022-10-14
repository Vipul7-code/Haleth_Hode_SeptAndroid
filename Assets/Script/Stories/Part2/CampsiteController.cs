using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampsiteController : MonoBehaviour
{
    [SerializeField]
    GameObject playble1, playble2;
    [SerializeField]
    GameObject wagon1, wagon2;
    // Start is called before the first frame update
    void Start()
    {
        if (Globals.currentObjective == "RandomAttack")
        {
            PlaybleSetting(false, true);
            wagon1.SetActive(false);
            wagon2.SetActive(false);
        }
        else
            PlaybleSetting(true, false);
    }

   void PlaybleSetting(bool p1, bool p2)
    {
        playble1.SetActive(p1);
        playble2.SetActive(p2);
    }
}
