using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroaManager : MonoBehaviour
{
    [SerializeField]
    GameObject smith, archer, acolyte;
    // Start is called before the first frame update
    void Start()
    {
        SettingOfIntro(); 
    }

   void SettingOfIntro()
    {
        if (Globals.isSmith || Globals.isSmithF)
            EnableDisableIntro(true, false, false);
        else if (Globals.isArcher || Globals.isArcherF)
            EnableDisableIntro(false, true, false);
        else if (Globals.isAcolyte || Globals.isAcolyteF)
            EnableDisableIntro(false, false, true);

    }
    void EnableDisableIntro(bool s,bool a,bool p)
    {
        smith.SetActive(s);
        archer.SetActive(a);
        acolyte.SetActive(p);
    }
}
