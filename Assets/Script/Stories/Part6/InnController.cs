using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class InnController : MonoBehaviour
{
    [SerializeField]
    GameObject playble1, playble2;
    [SerializeField]
    Transform[] GuardPos;
    GameObject guard1, guard2;
    [HideInInspector]
    public int clickCount=0;
    [SerializeField]
    GameObject fakeWall;
    // Start is called before the first frame update
    void Start()
    {
        Globals.innController = this;
    }
   void PlaybleSetting(bool p1,bool p2)
    {
        playble1.SetActive(p1);
        playble2.SetActive(p2);
    }
    public void SecondSetting()
    {
        Destroy(Globals.huntingtonChurch.character.gameObject);
        Destroy(Globals.huntingtonChurch.priest.gameObject);
        PlaybleSetting(true, false);
        if (Globals.avatarState.AvatarName == "WarriorMale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_M_Huntington_Town_Inn_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_F_Huntington_Town_Inn_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_M_Huntington_Town_Inn_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_F_Huntington_Town_Inn_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_M_Huntington_Town_Inn_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_F_Huntington_Town_Inn_CutScene") as TimelineAsset;
        FindObjectOfType<InnConversationControl>().playble.Play();
    }
    public void ThirdSetting()
    {
        Destroy(Globals.huntingtonChurch.character.gameObject);
        Destroy(Globals.huntingtonChurch.priest.gameObject);
        Globals.huntingtonChurch.SpawnBarmaidMale();
        PlaybleSetting(true, false);
        if (Globals.avatarState.AvatarName == "WarriorMale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_M_Huntington_Town_InnBarmaid_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_F_Huntington_Town_InnBarmaid_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_M_Huntington_Town_InnBarmaid_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_F_Huntington_Town_InnBarmaid_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_M_Huntington_Town_InnBarmaid_CutScene") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            FindObjectOfType<InnConversationControl>().playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_F_Huntington_Town_InnBarmaid_CutScene") as TimelineAsset;
        FindObjectOfType<InnConversationControl>().playble.Play();
        SpawnBarmaid();
    }
    void SpawnBarmaid()
    {
        GameObject gua1 = Resources.Load(("Enemy/Barmaid"), typeof(GameObject)) as GameObject;
        guard1 = Instantiate(gua1, GuardPos[0].position, Quaternion.identity);
        GameObject gua2 = Resources.Load(("Enemy/Barmaid"), typeof(GameObject)) as GameObject;
        guard2 = Instantiate(gua2, GuardPos[1].position, Quaternion.identity);
    }
    public void OnCompleteFirstCutscene()
    {
        PlaybleSetting(false, true);
        if (clickCount == 1)
        {
            Globals.huntingtonChurch.conversationBox1.SetActive(false);
            Globals.huntingtonChurch.conversationBox2.SetActive(false);
            Globals.huntingtonChurch.SpawnPlayer();
        }
        else
        {
            Globals.huntingtonChurch.conversationBox3.SetActive(false);
            StartCoroutine(startBattle());
        }
    }
IEnumerator startBattle()
    {
        yield return new WaitForSeconds(0.02f);
        SceneManager.LoadScene("Battle Scene_Castle");
    }
}
