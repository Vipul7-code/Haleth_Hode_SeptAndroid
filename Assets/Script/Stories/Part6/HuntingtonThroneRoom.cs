using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class HuntingtonThroneRoom : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, marium, john, tucker, guard, lordEdward, bodyGuard;
    [SerializeField]
    Transform playerPos, mariumPos, johnPos, tuckerPos, lordEdwardPos,startPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform[] guardsPos;
    [SerializeField]
    Transform[] bodyGuardPos;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    List<GameObject> guards = new List<GameObject>();
    [SerializeField]
    GameObject conversationStart;
    // Start is called before the first frame update
    void Start()
    {
        SpawnProtagnist();
    }
    void SpawnProtagnist()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (!Globals.secondFight)
            character = Instantiate(Globals.activePlayer, startPos.position, Quaternion.identity);
        else
        {
            conversationStart.SetActive(false);
            character = Instantiate(Globals.activePlayer, tuckerPos.position, Quaternion.identity);
        }
        Globals.ActiveControls(character, true);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player";
        if (!Globals.secondFight)
            SpawnReeve();
        //SpawnPartyMember();
    }
    public void ThronDialogs()
    {
        Globals.ActiveControls(character, false);
        conversationStart.SetActive(false);
        SpawnPartyMember();
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_M_Huntington_Castle_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_F_Huntington_Castle_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_M_Huntington_Castle_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_F_Huntington_Castle_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_M_Huntington_Castle_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_F_Huntington_Castle_Dialog") as TimelineAsset;
        playble.Play();
    }
    void SpawnPartyMember()
    {
        Globals.ActiveFaces(character, false, true, false, false);
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
        GameObject tuc = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        if(!Globals.secondFight)
         tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
        else
            tucker = Instantiate(tuc, playerPos.position, Quaternion.identity);
        if (Globals.secondFight)
        {
            Globals.ActiveFaces(character, true, false, false, false);
            Globals.ActiveFaces(john, true, false, false, false);
            Globals.ActiveFaces(marium, true, false, false, false);
            Globals.ActiveFaces(tucker, true, false, false, false);
            Globals.ActiveControls(character, true);
        }
        else
        {
           Globals.ActiveFaces(tucker, false, true, false, false);
           Globals.ActiveFaces(john, false, true, false, false);
            Globals.ActiveFaces(marium, false, true, false, false);
            
        }

    }
    void SpawnReeve()
    {
        GameObject lo = Resources.Load(("Enemy/LordEdwardEnemy"), typeof(GameObject)) as GameObject;
        lordEdward = Instantiate(lo, lordEdwardPos.position, Quaternion.identity);
        Globals.ActiveFaces(lordEdward, true, false, false, false);
        for(int i=0;i<2;i++)
        {
            GameObject body = Resources.Load(("Enemy/bodyguard"), typeof(GameObject)) as GameObject;
            bodyGuard = Instantiate(body, bodyGuardPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(bodyGuard, true, false, false, false);
            guards.Add(bodyGuard);
        }
        SpawnGuards();
    }
    void SpawnGuards()
    {
        for(int i=0;i<4;i++)
        {
            GameObject gu = Resources.Load(("Enemy/Guard"), typeof(GameObject)) as GameObject;
            guard = Instantiate(gu, guardsPos[i].position, Quaternion.identity);
            guards.Add(guard);
            if(i%2==0)
                Globals.ActiveFaces(guard, false, false, false, true);
            else
                Globals.ActiveFaces(guard, false, false, true, false);

        }
    }

    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (dialogCount == 15)
            AttackAnim();
    }

    void AttackAnim()
    {
        foreach(var v in guards)
        {
            foreach (Transform child in v.transform)
            {
                if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                    child.GetComponent<Animator>().SetTrigger("Attack");
            }
        }
        StartCoroutine(startBattle());
    }
    IEnumerator startBattle()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("Battle Scene_Castle");
    }

}
