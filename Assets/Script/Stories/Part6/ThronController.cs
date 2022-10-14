using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
public class ThronController : MonoBehaviour
{
    [SerializeField]
    GameObject playble1, playble2;
    // Start is called before the first frame update
    void Start()
    {
        Globals.activeScene = Globals.CurrentScene.HuntingtonThroneRoom;
        if (!Globals.isPart1Battle)
            PlaybleSetting(true, false);
        else
            PlaybleSetting(false, true);
    }

  public  void PlaybleSetting(bool p1,bool p2)
    {
        playble1.SetActive(p1);
        playble2.SetActive(p2);
    }
}

