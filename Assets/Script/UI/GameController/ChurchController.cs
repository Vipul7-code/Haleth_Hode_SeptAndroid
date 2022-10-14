using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChurchController : MonoBehaviour
{
    [SerializeField]
    GameObject hunsville1, hunsville2,common;
    Scene currentScene;
    void Start()
    {
        if (currentScene.name == "Death Wight Village" || currentScene.name == "Barghest Village" || currentScene.name == "Brigand Village")
            SetHunsville(false, false, true);
        else
        {
            Debug.Log("second visit::" + Globals.secondVisit + "  complete::" + Globals.isFirstCompleteStory+"church complete::"+Globals.isChurchComplete);
            if (Globals.secondVisit == 0 || !Globals.isFirstCompleteStory)
                SetHunsville(false, false, true);
            else if (Globals.secondVisit == 2 && !Globals.isChurchComplete)
            {
               
                SetHunsville(false, true, false);
               // hunsville2.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("second visit::" + Globals.secondVisit + "  church complete::" + Globals.isChurchComplete);
                if (Globals.isChurchComplete)
                    SetHunsville(false, false, true);
                else
                    SetHunsville(true, false, false);

            }
        }
    }
    void SetHunsville(bool one, bool two,bool c)
    {
        hunsville1.SetActive(one);
        hunsville2.SetActive(two);
        common.SetActive(c);
    }
}
