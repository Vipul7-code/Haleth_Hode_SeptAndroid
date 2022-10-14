using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class HuntingtonCastle : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, marium, john, tucker, guard, lordEdward,abbott,bodyGuard,smoke,guards;
    [SerializeField]
    Transform playerPos, guardPos, mariumPos, johnPos, tuckerPos,abbottPos,smokePos,playerNewPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform[] guardsPos;
    [SerializeField]
    GameObject[] gateGuard;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    [SerializeField]
    GameObject battlePoint;
    // Start is called before the first frame update
    void Start()
    {
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.isShop = true;
        Globals.activePart = "HuntingtonCastle";
        Globals.huntingtonCastle = this;
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
     //   Debug.Log("dialog count::" + dialogCount + "secod fight::" + Globals.secondFight + " third fight::" + Globals.thirdFight);
        if (Globals.secondFight)
        {
            if (dialogCount == 10)
                StartCoroutine(dissappearSmoke());
        }
       else if (Globals.thirdFight)
        {
            if (dialogCount == 11)
            {
                Globals.enterPos = character.transform.localPosition;
                StartCoroutine(startBattle());
            }
        }
        else
        {
            if (dialogCount == 6)
            {
                Globals.secondFight = true;
                StartCoroutine(startBattle());
            }
        }
    }
    public void ThroneEntryDialog()
    {
        Globals.ActiveControls(character, false);
        Globals.secondFight = false;
        Globals.thirdFight = true;
        playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Huntington_Throne_EntryDialogue") as TimelineAsset;
        playble.Play();
    }
    IEnumerator dissappearSmoke()
    {
        yield return new WaitForSeconds(0.3f);
        Globals.thirdFight = true;
        smoke.SetActive(false);
        marium.SetActive(false);
        john.SetActive(false);
        tucker.SetActive(false);
        Globals.ActiveControls(character, true);
    }
    IEnumerator startBattle()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene("Battle Scene_Castle");
    }
    void Common()
    {
        battlePoint.SetActive(false);
        foreach (var v in gateGuard)
        {
            v.SetActive(false);
        }
    }
public    void SpawnProtagnist()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.leavingThrone)
        {
            character = Instantiate(Globals.activePlayer, playerNewPos.position, Quaternion.identity);
            Common();
        }
        else
        {
            if (Globals.thirdFight)
            {
                Debug.Log("inside this");
                character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                Common();
            }
            else
                character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        }
        Globals.ActiveControls(character, false);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        Globals.ActiveFaces(character, false, false, true, false);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player";
        if (Globals.leavingThrone ||Globals.thirdFight)
            Globals.ActiveControls(character, true);
        else
          SpawnPartyMember();
    }
    void SpawnPartyMember()
    {
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
        GameObject tuc = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
        Globals.ActiveFaces(tucker, false, false, true, false);
        if (!Globals.secondFight)
            SecondWaveOfEnemy();
        else
            SpawnSmoke();

    }
    void SpawnSmoke()
    {
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_M_Huntington_BlackCloud_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Smith/Smith_F_Huntington_BlackCloud_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_M_Huntington_BlackCloud_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Archer/Archer_F_Huntington_BlackCloud_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_M_Huntington_BlackCloud_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Acolyte/Acolyte_F_Huntington_BlackCloud_Dialog") as TimelineAsset;
        playble.Play();
        GameObject sm = Resources.Load(("Enemy/Smoke"), typeof(GameObject)) as GameObject;
        smoke = Instantiate(sm, smokePos.position, Quaternion.identity);
        SecondWaveOfEnemy();
    }

    void SecondWaveOfEnemy()
    {
        GameObject abb = Resources.Load(("Enemy/AbbotChesterEnemy"), typeof(GameObject)) as GameObject;
        abbott = Instantiate(abb, abbottPos.position, Quaternion.identity);
        if (!Globals.secondFight)
            SpawnBodyGuard();
        else
            DeathAnim();
    }
    void DeathAnim()
    {
        foreach (Transform child in abbott.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Death");
        }
    }
    void SpawnBodyGuard()
    {
        playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington Castle/Huntington_Abbott(AfterFight)_Dialog") as TimelineAsset;
        playble.Play();
        for (int i=0;i<2;i++)
        {
            GameObject bdGuard = Resources.Load(("Enemy/bodyguard"), typeof(GameObject)) as GameObject;
            bodyGuard = Instantiate(bdGuard, guardsPos[i].position, Quaternion.identity);
        }
    }
  
}
