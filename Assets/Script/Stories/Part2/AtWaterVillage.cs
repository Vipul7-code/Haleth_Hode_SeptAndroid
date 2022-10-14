using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pathfinding;
public class AtWaterVillage : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
  public  GameObject character, marium, john,sargent,sage, soldier, soldier1,sol2,sol3,vill1,vill2,vill3;
    [SerializeField]
    Transform playerPos,sargentPos,sagePos;
    [SerializeField]
    Transform[] soldierPos;
    [SerializeField]
    Transform[] villagersPos;
    [SerializeField]
    Camera mainCamera;
    public PlayableDirector playble;
    public GameObject dialogBox,worldMap;
    int dialogCount;
    [HideInInspector]
   public string sideName;
   public GameObject[] targetPos;
    [SerializeField]
    GameObject[] boundaries;
    List<GameObject> soldiers = new List<GameObject>();

    [SerializeField]
    AudioSource bg;
   
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.isShop = true;
        Globals.atwater = this;
        Globals.activePart = "Atwater";
        FirstPart();

    }
    public void CompleteVideo()
    {
        Globals.InnVisit = 1;
        SetBoundaries();
        Globals.ActiveControls(character, true);
    }
    void FirstPart()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterChurch|| Globals.enterInn||Globals.enterShop||Globals.enterFarmhouse||Globals.enterMayor)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x,(Globals.enterPos.y-0.3f),0), Quaternion.identity);
            Globals.ActiveControls(character, true);
            Globals.enterChurch = false;
            Globals.enterInn = false;
            Globals.enterShop = false;
            Globals.enterFarmhouse = false;
            Globals.enterMayor = false;
            Globals.ActiveFaces(character, true, false, false, false);
            HaultedCondition();
        }
        else
        {
            if (Globals.atWaterCount == 0)
            {
                bg.clip = Resources.Load("Sound/TownAttack/TownUnderAttack") as AudioClip;
                bg.Play();
                if (!Globals.isPart1Battle)
                    character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                else
                    character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                if (!Globals.isSargent && !Globals.sargentKill)
                {
                    SpawnSoldier();
                    Globals.ActiveControls(character, true);
                }
                else
                {
                    Globals.isSargent = false;
                    Globals.sargentKill = true;
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_M_AtwaterVillage_TakeOn_Sargent_Dialog(Before Death)") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "WarriorFemale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_F_AtwaterVillage_TakeOn_Sargent_Dialog(Before Death)") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherMale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_M_AtwaterVillage_TakeOn_Sargent_Dialog(Before Death)") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherFemale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_F_AtwaterVillage_TakeOn_Sargent_Dialog(Before Death)") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestMale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_M_AtwaterVillage_TakeOn_Sargent_Dialog(Before Death)") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestFemale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_F_AtwaterVillage_TakeOn_Sargent_Dialog(Before Death)") as TimelineAsset;
                    playble.Play();
                    Globals.ActiveControls(character, false);
                }
                SpawnSargent();

            }
            else
            {
                bg.clip = Resources.Load("Sound/TownAttack/Peaceful Town Music mix") as AudioClip;
                bg.Play();
                character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                Villagers();
                SpawnSage();
            }
            
        }
        character.tag = "Player";
        if(Globals.sideName=="down")
          Globals.ActiveFaces(character, false, true, false, false);
        else if(Globals.sideName=="up")
            Globals.ActiveFaces(character, true, false, false, false);
        else if(Globals.sideName== "rSide")
            Globals.ActiveFaces(character, false, false, true, false);
        else if (Globals.sideName == "lSide")
            Globals.ActiveFaces(character, false, false, false, true);
        if (Globals.isPart1Battle)
            character.GetComponent<EntityGroup>().backFace.GetComponent<MeshRenderer>().sortingOrder = 1;
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
       

    }
    void SetBoundaries()
    {
        foreach(var v in boundaries)
        {
            v.tag = "LeftHunsville";
        }
    }
    void HaultedCondition()
    {
      //  if (Globals.ishault)
        {
            if (Globals.atWaterCount != 0)
            {
                SpawnSage();
                Villagers();
                if (Globals.conversationCount >= 3)
                {
                    SetBoundaries();
                    foreach (var v in soldiers)
                    {
                        foreach (var s in v.transform.GetChild(0).GetComponent<EntityGroup>().allSides)
                        {
                            s.gameObject.SetActive(false);
                        }
                    }
                }
                else if (Globals.isCompleteVid)
                    SetBoundaries();
            }
            else
            {
                if(Globals.isSargent || Globals.sargentKill)
                    SetBoundaries();
                else
                {
                    if (Globals.conversationCount >= 3)
                        SetBoundaries();
                    else
                    {
                        SpawnSoldier();
                        if (!Globals.sargentKill)
                            SpawnSargent();
                    }
                }
            }
        }
    }
  
    int value;
    void Villagers()
    {
        GameObject sol1 = Resources.Load(("Others/Moveable1"), typeof(GameObject)) as GameObject;
        soldier1 = Instantiate(sol1, villagersPos[0].position, Quaternion.identity);
        soldiers.Add(soldier1);
        GameObject soll2 = Resources.Load(("Others/Moveable2"), typeof(GameObject)) as GameObject;
        sol2 = Instantiate(soll2, villagersPos[1].position, Quaternion.identity);
        soldiers.Add(sol2);
        GameObject soll3 = Resources.Load(("Others/Moveable4"), typeof(GameObject)) as GameObject;
        sol3 = Instantiate(soll3, villagersPos[2].position, Quaternion.identity);
        soldiers.Add(sol3);

    }
    void SpawnSoldier()
    {
        if (Globals.conversationCount == 0)
            value = 3;                                                                                                                              
        else if (Globals.conversationCount == 1)
            value = 2;
        else if (Globals.conversationCount == 2)
            value = 1;
        else if (Globals.conversationCount >=2)
            SetBoundaries();
        for (int i=0;i<value; i++)
        {
            GameObject sol = Resources.Load(("Enemy/MoveableSoldiers"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(sol, soldierPos[i].position, Quaternion.identity);
            soldier.GetComponent<AIDestinationSetter>().isStart = true;
            soldier.GetComponent<AIDestinationSetter>().target = targetPos[i].transform;
            soldiers.Add(soldier);
        }
        Globals.ActiveControls(character, true);
    }
    bool isVill;
    private void Update()
    {
        if (Globals.PlayNow)
        {
            foreach(var v in Globals.collideObject.transform.parent.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            if (Globals.atWaterCount == 0)
            {
                if (Globals.collideObject.transform.parent.name == "SargentAtArms(Clone)" && !Globals.sargentKill)
                {
                    Globals.isSargent = true;
                    if (character.GetComponent<PlayerController>().isComplete)
                    {
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_M_AtwaterVillage_TakeOn_EntireVillage_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_F_AtwaterVillage_TakeOn_EntireVillage_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_M_AtwaterVillage_TakeOn_EntireVillage_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_F_AtwaterVillage_TakeOn_EntireVillage_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_M_AtwaterVillage_TakeOn_EntireVillage_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_F_AtwaterVillage_TakeOn_EntireVillage_Dialog") as TimelineAsset;
                    }
                    else
                    {
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_M_AtwaterVillage_TakeOn_Sargent_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_F_AtwaterVillage_TakeOn_Sargent_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_M_AtwaterVillage_TakeOn_Sargent_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_F_AtwaterVillage_TakeOn_Sargent_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_M_AtwaterVillage_TakeOn_Sargent_Dialog") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_F_AtwaterVillage_TakeOn_Sargent_Dialog") as TimelineAsset;
                    }
                }
                else
                {
                    if (Globals.conversationCount == 0)
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/AtwaterVillageExterior_Soldier1") as TimelineAsset;
                    else if (Globals.conversationCount == 1)
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/AtwaterVillageExterior_Soldier2") as TimelineAsset;
                    else if (Globals.conversationCount == 2)
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/AtwaterVillageExterior_Soldier3") as TimelineAsset;
                    Globals.collideObject.transform.parent.GetComponent<NPCMovement>().enabled = false;
                    Globals.collideObject.transform.parent.parent.GetComponent<AIDestinationSetter>().enabled = false;
                    Globals.collideObject.transform.parent.parent.GetComponent<AIPath>().enabled = false;
                    Globals.collideObject.transform.parent.parent.GetComponent<Seeker>().enabled = false;
                }
            }
            else
            {
                if (Globals.collideObject.transform.parent.name == "AtwaterSage(Clone)")
                {
                    Globals.isSargent = true;
                    dialogCount = 0;
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_M_AtwaterVillageExterior_OldSage_Dialogs_V01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "WarriorFemale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Smith/Smith_F_AtwaterVillageExterior_OldSage_Dialogs_V01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherMale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_M_AtwaterVillageExterior_OldSage_Dialogs_V01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherFemale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Archer/Archer_F_AtwaterVillageExterior_OldSage_Dialogs_V01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestMale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_M_AtwaterVillageExterior_OldSage_Dialogs_V01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestFemale")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/Acolyte/Acolyte_F_AtwaterVillageExterior_OldSage_Dialogs_V01") as TimelineAsset;
                }
                else
                {
                    isVill = true;
                    if (Globals.collideObject.transform.parent.name == "atwaterTown1")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/AtwaterVillageExterior_Villager1") as TimelineAsset;
                    else if (Globals.collideObject.transform.parent.name== "atwaterTown5")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/AtwaterVillageExterior_Villager2") as TimelineAsset;
                    else if (Globals.collideObject.transform.parent.name == "atwaterTown2")
                        playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/AtwaterVillageExterior_Villager3") as TimelineAsset;
                }
            }
            Globals.ActiveControls(character, false);
            playble.Play();
            Globals.collideObject = null;
            Globals.PlayNow = false;
        }
    }
  
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if(Globals.atWaterCount==0)
        {
            if(!Globals.isSargent)
            {
                if (!Globals.sargentKill)
                {
                    if (dialogCount == 1)
                    {
                        Globals.conversationCount++;
                        StartCoroutine(StartBattle());
                    }
                }
            }
            else
            {
                if (character.GetComponent<PlayerController>().isComplete)
                {
                    if (dialogCount == 12)
                        BattleStartCondition();
                }
                else
                {
                    if (dialogCount == 6)
                        BattleStartCondition();
                }
            }
        }
        else
        {
            if (Globals.isSargent)
            {
                if (isVill)
                {
                    StartCoroutine(Controls());
                    isVill = false;
                }
                else
                {
                    if (dialogCount == 22)
                    {
                        Globals.InnVisit = 1;
                        Globals.isCompleteVid = true;
                        SetBoundaries();
                        StartCoroutine(Controls());
                        //Globals.ActiveControls(character, true);
                        //SargentSetting(false);
                        //Globals.isSargent = false;
                        //Globals.sargentKill = true;
                    }
                }
            }
            else
            {
                Debug.Log("else");
                isVill = false;
                StartCoroutine(Controls());
                SetBoundaries();
                Globals.conversationCount++;
            }
        }                                      
    }
    IEnumerator Controls()
    {
        yield return new WaitForSeconds(0.6f);
        Globals.ActiveControls(character, true);
    }
    void BattleStartCondition()
    {
        character.GetComponent<PlayerController>().isComplete = false;
        Globals.isFirstCompleteStory = true;
        StartCoroutine(StartBattle());
    }
    IEnumerator StartBattle()
    {
        Globals.enterPos = character.transform.localPosition;
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadSceneAsync("Battle Scene_CobbleStone");
    }
    void SpawnSage()
    {
        GameObject sar = Resources.Load(("Others/AtwaterSage"), typeof(GameObject)) as GameObject;
        sargent = Instantiate(sar, sagePos.position, Quaternion.identity);
        Globals.ActiveFaces(sargent, false, false, true, false);
        if (!Globals.isCompleteVid)
            SargentSetting(true);
        else
            SargentSetting(false);
    }
    void SpawnSargent() 
    {
        GameObject sar = Resources.Load(("Enemy/SargentAtArms"), typeof(GameObject)) as GameObject;
        sargent = Instantiate(sar, sargentPos.position, Quaternion.identity);
        Globals.ActiveFaces(sargent, false, false, true, false);
        if (!Globals.sargentKill)
            SargentSetting(true);
        else
        {
            SargentSetting(false);
            if (Globals.atWaterCount == 0)
            {
                foreach (Transform child in sargent.transform)
                {
                    if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                        child.GetComponent<Animator>().SetTrigger("Death");
                }
                Debug.Log("sargent kill.........");
                sargent.GetComponent<BoxCollider2D>().size = new Vector2(3.825395f, 2.773944f);
                sargent.GetComponent<BoxCollider2D>().offset = new Vector2(0.9432187f, 0.6025465f);
            }
        }
    }
    void SargentSetting( bool set)
    {
        foreach(var v in sargent.GetComponent<EntityGroup>().allSides)
        {
            v.gameObject.SetActive(set);
        }
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
}
