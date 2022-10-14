using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroAcolyte : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    public PlayableDirector playble;
    public GameObject dialogBox;
    DatabaseManager db;
    int dialogCount;
    [SerializeField]
    AudioSource bgAudio,killSound, womanKillSound;

    int killCount = 0;
    // Start is called before the first frame update
    void Start()
    {
      //  Globals.isPart1Battle = true;
        Globals.completeIntro = true;
        Globals.activeScene = Globals.CurrentScene.Huntsville;
        db = FindObjectOfType<DatabaseManager>();
        if (!Globals.isPart1Battle)
        {
            if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Introduction/Acolyte/Male/Introduction(M_Acolyte) 1") as TimelineAsset;
            else
                playble.playableAsset = Resources.Load("Playables/Introduction/Acolyte/Female/Introduction(F_Acolyte) 1") as TimelineAsset;
        }
        else
        {
            Globals.isFirstTut = false;
            if (Globals.avatarState.AvatarName== "PriestMale")
               playble.playableAsset = Resources.Load("Playables/Introduction/Acolyte/Male/Introduction(M_Acolyte) 2") as TimelineAsset;
            else
                playble.playableAsset = Resources.Load("Playables/Introduction/Acolyte/Female/Introduction(F_Acolyte) 2") as TimelineAsset;
        }
        playble.Play();
    }
    public void OnSoundChange()
    {

        if (killCount == 1)
        {
            Debug.Log("woman kill...");
            womanKillSound.Play();
            StartCoroutine(delaySound());
        }
        else
        {
            Debug.Log("man kill..........");
            killSound.Play();
        }
        killCount++;

    }

    IEnumerator delaySound()
    {
        yield return new WaitForSeconds(2f);
        womanKillSound.Stop();
    }

    // Update is called once per frame
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (!Globals.isPart1Battle)
        {
            if (dialogCount == 20)//20
                StartCoroutine(StartBattle());
        }
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
        if (Globals.isPart1Battle)
        {
            Globals.isPart1Battle = false;
           
            SceneManager.LoadSceneAsync("Huntsville_Damaged");
        }
        else
            StartCoroutine(StartBattle());
    }
    IEnumerator StartBattle()
    {
        Globals.isFirstTut = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("Battle Scene_CobbleStone");
    }
}
