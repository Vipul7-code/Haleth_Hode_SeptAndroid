using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class IntroSmith : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    [SerializeField]
    AudioSource bgAudio,killPeopleAudio, womanKillSound;
    int killCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        Globals.completeIntro = true;
       // Globals.isPart1Battle = true;
        Globals.activeScene = Globals.CurrentScene.Huntsville;
        if (!Globals.isPart1Battle)
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Introduction/Smith/Male/Introduction(M_Smith) 1") as TimelineAsset;
            else
                playble.playableAsset = Resources.Load("Playables/Introduction/Smith/Female/Introduction(F_Smith) 1") as TimelineAsset;
        }
        else
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Introduction/Smith/Male/Introduction(M_Smith) 2") as TimelineAsset;
            else
                playble.playableAsset = Resources.Load("Playables/Introduction/Smith/Female/Introduction(F_Smith) 2") as TimelineAsset;
        }
        playble.Play();
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
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
            killPeopleAudio.Play();
        }
        killCount++;

    }
    IEnumerator delaySound()
    {
        yield return new WaitForSeconds(2f);
        womanKillSound.Stop();
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
            Globals.isPart1Battle = false;
           // Globals.conversationCount = 6;
            playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
             SceneManager.LoadSceneAsync("Huntsville_Damaged");
        }
    }
    IEnumerator StartBattle()
    {
        yield return new WaitForSeconds(0.35f);
        SceneManager.LoadSceneAsync("Battle Scene_CobbleStone");
    }
}
