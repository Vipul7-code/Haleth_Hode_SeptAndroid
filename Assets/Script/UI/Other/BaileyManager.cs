using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaileyManager : MonoBehaviour
{
  public  GameObject p1, p2;
    // Start is called before the first frame update
    void Start()
    {
        if(Globals.secondFight || Globals.beforeMottey)
            PlaybleSetting(false, true);
        else
            PlaybleSetting(true, false);
    }
    void PlaybleSetting(bool one,bool two)
    {
        p1.SetActive(one);
        p2.SetActive(two);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
