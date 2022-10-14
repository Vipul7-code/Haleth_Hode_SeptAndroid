using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScriptManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject hunsville1, hunsville2,hunsville3,common,smithIntro,scoutIntro,acolyteIntro;
    Scene currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Death Wight Village" || currentScene.name == "Barghest Village" || currentScene.name == "Brigand Village")
        {
            if (Globals.storyCount == 0)
            {
                if(!Globals.isFirstCompleteStory)
                   SetHunsville(true, false, false, false,false,false,false);
                else
                    SetHunsville(false, false, false, true, false, false, false);
            }
            else if (Globals.storyCount == 1)
            {
                SetHunsville(true, false, false, false, false, false, false);
                //if(Globals.isFirstCompleteStory)
                //    SetHunsville(false, false, false, true, false, false, false);
                //else

            }

        }
        else
        {
            if (Globals.secondVisit == 1 && !Globals.isSmith && !Globals.isArcher && !Globals.isAcolyte)
                SetHunsville(false, true, false, false,false,false,false);
            else if (Globals.secondVisit == 0 && !Globals.isSmith && !Globals.isArcher && !Globals.isAcolyte)
            {
                if (currentScene.name == "Atwater Village")
                    SetHunsville(true, false, false, false, false, false, false);
                else
                    SetHunsville(true, false, false, false, false, false, false);
            }
            else if (Globals.secondVisit == 2 && !Globals.isSmith && !Globals.isArcher && !Globals.isAcolyte)
                SetHunsville(true, false, false, false,false,false,false);
            else if(Globals.isSmith)
                SetHunsville(false, false, false, false,true,false,false);
            else if(Globals.isArcher)
                SetHunsville(false, false, false, false,false,true,false);
            else if(Globals.isAcolyte)
                SetHunsville(false, false, false, false,false,false,true);
            if (Globals.isFirstCompleteStory)
            {
                if(Globals.secondVisit==2 && Globals.isChurchComplete)
                    SetHunsville(false, true, false, false, false, false, false);
                else
                   SetHunsville(false, false, false, true, false, false, false);
            }
        }
    }
    void SetHunsville(bool one, bool two,bool third,bool c,bool s,bool sc,bool ac)
    {
        hunsville1.SetActive(one);
        hunsville2.SetActive(two);
        hunsville3.SetActive(third);
        common.SetActive(c);
        smithIntro.SetActive(s);
        scoutIntro.SetActive(sc);
        acolyteIntro.SetActive(ac);
    }
}
