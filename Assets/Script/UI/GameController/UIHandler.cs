using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIHandler : MonoBehaviour
{
    int pauseCount;
    [SerializeField]
    AudioSource bgSound,otherSound,otherSound2;
    [SerializeField]
   public GameObject companionPanel,inventoryPanel,questPanel,settingPanel;
    [SerializeField]
  public  GameObject merchantPanel;
   public GameObject popUp;
    string sceneName;
    DatabaseManager db;
    [SerializeField]
    Toggle musicOn, musicOff, cantrolOn, controlOff;
    public GameObject quitBtn, settingBtn;
    // Start is called before the first frame update
    private void Start()
    {
        Globals.uiHandler = this;
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("active scene :: "+ Globals.activeScene);
        //if (Globals.activeScene == Globals.CurrentScene.Tutorial && settingBtn.activeInHierarchy)
        //{
        //    Debug.Log("quit btn disabled:: ");
        //    quitBtn.SetActive(false);
        //}
        db = FindObjectOfType<DatabaseManager>();
        sceneName = currentScene.name;
        Debug.Log("bg track ::........... ");
        Globals.soundSetting.SoundPlay();
    }
    public void ClickOnButton(string btn_name)
    {
        Debug.Log("click........................");
        switch (btn_name)
        {
            case "Pause":
                pauseCount++;
                if (pauseCount % 2 == 0)
                {
                    Time.timeScale = 1;
                    SoundPlay();
                }
                else
                {
                    SoundPause();
                    Time.timeScale = 0;
                }
                break;
            case "Cross":
                Debug.Log("croosss::  .............");
                //Globals.carvan1 = false;
                //Globals.caravan2 = false;
                //Globals.caravan3 = false;
                //Globals.petrol1 = false;
                //Globals.petrol2 = false;
                //Globals.petrol3 = false;
                Globals.isBarghest = false;
                if (Globals.isEnemyTeam)
                    Globals.isPart1Battle = true;
                if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite)
                {
                    if(Globals.soldierCampsiteVisit==0)
                      SceneManager.LoadScene("Soldier Campsite");
                    else
                        SceneManager.LoadScene("Second Soldier Campsite");
                }
                else if (Globals.activeScene == Globals.CurrentScene.WagonCaravan)
                    SceneManager.LoadScene("Wagon Carvan");
                else if (Globals.activeScene == Globals.CurrentScene.SecondSoldierCaravan)
                    SceneManager.LoadScene("Second Soldier Campsite");
                else if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage)
                    SceneManager.LoadScene("Atwater Village");
                else if (Globals.activeScene == Globals.CurrentScene.Huntsville)
                    SceneManager.LoadScene("Huntsville_Intro Scene");
                else if (Globals.activeScene == Globals.CurrentScene.monastery)
                    SceneManager.LoadScene("Monastery_ext");
                else if (Globals.activeScene == Globals.CurrentScene.CellarInt)
                    SceneManager.LoadScene("Monastery2ndFloor_int");
                else if (Globals.activeScene == Globals.CurrentScene.CellarTucker)
                    SceneManager.LoadScene("Monastery1stFloor_int");
                else if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
                    SceneManager.LoadScene("Motte and Baley Castle");
                else if (Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.BarghestVillage)
                {
                    if (!Globals.random)
                        SceneManager.LoadScene("Barghest Lair-Dungeon");
                    else
                    {
                        Globals.isWolf = false;
                        Globals.isHound = false;
                        SceneManager.LoadSceneAsync("Barghest Trail to Dungeon");
                    }
                }
                else if (Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon || Globals.activeScene == Globals.CurrentScene.TheDeathWeight)
                {
                    Globals.conversationCount++;
                    if (Globals.deathWightCount == 1)
                    {
                        Globals.iszombie = false;
                        Globals.isHound = false;
                        SceneManager.LoadScene("DeathWight Trail to Dungeon");
                    }
                    else if (Globals.deathWightCount == 2)
                        SceneManager.LoadScene("Death WIght Lair");
                }
                else if (Globals.activeScene == Globals.CurrentScene.TheBrigand)
                    SceneManager.LoadScene("Brigand Village");
                else if (Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
                {
                    if (Globals.brigandCount == 1)
                        SceneManager.LoadSceneAsync("Brigand Trail to Dungeon");
                    else
                        SceneManager.LoadSceneAsync("Brigand Lair");
                }
                else if (Globals.activeScene == Globals.CurrentScene.HuntingtonVillage)
                    SceneManager.LoadScene("Huntington_Inn_1stFloor");
                else if (Globals.activeScene == Globals.CurrentScene.CastleEscapeTunnel)
                    SceneManager.LoadScene("Huntington Castle Escape Tunnel");
                else if (Globals.activeScene == Globals.CurrentScene.HuntingtonCastle)
                    SceneManager.LoadScene("Huntington Castle Interior");
                else if (Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
                    SceneManager.LoadScene("Huntigton Castle Throne Room");
                else if (Globals.activeScene == Globals.CurrentScene.RandomAttack)
                {
                    if (Globals.activeRandom == Globals.CurrentRandom.caravans)
                        SceneManager.LoadScene("Caravan");
                    else if (Globals.activeRandom == Globals.CurrentRandom.petrols)
                        SceneManager.LoadScene("Petrols");
                }
                else if (Globals.activeScene == Globals.CurrentScene.Tutorial)
                    SceneManager.LoadScene("Huntsville_Well_Dungeon");
                break;
            case "Coconut":
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Smith/Smith_M_Password_Coconut";
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Smith/Smith_F_Password_Coconut";
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Archer/Archer_M_Password_Coconut";
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Archer/Archer_F_Password_Coconut";
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Acolyte/Acolyte_M_Password_Coconut";
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Acolyte/Acolyte_F_Password_Coconut";
                FindObjectOfType<HuntingtonController>().popUp.SetActive(false);
                FindObjectOfType<HuntingtonController>().SecondPlayble();
                break;
            case "12345":
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Smith/Smith_M_Password_12345";
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Smith/Smith_F_Password_12345";
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Archer/Archer_M_Password_12345";
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Archer/Archer_F_Password_12345";
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Acolyte/Acolyte_M_Password_12345";
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Acolyte/Acolyte_F_Password_12345";
                FindObjectOfType<HuntingtonController>().popUp.SetActive(false);
                FindObjectOfType<HuntingtonController>().SecondPlayble();
                break;
            case "42":
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Smith/Smith_M_Password_42";
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Smith/Smith_F_Password_42";
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Archer/Archer_M_Password_42";
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Archer/Archer_F_Password_42";
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Acolyte/Acolyte_M_Password_42";
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    Globals.playbleAssets = "Playables/Huntington Village/Password Dialogs/Acolyte/Acolyte_F_Password_42";
                FindObjectOfType<HuntingtonController>().popUp.SetActive(false);
                FindObjectOfType<HuntingtonController>().SecondPlayble();
                break;

            case "CompanionBack":
                if(Globals.activeScene==Globals.CurrentScene.HuntingtonVillage)
                {
                    if (FindObjectOfType<CompanionSetting>().count >= 3 || FindObjectOfType<CompanionSetting>().count == 0)
                    {
                        companionPanel.SetActive(false);
                        SceneManager.LoadSceneAsync(Globals.objectiveScene);
                    }
                    else
                    {
                        popUp.SetActive(true);
                        popUp.transform.GetChild(0).GetComponent<Text>().text = "Please select 3 companions";
                    }
                }
                break;
            case "Okay":
                popUp.SetActive(false);
                SceneManager.LoadSceneAsync("World Map");
                break;
            case "Merchant":
                merchantPanel.SetActive(true);
                Debug.Log("enter pos here::" + Globals.enterPos);
                break;
            case "Inventory":
                if (Globals.selectedInventoryCharacter == null)
                    Globals.selectedInventoryCharacter = Globals.avatarState.AvatarName;
                inventoryPanel.SetActive(true);
                Time.timeScale = 1;
                //VIPUL
                if (!Globals.isAnim)
                    Globals.characterSlot.UpdateCharacter();
                else
                    Globals.armourImplimentation.UpdateCharacter();
                break;
            case "InventoryBack":
                Time.timeScale = 1;
                Globals.inventoryHandler.BackToInventory();
                inventoryPanel.SetActive(false);
                //VIPUL
                if (!Globals.isAnim)
                    Globals.characterSlot.UpdateCharacter();
                else
                    Globals.armourImplimentation.UpdateCharacter();
                break;
            case "MerchantBack":
                merchantPanel.SetActive(false);
                Debug.Log("bool::" + Globals.completeIntro);
                if (sceneName == "Blacksmith Shop_int_Huntsville")
                    Globals.ActiveControls(Globals.controller.character, true);
                else if (Globals.completeIntro)
                    Globals.ActiveControls(Globals.tutorialPart.character, true);
                if (sceneName == "Berghest_MerchantShop_Potions" && Globals.againVisit==1)
                    Globals.ActiveControls(Globals.barghestShop.character, true);
                Debug.Log("enter pos here2222::" + Globals.enterPos);
                break;
            case "CreditBack":
                Globals.isHome = false;
                Globals.isCompleteGame = true;
                
                popUp.SetActive(true);
                break;
            case "QuestLog":
                if (Globals.selectedInventoryCharacter == null)
                    Globals.selectedInventoryCharacter = Globals.avatarState.AvatarName;
                Globals.questHandler.ShowQuest();
                Time.timeScale = 1;

                questPanel.SetActive(true);
                //VIPUL
                if (!Globals.isAnim)
                    Globals.characterSlot.UpdateCharacter();
                else
                    Globals.armourImplimentation.UpdateCharacter();
                break;
            case "QuestBack":
                Time.timeScale = 1;
                questPanel.SetActive(false);
                //VIPUL
                if (!Globals.isAnim)
                    Globals.characterSlot.UpdateCharacter();
                else
                    Globals.armourImplimentation.UpdateCharacter();
                break;
            case "Setting":
                settingPanel.SetActive(true);
                if (Globals.avatarState.SoundLevel == 1)
                    MusicSetting(true, false);
                else
                    MusicSetting(false, true);
                if (Globals.avatarState.ControlLevel == 1)
                    ControlSetting(true, false);
                else
                    ControlSetting(false, true);
                break;
            case "SettingBack":
                //Globals.isHome = true;
                //SceneManager.LoadSceneAsync("World Map");
                settingPanel.SetActive(false);
                break;
            case "MusicOff":
               Globals.avatarState.SoundLevel = 1;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                MusicSetting(true, false);
                Globals.soundSetting.SoundPlay();
                break;
            case "MusicOn":
               Globals.avatarState.SoundLevel = 0;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                MusicSetting(false, true);
                Globals.soundSetting.SoundPlay();
                break;
            case "controlOff":
               Globals.avatarState.ControlLevel = 0;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                ControlSetting(false, true);
                break;
            case "controlOn":
               Globals.avatarState.ControlLevel = 1;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                ControlSetting(true, false);
                break;
            case "quitGame":
                Debug.Log("here world map to move "+ Globals.objectiveScene + " current objective : "+ Globals.currentObjective);
                Globals.backToMenu = true;
                SaveContinueGameData();
                if (Globals.activeScene == Globals.CurrentScene.CellarInt || Globals.activeScene == Globals.CurrentScene.CellarTucker || Globals.activeScene == Globals.CurrentScene.monastery)
                {
                    Debug.Log("monastery village...............");
                    Globals.objectiveScene = "Monastery_ext";
                    Globals.currentObjective = "Monastery";
                    Globals.isPart1Battle = false;
                }
                if(Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.BarghestVillage)
                {
                    Debug.Log("barghest village...............");
                    Globals.objectiveScene = "Barghest Village";
                }
                if (Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
                {
                    Debug.Log("barghest village...............");
                    Globals.objectiveScene = "Death Wight Village";
                    Globals.currentObjective = "TheDeathWeight";
                }
                if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
                {
                    Debug.Log("brigand village...............");
                    Globals.objectiveScene = "Brigand Village";
                    Globals.currentObjective = "Brigand Village";
                }
                if (Globals.activeScene == Globals.CurrentScene.HuntingtonVillage || Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.HuntingtonCastleCourtyard || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
                {
                    Debug.Log("huntington village...............");
                    Globals.objectiveScene = "Huntington Town_Alley Scenes";
                    Globals.currentObjective = "Huntington";
                }
                Globals.avatarState.SaveState = Globals.objectiveScene;
                Globals.avatarState.SaveObjective = Globals.currentObjective;
                Globals.avatarState.atwaterCount = Globals.atWaterCount;
                Globals.avatarState.petrol = Globals.petrolCount;
                Globals.avatarState.carvan = Globals.caravnCount;
                Globals.avatarState.companionCount = Globals.noOfCompanions;
                Globals.avatarState.lastposx = Globals.lastPos.x;
                Globals.avatarState.lastposy = Globals.lastPos.y;
                Debug.Log("get active scene : " + Globals.activeScene);
                SetPetrolValue();
               
                if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite)
                {
                    Debug.Log("soldier campsite...............");
                    Globals.avatarState.secondVisit = Globals.soldierCampsiteVisit;
                }
                else if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage)
                {
                    Debug.Log("atwarer village..............."+ Globals.atWaterCount);
                    Globals.isCompleteVid = false;
                    Globals.isSargent = false;
                    Globals.conversationCount = 0;
                }
                else
                {
                    Debug.Log("others village..............."+ Globals.secondVisit);
                    Globals.avatarState.secondVisit = Globals.secondVisit;
                }
            
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);

                Globals.isHome = false;
                ResetVillageGlobalValue();
                SceneManager.LoadScene("World Map");


              //  Globals.uiManager.LoadingSetting2();
                
                break;

        }
    }
    void MusicSetting(bool on, bool off)
    {
        musicOn.gameObject.SetActive(on);
        musicOff.gameObject.SetActive(off);
    }
    void ControlSetting(bool on, bool off)
    {
        cantrolOn.gameObject.SetActive(on);
        controlOff.gameObject.SetActive(off);
    }
    void SoundPlay()
    {
        if (Globals.avatarState.SoundLevel == 1)
        {
            bgSound.Play();
            otherSound.Play();
            otherSound2.Play();
        }
    }
    void SoundPause()
    {
        if (Globals.avatarState.SoundLevel == 1)
        {
            bgSound.Pause();
            otherSound.Pause();
            otherSound2.Pause();
        }
    }
    
    public void ResetVillageGlobalValue()
    {
        if(Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
        {
            Debug.Log("reset motte bailey global value.........");
            Globals.secondFight = false;
            Globals.thirdFight = false;
            Globals.forthFight = false;
            Globals.beforeMottey = false;
            Globals.isPart1Battle = false;
            Globals.isRetreate = false;
            Globals.isMotteyRetreat = false;
            Globals.isShieldWall = false;
        }
        if (Globals.activeScene == Globals.CurrentScene.RandomAttack)
        {
            Debug.Log("reset random attack global value.........");

            Globals.isPart1Battle = false;
        }
        if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite)
        {
            Debug.Log("reset soldier campsite global value.........");

            Globals.isPart1Battle = false;
        }
        if (Globals.activeScene == Globals.CurrentScene.BarghestLairDengeon || Globals.activeScene == Globals.CurrentScene.BarghestVillage)
        {
            Debug.Log("reset barghest global value.........");
            Globals.isPart1Battle = false;
            Globals.isShopDialog = false;
            Globals.PlayNow = false;
            Globals.isChurchComplete = false;
            Globals.isChurchComplete = false;
            Globals.conversationCount = 0;
        }
        if(Globals.activeScene == Globals.CurrentScene.TheDeathWeight || Globals.activeScene == Globals.CurrentScene.TheDeathWeightDengeon)
        {
            Debug.Log("reset death weight global value.........");
            Globals.conversationCount = 0;
            Globals.iszombie = false;
            Globals.isHound = false;
            Globals.isWolf = false;
        }
        if (Globals.activeScene == Globals.CurrentScene.TheBrigand || Globals.activeScene == Globals.CurrentScene.BrigandLairDengeon)
        {
            Debug.Log("reset brigand village global value ...............");
            Globals.isPart1Battle = false;
            Globals.secondVisitMon = false;
            Globals.thirdFight = false;
            Globals.lastRandom = "";
            Globals.secondFight = false;
            Globals.PlayNow = false;
            Globals.isArcherBrigand = false;
            Globals.isVeteran = false;
            Globals.isFirstCompleteStory = false;
            Globals.enterChurch = false;
            Globals.enterInn = false;
            Globals.enterShop = false;
            Globals.enterFarmhouse = false;
            Globals.conversationCount = 0;
        }
        if (Globals.activeScene == Globals.CurrentScene.HuntingtonVillage || Globals.activeScene == Globals.CurrentScene.HuntingtonCastle || Globals.activeScene == Globals.CurrentScene.HuntingtonCastleCourtyard || Globals.activeScene == Globals.CurrentScene.HuntingtonThroneRoom)
        {
            Debug.Log("reset globals huntington village...............");
            Globals.leavingThrone = false;
            Globals.huntingtonVill.clickCount = 1;
            Globals.drunkenGuy = false;
            Globals.isGuard = false;
            Globals.isSpecial = false;
            Globals.isPart1Battle = false;
            Globals.completeEfforts = false;
            Globals.enterChurch = false;
            Globals.enterInn = false;
            Globals.enterShop = false;
            Globals.secondFight = false;
            Globals.thirdFight = false;
        }
    }
    void SetPetrolValue()
    {
        if (Globals.petrol1)
            Globals.avatarState.petrol1 = 1;
        else
            Globals.avatarState.petrol1 = 0;
        if (Globals.petrol2)
            Globals.avatarState.petrol2 = 1;
        else
            Globals.avatarState.petrol2 = 0;
        if (Globals.petrol3)
            Globals.avatarState.petrol3 = 1;
        else
            Globals.avatarState.petrol3 = 0;
        if (Globals.carvan1)
            Globals.avatarState.carvan1 = 1;
        else
            Globals.avatarState.carvan1 = 0;
        if (Globals.caravan2)
            Globals.avatarState.carvan2 = 1;
        else
            Globals.avatarState.carvan2 = 0;
        if (Globals.caravan3)
            Globals.avatarState.carvan3 = 1;
        else
            Globals.avatarState.carvan3 = 0;

    }
    public void SaveContinueGameData()
    {
        Globals.loadData = false;
        Globals.ContinueGame saveGame = new Globals.ContinueGame();
        db.UpdateRecord<Globals.ContinueGame>(saveGame);
    }
}
