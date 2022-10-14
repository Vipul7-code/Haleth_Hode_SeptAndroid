using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    GameObject character, priest,innKeeper,wife,son,daughter;
    [SerializeField]
    Transform playerPos, priestPos,playerPos1,innKeeperPos,sonPos,daughterPos,wifePos;
    [SerializeField]
    Camera mainCamera;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    DatabaseManager db;
    Scene currentScene;
    [SerializeField]
    GameObject townPeople;
    [SerializeField]
    Transform playerNewPos;
    [SerializeField]
     GameObject entryPoint1,entryPoint2,entryPoint3,worldMapButton;
    [SerializeField]
    GameObject exit,SacretCave,openBox,revealCave;
    [SerializeField]
    GameObject[] boundaries,hideObject;
    [SerializeField]
    AudioSource bgMusic;
    [SerializeField]
    Transform[] nunPos;
    [SerializeField]
    Transform[] monkPos;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        db = FindObjectOfType<DatabaseManager>();
       // Globals.againVisit = 1;
      //  Globals.isPart1Battle = true;
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
         Globals.isShop = true;
        StoryParts();

    }
    void StoryParts()
    {
        switch(Globals.activeScene)
        {
            case Globals.CurrentScene.SoldierCampsite:
                Globals.isAnim = true;
                Debug.Log("soldier campsite :: "+ Globals.soldierCampsiteVisit);
                if (Globals.soldierCampsiteVisit == 0)
                {
                    if (Globals.isPart1Battle)
                    {
                        bgMusic.Stop();
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Smith/Smith_M_SoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Smith/Smith_F_SoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Archer/Archer_M_SoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Archer/Archer_F_SoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Acolyte/Acolyte_M_SoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Acolyte/Acolyte_F_SoldierCampsite_02") as TimelineAsset;
                    }
                    else if (!Globals.isPart1Battle)
                    {
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Smith/Smith_M_SoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Smith/Smith_F_SoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Archer/Archer_M_SoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Archer/Archer_F_SoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Acolyte/Acolyte_M_SoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/Soldier Campsite/Acolyte/Acolyte_F_SoldierCampsite_01") as TimelineAsset;
                    }
                }
                else
                {
                    if (Globals.isPart1Battle)
                    {
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Smith/Smith_M_SecondSoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Smith/Smith_F_SecondSoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Archer/Archer_M_SecondSoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Archer/Archer_F_SecondSoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Acolyte/Acolyte_M_SecondSoldierCampsite_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Acolyte/Acolyte_F_SecondSoldierCampsite_02") as TimelineAsset;
                    }
                    else if (!Globals.isPart1Battle)
                    {
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Smith/Smith_M_SecondSoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Smith/Smith_F_SecondSoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Archer/Archer_M_SecondSoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Archer/Archer_F_SecondSoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Acolyte/Acolyte_M_SecondSoldierCampsite_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/SecondCampsite/Acolyte/Acolyte_F_SecondSoldierCampsite_01") as TimelineAsset;
                    }
                }
                playble.Play();
                break;
            case Globals.CurrentScene.WagonCaravan:
                Globals.isAnim = true;
                if (Globals.isPart1Battle)
                {
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Smith/M_WagonCarvan_02") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "WarriorFemale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Smith/F_WagonCarvan_02") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherMale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Archer/Archer_M_WagonCarvan_02") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherFemale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Archer/Archer_F_WagonCarvan_02") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestMale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Acolyte/Acolyte_M_WagonCarvan_02") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestFemale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Acolyte/Acolyte_F_WagonCarvan_02") as TimelineAsset;
                }
                else if (!Globals.isPart1Battle)
                {
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Smith/M_WagonCarvan_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "WarriorFemale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Smith/F_WagonCarvan_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherMale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Archer/Archer_M_WagonCarvan_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherFemale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Archer/Archer_F_WagonCarvan_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestMale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Acolyte/Acolyte_M_WagonCarvan_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestFemale")
                        playble.playableAsset = Resources.Load("Playables/WagonCarvan/Acolyte/Acolyte_F_WagonCarvan_01") as TimelineAsset;
                }
                playble.Play();
                break;
            case Globals.CurrentScene.SecondSoldierCaravan:
                Globals.isAnim = true;
                if (Globals.isPart1Battle)
                    playble.playableAsset = Resources.Load("Playables/SecondCampsite/SecondSoldierCampsite_02") as TimelineAsset;
                else if (!Globals.isPart1Battle)
                {
                  //  Globals.avatarState.TotalXp = 940;
                  //  Globals.avatarState.Level = 3;
                   // db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                    playble.playableAsset = Resources.Load("Playables/SecondCampsite/SecondSoldierCampsite_01") as TimelineAsset;
                }
                playble.Play();
                break;
            case Globals.CurrentScene.Huntsville:
                Globals.isAnim = true;
                Globals.objectiveScene = "Huntsville";
                if (Globals.secondVisit == 0)
                {
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Smith/Smith_M_HuntsvilleVillageExterior_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "WarriorFemale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Smith/Smith_F_HuntsvilleVillageExterior_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherMale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Archer/Archer_M_HuntsvilleVillageExterior_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherFemale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Archer/Archer_F_HuntsvilleVillageExterior_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestMale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Acolyte/Acolyte_M_HuntsvilleVillageExterior_01") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestFemale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Acolyte/Acolyte_F_HuntsvilleVillageExterior_01") as TimelineAsset;
                    playble.Play();
                }
                else if (Globals.secondVisit == 1)
                {
                    if (currentScene.name == "Huntsville Chruch_int")
                    {
                        playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville_Church_Interior_Dialogue_01") as TimelineAsset;
                        playble.Play();
                    }
                       
                }
                else if(Globals.secondVisit==2)
                {
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Smith/Smith_M_HunsvilleVillageExtirior_Part4") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "WarriorFemale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Smith/Smith_F_HunsvilleVillageExtirior_Part4") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherMale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Archer/Archer_M_HunsvilleVillageExtirior_Part4") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherFemale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Archer/Archer_F_HunsvilleVillageExtirior_Part4") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestMale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Acolyte/Acolyte_M_HunsvilleVillageExtirior_Part4") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestFemale")
                        playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Acolyte/Acolyte_F_HunsvilleVillageExtirior_Part4") as TimelineAsset;
                    playble.Play();
                }
                break;
            case Globals.CurrentScene.AtwaterVillage:
                playble.playableAsset = Resources.Load("Playables/AtwaterVillageExt/AtwaterVillageExterior_Dialogs_V01") as TimelineAsset;
                playble.Play();
                break;
            case Globals.CurrentScene.SacredPlace:
               
                if (Globals.storyCount == 0)
                {
                    Globals.isAnim = true;
                    SacretPlaceStart();
                }
                break; 
            case Globals.CurrentScene.MonkCampsite:
                Globals.isAnim = true;
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Monk's Campsite/Smith/Smith_M_Monk's_Campsite_V02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Monk's Campsite/Smith/Smith_F_Monk's_Campsite_V02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Monk's Campsite/Archer/Archer_M_Monk's_Campsite_V02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Monk's Campsite/Archer/Archer_F_Monk's_Campsite_V02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Monk's Campsite/Acolyte/Acolyte_M_Monk's_Campsite_V02") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Monk's Campsite/Acolyte/Acolyte_F_Monk's_Campsite_V02") as TimelineAsset;
                playble.Play();
                break;
            case Globals.CurrentScene.CellarInt:
                if (!Globals.secondVisitMon)
                {
                    if (!Globals.isPart1Battle)
                    {
                        Globals.isAnim = true;
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_M_Monastery_Cellar_Dialogue_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_F_Monastery_Cellar_Dialogue_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_M_Monastery_Cellar_Dialogue_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_F_Monastery_Cellar_Dialogue_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_M_Monastery_Cellar_Dialogue_01") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_F_Monastery_Cellar_Dialogue_01") as TimelineAsset;
                        playble.Play();
                    }
                    else
                        SpawnCharacters();
                }
                else
                    SpawnCharacters();
                break;
            case Globals.CurrentScene.CellarTucker:
                if (!Globals.secondVisitMon)
                {
                    if (!Globals.isPart1Battle)
                    {
                        Globals.isAnim = true;
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_M_Monastery_Cellar_Dialogue_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_F_Monastery_Cellar_Dialogue_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_M_Monastery_Cellar_Dialogue_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_F_Monastery_Cellar_Dialogue_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_M_Monastery_Cellar_Dialogue_02") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_F_Monastery_Cellar_Dialogue_02") as TimelineAsset;
                        playble.Play();
                    }
                }
                
                break;
        }
    }
    public void OnSoundChange()
    {
        Globals.noOfCompanions += 1;

        Debug.Log("companion badh gya.......................");
    }
    void SacretPlaceStart()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.againVisit == 1)
        {
            entryPoint1.SetActive(false);
            entryPoint2.SetActive(false);
            entryPoint3.SetActive(false);
            if (currentScene.name == "Sacred Place Exterior_New")
            {
                foreach (var v in boundaries)
                {
                    v.tag = "LeftCampsite";
                }
            }

        }
        if(Globals.againVisit==0)
           character = Instantiate(Globals.activePlayer, playerNewPos.position, Quaternion.identity);
        else
            character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, true, false, false, false);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
       
    }
  public  void OnConversationStart()
    {
        Destroy(character.gameObject);
       // if (currentScene.name == "Sacred Place Exterior_New")
        {
            entryPoint1.SetActive(false);
            entryPoint2.SetActive(false);
            entryPoint3.SetActive(false);
        }
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        playble.gameObject.SetActive(true);
        playble.transform.parent.GetComponent<GameManager>().enabled = false;
        mainCamera.orthographicSize = 3.72f;
        mainCamera.nearClipPlane = 0.1f;
        mainCamera.farClipPlane = 5000;
        if (currentScene.name == "Sacred Place Exterior_New")
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Exterior/Smith/Smith_M_SacredPlace_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Exterior/Smith/Smith_F_SacredPlace_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Exterior/Archer/Archer_M_SacredPlace_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Exterior/Archer/Archer_F_SacredPlace_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Exterior/Acolyte/Acolyte_M_SacredPlace_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Exterior/Acolyte/Acolyte_F_SacredPlace_Dialog_01") as TimelineAsset;
        }
        else
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Dungeon/Smith/Smith_M_SacredPlace_Dungeon_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Dungeon/Smith/Smith_F_SacredPlace_Dungeon_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Dungeon/Archer/Archer_M_SacredPlace_Dungeon_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Dungeon/Archer/Archer_F_SacredPlace_Dungeon_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Dungeon/Acolyte/Acolyte_M_SacredPlace_Dungeon_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Sacred Place Dungeon/Acolyte/Acolyte_F_SacredPlace_Dungeon_01") as TimelineAsset;
          //  openBox.SetActive(true);
        }
            playble.Play();
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite)
        {
            if (Globals.soldierCampsiteVisit == 0)
            {
                if (dialogCount == 4)//4
                {
                    if (!Globals.isPart1Battle)
                        StartCoroutine(StartBattle());
                }
                else if (dialogCount == 22)
                {
                    Globals.noOfCompanions = 2;
                    Globals.UpdateDefaultEquipment();
                    DatabaseManager.instance.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                    DatabaseManager.instance.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
                }
                 
            }
            else
            {
                
                if (dialogCount == 15)//15
                {
                    if (!Globals.isPart1Battle)
                        StartCoroutine(StartBattle());
                }
            }
        }
        else if (Globals.activeScene == Globals.CurrentScene.WagonCaravan)
        {
            if (dialogCount == 10)//10
            {
                if (!Globals.isPart1Battle)
                    StartCoroutine(StartBattle());
            }
        }
    }
    IEnumerator StartBattle()
    {
        if(Globals.activeScene==Globals.CurrentScene.WagonCaravan)
            yield return new WaitForSeconds(5.2f);
        else
          yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("BattleScene");
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        Globals.isAnim = false;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
        if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage)
            SpawnCharacters();
        else if (Globals.activeScene == Globals.CurrentScene.SacredPlace)
        {
            Debug.Log("complete vid");
            Globals.isLightening = true;
            StartCoroutine(ShowPlayer());
        }

        else if (Globals.activeScene == Globals.CurrentScene.Huntsville && Globals.secondVisit == 2)
            SceneManager.LoadSceneAsync("Huntsville Chruch_int");
        else if (Globals.activeScene == Globals.CurrentScene.CellarInt || Globals.activeScene == Globals.CurrentScene.CellarTucker)
            SceneManager.LoadSceneAsync("Battle Scene_Castle");
        else
        {
            playble.transform.parent.GetComponent<GameManager>().enabled = true;
            playble.transform.parent.GetComponent<GameManager>().SpawnCharacters();
            playble.gameObject.SetActive(false);
            if (Globals.activeScene == Globals.CurrentScene.WagonCaravan)
                SpawnTownPeople();
        } 
        OnStoryComplete(Globals.activeScene);
        Globals.isFirstCompleteStory = true;
    }
    void SpawnTownPeople()
    {
        GameObject sar = Resources.Load(("Others/atwaterTown1"), typeof(GameObject)) as GameObject;
        innKeeper = Instantiate(sar, innKeeperPos.position, Quaternion.identity);
        GameObject wi = Resources.Load(("Others/atwaterTown2"), typeof(GameObject)) as GameObject;
        wife = Instantiate(wi, wifePos.position, Quaternion.identity);
        GameObject so = Resources.Load(("Others/FemaleTOwn1"), typeof(GameObject)) as GameObject;
        son = Instantiate(so, sonPos.position, Quaternion.identity);
        GameObject dau = Resources.Load(("Others/FemaleTown2"), typeof(GameObject)) as GameObject;
        daughter = Instantiate(dau, daughterPos.position, Quaternion.identity);
        Globals.ActiveFaces(innKeeper, false, true, false, false);
        Globals.ActiveFaces(wife, false, true, false, false);
        Globals.ActiveFaces(son, false, true, false, false);
        Globals.ActiveFaces(daughter, false, true, false, false);
    }
    void SpawnInnKeeper()
    {
        GameObject sar = Resources.Load(("Others/HunsvilleBlackSmith"), typeof(GameObject)) as GameObject;
        innKeeper = Instantiate(sar, innKeeperPos.position, Quaternion.identity);
        GameObject wi = Resources.Load(("Others/InnkeeperWife"), typeof(GameObject)) as GameObject;
        wife = Instantiate(wi, wifePos.position, Quaternion.identity);
        GameObject so = Resources.Load(("Others/InnKeeperSon"), typeof(GameObject)) as GameObject;
        son = Instantiate(so, sonPos.position, Quaternion.identity);
        GameObject dau = Resources.Load(("Others/InnKeeperDoughter"), typeof(GameObject)) as GameObject;
        daughter = Instantiate(dau, daughterPos.position, Quaternion.identity);
    }
    IEnumerator ShowPlayer()
    {
        yield return new WaitForSeconds(2f);
        if (currentScene.name == "Sacred Place Interior")
        {
            openBox.SetActive(true);
            playble.transform.parent.GetComponent<GameManager>().enabled = true;
            playble.transform.parent.GetComponent<GameManager>().SpawnCharacters();
            playble.gameObject.SetActive(false);
        }
        else
           SpawnCharacters();
    }
    void SpawnCharacters()
    {
        if (Globals.activeScene == Globals.CurrentScene.CellarInt)
            playble.gameObject.SetActive(false);
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite && Globals.soldierCampsiteVisit == 1)
        {
            SpawnInnKeeper();
            Globals.ActiveFaces(character, false, true, false, false);
        }
        else if (currentScene.name == "Sacred Place Interior")
        {
            exit.SetActive(true);
            openBox.SetActive(true);
            Globals.ActiveFaces(character, true, false, false, false);
        }
        else if (currentScene.name == "Sacred Place Exterior_New")
        {
            revealCave.SetActive(false);
            SacretCave.SetActive(true);
            Globals.ActiveFaces(character, false, false, true, false);
        }
        else if (currentScene.name == "Monastery2ndFloor_int")
            spawnNunMonk();
        else
            Globals.ActiveFaces(character, false, false, true, false);

    }
    void spawnNunMonk()
    {
        for(int i=0;i<3;i++)
        {
            GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
            GameObject nun = Instantiate(non, nunPos[i].transform.position, Quaternion.identity);
            Globals.ActiveFaces(nun, true, false, false, false);
             foreach(var v in nun.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(false);
            }
        }
        for(int i=0;i<3;i++)
        {
            GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
            GameObject mnk= Instantiate(monk, monkPos[i].transform.position, Quaternion.identity);
            Globals.ActiveFaces(mnk, true, false, false, false);
            foreach (var v in mnk.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(false);
            }
        }
    }
    void OnStoryComplete(Globals.CurrentScene scene)
    {
        switch(scene)
        {
            case Globals.CurrentScene.Huntsville: 
                if (Globals.secondVisit == 0 && !Globals.loadSaveGame)
                {
                  //  Globals.loadSaveGame = false;
                    Globals.avatarState.TotalXp += 500;
                } 
                else if (Globals.secondVisit == 1)
                    Globals.avatarState.TotalXp += 800;
                else if (Globals.secondVisit == 2)
                    Globals.avatarState.TotalXp += 1250;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.CellarTucker:
                    Globals.avatarState.TotalXp += 1000;
                    db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                break;
            case Globals.CurrentScene.TheBrigand:
                if(Globals.secondVisit==1)
                {
                    Globals.avatarState.TotalXp += 1500;
                    db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                }
                break;
        }
        LevelCalculation.instance.CalculateXpPoints();
    }
}
