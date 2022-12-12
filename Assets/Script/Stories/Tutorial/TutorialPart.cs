using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;


public class TutorialPart : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character,rat1,rat2,rat3, priest,barmaid;
    [SerializeField]
  public  PlayableDirector playble;
    [SerializeField]
    GameObject dialogBox,churchGate,dengeonWall,innDoor,innSecond,innthird,mayorConvo,john,thirdDoor, thirdDoorCube,forthCube;
    int dialogCount;
    DatabaseManager db;
    Scene currentScene;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform playerPos,wellDengeonPos,wellPos2,ratPos1,ratPos2,ratPos3,priestPos,innStartPos,barmailPos;
    [SerializeField]
    GameObject[] boundaries,fireObject;
    [SerializeField]
    GameObject particleSystem, questButton,battleWall;
    [SerializeField]
    AudioSource fireBg;
    [SerializeField]
    AudioSource doorPlay,ratSounds,stairsSound,bgSound, giantSound;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("here ..........."+ Globals.conversationCount);
        Globals.completeIntro = true;
        Globals.tutorialPart = this;
        Globals.isShop = true;
        Globals.activeScene = Globals.CurrentScene.Tutorial;
        currentScene = SceneManager.GetActiveScene();
        db = FindObjectOfType<DatabaseManager>();
        if(currentScene.name== "Huntsville Chruch_int")
        {
            thirdDoor.SetActive(false);
            thirdDoorCube.SetActive(false);
            forthCube.SetActive(true);
        }
        if (Globals.conversationCount == 0)
            FirstPart();
        else if (Globals.conversationCount == 1)
            Second();
        else if (Globals.conversationCount == 2)
        {
            ratSounds.Stop();
            battleWall.SetActive(false);
            if (Globals.dengeonTreasure == 0)
            {
                dengeonWall.tag = "Untagged";
                SpawnPlayer();
            }
            else
            {
                dengeonWall.tag = "exitDengeon";
                SecondPart();
            }
        }
        else if (Globals.conversationCount == 3)
        {
            if (Globals.dengeonTreasure == 1)
            {
                if (Globals.avatarState.SoundLevel == 1)
                    stairsSound.Play();
                SpawnPriest();
                SpawnPlayer();
            }
            else
                ThirdPart();
        }
        else if (Globals.conversationCount == 4)
            ForthPart();
        else if (Globals.conversationCount == 7)
        {
            if (currentScene.name == "Huntsville_Inn_1stFloor")
            {
                if (Globals.innDialog == 0)
                    innDoor.tag = "Untagged";
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Tutorial/M_Smith/Tutorial(M_Smith)8_HuntsvilleInn 1") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Tutorial/F_Smith/Tutorial(F_Smith)8_HuntsvilleInn") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Tutorial/M_Scout/Tutorial(M_Scout)8_HuntsvilleInn") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Tutorial/F_Scout/Tutorial(F_Scout)8_HuntsvilleInn") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Tutorial/M_Acolyte/Tutorial(M_Acolyte)8_HuntsvilleInn") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Tutorial/F_Acolyte/Tutorial(F_Acolyte)8_HuntsvilleInn") as TimelineAsset;
                playble.Play();
                SpawnPlayer();

            }
        }
        if (currentScene.name == "Huntsville_Mayor_Int" || currentScene.name == "Huntsville_MerchantShop_Int")
            SpawnPlayer();
        if (currentScene.name == "Huntsville_Damaged" && Globals.conversationCount >= 6)
        {

            OnBattle();
            bgSound.clip = Resources.Load("Sound/TownAttack/Peaceful Town Music mix") as AudioClip;
            bgSound.Play();
            particleSystem.SetActive(false);
            SpawnPlayer();
            foreach(var v in fireObject)
            {
                v.SetActive(false);
            }
            if (Globals.conversationCount >= 8)
            {
                //questButton.SetActive(false);
                SetBoundaries();
            }
        }
    }
    public void ChurchSetting()
    {
        thirdDoor.SetActive(true);
        thirdDoorCube.SetActive(true);
        Debug.Log("nikl gya............");
        //Invoke("DealyInDoorCollider", 2f);
    }
    void  DealyInDoorCollider()
    {
        thirdDoorCube.SetActive(true);
    }
    void SpawnPriest()
    {
        GameObject mar = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
        priest = Instantiate(mar, priestPos.position, Quaternion.identity);
        Globals.ActiveFaces(priest, true, false, false, false);
    }
    public void StairsSoundPlay()
    {
        if(Globals.avatarState.SoundLevel==1)
            stairsSound.Play();
    }
    public void TreasureDialogs()
    {
        if (Globals.dengeonTreasure == 0)
        {
            character.SetActive(false);
            playble.gameObject.SetActive(true);
            Globals.dengeonTreasure++;
            playble.transform.parent.GetComponent<TutorialPart>().enabled = false;
        }
    }
    public void ChurchDialog()
    {
        character.SetActive(false);
        priest.SetActive(false);
        playble.gameObject.SetActive(true);
        Globals.dengeonTreasure++;
        playble.transform.parent.GetComponent<TutorialPart>().enabled = false;
    }
    public void OnSoundChange()
    {
        Debug.Log("on sound change...............");
        bgSound.clip = Resources.Load("Sound/ThemeSong/soundChange") as AudioClip;
        bgSound.Play();
    }
    public void OnBattle()
    {
        fireBg.gameObject.SetActive(false);
        bgSound.Stop();
    }
    public void OnDoorOpen()
    {
        if (Globals.avatarState.SoundLevel == 1)
            doorPlay.Play();
        if(Globals.dengeonDoor==0)
             SpawnRats();

    }
    void SpawnRats()
    {
        GameObject r1= Resources.Load(("Enemy/Rat"), typeof(GameObject)) as GameObject;
        rat1= Instantiate(r1, ratPos1.position, Quaternion.identity);
        Globals.ActiveFaces(rat1, false, false, true, false);
        GameObject r2 = Resources.Load(("Enemy/Rat"), typeof(GameObject)) as GameObject;
        rat2 = Instantiate(r2, ratPos2.position, Quaternion.identity);
        Globals.ActiveFaces(rat2, false, false, true, false);
        GameObject r3 = Resources.Load(("Enemy/Rat"), typeof(GameObject)) as GameObject;
        rat3 = Instantiate(r3, ratPos3.position, Quaternion.identity);
        Globals.ActiveFaces(rat3, false, false, false, true);
        if (Globals.avatarState.SoundLevel == 1)
            ratSounds.Play();
        Globals.dengeonDoor = 1;
    }
  public  void SeventhPart()
    {
        Debug.Log("in seventh part...............");
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        Globals.ActiveControls(character, false);
        playble.Play();
    }
    void ForthPart()
    {
        bgSound.Play();
        Debug.Log("in fourth part...............");
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Smith/Tutorial(M_Smith)5_HuntsvilleVillage Exterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Smith/Tutorial(F_Smith)5_HuntsvilleVillage Exterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Scout/Tutorial(M_Scout)5_HuntsvilleVillage Exterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Scout/Tutorial(F_Scout)5_HuntsvilleVillage Exterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Acolyte/Tutorial(M_Acolyte)5_HuntsvilleVillage Exterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Acolyte/Tutorial(F_Acolyte)5_HuntsvilleVillage Exterior") as TimelineAsset;
        playble.Play();
        Invoke("PlayGustSound", 5.5f);
    }
    void PlayGustSound()
    {
        Debug.Log("play guest sound :: ");
        giantSound.gameObject.SetActive(true);
    }
    void ThirdPart()
    {
        //  Globals.ActiveFaces(priest, false, false, false, false);
        Debug.Log("in third part...............");
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Smith/Tutorial(M_Smith)4_Huntsville_ChurchInterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Smith/Tutorial(F_Smith)4_Huntsville_ChurchInterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Scout/Tutorial(M_Scout)4_Huntsville_ChurchInterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Scout/Tutorial(F_Scout)4_Huntsville_ChurchInterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Acolyte/Tutorial(M_Acolyte)4_Huntsville_ChurchInterior") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Acolyte/Tutorial(F_Acolyte)4_Huntsville_ChurchInterior") as TimelineAsset;
        playble.Play();
    }
    void Second()
    {
        Debug.Log("in second...............");
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Smith/Tutorial(M_Smith)2_Huntsville_WellDungeon 1") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Smith/Tutorial(F_Smith)2_Huntsville_WellDungeon 1") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Scout/Tutorial(M_Scout)2_Huntsville_WellDungeon 1") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Scout/Tutorial(F_Scout)2_Huntsville_WellDungeon 1") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Acolyte/Tutorial(M_Acolyte)2_Huntsville_WellDungeon 1") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Acolyte/Tutorial(F_Acolyte)2_Huntsville_WellDungeon 1") as TimelineAsset;
        playble.Play();
    }
    void SecondPart()
    {
        Debug.Log("in second part...............");
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Smith/Tutorial(M_Smith)3_Huntsville_WellDungeon 2") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Smith/Tutorial(F_Smith)3_Huntsville_WellDungeon 2") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Scout/Tutorial(M_Scout)3_Huntsville_WellDungeon 2") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Scout/Tutorial(F_Scout)3_Huntsville_WellDungeon 2") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Acolyte/Tutorial(M_Acolyte)3_Huntsville_WellDungeon 2") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Acolyte/Tutorial(F_Acolyte)3_Huntsville_WellDungeon 2") as TimelineAsset;
        playble.Play();
        Globals.dengeonTreasure = 1;
    }
    void FirstPart()
    {
        Debug.Log("in first part...............");
        if(Globals.avatarState.AvatarName== "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Smith/Tutorial(M_Smith)1_Huntsville_Damaged") as TimelineAsset;
        else if(Globals.avatarState.AvatarName== "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Smith/Tutorial(F_Smith)1_Huntsville_Damaged") as TimelineAsset;
        else if(Globals.avatarState.AvatarName== "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Scout/Tutorial(M_Scout)1_Huntsville_Damaged") as TimelineAsset;
        else if(Globals.avatarState.AvatarName== "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Scout/Tutorial(F_Scout)1_Huntsville_Damaged") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/M_Acolyte/Tutorial(M_Acolyte)1_Huntsville_Damaged") as TimelineAsset;
        else if(Globals.avatarState.AvatarName== "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Tutorial/F_Acolyte/Tutorial(F_Acolyte)1_Huntsville_Damaged") as TimelineAsset;
        playble.Play();
    }
    public void PauseClip()
    {
        Debug.Log("in pause clip...............");
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if(currentScene.name== "Huntsville_Well_Dungeon")
        {
            if (!Globals.isPart1Battle)
                SpawnPlayer();

        }
        else if(currentScene.name== "Huntsville_Inn_1stFloor")
        {
            if (dialogCount == 3)
            {
                StartCoroutine(ShowControls());
                innSecond.SetActive(true);
                Globals.innDialog = 1;
                StartCoroutine(StopPlaybles());
            }
            else if(dialogCount==11)
            {
                innSecond.SetActive(false);
                innthird.SetActive(true);
                StartCoroutine(ShowControls());
                StartCoroutine(StopPlaybles());
            }
            else if(dialogCount==17)    
                innDoor.transform.tag = "LeaveChurch";
        }
        else if(currentScene.name == "Huntsville_Damaged")
        {
            Debug.Log("dialog count :: "+dialogCount);
            if(dialogCount == 2)
                bgSound.Play();
        }
    }
    IEnumerator ShowControls()
    {
        yield return new WaitForSeconds(0.8f);
        Globals.ActiveControls(character, true);
    }
    IEnumerator StopPlaybles()
    {
        yield return new WaitForSeconds(0.5f);
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
        if (currentScene.name == "Huntsville_Inn_1stFloor")
            Globals.InnVisit = 1;
        else if (currentScene.name == "Huntsville_MerchantShop_Int")
        {
            Globals.merchantVisit = 1;
            Globals.shopMerchant.Gold += 50;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
          //  Globals.ActiveControls(character, true);
        }
        else if (currentScene.name == "Huntsville_Mayor_Int")
        {
            Globals.mayorVisit = 1;
            Globals.shopMerchant.Gold += 200;
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
            mayorConvo.SetActive(false);
            Globals.ActiveControls(character, true);
        }
       
          StartCoroutine(LoadNextScene());
        Globals.conversationCount++;
        Debug.Log("conversation count :: "+Globals.conversationCount);
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(0.3f);
        Debug.Log("conversation count ::>............ " + Globals.conversationCount);
        if (Globals.conversationCount == 1)
            SceneManager.LoadSceneAsync("Huntsville_Well_Dungeon");
        else if (Globals.conversationCount == 3)
            SpawnPlayer();
        else if (Globals.conversationCount == 4)
        {
            Globals.objectiveScene = "Huntsville_Damaged";
            SpawnPlayer();
        }
        else if (Globals.conversationCount == 5)
        {
            Globals.objectiveScene = "Huntsville_Mayor_Int";//Huntsville_Mayor_Int
            if (currentScene.name == "Huntsville_Inn_1stFloor" || currentScene.name == "Huntsville_Damaged")
                SpawnPlayer();
        }
        else if (Globals.conversationCount == 6)
        {
            Globals.objectiveScene = "Huntsville_Damaged";//Huntsville_MerchantShop_Int
            if (currentScene.name == "Huntsville_Inn_1stFloor" || currentScene.name == "Huntsville_Damaged")
                SpawnPlayer();
        }
        else if (Globals.conversationCount == 7)
        {
            Globals.objectiveScene = "Huntsville_Damaged";//Huntsville_Inn_1stFloor
            Globals.uiHandler.ClickOnButton("Merchant");
            if (currentScene.name == "Huntsville_Inn_1stFloor" || currentScene.name == "Huntsville_Damaged")
                SpawnPlayer();
        }
        else if (Globals.conversationCount == 8)
        {
            john.SetActive(false);
            innSecond.SetActive(false);
            innthird.SetActive(false);
            Globals.noOfCompanions = 1;
            Globals.UpdateDefaultEquipment();
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
            db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
            //playble.gameObject.SetActive(false);
            Globals.ActiveControls(character, true);
            OnCompleteTutorial();
        }
    }
    void SpawnBarmaid()
    {
        GameObject bar = Resources.Load(("Others/FemaleBarmaid"), typeof(GameObject)) as GameObject;
        barmaid = Instantiate(bar, barmailPos.position, Quaternion.identity);
        Globals.ActiveFaces(barmaid, true, false, false, false);
    }
    public void CompleteMerchantDialo()
    {
        Debug.Log("merchant dialog...........");

        playble.playableAsset = Resources.Load("Playables/Tutorial/Tutorial(ForAll)7_Huntsville_MerchantShop") as TimelineAsset;
        playble.Play();
        
    }
    public void CompleteMayorDialog()
    {
        Globals.ActiveControls(character, false);
        playble.playableAsset = Resources.Load("Playables/Tutorial/Tutorial(ForAll)6_Huntsville_Mayor'sHouse") as TimelineAsset;
        playble.Play();
    }
    void SpawnPlayer()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (currentScene.name == "Huntsville_Damaged")
        {
            Debug.Log("11111111111111");
            if (Globals.enterChurch || Globals.enterInn || Globals.enterMayor || Globals.enterShop)
            {
                character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.8f), 0), Quaternion.identity) as GameObject;
                Globals.enterChurch = false;
                Globals.enterInn = false;
                Globals.enterShop = false;
                Globals.enterMayor = false;
                Globals.ActiveFaces(character, true, false, false, false);
            }
            else
                character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("22222222222222222222"+currentScene.name);
            if (currentScene.name == "Huntsville_Well_Dungeon")
            {
                if (Globals.isPart1Battle)
                {
                    if (Globals.conversationCount == 3)
                        character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                    else
                        character = Instantiate(Globals.activePlayer, wellPos2.position, Quaternion.identity);
                }
                else
                {
                    character = Instantiate(Globals.activePlayer, wellDengeonPos.position, Quaternion.identity);
                    Globals.ActiveFaces(character, false, false, false, true);
                }
            }
            else if(currentScene.name== "Huntsville Chruch_int")
            {
                if (Globals.dengeonTreasure == 1)
                    character = Instantiate(Globals.activePlayer, wellPos2.position, Quaternion.identity);
                else
                {
                    Debug.Log("3333333333");
                    character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                    Globals.ActiveFaces(character, true, false, false, false);
                }
            }
            else if(currentScene.name== "Huntsville_Inn_1stFloor")
            {
                if (Globals.innDialog == 0)
                    character = Instantiate(Globals.activePlayer, innStartPos.position, Quaternion.identity);
                else
                {
                    Debug.Log("44444444444444");
                    innSecond.SetActive(false);
                    character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                }
                SpawnBarmaid();
            }
            else
                character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        }
        character.tag = "Player";
        if (currentScene.name == "Huntsville_Well_Dungeon" || currentScene.name == "Huntsville Chruch_int" || currentScene.name == "Huntsville_Damaged")
        {
            if (currentScene.name == "Huntsville Chruch_int")
            {
                Debug.Log("444444444444444 "+Globals.conversationCount);
                if (Globals.conversationCount == 3)
                    churchGate.transform.tag = "Untagged";
                else
                    churchGate.transform.tag = "LeaveChurch";
            }
            Globals.ActiveControls(character, true);
            playble.transform.parent.GetComponent<TutorialPart>().enabled = true;
            playble.gameObject.SetActive(false);
            if (currentScene.name == "Huntsville_Damaged")
            {
                Globals.ActiveFaces(character, true, false, false, false);
                questButton.SetActive(true);
            }
        }
        else if (currentScene.name == "Huntsville_Inn_1stFloor")
        {
            Debug.Log("555555555555555555 ");
            Globals.ActiveFaces(character, false, true, false, false);
            Globals.ActiveControls(character, false);
        }
        else
        {
            if (currentScene.name == "Huntsville_MerchantShop_Int")
                Globals.ActiveControls(character, true);
            else
                Globals.ActiveControls(character, true);
        }
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
    }
  public  void OnCompleteTutorial()
    {
        Globals.completeIntro = true;
        Globals.isPart1Battle = false;
        Globals.isFirstCompleteStory=true;
        if (Globals.avatarState.AvatarName == "WarriorMale")
        {
            Globals.isSmith = false;
            Globals.avatarState.Smith = 1;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
        else if (Globals.avatarState.AvatarName == "ArcherMale")
        {
            Debug.Log("archer male");
            Globals.isArcher = false;
            Globals.avatarState.Archer = 1;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
        else if (Globals.avatarState.AvatarName == "PriestMale")
        {
            Globals.isAcolyte = false;
            Globals.avatarState.Priest = 1;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
        else if(Globals.avatarState.AvatarName== "WarriorFemale")
        {
            Globals.isSmithF = false;
            Globals.avatarState.SmithF = 1;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
        else if(Globals.avatarState.AvatarName== "ArcherFemale")
        {
            Globals.isArcherF = false;
            Globals.avatarState.ArcherF = 1;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
        else if(Globals.avatarState.AvatarName== "PriestFemale")
        {
            Globals.isAcolyteF = false;
            Globals.avatarState.PriestF = 1;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
        Globals.isHome = true;
        Globals.innDialog = 0;
        Globals.avatarState.IntroValue = 1;
        db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
       
        Globals.objectiveScene = "Huntsville_Damaged";
    }
    public void BattleStart()
    {
        Globals.conversationCount++;
        SceneManager.LoadSceneAsync("Battle Scene_Death Wight Lair");
    }
    void SetBoundaries()
    {
        foreach(var v in boundaries)
        {
            v.tag = "DamageHuns";
        }
    }
}
