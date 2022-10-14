using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class CastleEscapeTunnel : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, marium, john, tucker, helena, guard1, guard2;
    [SerializeField]
    Transform playerPos, guard1Pos, mariumPos, johnPos, tuckerPos, guard2Pos;
    [SerializeField]
    Camera mainCamera;
    public PlayableDirector playble;
    public GameObject dialogBox;
    [HideInInspector]
    public int dialogCount;
    List<GameObject> guards = new List<GameObject>();
    [SerializeField]
    GameObject convo;
    // Start is called before the first frame update
    void Start()
    {
        Globals.activeScene = Globals.CurrentScene.CastleEscapeTunnel;
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.isShop = true;
        Globals.activePart = "CastleEscapeTunnel";
        Globals.castleTunnel = this;
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (!Globals.isPart1Battle)
        {
            character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
            SpawnGuard();
        }
        else
        {
            convo.SetActive(false);
            character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
            Globals.ActiveControls(character, true);
        }
        Globals.ActiveFaces(character, false, true, false, false);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
    }
    public void Complete()
    {
        Globals.ActiveControls(character, true);
    }
    void SpawnPartyMember()
    {
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        Globals.ActiveFaces(marium, false, true, false, false);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
        Globals.ActiveFaces(john, false, true, false, false);
        GameObject tuc = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
        Globals.ActiveFaces(tucker, false, true, false, false);
    }
    public void Attaintion()
    {
       foreach(var v in guards)
        {
            Globals.ActiveFaces(v.gameObject, true, false, false, false);
        }
        Globals.ActiveControls(character, false);
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_M_Huntington_CastleEscapeTunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_F_Huntington_CastleEscapeTunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_M_Huntington_CastleEscapeTunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_F_Huntington_CastleEscapeTunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_M_Huntington_CastleEscapeTunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_F_Huntington_CastleEscapeTunnel_Dialog") as TimelineAsset;
        playble.Play();
    }
    void SpawnGuard()
    {
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_Caved-In_Tunnel_Dialog") as TimelineAsset;
        playble.Play();
        Globals.ActiveControls(character, false);
        GameObject gd1 = Resources.Load(("Enemy/Guard"), typeof(GameObject)) as GameObject;
        guard1 = Instantiate(gd1, guard1Pos.position, Quaternion.identity);
        guards.Add(guard1);
        Globals.ActiveFaces(guard1, true, false, false, false);
        GameObject gd2 = Resources.Load(("Enemy/Guard"), typeof(GameObject)) as GameObject;
        guard2 = Instantiate(gd2, guard2Pos.position, Quaternion.identity);
        guards.Add(guard2);
        Globals.ActiveFaces(guard2, true, false, false, false);
    }
    public void CompleteVideo()
    {
        Globals.ActiveControls(character, true);
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
        if (dialogCount == 12)
            StartCoroutine(startBattle());

    }
    IEnumerator startBattle()
    {
        foreach (Transform child in guard1.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Attack");
        }
        foreach (Transform child in guard2.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Attack");
        }
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadSceneAsync("BattleScene_Cave");
    }
}
