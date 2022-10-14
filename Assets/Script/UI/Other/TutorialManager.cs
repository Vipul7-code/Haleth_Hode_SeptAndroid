using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
  public  GameObject p1, p2;
    // Start is called before the first frame update
    void Start()
    {
        if (Globals.conversationCount == 0)
            PlaybleSetting(true, false);
        else if (Globals.conversationCount >= 4)
        {
            PlaybleSetting(false, true);
            if (Globals.conversationCount != 4)
            {
                p2.transform.GetChild(0).gameObject.SetActive(false);
                p2.GetComponent<TutorialPart>().enabled = true;
            }
        }
    }
    void PlaybleSetting(bool f,bool s)
    {
        p1.SetActive(f);
        p2.SetActive(s);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
