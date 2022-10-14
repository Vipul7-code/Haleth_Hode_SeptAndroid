using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroArcher : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    public PlayableDirector playble;
    public GameObject dialogBox;
    DatabaseManager db;
    int dialogCount;
    [SerializeField]
    AudioSource bgAudio,killSound,womanKillSound;
    int killCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Globals.completeIntro = true;
        
        Globals.activeScene = Globals.CurrentScene.Huntsville;
        db = FindObjectOfType<DatabaseManager>();
        if (!Globals.isPart1Battle)
        {
            if(Globals.avatarState.AvatarName== "ArcherMale")
              playble.playableAsset = Resources.Load("Playables/Introduction/Archer/Male/Introduction(M_Scout) 1") as TimelineAsset;
            else
                playble.playableAsset = Resources.Load("Playables/Introduction/Archer/Female/Introduction(F_Scout) 1") as TimelineAsset;
        }
        else
        {
            if (Globals.secondFight)
            {
                if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Introduction/Archer/Male/Introduction(M_Scout) 3") as TimelineAsset;
                else
                    playble.playableAsset = Resources.Load("Playables/Introduction/Archer/Female/Introduction(F_Scout) 3") as TimelineAsset;
            }
            else
            {
                if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Introduction/Archer/Male/Introduction(M_Scout) 2") as TimelineAsset;
                else
                    playble.playableAsset = Resources.Load("Playables/Introduction/Archer/Female/Introduction(F_Scout) 2") as TimelineAsset;
            }
        }
        playble.Play();
    }
    public void OnSoundChange()
    {

        if(killCount == 1)
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
    public void OnWomenKillSound()
    {
        Debug.Log("woman kill");
        womanKillSound.Play();
        StartCoroutine(delaySound());
        killCount++;
    }
    IEnumerator delaySound()
    {
        yield return new WaitForSeconds(2f);
        womanKillSound.Stop();
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (Globals.isPart1Battle)
        {
            if (!Globals.secondFight)
            {
                if (dialogCount == 1)//20
                {
                    Globals.secondFight = true;
                    StartCoroutine(StartBattle());
                }
            }
        }
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        if (!Globals.isPart1Battle)
            StartCoroutine(StartBattle());
        else
        {
            Globals.secondFight = false;
            Globals.isPart1Battle = false;
            SceneManager.LoadSceneAsync("Huntsville_Damaged");
        } 
    }
    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync("Battle Scene_CobbleStone");
    }
}
