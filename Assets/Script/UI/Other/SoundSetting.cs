using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{
    public AudioSource bg;
    int count;
    // Start is called before the first frame update
    void Start()
    {
        Globals.soundSetting = this;

        bg = GetComponent<AudioSource>();

    }

    public void SoundPlay()
    {
        count = 1;
        //if (Globals.avatarState.SoundLevel == 1)
        //    GetComponent<AudioSource>().Play();
        //else
        //    GetComponent<AudioSource>().Stop();
        if (Globals.avatarState.SoundLevel == 1)
        {
            InvokeRepeating("PlayBgSound", 0, bg.clip.length);
        }
        else
        {
            bg.Stop();
            count = 1;
        }
           
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayBgSound()
    {
     
        if(count == 3)
            bg.volume = bg.volume / 2;
        if (count <= 3)
        {
            Debug.Log("play bg sound............"+Globals.activeScene);
            bg.Play();
            count++;
        }
        else
        {
            Debug.Log("cancel all invoke......");
            CancelInvoke("PlayBgSound");
        }
     
    }
}
