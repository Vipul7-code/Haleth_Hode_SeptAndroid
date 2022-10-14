using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
public class CastlerController : MonoBehaviour
{
    [SerializeField]
    GameObject playble1, playble2;

    [SerializeField]
     GameObject[] walls;
    
    // Start is called before the first frame update
    void Start()
    {
        Globals.activeScene = Globals.CurrentScene.HuntingtonCastle;
        if (Globals.isPart1Battle || Globals.leavingThrone)
        {
            PlaybleSetting(false, true);
            if(Globals.leavingThrone)
              WallSetting();
        }
        else
            PlaybleSetting(true, false);
    }
    void PlaybleSetting(bool p1, bool p2)
    {
        playble1.SetActive(p1);
        playble2.SetActive(p2);
        PlayVideo();
    }
    void WallSetting()
    {
        foreach(var v in walls)
        {
            v.transform.tag = "Untagged";
        }
    }
    void PlayVideo()
    {
        if (!Globals.isPart1Battle)
        {
            if (!Globals.leavingThrone)
            {
                Debug.Log("here");
                FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_M_Huntington_CastleIntirior_GeneralDialogs_CutScene_01") as TimelineAsset;
                FindObjectOfType<InnConversationControl>().playble.Play();
            }
            else
                FindObjectOfType<HuntingtonCastle>().SpawnProtagnist();
        }
        else
           FindObjectOfType<HuntingtonCastle>().SpawnProtagnist();
    }
}
