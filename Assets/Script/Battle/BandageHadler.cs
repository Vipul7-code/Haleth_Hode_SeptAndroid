using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageHadler : MonoBehaviour
{
    public GameObject protagnist, john, marium, tucker;
    // Start is called before the first frame update
    void Start()
    {
        Globals.bandageHandler = this;
        UpdateCompanion();
    }

   public void UpdateCompanion()
    {
       foreach(var v in Globals.battleManager.myTeam)
        {
            if (v.tag == "Player")
                protagnist.SetActive(true);
            if (v.name == "JohnCompanion(Clone)")
                john.SetActive(true);
            if (v.name == "Marium(Clone)")
                marium.SetActive(true);
            if (v.name == "Tucker(Clone)")
                tucker.SetActive(true);
        }
    }
}
