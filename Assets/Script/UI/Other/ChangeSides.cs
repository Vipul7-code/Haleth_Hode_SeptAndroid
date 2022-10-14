using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ChangeSides : MonoBehaviour
{
    TimelinePlayable timeline;
    void Start()
    {
        timeline = this.transform.GetChild(0).GetComponent<TimelinePlayable>();
        Debug.Log("name:: " + timeline);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
