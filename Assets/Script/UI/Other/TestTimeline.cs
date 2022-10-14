using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class TestTimeline : MonoBehaviour
{
     PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    private void Start()
    {
        playble =GetComponent<PlayableDirector>();
    }
    public void PlayClip()
    {
        playble.Play();
        dialogBox.GetComponent<Animator>().enabled = true;
        dialogCount++;
        Debug.Log("count:: " + dialogCount);
        //   playble.playableAsset = Resources.Load("Test/TestFile") as TimelineAsset;
    }
    public void PauseClip()
    {
        playble.Pause();
        dialogBox.GetComponent<Animator>().enabled = false;
    }
}
