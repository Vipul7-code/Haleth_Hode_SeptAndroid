using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Globals;
public class UIManager : MonoBehaviour
{
   [SerializeField]
    GameObject loadingScreen, bar,loginScreen,settingScreen,editNameScreen,confirmationScreen,inventoryScreen,Bag,questLog,characterPanel,equipmentPanel,staticsPanel;
    int totalFriends,pauseCount,genderCount;
    [SerializeField]
  public  GameObject StartPanel;
    [SerializeField]
    GameObject weaponList, armourList, helmetList, shieldList;
    public GameObject popUp;
    bool isContinue,isLoad;
    DatabaseManager db;
    public Toggle maleToggle, femaleToggle,musicOn,musicOff,controlOn,controlOff,attackOn,attackOff,defenceOn,defenceOff;
    public GameObject maleSelection, femaleSelection, avatarSelection, startBattle,pauseButton,weapenPanel,armourPanel, LoadGamePanel,LoadPopUp,loading,weapon,defencePanel,helmetPanel;
    [SerializeField]
    GameObject firstLoad,firstEmpty, secondLoad,secondEmpty, thirdLoad,thirdtEmpty, ForthLoad,forthEmpty, fifthLoad,fifthEmpty, sixthLoad,sixthEmpty,ContinuePanel,newGame;
    [SerializeField]
    Button Continue, Load;
    public AvatarContainer avatarContainer;
    public List<GameObject> inventoryText = new List<GameObject>();
    public List<GameObject> inventoryButtons = new List<GameObject>();
    float lerpSpeed =1f, t = 0;
    Globals.userData user = new Globals.userData();
    public LevelManager level;
   public bool isCharacter, isEquipement, isStatistics,isArmor,isHelmet,isWeapon, isShield;
    [SerializeField]
  public  GameObject[] colliders;
    [SerializeField]
    InputField nameInput;
    [SerializeField]
    AudioSource worldMap;
    public Text nameText,confirmationText,typeText;
    GameObject prefabObject;
    public GameObject atwaterCaravan1, atwaterCaravan2, atwaterpetrol1, atwaterpetrol2, atwaterPetrol3, atwaterCar1Col, atwaterCar2Col, atwaterPet1Col, atwaterPet2Col, atwaterPet3Col;

    public GameObject otherCaravan1, otherCaravan2,otherCaravan3, otherpetrol1, otherpetrol2, otherPetrol3, otherCar1Col, otherCar2Col,otherCar3Col, otherPet1Col, otherPet2Col, otherPet3Col,leftCamera,rightCamera;

    [SerializeField]
    GameObject[] soldierCampsite, wagonCaravan, secondSoldierCampsite, hunsville, atwater, sacredPlace, Part2Hunsville, monkCampsite, monestry, motteyAndBailey, Part3Hunsville, Barghest, DeathWeight, Brigand, huntington;
    [SerializeField]
   public GameObject soldier, wagon, secondSoldier, monkCaravan, soldierCol, wagonCol, secondSoldierCol, monkCaravanCol, questPopUp,sacretPlace,sacretPlaceCol,barghest,barghestCol,deathweight,deathWeightCol,brigand,brigandCol;
    bool atwatercar1, atwatercar2,atwatercar3, atwaterPet1, atwaterPet2, atwaterPet3, otherCar1, otherCar2, otherCar3, otherPet1, otherPet2, otherPet3;

  
    void Start()
    {
        Debug.Log("here.......................");

        worldMap.Play();
        Globals.uiManager = this;
        isCharacter = true;
        db = FindObjectOfType<DatabaseManager>();
        totalFriends = Globals.playerState.Friend;
        Globals.PlayNow = false;
        Globals.isShrine = false;
        Globals.secondFight = false;
        Globals.enterMayor = false;
        Globals.enterShop = false;
        Globals.enterInn = false;
        Globals.enterFarmhouse = false;
        Globals.enterBlackSmith = false;
        Globals.enterChurch = false;
        Globals.thirdFight = false;
        Globals.isFirstCompleteStory = false;
        Globals.isChurchComplete = false;
        if (Globals.isHome)
        {
            StartPanel.SetActive(false);
            GameStartFinal();
            Globals.isHome = false;
        }
        else
             SetPanel();
        SettingRandomEncounters();
        
    }
    void SettingRandomEncounters()
    {
        if((atWaterCount==0 ||atWaterCount==1 || atWaterCount == 2 || atWaterCount == 3 || atWaterCount == 4 || atWaterCount == 5) && currentObjective== "RandomAttack")
        {
            Debug.Log("At water RandomAttack" + atWaterCount);
            SettingOfCaravan();
            PetrolsSetting();
        }
        else if(atWaterCount==6 && currentObjective== "Sacred Place")
        {
            AtwaterCaravan(false,false,false);
            AtwaterPetrols(false,false,false);
        }
        else if((atWaterCount==6 ||atWaterCount == 7 || atWaterCount == 8 || atWaterCount == 9 || atWaterCount == 10 || atWaterCount==11) && currentObjective== "RandomAttack")
        {
            SettingOfCaravan();
            PetrolsSetting();
        }
        else  
        {
            OtherCaravan(false,false,false);
            OtherPetrols(false,false,false);
        }
    }
    void SettingOfCaravan()
    {
        if (Globals.caravanName.Count != 0)
        {
            Debug.Log("set UI iCOns Caravan1");
            foreach (var v in Globals.caravanName)
            {
                if (v == "Caravan1")
                    atwatercar1 = true;
                if (v == "Caravan2")
                    atwatercar2 = true;
                if (v == "Caravan3")
                    atwatercar3 = true;
                if (v == "Caravans1")
                    otherCar1 = true;
                if (v == "Caravans2")
                    otherCar2 = true;
                if (v == "Caravans3")
                    otherCar3 = true;
            }
            EnableCaravans();
        }
        else
        {
            if (atWaterCount <= 5)
                AtwaterCaravan(true, true,true);
            else
                OtherCaravan(true, true, true);
        }
    }
    void PetrolsSetting()
    {
        if (Globals.petrolsName.Count != 0)
        {
            Debug.Log("set UI Petrol true");
            foreach (var v in Globals.petrolsName)
            {
                if (v == "Petrol1")
                    atwaterPet1 = true;
                if (v == "Petrol2")
                    atwaterPet2 = true;
                if (v == "Petrol3")
                    atwaterPet3 = true;
                if (v == "Petrols1")
                    otherPet1 = true;
                if (v == "Petrols2")
                    otherPet2 = true;
                if (v == "Petrols3")
                    otherPet3 = true;
            }
            EnablePetrols();
        }
        else
        {
            if (atWaterCount <= 5)
                AtwaterPetrols(true,true,true);
            else
               OtherPetrols(true,true,true);
        }
    }
    void EnablePetrols()
    {
        if (atwaterPet1 && atwaterPet2 && atwaterPet3)
            AtwaterPetrols(false, false, false);
        else if (atwaterPet1 && !atwaterPet2 && !atwaterPet3)
            AtwaterPetrols(false, true, true);
        else if (atwaterPet1 && atwaterPet2 && !atwaterPet3)
            AtwaterPetrols(false, false, true);
        else if (atwaterPet1 && !atwaterPet2 && atwaterPet3)
            AtwaterPetrols(false, true, false);
        else if (!atwaterPet1 && atwaterPet2 && !atwaterPet3)
            AtwaterPetrols(true, false, true);
        else if (!atwaterPet1 && atwaterPet2 && atwaterPet3)
            AtwaterPetrols(true, false, false);
        else if (!atwaterPet1 && !atwaterPet2 && atwaterPet3)
            AtwaterPetrols(true, true, false);
        else if (otherPet1 && otherPet2 && otherPet3)
            OtherPetrols(false, false, false);
        else if (otherPet1 && !otherPet2 && !otherPet3)
            OtherPetrols(false, true, true);
        else if (otherPet1 && otherPet2 && !otherPet3)
            OtherPetrols(false, false, true);
        else if (otherPet1 && !otherPet2 && otherPet3)
            OtherPetrols(false, true, false);
        else if (!otherPet1 && otherPet2 && !otherPet3)
            OtherPetrols(true, false, true);
        else if (!otherPet1 && otherPet2 && otherPet3)
            OtherPetrols(true, false, false);
        else if (!otherPet1 && !otherPet2 && otherPet3)
            OtherPetrols(true, true, false);
    }
    void EnableCaravans()
    {
        if (atwatercar1 && atwatercar2 && atwatercar3)
            AtwaterCaravan(false, false,false);
        else if (atwatercar1 && !atwatercar2 && !atwatercar3)
            AtwaterCaravan(false, true,true);
        else if (atwatercar1 && atwatercar2 && !atwatercar3)
            AtwaterCaravan(false, false,true);
        else if (atwatercar1 && !atwatercar2 && atwatercar3)
            AtwaterCaravan(false, true, false);
        else if (!atwatercar1 && atwatercar2 && !atwatercar3)
            AtwaterCaravan(true, false, true);
        else if (!atwatercar1 && atwatercar2 && atwatercar3)
            AtwaterCaravan(true, false, false);
        else if (!atwatercar1 && !atwatercar2 && atwatercar3)
            AtwaterCaravan(true, true, false);
        else if (otherCar1 && otherCar2 && otherCar3)
            OtherCaravan(false, false, false);
        else if (otherCar1 && !otherCar2 && !otherCar3)
            OtherCaravan(false, true, true);
        else if (otherCar1 && otherCar2 && !otherCar3)
            OtherCaravan(false, false, true);
        else if (otherCar1 && !otherCar2 && otherCar3)
            OtherCaravan(false, true, false);
        else if (!otherCar1 && otherCar2 && !otherCar3)
            OtherCaravan(true, false, true);
        else if (!otherCar1 && otherCar2 && otherCar3)
            OtherCaravan(true, false, false);
        else if (!otherCar1 && !otherCar2 && otherCar3)
            OtherCaravan(true, true, false);
    }
 
    void AtwaterCaravan(bool car1,bool car2,bool car3)
    {
        Globals.uiManager.atwaterCaravan1.SetActive(car1);
        Globals.uiManager.atwaterCaravan2.SetActive(car2);
        Globals.uiManager.atwaterCar1Col.SetActive(car1);
        Globals.uiManager.atwaterCar2Col.SetActive(car2);
    }
    void OtherCaravan(bool car1,bool car2,bool car3)
    {
        Globals.uiManager.otherCaravan1.SetActive(car1);
        Globals.uiManager.otherCaravan2.SetActive(car2);
        otherCaravan3.SetActive(car3);
        otherCar3Col.SetActive(car3);
        Globals.uiManager.otherCar1Col.SetActive(car1);
        Globals.uiManager.otherCar2Col.SetActive(car2);
    }
    void AtwaterPetrols(bool pet1,bool pet2,bool pet3)
    {
        Debug.Log("enable collider at petrol");
        Globals.uiManager.atwaterpetrol1.SetActive(pet1);
        Globals.uiManager.atwaterpetrol2.SetActive(pet2);
        Globals.uiManager.atwaterPetrol3.SetActive(pet3);
        Globals.uiManager.atwaterPet1Col.SetActive(pet1);
        Globals.uiManager.atwaterPet2Col.SetActive(pet2);
        Globals.uiManager.atwaterPet3Col.SetActive(pet3);
    }
    void OtherPetrols(bool pet1,bool pet2,bool pet3)
    {
        Globals.uiManager.otherpetrol1.SetActive(pet1);
        Globals.uiManager.otherpetrol2.SetActive(pet2);
        Globals.uiManager.otherPetrol3.SetActive(pet3);
        Globals.uiManager.otherPet1Col.SetActive(pet1);
        Globals.uiManager.otherPet2Col.SetActive(pet2);
        Globals.uiManager.otherPet3Col.SetActive(pet3);
    }
    public void ClickOnButton(string btn_name)
    {
        switch (btn_name)
        {
            case "Pause":
                pauseCount++;
                if (pauseCount % 2 == 0)
                    Time.timeScale = 1;
                else
                    Time.timeScale = 0;
                break;
            case "PlayButton":
                isContinue = false;
                SplashSetting();
                break;
            case "MusicOff":
                Debug.Log("off button");
                avatarState.SoundLevel = 1;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                MusicSetting(true, false);
                Globals.soundSetting.SoundPlay();
                break;
            case "MusicOn":
                Debug.Log("on");
                avatarState.SoundLevel = 0;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                MusicSetting(false, true);
                Globals.soundSetting.SoundPlay();
                break;
            case "controlOff":
                avatarState.ControlLevel = 0;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                ControlSetting(false, true);
                break;
            case "controlOn":
                avatarState.ControlLevel = 1;
                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
                ControlSetting(true, false);
                break;
            case "GenderSwitch":
                if(genderCount%2==0)
                    avatarContainer.Initialize(Globals.Gender.Female);
                else
                    avatarContainer.Initialize(Globals.Gender.Male);
                genderCount++;
                break;
            case "maleToggle":
                //if (maleToggle.isOn)
                //    femaleToggle.isOn = false;
                avatarContainer.Initialize(Globals.Gender.Male);
                break;
            case "femaleToggle":
                //if (femaleToggle.isOn)
                //    maleToggle.isOn = false;
                avatarContainer.Initialize(Globals.Gender.Female);
                break;
            case "OkayPopUp":
                LoadPopUp.SetActive(false);
                break;
            case "InputSelect":
                nameInput.Select();
                break;
            case "Confirm":
                confirmationScreen.SetActive(true);
              
               // typeText.text = "ROBYN";
                Globals.characterName = typeText.text;
                confirmationText.text = Globals.characterName;
                break;
            case "Continue":
                isContinue = true;
                loading.SetActive(true);
                ContinueSetting();
                break;
            case "Yes":
                StartPanel.SetActive(false);
                confirmationScreen.SetActive(false);
                editNameScreen.SetActive(false);
                GameStartFinal();
                break;
            case "No":
                confirmationScreen.SetActive(false);
                //  nameText.text = "ROBYN";
                typeText.transform.parent.GetComponent<InputField>().text = Globals.characterName;
                // nameText.text = Globals.playerState.GoogleName;
                break;
            case "Setting":
                settingScreen.SetActive(true);
                if(Globals.avatarState.SoundLevel==1)
                    MusicSetting(true, false);
                else
                    MusicSetting(false, true);
                if (Globals.avatarState.ControlLevel == 1)
                    ControlSetting(true, false);
                else
                    ControlSetting(false, true);
                break;
            case "SettingBack":
                settingScreen.SetActive(false);
                if (Globals.avatarState.ControlLevel == 0)
                    CameraSetting(true, false);
                else
                    CameraSetting(false, true);
                break;
            case "BackSelection":
                avatarContainer.DestroyContainer();
                avatarSelection.SetActive(false);
                loadingScreen.SetActive(true);
                break;
            case "LoadBack":
                LoadGamePanel.SetActive(false);
                break;
            case "LoadGame":
                ShowLoadGames();
                break;
            case "Load1":
                isLoad = true;
                Globals.loadSaveGame = true;
                loading.SetActive(true);
                Globals.currentObjective = Globals.loadGame.SaveObjective1;
                Globals.objectiveScene = Globals.loadGame.SaveState1;
               
                Globals.secondVisit = Globals.loadGame.secondVisit1;
                Globals.noOfCompanions = Globals.loadGame.companionCount1;

                Globals.avatarState.TotalXp = Globals.loadGame.Xp1;
                Globals.shopMerchant.Gold = Globals.loadGame.gold1;
                LoadGameData(1);
                Globals.lastPos = new Vector3(Globals.loadGame.lastpos1x, Globals.loadGame.lastpos1y);
               Globals.atWaterCount = Globals.loadGame.atwater1;
                DatabaseManager.instance.UpdateRecord<SelectedAvatar>(avatarState);
                LevelCalculation.instance.CalculateXpPoints();
                //if (Globals.objectiveScene == "Huntsville")
                //{
                //    Globals.noOfCompanions = 2;
                //}
                // Globals.activePlayer = Globals.loadGame.savePlayer1;
                Globals.avatarState.AvatarName = Globals.loadGame.savePlayer1;
              
                Globals.activePlayer = Resources.Load(Globals.loadGame.savePlayer1) as GameObject;
                if (Globals.objectiveScene != "Atwater Village" && Globals.objectiveScene != "Brigand Village" && Globals.objectiveScene != "Huntington Town_Alley Scenes")
                    Globals.activeScene = (CurrentScene)System.Enum.Parse(typeof(CurrentScene), loadGame.SaveObjective1);
                else if (Globals.objectiveScene == "Atwater Village")
                {
                    Globals.activeScene = CurrentScene.AtwaterVillage;
                    Globals.atWaterCount = 5;
                    Globals.noOfCompanions = 2;
                }
                else if (Globals.objectiveScene == "Huntington Town_Alley Scenes")
                {
                    Globals.activeScene = CurrentScene.HuntingtonVillage;
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                if(Globals.objectiveScene == "Barghest Village" || Globals.objectiveScene == "Death Wight Village" || Globals.objectiveScene == "Brigand Village")
                {
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
                break;
            case "Load2":
                isLoad = true;
                Globals.loadSaveGame = true;
                loading.SetActive(true);
                Globals.currentObjective = Globals.loadGame.SaveObjective2;
                Globals.objectiveScene = Globals.loadGame.SaveState2;
                Globals.secondVisit = Globals.loadGame.secondVisit2;
                Globals.avatarState.AvatarName = Globals.loadGame.savePlayer2;
                Globals.noOfCompanions = Globals.loadGame.companionCount2;
                Globals.avatarState.TotalXp = Globals.loadGame.Xp2;
                LoadGameData(2);
                Globals.shopMerchant.Gold = Globals.loadGame.gold2;
                Globals.lastPos = new Vector3(Globals.loadGame.lastpos2x, Globals.loadGame.lastpos2y);
                Globals.atWaterCount = Globals.loadGame.atwater2;
                DatabaseManager.instance.UpdateRecord<SelectedAvatar>(avatarState);
                LevelCalculation.instance.CalculateXpPoints();
                //if (Globals.objectiveScene == "Huntsville")
                //{
                //    Globals.noOfCompanions = 2;
                //}
                Globals.activePlayer = Resources.Load(Globals.loadGame.savePlayer2) as GameObject;
                Debug.Log("scene::" + Globals.objectiveScene);
                if (Globals.objectiveScene != "Atwater Village" && Globals.objectiveScene != "Brigand Village" && Globals.objectiveScene != "Huntington Town_Alley Scenes")
                    Globals.activeScene = (CurrentScene)System.Enum.Parse(typeof(CurrentScene), loadGame.SaveObjective2);
                else if (Globals.objectiveScene == "Atwater Village")
                {
                    Globals.activeScene = CurrentScene.AtwaterVillage;
                    atWaterCount = 5;
                    Globals.noOfCompanions = 2;

                }
                else if (Globals.objectiveScene == "Huntington Town_Alley Scenes")
                {
                    Globals.activeScene = CurrentScene.HuntingtonVillage;
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                if (Globals.objectiveScene == "Barghest Village" || Globals.objectiveScene == "Death Wight Village" || Globals.objectiveScene == "Brigand Village")
                {
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
                break;
            case "Load3":
                isLoad = true;
                Globals.loadSaveGame = true;
                loading.SetActive(true);
                Globals.currentObjective = Globals.loadGame.SaveObjective3;
                Globals.objectiveScene = Globals.loadGame.SaveState3;
                Globals.secondVisit = Globals.loadGame.secondVisit3;
                Globals.avatarState.AvatarName = Globals.loadGame.savePlayer3;
                Globals.noOfCompanions = Globals.loadGame.companionCount3;
                Globals.activePlayer = Resources.Load(Globals.loadGame.savePlayer3) as GameObject;
                Globals.avatarState.TotalXp = Globals.loadGame.Xp3;
                LoadGameData(3);
                Globals.shopMerchant.Gold = Globals.loadGame.gold3;
                Globals.lastPos = new Vector3(Globals.loadGame.lastpos3x, Globals.loadGame.lastpos3y);
                Globals.atWaterCount = Globals.loadGame.atwater3;
                DatabaseManager.instance.UpdateRecord<SelectedAvatar>(avatarState);
                LevelCalculation.instance.CalculateXpPoints();
                if (Globals.objectiveScene != "Atwater Village" && Globals.objectiveScene != "Brigand Village" && Globals.objectiveScene != "Huntington Town_Alley Scenes")
                    Globals.activeScene = (CurrentScene)System.Enum.Parse(typeof(CurrentScene), loadGame.SaveObjective3);
                else if (Globals.objectiveScene == "Atwater Village")
                {
                    Globals.activeScene = CurrentScene.AtwaterVillage;
                    atWaterCount = 5;
                }
                else if (Globals.objectiveScene == "Huntington Town_Alley Scenes")
                {
                    Globals.activeScene = CurrentScene.HuntingtonVillage;
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                if(Globals.objectiveScene == "Barghest Village" || Globals.objectiveScene == "Death Wight Village" || Globals.objectiveScene == "Brigand Village")
                {
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
                break;
            case "Load4":
                isLoad = true;
                Globals.loadSaveGame = true;
                loading.SetActive(true);
                Globals.currentObjective = Globals.loadGame.SaveObjective4;
                Globals.objectiveScene = Globals.loadGame.SaveState4;
                Globals.secondVisit = Globals.loadGame.secondVisit4;
                Globals.avatarState.AvatarName = Globals.loadGame.savePlayer4;
                Globals.noOfCompanions = Globals.loadGame.companionCount4;
                Globals.shopMerchant.Gold = Globals.loadGame.gold4;
                LoadGameData(4);
                Globals.activePlayer = Resources.Load(Globals.loadGame.savePlayer4) as GameObject;
                Globals.avatarState.TotalXp = Globals.loadGame.Xp4;
                Globals.lastPos = new Vector3(Globals.loadGame.lastpos4x, Globals.loadGame.lastpos4y);
                Globals.atWaterCount = Globals.loadGame.atwater4;
                DatabaseManager.instance.UpdateRecord<SelectedAvatar>(avatarState);
                LevelCalculation.instance.CalculateXpPoints();
                if (Globals.objectiveScene != "Atwater Village" && Globals.objectiveScene != "Brigand Village" && Globals.objectiveScene != "Huntington Town_Alley Scenes")
                    Globals.activeScene = (CurrentScene)System.Enum.Parse(typeof(CurrentScene), loadGame.SaveObjective4);
                else if (Globals.objectiveScene == "Atwater Village")
                {
                    Globals.activeScene = CurrentScene.AtwaterVillage;
                    atWaterCount = 5;
                }
                else if (Globals.objectiveScene == "Huntington Town_Alley Scenes")
                {
                    Globals.activeScene = CurrentScene.HuntingtonVillage;
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                if(Globals.objectiveScene == "Barghest Village" || Globals.objectiveScene == "Death Wight Village" || Globals.objectiveScene == "Brigand Village")
                {
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
                break;
            case "Load5":
                isLoad = true;
                loading.SetActive(true);
                Globals.currentObjective = Globals.loadGame.SaveObjective5;
                Globals.objectiveScene = Globals.loadGame.SaveState5;
                Globals.secondVisit = Globals.loadGame.secondVisit5;
                Globals.avatarState.AvatarName = Globals.loadGame.savePlayer5;
                Globals.noOfCompanions = Globals.loadGame.companionCount5;
                Globals.activePlayer = Resources.Load(Globals.loadGame.savePlayer5) as GameObject;
                Globals.avatarState.TotalXp = Globals.loadGame.Xp5;
                Globals.lastPos = new Vector3(Globals.loadGame.lastpos5x, Globals.loadGame.lastpos5y);
                Globals.atWaterCount = Globals.loadGame.atwater5;
                LoadGameData(5);
                Globals.shopMerchant.Gold = Globals.loadGame.gold5;
                DatabaseManager.instance.UpdateRecord<SelectedAvatar>(avatarState);
                LevelCalculation.instance.CalculateXpPoints();
                if (Globals.objectiveScene != "Atwater Village" && Globals.objectiveScene != "Brigand Village" && Globals.objectiveScene != "Huntington Town_Alley Scenes")
                    Globals.activeScene = (CurrentScene)System.Enum.Parse(typeof(CurrentScene), loadGame.SaveObjective5);
                else if (Globals.objectiveScene == "Atwater Village")
                {
                    Globals.activeScene = CurrentScene.AtwaterVillage;
                    atWaterCount = 5;
                }
                else if (Globals.objectiveScene == "Huntington Town_Alley Scenes")
                {
                    Globals.activeScene = CurrentScene.HuntingtonVillage;
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                if(Globals.objectiveScene == "Barghest Village" || Globals.objectiveScene == "Death Wight Village" || Globals.objectiveScene == "Brigand Village")
                {
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
                break;
            case "Load6":
                isLoad = true;
                loading.SetActive(true);
                Globals.currentObjective = Globals.loadGame.SaveObjective6;
                Globals.objectiveScene = Globals.loadGame.SaveState6;
                Globals.secondVisit = Globals.loadGame.secondVisit6;
                Globals.avatarState.AvatarName = Globals.loadGame.savePlayer6;
                Globals.noOfCompanions = Globals.loadGame.companionCount6;
                Globals.activePlayer = Resources.Load(Globals.loadGame.savePlayer6) as GameObject;
                Globals.avatarState.TotalXp = Globals.loadGame.Xp6;
                Globals.lastPos = new Vector3(Globals.loadGame.lastpos6x, Globals.loadGame.lastpos6y);
                Globals.atWaterCount = Globals.loadGame.atwater6;
                LoadGameData(6);
                Globals.shopMerchant.Gold = Globals.loadGame.gold6;
                DatabaseManager.instance.UpdateRecord<SelectedAvatar>(avatarState);
                LevelCalculation.instance.CalculateXpPoints();
                if (Globals.objectiveScene != "Atwater Village" && Globals.objectiveScene != "Brigand Village" && Globals.objectiveScene != "Huntington Town_Alley Scenes")
                    Globals.activeScene = (CurrentScene)System.Enum.Parse(typeof(CurrentScene), loadGame.SaveObjective6);
                else if (Globals.objectiveScene == "Atwater Village")
                {
                    Globals.activeScene = CurrentScene.AtwaterVillage;
                    atWaterCount = 5;
                }
                else if (Globals.objectiveScene == "Huntington Town_Alley Scenes")
                {
                    Globals.activeScene = CurrentScene.HuntingtonVillage;
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                if(Globals.objectiveScene == "Barghest Village" || Globals.objectiveScene == "Death Wight Village" || Globals.objectiveScene == "Brigand Village")
                {
                    Globals.afterPromotion = true;
                    Globals.isLightening = true;
                    Globals.noOfCompanions = 3;
                }
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
                break;
            case "Inventory":
                inventoryScreen.SetActive(true);
                Debug.Log("inventory ;;; "+inventoryScreen.name);
                Bag.SetActive(false);
                questLog.SetActive(false);
                if (Globals.selectedInventoryCharacter == null)
                {
                    Globals.selectedInventoryCharacter = Globals.avatarState.AvatarName;
                }
                Globals.ActiveControls(GameObject.FindGameObjectWithTag("Player").gameObject, false);
                break;
            case "InventoryBack":
               
                Bag.SetActive(true);
                questLog.SetActive(true);
                Globals.inventoryHandler.BackToInventory();
                inventoryScreen.SetActive(false);
                Globals.characterSlot.UpdateCharacter();
                Globals.ActiveControls(GameObject.FindGameObjectWithTag("Player").gameObject, true);
                break;
            case "Quest":
                questPopUp.SetActive(true);
                Globals.questHandler.ShowQuest();
                Globals.ActiveControls(GameObject.FindGameObjectWithTag("Player").gameObject, false);
                Time.timeScale = 0;
                break;
            case "QuestBack":
                Time.timeScale = 1;
                questPopUp.SetActive(false);
                Globals.ActiveControls(GameObject.FindGameObjectWithTag("Player").gameObject, true);
                break;
            case "quitGame":
                Debug.Log("here world map to move " + Globals.objectiveScene);
                Globals.backToMenu = true;

                Globals.avatarState.SaveState = Globals.objectiveScene;
                Globals.avatarState.SaveObjective = Globals.currentObjective;
                Globals.avatarState.atwaterCount = Globals.atWaterCount;
                Globals.avatarState.petrol = Globals.petrolCount;
                Globals.avatarState.carvan = Globals.caravnCount;
                SetPetrolValue();

                if (Globals.activeScene == Globals.CurrentScene.SoldierCampsite)
                {
                    Debug.Log("soldier campsite...............");
                    Globals.avatarState.secondVisit = Globals.soldierCampsiteVisit;
                }
                if (Globals.activeScene == Globals.CurrentScene.AtwaterVillage && Globals.atWaterCount == 5)
                {
                    Debug.Log("atwarer village..............." + Globals.atWaterCount);
                    Globals.isCompleteVid = false;
                    Globals.isSargent = false;
                    Globals.conversationCount = 0;
                }
                if (Globals.activeScene == Globals.CurrentScene.monastery)
                {
                    Debug.Log("monastery village...............");
                    Globals.objectiveScene = "Monastery_ext";
                    Globals.currentObjective = "Monastery";
                }
                else
                {
                    Debug.Log("others village...............");
                    Globals.avatarState.secondVisit = Globals.secondVisit;
                }

                db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);

             //   Globals.isHome = false;
                ResetVillageGlobalValue();
                SceneManager.LoadScene("World Map");

                Debug.Log("get active scene : " + SceneManager.GetActiveScene().name);


                break;
        }

    }

    void ContinueSetting()
    {
        Debug.Log("continue setting......."+ Globals.avatarState.SaveState + "at water count "+ Globals.avatarState.atwaterCount);
        Bag.SetActive(true);
        questLog.SetActive(true);
        Globals.currentObjective = Globals.avatarState.SaveObjective;
        Globals.objectiveScene = Globals.avatarState.SaveState;
        Globals.activePlayer = Resources.Load(Globals.avatarState.AvatarName) as GameObject;
        Globals.noOfCompanions = Globals.avatarState.companionCount;
        Globals.atWaterCount = Globals.avatarState.atwaterCount;
        Globals.lastPos = new Vector3(Globals.avatarState.lastposx, Globals.avatarState.lastposy);
        LoadGameData(10);
        Globals.loadSaveGame = true;
        if (Globals.objectiveScene == "Soldier Campsite" || Globals.objectiveScene == "Second Soldier Campsite")
        {
            Globals.activeScene = CurrentScene.SoldierCampsite;
            if(Globals.objectiveScene == "Second Soldier Campsite")
            {
                Globals.soldierCampsiteVisit = 1;
            }
            else
                Globals.soldierCampsiteVisit = 0;
            Debug.Log("soldier camsite visit :: "+Globals.soldierCampsiteVisit);
        }
        else if (Globals.objectiveScene == "Wagon Carvan")
        {
            Debug.Log("wagon caravan.........");
            Globals.activeScene = CurrentScene.WagonCaravan;
        }
        else if (Globals.objectiveScene == "SecondSoldierCaravan")
        {
            Globals.activeScene = CurrentScene.SecondSoldierCaravan;
        }
        else if (Globals.objectiveScene == "Huntsville")
        {
            Globals.activeScene = CurrentScene.Huntsville;
        }
        else if (Globals.objectiveScene == "Sacred Place Exterior_New") //Sacred Place Exterior_New //Sacred Place
        {
            Globals.activeScene = CurrentScene.SacredPlace;
        }
        else if (Globals.objectiveScene == "Monk Campsite")
        {
            Globals.activeScene = CurrentScene.MonkCampsite;
        }
        else if (Globals.objectiveScene == "Monastary Crypt" || Globals.objectiveScene == "Monastery_ext" || Globals.objectiveScene == "Monastery2ndFloor_int"|| Globals.objectiveScene == "Monastery1stFloor_int")
        {
            Debug.Log("here............monastery");
            Globals.activeScene = CurrentScene.monastery;
        }
        else if(Globals.objectiveScene == "Motte and Baley Castle")
        {
            Debug.Log("inside motte and bailey........");
            Globals.activeScene = CurrentScene.MotteAndBaileyCastle;
        }
        else if (Globals.objectiveScene != "Atwater Village" && Globals.objectiveScene != "Brigand Village" && Globals.objectiveScene != "Huntington Town_Alley Scenes")
            Globals.activeScene = (CurrentScene)System.Enum.Parse(typeof(CurrentScene), avatarState.SaveObjective);
        else if (Globals.objectiveScene == "Atwater Village")
        {
            Globals.activeScene = CurrentScene.AtwaterVillage;
            atWaterCount = Globals.avatarState.atwaterCount; //5
        }
        else if (Globals.objectiveScene == "Huntington Town_Alley Scenes")
        {
            Globals.activeScene = CurrentScene.HuntingtonVillage;
            Globals.afterPromotion = true;
            Globals.isLightening = true;
            Globals.noOfCompanions = 3;
        }
        if(Globals.objectiveScene == "Barghest Village" || Globals.objectiveScene == "Death Wight Village" || Globals.objectiveScene == "Brigand Village" || Globals.objectiveScene == "Motte and Baley Castle")
        {
            Globals.afterPromotion = true;
            Globals.isLightening = true;
            Globals.noOfCompanions = 3;
        }
        atWaterCount = Globals.avatarState.atwaterCount;
        SetPetrolCarvanValue();
        if(Globals.activeScene == CurrentScene.RandomAttack)
        {
            Debug.Log("here....... random petrols");
            activeRandom = CurrentRandom.petrols;
        }
        Globals.secondVisit = Globals.avatarState.secondVisit;
        SceneManager.LoadScene(Globals.avatarState.SaveState);
        Time.timeScale = 1;
    }
    void SetFogOnDestination()
    {
        Debug.Log("objective::" + currentObjective);
        if (currentObjective == "Soldier Campsite")
        {
            if (soldierCampsiteVisit == 0)
            {
                soldier.SetActive(true);
                soldierCol.SetActive(true);
                secondSoldierCol.SetActive(false);
                secondSoldier.SetActive(false);
                FogOfWar(soldierCampsite);
                Globals.noOfCompanions = 1;
            }
            else
            {
                soldier.SetActive(false);
                soldierCol.SetActive(false);
                wagon.SetActive(false);
                wagonCol.SetActive(false);
                secondSoldier.SetActive(true);
                secondSoldierCol.SetActive(true);
                Globals.noOfCompanions = 2;
                FogOfWar(secondSoldierCampsite);
            }
        }
        else if (currentObjective == "Wagon Caravan")
        {
            Debug.Log("wagon caravan :: ");
            soldier.SetActive(false);
            soldierCol.SetActive(false);
            wagon.SetActive(true);
            wagonCol.SetActive(true);
            Globals.noOfCompanions = 2;
            FogOfWar(wagonCaravan);
        }
        else if (currentObjective == "SecondSoldierCaravan")
        {
            Debug.Log("second soldier caravan :: ");
            wagon.SetActive(false);
            wagonCol.SetActive(false);
            soldier.SetActive(true);
            soldierCol.SetActive(true);
            Globals.noOfCompanions = 2;
            FogOfWar(secondSoldierCampsite);
        }
        else if (currentObjective == "Huntsville")
        {
            if (Globals.secondVisit == 0)
            {
                Debug.Log("zero visit huntsville :: ");
                soldier.SetActive(false);
                soldierCol.SetActive(false);
                secondSoldier.SetActive(false);
                secondSoldierCol.SetActive(false);
                Globals.noOfCompanions = 2;
                FogOfWar(hunsville);
            }
            else if (Globals.secondVisit == 1)
            {
                Debug.Log("first visit huntsville :: ");
                sacretPlaceCol.SetActive(false);
                Globals.noOfCompanions = 2;
                FogOfWar(Part2Hunsville);
            }
            else if (Globals.secondVisit == 2)
            {
                Debug.Log("second visit huntsville :: ");
                Globals.noOfCompanions = 3;
                FogOfWar(Part3Hunsville);
            }
        }
        else if (currentObjective == "Atwater Village")
        {
            Debug.Log("curent atwater :: ");
            soldier.SetActive(false);
            soldierCol.SetActive(false);
            secondSoldier.SetActive(false);
            secondSoldierCol.SetActive(false);
            Globals.noOfCompanions = 2;
            FogOfWar(atwater);
        }
        else if (currentObjective == "Sacred Place")
        {
            sacretPlaceCol.SetActive(true);
            Globals.noOfCompanions = 2;
            FogOfWar(sacredPlace);
        }
        else if (currentObjective == "Campsite")
        {
            monkCaravan.SetActive(true);
            monkCaravanCol.SetActive(true);
            Globals.noOfCompanions = 2;
            FogOfWar(monkCampsite);
        }
        else if (currentObjective == "Monastery")
        {
            monkCaravan.SetActive(false);
            monkCaravanCol.SetActive(false);
            Globals.noOfCompanions = 3;
            FogOfWar(monestry);
        }
        else if (currentObjective == "RandomAttack" && atWaterCount == 6)
            Globals.noOfCompanions = 3;
        else if (currentObjective == "Motte and Bailey Castle")
        {
            Globals.noOfCompanions = 3;
            FogOfWar(motteyAndBailey);
        }
        else if (currentObjective == "BarghestVillage")
        {
            barghest.SetActive(true);
            barghestCol.SetActive(true);
            Globals.noOfCompanions = 3;
            FogOfWar(Barghest);
        }
        else if (currentObjective == "TheDeathWeight")
        {
            barghest.SetActive(false);
            barghestCol.SetActive(false);
            deathweight.SetActive(true);
            deathWeightCol.SetActive(true);
            Globals.noOfCompanions = 3;
            FogOfWar(DeathWeight);
        }
        else if (currentObjective == "Brigand Village")
        {
            deathweight.SetActive(false);
            deathWeightCol.SetActive(false);
            brigand.SetActive(true);
            brigandCol.SetActive(true);
            Globals.noOfCompanions = 3;
            FogOfWar(Brigand);
        }
        else if (currentObjective == "Huntington")
        {
            brigand.SetActive(false);
            brigandCol.SetActive(false);
            Globals.noOfCompanions = 4;
            FogOfWar(huntington);
        }

    }
  public  void FogOfWar(GameObject[] currentFog)
    {
        foreach (GameObject item in currentFog)
        {
            item.SetActive(false);
        }
    }
    public void OnSelect()
    {
        Globals.playerState.GoogleName = typeText.GetComponent<Text>().text;
        db.UpdateRecord<Player>(Globals.playerState);
    }
    void MusicSetting(bool on,bool off)
    {
        musicOn.gameObject.SetActive(on);
        musicOff.gameObject.SetActive(off);
    }
    void ControlSetting(bool on, bool off)
    {
        controlOn.gameObject.SetActive(on);
        controlOff.gameObject.SetActive(off);
    }
   
    void ShowLoadGames()
    {
        LoadGamePanel.SetActive(true);
        if (Globals.loadGame.SaveState1 != "")
        {
            firstLoad.SetActive(true);
            firstLoad.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Globals.loadGame.SaveState1;
        }
        else
            firstEmpty.SetActive(true);
        if (Globals.loadGame.SaveState2 !="")
        {
            secondLoad.SetActive(true);
            secondLoad.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Globals.loadGame.SaveState2;
        }
        else
            secondEmpty.SetActive(true);
        if (Globals.loadGame.SaveState3 !="")
        {
            thirdLoad.SetActive(true);
            thirdLoad.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Globals.loadGame.SaveState3;
        }
        else
            thirdtEmpty.SetActive(true);
        if (Globals.loadGame.SaveState4 != "")
        {
            ForthLoad.SetActive(true);
            ForthLoad.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Globals.loadGame.SaveState4;
        }
        else
            forthEmpty.SetActive(true);
        if (Globals.loadGame.SaveState5 != "")
        {
            fifthLoad.SetActive(true);
            fifthLoad.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Globals.loadGame.SaveState5;
        }
        else
            fifthEmpty.SetActive(true);
        if (Globals.loadGame.SaveState6!= "")
        {
            sixthLoad.SetActive(true);
            sixthLoad.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Globals.loadGame.SaveState6;
        }
        else
            sixthEmpty.SetActive(true);

    }
    void SplashSetting()
    {
        loadingScreen.SetActive(false);
        avatarSelection.SetActive(true);
      avatarContainer.Initialize(Globals.Gender.Male);
    }
    void UpdateInventory(int value)
    {
        foreach(var v in inventoryButtons)
        {
            v.gameObject.SetActive(true);
        }
      foreach(var v in inventoryText)
        {
            v.gameObject.SetActive(false);
        }
        inventoryText[value].SetActive(true);
        inventoryButtons[value].SetActive(false);
        foreach(var v in Globals.inventoryData)
        {
            v.value = 0;
            db.UpdateRecord<Globals.Inventory>(Globals.inventory);
        }
        Globals.Inventory _updateQuery = Globals.inventoryData.Find((x) => { return x.Key == value + 1; });
        _updateQuery.value = 1;
        db.UpdateRecord<Globals.Inventory>(_updateQuery);
    }
    public void SelectedAvatarOfGame(string nameOfAvatar, GameObject prefab)
    {
        ResetValues(nameOfAvatar);
        Globals.avatarState.AvatarName = nameOfAvatar;
        Globals.avatarState.AvatarType = "Basic";
        db.UpdateRecord<SelectedAvatar>(Globals.avatarState);
        prefabObject = prefab;
        StartPanel.SetActive(true);
        avatarSelection.SetActive(false);
        editNameScreen.SetActive(true);
        typeText.transform.parent.GetComponent<InputField>().text = Globals.characterName.ToUpper();
    }
    void CameraSetting(bool l, bool r)
    {
        //leftCamera.SetActive(l);
        //rightCamera.SetActive(r);
    }
    void GameStartFinal()
    {
        if (avatarState.AvatarName == "WarriorMale")
        {
            if (avatarState.Smith == 0)
            {
                Globals.isSmith = true;
                IntroSetting();
            }
            else
                StartNormalGame();
           
        }
        else if (avatarState.AvatarName == "ArcherMale")
        {
            if (avatarState.Archer == 0)
            {
                Globals.isArcher = true;
                IntroSetting();
            }
            else
                StartNormalGame();
           
        }
        else if (avatarState.AvatarName == "PriestMale")
        {
            if (avatarState.Priest == 0)
            {
                Globals.isAcolyte = true;
                IntroSetting();
            }
            else
                StartNormalGame();
        }
        else if(avatarState.AvatarName== "WarriorFemale")
        {
            if (avatarState.SmithF == 0)
            {
                Globals.isSmithF = true;
                IntroSetting();
            }
            else
                StartNormalGame();
        }
        else if (avatarState.AvatarName == "ArcherFemale")
        {
            if (avatarState.ArcherF == 0)
            {
                Globals.isArcherF = true;
                IntroSetting();
            }
            else
                StartNormalGame();
        }
        else if (avatarState.AvatarName == "PriestFemale")
        {
            if (avatarState.PriestF == 0)
            {
                Globals.isAcolyteF = true;
                IntroSetting();
            }
            else
                StartNormalGame();
        }
        else
            StartNormalGame();
        Globals.soundSetting.SoundPlay();
        Globals.isGameStart = true;
    }
    void IntroSetting()
    {
        loading.SetActive(true);
        SceneManager.LoadSceneAsync("Huntsville_Intro Scene");
    }
  public  void EmptyShopData()
    {
        Globals.inventoryProtagnist.Dragger = 0;
        Globals.inventoryProtagnist.ShortSword = 0;
        Globals.inventoryProtagnist.ShortAxe = 0;
        Globals.inventoryProtagnist.Club = 0;
        Globals.inventoryProtagnist.ShortBow = 0;
        Globals.inventoryProtagnist.LongSword = 0;
        Globals.inventoryProtagnist.LongBow = 0;
        Globals.inventoryProtagnist.Mace = 0;
        Globals.inventoryProtagnist.Warhammer = 0;
        Globals.inventoryProtagnist.Spear = 0;
        Globals.inventoryProtagnist.LongAxe = 0;
        Globals.inventoryProtagnist.DoubleHeadedAxe = 0;
        Globals.inventoryProtagnist.Flair = 0;
        Globals.inventoryProtagnist.Maul = 0;
        Globals.inventoryProtagnist.CompositeBow = 0;
        Globals.inventoryProtagnist.CrossBow = 0;
        Globals.inventoryProtagnist.WoodenBuckler = 0;
        Globals.inventoryProtagnist.WoodenSmallRounded = 0;
        Globals.inventoryProtagnist.MetalBuckler = 0;
        Globals.inventoryProtagnist.MetalSmallRounded = 0;
        Globals.inventoryProtagnist.WoodenMediumShield = 0;
        Globals.inventoryProtagnist.MetalMediumShield = 0;
        Globals.inventoryProtagnist.ClothArmour = 0;
        Globals.inventoryProtagnist.PaddedArmour = 0;
        Globals.inventoryProtagnist.LeatherArmour = 0;
        Globals.inventoryProtagnist.HideArmour = 0;
        Globals.inventoryProtagnist.LeatherCap = 0;
        Globals.inventoryProtagnist.KettleHat = 0;
        Globals.inventoryProtagnist.NesalHelmet = 0;
        Globals.inventoryProtagnist.BrigadineArmor = 0;
        Globals.inventoryProtagnist.ScaleArmour = 0;
        Globals.inventoryProtagnist.ChainArmour = 0;
        Globals.inventoryProtagnist.Aventail = 0;
        Globals.inventoryProtagnist.MailCoif = 0;
        Globals.inventoryProtagnist.Ale = 0;
        Globals.inventoryProtagnist.CurePotion = 0;
        Globals.inventoryProtagnist.Meat = 0;
        Globals.inventoryProtagnist.Food = 0;
        Globals.inventoryProtagnist.HealPotion = 0;
        Globals.inventoryProtagnist.Rum = 0;
        Globals.inventoryProtagnist.magicSword = 0;
        db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
       // Globals.shopMerchant.Gold = 0; //0
        db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        Globals.inventoryJohn.Dragger = 0;
        Globals.inventoryJohn.ShortSword = 0;
        Globals.inventoryJohn.ShortAxe = 0;
        Globals.inventoryJohn.Warhammer = 0;
        Globals.inventoryJohn.LongAxe = 0;
        Globals.inventoryJohn.LongSword = 0;
        Globals.inventoryJohn.Mace = 0;
        Globals.inventoryJohn.Spear = 0;
        Globals.inventoryJohn.WoodenBuckler = 0;
        Globals.inventoryJohn.WoodenSmallRound = 0;
        Globals.inventoryJohn.metalBuckler = 0;
        Globals.inventoryJohn.metalSmallRound = 0;
        Globals.inventoryJohn.WoodenMedium = 0;
        Globals.inventoryJohn.metalMedium = 0;
        Globals.inventoryJohn.PaddedArmour = 0;
        Globals.inventoryJohn.HideArmour = 0;
        Globals.inventoryJohn.LeatherArmour = 0;
        Globals.inventoryJohn.BrigadineArmour = 0;
        Globals.inventoryJohn.ScaleArmour = 0;
        Globals.inventoryJohn.ChainArmour = 0;
        Globals.inventoryJohn.LeatherCap = 0;
        Globals.inventoryJohn.KettleHat = 0;
        Globals.inventoryJohn.NasalHelmet = 0;
        Globals.inventoryJohn.Avaintail = 0;
        Globals.inventoryJohn.MailCoif = 0;
        Globals.inventoryJohn.ClothArmour = 0;
        db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
        Globals.inventoryMarium.Dragger = 0;
        Globals.inventoryMarium.ShortSword = 0;
        Globals.inventoryMarium.ShortAxe = 0;
        Globals.inventoryMarium.ShortBow = 0;
        Globals.inventoryMarium.Warhammer = 0;
        Globals.inventoryMarium.Spear = 0;
        Globals.inventoryMarium.LongBow = 0;
        Globals.inventoryMarium.WoodenBuckler = 0;
        Globals.inventoryMarium.MetalBuckler = 0;
        Globals.inventoryMarium.woodenSmall = 0;
        Globals.inventoryMarium.MetalSmall = 0;
        Globals.inventoryMarium.ClothArmour = 0;
        Globals.inventoryMarium.PaddedArmour = 0;
        Globals.inventoryMarium.HideArmour = 0;
        Globals.inventoryMarium.LeatherArmour = 0;
        Globals.inventoryMarium.BrigadineArmour = 0;
        Globals.inventoryMarium.LeatherCap = 0;
        Globals.inventoryMarium.KettleHat = 0;
        Globals.inventoryMarium.NasalHelmet = 0;
        Globals.inventoryMarium.ClothArmour = 0;
        db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
        Globals.inventoryTucker.Dragger = 0;
        Globals.inventoryTucker.Mace = 0;
        Globals.inventoryTucker.Warhammer = 0;
        Globals.inventoryTucker.Flair = 0;
        Globals.inventoryTucker.Maul = 0;
        Globals.inventoryTucker.WoodenBuckler = 0;
        Globals.inventoryTucker.WoodenSmall = 0;
        Globals.inventoryTucker.MetalBuckler = 0;
        Globals.inventoryTucker.MetalSmall = 0;
        Globals.inventoryTucker.MetalMedium = 0;
        Globals.inventoryTucker.ClothArmour = 0;
        Globals.inventoryTucker.PaddedArmour = 0;
        Globals.inventoryTucker.HideArmour = 0;
        Globals.inventoryTucker.LeatherArmour = 0;
        Globals.inventoryTucker.LeatherCap = 0;
        Globals.inventoryTucker.KettleHat = 0;
        Globals.inventoryTucker.ClothArmour = 0;
        db.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);

    }
    void EmptyMerchantShop()
    {
     
    }
    void StartNormalGame()
    {
        if (Globals.avatarState.AvatarName != Globals.shopMerchant.LastAvatar)
            EmptyMerchantShop();
        if (Globals.avatarState.ControlLevel == 0)
            CameraSetting(true, false);
        else
            CameraSetting(false, true);
        if (Globals.isHome)
        {
            worldMap.Play();
            Bag.SetActive(true);
            questLog.SetActive(true);
            Globals.levelManager.BackToVillage(); //level.BackToVillage()
            SetCurrentObjective();
            SetFogOnDestination();
        }
        else
        {
            if (!isContinue && !isLoad)
            {
                worldMap.Play();
                Bag.SetActive(true);
                questLog.SetActive(true);
                Globals.levelManager.SpawnPlayer(prefabObject);            
                Globals.avatarState.TotalXp = 300; // 19870  300 41990 6760
                Globals.avatarState.Level = 1;  //11 1 15 5
                db.UpdateRecord<SelectedAvatar>(avatarState);
                Globals.currentObjective = "BarghestVillage";  // Monastery Sacred Place  Soldier Campsite Atwater Village   Motte and Bailey Castle  Brigand Village  // Huntington  //BarghestVillage TheDeathWeight
                activeScene = CurrentScene.BarghestVillage;
                Globals.afterPromotion = false;
                Globals.isLightening = false;
                Globals.noOfCompanions = 1; 
                Debug.Log("load level.........."+ currentObjective);
                Time.timeScale = 1;
                FogOfWar(soldierCampsite);
                SetCurrentObjective();
            }
            else if (isContinue && !isLoad)
                ContinueSetting();
            else if (isLoad && !isContinue)
                SceneManager.LoadScene(Globals.objectiveScene);
        }
    }
    void SetCurrentObjective()
    {
        foreach(var v in colliders)
        {
            if(v.gameObject.name==currentObjective)
            {
                Debug.Log("collided objectived :"+ v.gameObject.name);
                v.SetActive(true);
                foreach (Transform item in v.transform)
                {
                    item.tag = currentObjective;
                }
            }
            //else
            //{
            //    Debug.Log(" objectived :" + v.gameObject.name);
            //    v.SetActive(false);
            //}
            if (v.gameObject.name == previosObjective)
                v.SetActive(false);
        }
    }
    void SetPanel()
    {
        if (!Globals.isCompleteGame)
            StartPanel.SetActive(true);
        else
        {
            StartPanel.SetActive(false);
            Globals.isHome = true;
        }
        if (Globals.backToMenu)
        {
            EnableButton();
            Globals.backToMenu = false;
        }
           
        else
            Invoke("EnableButton", 1.5f);
    }
    
    void EnableButton()
    {
        StartPanel.SetActive(false);
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    if (Globals.playerState.GoogleName.Equals(""))
        //        Globals.gameCenterHandler.OnGuestLogin();
        //    if (Globals.playerState.GoogleEmail.Equals(""))
        //        StartPanelSetting(true, false);
        //    else
        //        LoadingSetting();
        //}
        //else
        {
            //if (Globals.playerState.GameCenterId.Equals(""))
            //    Globals.gameCenterHandler.GameCenterInfo();

            Debug.Log("global value :: "+ Globals.playerState.AccessToken);
            if (Globals.playerState.AccessToken.Equals("") || Globals.playerState.AccessToken == null)
                StartPanelSetting(true, false);

            else
                LoadingSetting();
        }
    }
    void StartPanelSetting(bool s,bool l)
    {
        loginScreen.SetActive(s);
        loadingScreen.SetActive(l);
    }
    public void LoadingSetting()
    {
        Debug.Log(" scene :: "+ SceneManager.GetActiveScene().name);
        loginScreen.SetActive(false);
        loadingScreen.SetActive(true);
        Debug.Log("state........."+ Globals.avatarState.SaveState+"..........");
        if (Globals.avatarState.SaveState == "" || Globals.avatarState.SaveState == null)
            SettingSpash(false, true);
        else
            SettingSpash(true, false);
    }


    void SettingSpash(bool con,bool n)
    {
        ContinuePanel.SetActive(con);
        newGame.SetActive(n);
    }
    void SplashSetting(bool c,bool l)
    {
        Continue.interactable = c;
        Load.interactable = l;
    }

    void LoadingBar()
    {
        bar.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, t);
        t += Time.deltaTime * lerpSpeed;
        if (bar.GetComponent<Image>().fillAmount == 1)
        {
                                                                                                                                                                                                           CancelInvoke();
            loadingScreen.SetActive(false);
            avatarSelection.SetActive(true);
            if (Globals.playerState.GoogleName == "")
              Globals.gameCenterHandler.welcomeText.text = "Welcome " + Globals.playerState.GameCenterName;
            else
              Globals.gameCenterHandler.welcomeText.text = "Welcome " + Globals.playerState.GoogleName;
        }
    }

    void ResetValues(string currentAvatar)
    {
        if (Globals.avatarState.AvatarName != currentAvatar)
        {
            Globals.inventoryProtagnist.AttackWeapon = "";
            Globals.inventoryProtagnist.Armour = "";
            Globals.inventoryProtagnist.Helmet = "";
          
            if (currentAvatar == "WarriorMale" || currentAvatar == "WarriorFemale")
            {
                Globals.inventoryProtagnist.AttackWeapon = "ShortSword";
                Globals.inventoryProtagnist.Armour = "";
                Globals.inventoryProtagnist.Helmet = "";

                Globals.inventoryProtagnist.ShortSword = 1;
                Globals.shopMerchant.ShortSword += 1;
                ResetWeaponData(0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
            }
            else if(currentAvatar == "ArcherMale" || currentAvatar == "ArcherFemale")
            {
                Globals.inventoryProtagnist.AttackWeapon = "shortBow";
                Globals.inventoryProtagnist.Armour = "";
                Globals.inventoryProtagnist.Helmet = "";

                Globals.shopMerchant.ShortBow += 1;
                Globals.inventoryProtagnist.ShortBow = 1;
                ResetWeaponData(0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            else if (currentAvatar == "PriestMale" || currentAvatar == "PriestFemale")
            {
                Globals.inventoryProtagnist.AttackWeapon = "Mace";
                Globals.inventoryProtagnist.Armour = "";
                Globals.inventoryProtagnist.Helmet = "";

                Globals.shopMerchant.Mace += 1;
                Globals.inventoryProtagnist.Mace = 1;
                ResetWeaponData(0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            }
            db.UpdateRecord<ProtagnistInventory>(Globals.inventoryProtagnist);
            db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        }
    }
    void ResetWeaponData(int d, int s, int sa, int c, int sb, int ls, int lb, int m, int w, int sp, int la, int da, int f, int ma, int co, int cb, int ms)
    {
        Globals.inventoryProtagnist.Dragger = d;
        Globals.inventoryProtagnist.ShortSword = s;
        Globals.inventoryProtagnist.ShortAxe = sa;
        Globals.inventoryProtagnist.Club = c;
        Globals.inventoryProtagnist.ShortBow = sb;
        Globals.inventoryProtagnist.LongSword = ls;
        Globals.inventoryProtagnist.LongBow = lb;
        Globals.inventoryProtagnist.Mace = m;
        Globals.inventoryProtagnist.Warhammer = w;
        Globals.inventoryProtagnist.Spear = sp;
        Globals.inventoryProtagnist.LongAxe = la;
        Globals.inventoryProtagnist.DoubleHeadedAxe = da;
        Globals.inventoryProtagnist.Flair = f;
        Globals.inventoryProtagnist.Maul = ma;
        Globals.inventoryProtagnist.CompositeBow = co;
        Globals.inventoryProtagnist.CrossBow = cb;
        Globals.inventoryProtagnist.magicSword = ms;
    }

    void SetPetrolCarvanValue()
    {
        Globals.petrolCount = Globals.avatarState.petrol;
        Globals.caravnCount = Globals.avatarState.carvan;
        if (Globals.avatarState.petrol1 == 1)
            Globals.petrol1 = true;
        else
            Globals.petrol1 = false;

        if (Globals.avatarState.petrol2 == 1)
            Globals.petrol2 = true;
        else
            Globals.petrol2 = false;

        if (Globals.avatarState.petrol3 == 1)
            Globals.petrol3 = true;
        else
            Globals.petrol3 = false;

        if (Globals.avatarState.carvan1 == 1)
            Globals.carvan1 = true;
        else
            Globals.carvan1 = false;

        if (Globals.avatarState.carvan2 == 1)
            Globals.caravan2 = true;
        else
            Globals.caravan2 = false;

        if (Globals.avatarState.carvan3 == 1)
            Globals.caravan3 = true;
        else
            Globals.caravan3 = false;
    }

    void ResetVillageGlobalValue()
    {
        if (Globals.activeScene == Globals.CurrentScene.MotteAndBaileyCastle)
        {
            Debug.Log("reset motte bailey global value.........");
            Globals.secondFight = false;
            Globals.thirdFight = false;
            Globals.forthFight = false;
            Globals.beforeMottey = false;
            Globals.isPart1Battle = false;
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
    public void DecryptData<T>(List<T> list)
    {
        Globals.SaveGame save = (Globals.SaveGame)Convert.ChangeType(list, typeof(Globals.SaveGame));

      //  Globals.SaveGame save = new Globals.SaveGame();
        Debug.Log("here list :: "+save.Dragger);
       // save.Dragger = list[;
    }
    public static List<Globals.SaveGame1> savegame1;
    public static List<Globals.SaveGame2> savegame2;
    public static List<Globals.SaveGame3> savegame3;
    public static List<Globals.SaveGame4> savegame4;
    public static List<Globals.SaveGame5> savegame5;
    public static List<Globals.SaveGame6> savegame6;
    public static List<Globals.ContinueGame> continueSaveGame;
    public void LoadGameData(int loadnumber) {
        Globals.loadData = true;
        switch (loadnumber)
        {
            case 1:
               savegame1 = db.ReadTable<Globals.SaveGame1>().ToList<Globals.SaveGame1>();
                Globals.SaveGame1 data1 = savegame1[0];
                Debug.Log("dragger :: "+data1.Dragger);
                loadWeaponData(data1.Dragger, data1.ShortSword, data1.ShortAxe,data1.Club,data1.ShortBow,data1.LongSword,data1.LongBow,data1.Mace,data1.Warhammer,data1.Spear,data1.LongAxe,data1.DoubleHeadedAxe,data1.Flair,data1.Maul);
                loadShieldData(data1.WoodenBuckler, data1.WoodenSmallRounded, data1.MetalBuckler, data1.MetalSmallRounded, data1.WoodenMediumShield, data1.MetalMediumShield, data1.WoodenKiteShield, data1.WoodenTowerShield, data1.MetalKiteShield);
                loadArmorData(data1.PaddedArmour, data1.HideArmour, data1.LeatherArmour, data1.BrigadineArmor, data1.ScaleArmour, data1.ChainArmour);
                loadHelmetData(data1.LeatherCap, data1.KettleHat, data1.NesalHelmet, data1.Aventail, data1.MailCoif);
                loadItemsData(data1.Ale, data1.CurePotion, data1.Food, data1.Meat, data1.HealPotion, data1.Rum,data1.SoulGem, data1.SoulEyeGems,data1.BarghestHeart,data1.Bones, data1.RottenFlesh);
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                loadProtagData(data1.ProtagonistWeapon, data1.ProtagonistArmor, data1.ProtagonistShield, data1.ProtagonistHelmet);
                loadMariumData(data1.MariumWeapon, data1.MariumArmor, data1.MariumShield, data1.MariumHelmet);
                loadJohnData(data1.JohnWeapon, data1.JohnArmor, data1.JohnShield, data1.JohnHelmet);
                loadTuckerData(data1.TuckerWeapon, data1.TuckerArmor, data1.TuckerShield, data1.TuckerHelmet);
                break;
            case 2:
                savegame2 = db.ReadTable<Globals.SaveGame2>().ToList<Globals.SaveGame2>();
                Globals.SaveGame2 data2 = savegame2[0];
                loadWeaponData(data2.Dragger, data2.ShortSword, data2.ShortAxe, data2.Club, data2.ShortBow, data2.LongSword, data2.LongBow, data2.Mace, data2.Warhammer, data2.Spear, data2.LongAxe, data2.DoubleHeadedAxe, data2.Flair, data2.Maul);
                loadShieldData(data2.WoodenBuckler, data2.WoodenSmallRounded, data2.MetalBuckler, data2.MetalSmallRounded, data2.WoodenMediumShield, data2.MetalMediumShield, data2.WoodenKiteShield, data2.WoodenTowerShield, data2.MetalKiteShield);
                loadArmorData(data2.PaddedArmour, data2.HideArmour, data2.LeatherArmour, data2.BrigadineArmor, data2.ScaleArmour, data2.ChainArmour);
                loadHelmetData(data2.LeatherCap, data2.KettleHat, data2.NesalHelmet, data2.Aventail, data2.MailCoif);
                loadItemsData(data2.Ale, data2.CurePotion, data2.Food, data2.Meat, data2.HealPotion, data2.Rum, data2.SoulGem, data2.SoulEyeGems, data2.BarghestHeart, data2.Bones, data2.RottenFlesh);
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                loadProtagData(data2.ProtagonistWeapon, data2.ProtagonistArmor, data2.ProtagonistShield, data2.ProtagonistHelmet);
                loadMariumData(data2.MariumWeapon, data2.MariumArmor, data2.MariumShield, data2.MariumHelmet);
                loadJohnData(data2.JohnWeapon, data2.JohnArmor, data2.JohnShield, data2.JohnHelmet);
                loadTuckerData(data2.TuckerWeapon, data2.TuckerArmor, data2.TuckerShield, data2.TuckerHelmet);
                break;
            case 3:
                savegame3 = db.ReadTable<Globals.SaveGame3>().ToList<Globals.SaveGame3>();
                Globals.SaveGame3 data3 = savegame3[0];
                loadWeaponData(data3.Dragger, data3.ShortSword, data3.ShortAxe, data3.Club, data3.ShortBow, data3.LongSword, data3.LongBow, data3.Mace, data3.Warhammer, data3.Spear, data3.LongAxe, data3.DoubleHeadedAxe, data3.Flair, data3.Maul);
                loadShieldData(data3.WoodenBuckler, data3.WoodenSmallRounded, data3.MetalBuckler, data3.MetalSmallRounded, data3.WoodenMediumShield, data3.MetalMediumShield, data3.WoodenKiteShield, data3.WoodenTowerShield, data3.MetalKiteShield);
                loadArmorData(data3.PaddedArmour, data3.HideArmour, data3.LeatherArmour, data3.BrigadineArmor, data3.ScaleArmour, data3.ChainArmour);
                loadHelmetData(data3.LeatherCap, data3.KettleHat, data3.NesalHelmet, data3.Aventail, data3.MailCoif);
                loadItemsData(data3.Ale, data3.CurePotion, data3.Food, data3.Meat, data3.HealPotion, data3.Rum, data3.SoulGem, data3.SoulEyeGems, data3.BarghestHeart, data3.Bones, data3.RottenFlesh);
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                loadProtagData(data3.ProtagonistWeapon, data3.ProtagonistArmor, data3.ProtagonistShield, data3.ProtagonistHelmet);
                loadMariumData(data3.MariumWeapon, data3.MariumArmor, data3.MariumShield, data3.MariumHelmet);
                loadJohnData(data3.JohnWeapon, data3.JohnArmor, data3.JohnShield, data3.JohnHelmet);
                loadTuckerData(data3.TuckerWeapon, data3.TuckerArmor, data3.TuckerShield, data3.TuckerHelmet);
                break;
            case 4:
                savegame4 = db.ReadTable<Globals.SaveGame4>().ToList<Globals.SaveGame4>();
                Globals.SaveGame4 data4 = savegame4[0];
                loadWeaponData(data4.Dragger, data4.ShortSword, data4.ShortAxe, data4.Club, data4.ShortBow, data4.LongSword, data4.LongBow, data4.Mace, data4.Warhammer, data4.Spear, data4.LongAxe, data4.DoubleHeadedAxe, data4.Flair, data4.Maul);
                loadShieldData(data4.WoodenBuckler, data4.WoodenSmallRounded, data4.MetalBuckler, data4.MetalSmallRounded, data4.WoodenMediumShield, data4.MetalMediumShield, data4.WoodenKiteShield, data4.WoodenTowerShield, data4.MetalKiteShield);
                loadArmorData(data4.PaddedArmour, data4.HideArmour, data4.LeatherArmour, data4.BrigadineArmor, data4.ScaleArmour, data4.ChainArmour);
                loadHelmetData(data4.LeatherCap, data4.KettleHat, data4.NesalHelmet, data4.Aventail, data4.MailCoif);
                loadItemsData(data4.Ale, data4.CurePotion, data4.Food, data4.Meat, data4.HealPotion, data4.Rum, data4.SoulGem, data4.SoulEyeGems, data4.BarghestHeart, data4.Bones, data4.RottenFlesh);
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                loadProtagData(data4.ProtagonistWeapon, data4.ProtagonistArmor, data4.ProtagonistShield, data4.ProtagonistHelmet);
                loadMariumData(data4.MariumWeapon, data4.MariumArmor, data4.MariumShield, data4.MariumHelmet);
                loadJohnData(data4.JohnWeapon, data4.JohnArmor, data4.JohnShield, data4.JohnHelmet);
                loadTuckerData(data4.TuckerWeapon, data4.TuckerArmor, data4.TuckerShield, data4.TuckerHelmet);
                break;
            case 5:
                savegame5 = db.ReadTable<Globals.SaveGame5>().ToList<Globals.SaveGame5>();
                Debug.Log("here............data count :: "+savegame5.ToList().Count);
                Globals.SaveGame5 data5 = savegame5[0];
                loadWeaponData(data5.Dragger, data5.ShortSword, data5.ShortAxe, data5.Club, data5.ShortBow, data5.LongSword, data5.LongBow, data5.Mace, data5.Warhammer, data5.Spear, data5.LongAxe, data5.DoubleHeadedAxe, data5.Flair, data5.Maul);
                loadShieldData(data5.WoodenBuckler, data5.WoodenSmallRounded, data5.MetalBuckler, data5.MetalSmallRounded, data5.WoodenMediumShield, data5.MetalMediumShield, data5.WoodenKiteShield, data5.WoodenTowerShield, data5.MetalKiteShield);
                loadArmorData(data5.PaddedArmour, data5.HideArmour, data5.LeatherArmour, data5.BrigadineArmor, data5.ScaleArmour, data5.ChainArmour);
                loadHelmetData(data5.LeatherCap, data5.KettleHat, data5.NesalHelmet, data5.Aventail, data5.MailCoif);
                loadItemsData(data5.Ale, data5.CurePotion, data5.Food, data5.Meat, data5.HealPotion, data5.Rum, data5.SoulGem, data5.SoulEyeGems, data5.BarghestHeart, data5.Bones, data5.RottenFlesh);
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                loadProtagData(data5.ProtagonistWeapon, data5.ProtagonistArmor, data5.ProtagonistShield, data5.ProtagonistHelmet);
                loadMariumData(data5.MariumWeapon, data5.MariumArmor, data5.MariumShield, data5.MariumHelmet);
                loadJohnData(data5.JohnWeapon, data5.JohnArmor, data5.JohnShield, data5.JohnHelmet);
                loadTuckerData(data5.TuckerWeapon, data5.TuckerArmor, data5.TuckerShield, data5.TuckerHelmet);
                break;
            case 6:
                savegame6 = db.ReadTable<Globals.SaveGame6>().ToList<Globals.SaveGame6>();
                Debug.Log("here............data count :: " + savegame6.ToList().Count);
                Globals.SaveGame6 data6 = savegame6[0];
                loadWeaponData(data6.Dragger, data6.ShortSword, data6.ShortAxe, data6.Club, data6.ShortBow, data6.LongSword, data6.LongBow, data6.Mace, data6.Warhammer, data6.Spear, data6.LongAxe, data6.DoubleHeadedAxe, data6.Flair, data6.Maul);
                loadShieldData(data6.WoodenBuckler, data6.WoodenSmallRounded, data6.MetalBuckler, data6.MetalSmallRounded, data6.WoodenMediumShield, data6.MetalMediumShield, data6.WoodenKiteShield, data6.WoodenTowerShield, data6.MetalKiteShield);
                loadArmorData(data6.PaddedArmour, data6.HideArmour, data6.LeatherArmour, data6.BrigadineArmor, data6.ScaleArmour, data6.ChainArmour);
                loadHelmetData(data6.LeatherCap, data6.KettleHat, data6.NesalHelmet, data6.Aventail, data6.MailCoif);
                loadItemsData(data6.Ale, data6.CurePotion, data6.Food, data6.Meat, data6.HealPotion, data6.Rum, data6.SoulGem, data6.SoulEyeGems, data6.BarghestHeart, data6.Bones, data6.RottenFlesh);
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                loadProtagData(data6.ProtagonistWeapon, data6.ProtagonistArmor, data6.ProtagonistShield, data6.ProtagonistHelmet);
                loadMariumData(data6.MariumWeapon, data6.MariumArmor, data6.MariumShield, data6.MariumHelmet);
                loadJohnData(data6.JohnWeapon, data6.JohnArmor, data6.JohnShield, data6.JohnHelmet);
                loadTuckerData(data6.TuckerWeapon, data6.TuckerArmor, data6.TuckerShield, data6.TuckerHelmet);
                break;
            case 10:
                continueSaveGame = db.ReadTable<Globals.ContinueGame>().ToList<Globals.ContinueGame>();
                Debug.Log("here............data count :: " + continueSaveGame.ToList().Count);
                Globals.ContinueGame data10 = continueSaveGame[0];
                loadWeaponData(data10.Dragger, data10.ShortSword, data10.ShortAxe, data10.Club, data10.ShortBow, data10.LongSword, data10.LongBow, data10.Mace, data10.Warhammer, data10.Spear, data10.LongAxe, data10.DoubleHeadedAxe, data10.Flair, data10.Maul);
                loadShieldData(data10.WoodenBuckler, data10.WoodenSmallRounded, data10.MetalBuckler, data10.MetalSmallRounded, data10.WoodenMediumShield, data10.MetalMediumShield, data10.WoodenKiteShield, data10.WoodenTowerShield, data10.MetalKiteShield);
                loadArmorData(data10.PaddedArmour, data10.HideArmour, data10.LeatherArmour, data10.BrigadineArmor, data10.ScaleArmour, data10.ChainArmour);
                loadHelmetData(data10.LeatherCap, data10.KettleHat, data10.NesalHelmet, data10.Aventail, data10.MailCoif);
                loadItemsData(data10.Ale, data10.CurePotion, data10.Food, data10.Meat, data10.HealPotion, data10.Rum, data10.SoulGem, data10.SoulEyeGems, data10.BarghestHeart, data10.Bones, data10.RottenFlesh);
                db.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
                loadProtagData(data10.ProtagonistWeapon, data10.ProtagonistArmor, data10.ProtagonistShield, data10.ProtagonistHelmet);
                loadMariumData(data10.MariumWeapon, data10.MariumArmor, data10.MariumShield, data10.MariumHelmet);
                loadJohnData(data10.JohnWeapon, data10.JohnArmor, data10.JohnShield, data10.JohnHelmet);
                loadTuckerData(data10.TuckerWeapon, data10.TuckerArmor, data10.TuckerShield, data10.TuckerHelmet);
                break;
        }
       
    }
    void loadWeaponData(int d, int ss, int sa,int c,int sb,int ls,int lb, int ma, int wh, int s, int la, int da,int f, int m)
    {
        Globals.shopMerchant.Dragger = d;
        Globals.shopMerchant.ShortSword = ss;
        Globals.shopMerchant.ShortAxe = sa;
        Globals.shopMerchant.Club = c;
        Globals.shopMerchant.ShortBow = sb;
        Globals.shopMerchant.LongSword = ls;
        Globals.shopMerchant.LongBow = lb;
        Globals.shopMerchant.Mace = ma;
        Globals.shopMerchant.Warhammer = wh;
        Globals.shopMerchant.Spear = s;
        Globals.shopMerchant.LongAxe = la;
        Globals.shopMerchant.DoubleHeadedAxe = da;
        Globals.shopMerchant.Flair = f;
        Globals.shopMerchant.Maul = m;
    }
    void loadShieldData(int wb, int wsr, int mb, int msr, int wms, int mms, int wks, int wts, int mks)
    {
        Globals.shopMerchant.WoodenBuckler = wb;
        Globals.shopMerchant.WoodenSmallRounded = wsr;
        Globals.shopMerchant.MetalBuckler = mb;
        Globals.shopMerchant.MetalSmallRounded = msr;
        Globals.shopMerchant.WoodenMediumShield = wms;
        Globals.shopMerchant.MetalMediumShield = mms;
        Globals.shopMerchant.WoodenKiteShield = wks;
        Globals.shopMerchant.WoodenTowerShield = wts;
        Globals.shopMerchant.MetalKiteShield = mks;
    }
    void loadArmorData(int pa, int ha, int la, int ba, int sa, int ca)
    {
        Globals.shopMerchant.PaddedArmour = pa;
        Globals.shopMerchant.HideArmour = ha;
        Globals.shopMerchant.LeatherArmour = la;
        Globals.shopMerchant.BrigadineArmor = ba;
        Globals.shopMerchant.ScaleArmour = sa;
        Globals.shopMerchant.ChainArmour = ca;
    }
    void loadHelmetData(int lc, int kh, int nh, int a, int mc)
    {
        Globals.shopMerchant.LeatherCap = lc;
        Globals.shopMerchant.KettleHat = kh;
        Globals.shopMerchant.NesalHelmet = nh;
        Globals.shopMerchant.Aventail = a;
        Globals.shopMerchant.MailCoif = mc;
    }

    void loadItemsData(int a, int cp, int f, int m, int hp, int rum, int sg, int syg, int bh, int bon, int rf)
    {
        Globals.shopMerchant.Ale = a;
        Globals.shopMerchant.CurePotion = cp;
        Globals.shopMerchant.Food = f;
        Globals.shopMerchant.Meat = m;
        Globals.shopMerchant.HealPotion = hp;
        Globals.shopMerchant.Rum = rum;
        Globals.shopMerchant.SoulGem = sg;
        Globals.shopMerchant.SoulEyeGems = syg;
        Globals.shopMerchant.BarghestHeart = bh;
        Globals.shopMerchant.Bones = bon;
        Globals.shopMerchant.RottenFlesh = rf;
    }
    void loadProtagData(string ProtagonistWeapon, string ProtagonistArmor, string ProtagonistShield, string ProtagonistHelmet)
    {
        Globals.inventoryProtagnist.AttackWeapon = ProtagonistWeapon;
        Globals.inventoryProtagnist.Armour = ProtagonistArmor;
        Globals.inventoryProtagnist.Helmet = ProtagonistHelmet;
        Globals.inventoryProtagnist.Shield = ProtagonistShield;
        SetWeaponProtag(ProtagonistWeapon);
        SetArmorProtag(ProtagonistArmor);
        SetHelmetProtag(ProtagonistHelmet);
        SetShieldProtag(ProtagonistShield);
        db.UpdateRecord<Globals.ProtagnistInventory>(Globals.inventoryProtagnist);
    }
    void SetWeaponProtag(string weapon)
    {
        switch (weapon)
        {
            case "Dragger":
                ProtagnistWeaponSetting(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "ShortSword":
                ProtagnistWeaponSetting(0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "ShortAxe":
                ProtagnistWeaponSetting(0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "Club":
                ProtagnistWeaponSetting(0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "shortBow":
               ProtagnistWeaponSetting(0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "longSword":
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "longBow":
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "Mace":
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "warHammer":
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "Spear":
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "longAxe":
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0);
                break;
            case "DoubleHeadedAxe":
                ProtagnistWeaponSetting(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0);
                break;

        }

    }
    void SetArmorProtag(string armor)
    {
        switch (armor)
        {
            case "Leahter":
                ArmourForProtagnist(1, 0, 0, 0, 0, 0);
                break;
            case "Brigadine":
               ArmourForProtagnist(0, 1, 0, 0, 0, 0);
                break;
            case "padded":
                ArmourForProtagnist(0, 0, 1, 0, 0, 0);
                break;
            case "Chainmail":
               ArmourForProtagnist(0, 0, 0, 1, 0, 0);
                break;
            case "Scale":
                ArmourForProtagnist(0, 0, 0, 0, 1, 0);
                break;
            case "Hide":
                ArmourForProtagnist(0, 0, 0, 0, 0, 1);
                break;
        }
    }
    void SetHelmetProtag(string helmet)
    {
        switch (helmet)
        {
            case "LeatherHelmet":
               HelmetForProtagnist(1, 0, 0, 0, 0);
                break;
            case "KettleHelmet":
                HelmetForProtagnist(0, 1, 0, 0, 0);
                break;
            case "NasalHelmet":
               HelmetForProtagnist(0, 0, 1, 0, 0);
                break;
            case "AvainTail":
               HelmetForProtagnist(0, 0, 0, 1, 0);
                break;
            case "MailCoif":
                HelmetForProtagnist(0, 0, 0, 0, 1);
                break;
        }
    }
    void SetShieldProtag(string shield)
    {
        switch (shield)
        {
            case "woodenBuckler":
                ShieldForProtagnist(1, 0, 0, 0, 0, 0);
                break;
            case "WoodenRound":
                ShieldForProtagnist(0, 1, 0, 0, 0, 0);
                break;
            case "WoodenRoundMed":
                ShieldForProtagnist(0, 0, 1, 0, 0, 0);
                break;
            case "MetalBuckler":
                ShieldForProtagnist(0, 0, 0, 1, 0, 0);
                break;
            case "MetalSmall":
                ShieldForProtagnist(0, 0, 0, 0, 1, 0);
                break;
            case "MetalMedium":
                ShieldForProtagnist(0, 0, 0, 0, 0, 1);
                break;
        }
    }
    void loadMariumData(string w, string a, string s, string h)
    {
        Globals.inventoryMarium.WeaponAttack = w;
        Globals.inventoryMarium.Armour = a;
        Globals.inventoryMarium.Helmet = h;
        Globals.inventoryMarium.Shield = s;
        SetWeaponMarium(w);
        SetArmorMarium(a);
        SetHelmetMarium(h);
        SetShieldMarium(s);
        db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
    }
    void SetWeaponMarium(string weapon)
    {
        switch (weapon)
        {
            case "ShortSword":
                WeaponForMarium(1, 0, 0, 0, 0, 0, 0);
                break;
            case "ShortAxe":
                WeaponForMarium(0, 1, 0, 0, 0, 0, 0);
                break;
            case "warHammer":
                WeaponForMarium(0, 0, 1, 0, 0, 0, 0);
                break;
            case "Spear":
                WeaponForMarium(0, 0, 0, 1, 0, 0, 0);
                break;
            case "shortBow":
                WeaponForMarium(0, 0, 0, 0, 1, 0, 0);
                break;
            case "longBow":
                WeaponForMarium(0, 0, 0, 0, 0, 1, 0);
                break;
            case "Dragger":
                WeaponForMarium(0, 0, 0, 0, 0, 0, 1);
                break;


        }

    }
    void SetArmorMarium(string armor)
    {
        switch (armor)
        {
            case "Leahter":
                ArmourForMarium(1, 0, 0, 0);
                break;
            case "Brigadine":
                ArmourForMarium(0, 1, 0, 0);
                break;
            case "padded":
                ArmourForMarium(0, 0, 1, 0);
                break;
            case "Hide":
                ArmourForMarium(0, 0, 0, 1);
                break;
        }
    }
    void SetHelmetMarium(string helmet)
    {
        switch (helmet)
        {
            case "LeatherHelmet":
                HelmetForMarium(1, 0, 0);
                break;
            case "KettleHelmet":
                HelmetForMarium(0, 1, 0);
                break;
            case "NasalHelmet":
                HelmetForMarium(0, 0, 1);
                break;
        }
    }
    void SetShieldMarium(string shield)
    {
        switch (shield)
        {
            case "woodenBuckler":
                ShieldForMarium(1, 0, 0, 0);
                break;
            case "WoodenRound":
                ShieldForMarium(0, 1, 0, 0);
                break;
            case "MetalBuckler":
                ShieldForMarium(0, 0, 1, 0);
                break;
            case "MetalSmall":
                ShieldForMarium(0, 0, 0, 1);
                break;
        }
    }
    void loadJohnData(string w, string a, string s, string h)
    {
        Globals.inventoryJohn.WeaponAttack = w;
        Globals.inventoryJohn.Armour = a;
        Globals.inventoryJohn.Helmet = h;
        Globals.inventoryJohn.Shield = s;
        SetWeaponJohn(w);
        SetArmorJohn(a);
        SetHelmetJohn(h);
        SetShieldJohn(s);
        db.UpdateRecord<Globals.JohnInventory>(Globals.inventoryJohn);
    }
    void SetWeaponJohn(string weapon)
    {
        switch (weapon)
        {
            case "ShortSword":
                WeaponForJohn(1, 0, 0, 0, 0, 0, 0, 0);
                break;
            case "ShortAxe":
                WeaponForJohn(0, 1, 0, 0, 0, 0, 0, 0);
                break;
            case "warHammer":
                WeaponForJohn(0, 0, 1, 0, 0, 0, 0, 0);
                break;
            case "longAxe":
                WeaponForJohn(0, 0, 0, 1, 0, 0, 0, 0);
                break;
            case "Spear":
                WeaponForJohn(0, 0, 0, 0, 1, 0, 0, 0);
                break;
            case "longSword":
                WeaponForJohn(0, 0, 0, 0, 0, 1, 0, 0);
                break;
            case "Dragger":
                WeaponForJohn(0, 0, 0, 0, 0, 0, 1, 0);
                break;
            case "Mace":
                WeaponForJohn(0, 0, 0, 0, 0, 0, 0, 1);
                break;


        }

    }
    void SetArmorJohn(string armor)
    {
        switch (armor)
        {
            case "Leahter":
                ArmourForJohn(1, 0, 0, 0, 0, 0);
                break;
            case "Brigadine":
                ArmourForJohn(0, 1, 0, 0, 0, 0);
                break;
            case "padded":
                ArmourForJohn(0, 0, 1, 0, 0, 0);
                break;
            case "Chainmail":
                ArmourForJohn(0, 0, 0, 1, 0, 0);
                break;
            case "Scale":
                ArmourForJohn(0, 0, 0, 0, 1, 0);
                break;
            case "Hide":
                ArmourForJohn(0, 0, 0, 0, 0, 1);
                break;
        }
    }
    void SetHelmetJohn(string helmet)
    {
        switch (helmet)
        {
            case "LeatherHelmet":
                HelmetForJohn(1, 0, 0, 0, 0);
                break;
            case "KettleHelmet":
                HelmetForJohn(0, 1, 0, 0, 0);
                break;
            case "NasalHelmet":
                HelmetForJohn(0, 0, 1, 0, 0);
                break;
            case "AvainTail":
                HelmetForJohn(0, 0, 0, 1, 0);
                break;
            case "MailCoif":
                HelmetForJohn(0, 0, 0, 0, 1);
                break;
        }
    }
    void SetShieldJohn(string shield)
    {
        switch (shield)
        {
            case "woodenBuckler":
                ShieldForJohn(1, 0, 0, 0, 0, 0);
                break;
            case "WoodenRound":
                ShieldForJohn(0, 1, 0, 0, 0, 0);
                break;
            case "WoodenRoundMed":
                ShieldForJohn(0, 0, 1, 0, 0, 0);
                break;
            case "MetalBuckler":
                ShieldForJohn(0, 0, 0, 1, 0, 0);
                break;
            case "MetalSmall":
                ShieldForJohn(0, 0, 0, 0, 1, 0);
                break;
            case "MetalMedium":
                ShieldForJohn(0, 0, 0, 0, 0, 1);
                break;
        }
    }
    void loadTuckerData(string w, string a, string s, string h)
    {
        Globals.inventoryTucker.WeaponAttack = w;
        Globals.inventoryTucker.Armour = a;
        Globals.inventoryTucker.Helmet = h;
        Globals.inventoryTucker.Shield = s;
        SetWeaponTucker(w);
        SetArmorTucker(a);
        SetHelmetTucker(h);
        SetShieldTucker(s);
        db.UpdateRecord<Globals.MariumInventory>(Globals.inventoryMarium);
    }
    void SetWeaponTucker(string weapon)
    {
        switch (weapon)
        {
            //case "ShortSword":
            //    WeaponForTucker(1, 0, 0, 0, 0, 0, 0);
            //    break;
            //case "ShortAxe":
            //    WeaponForMarium(0, 1, 0, 0, 0, 0, 0);
            //    break;
            case "warHammer":
                WeaponForTucker(1, 0, 0, 0, 0);
                break;
            case "Flair":
                WeaponForTucker(0, 1, 0, 0, 0);
                break;
            case "Maul":
                WeaponForTucker(0, 0, 1, 0, 0);
                break;
            case "Dragger":
                WeaponForTucker(0, 0, 0, 1, 0);
                break;
            case "Mace":
                WeaponForTucker(0, 0, 0, 0, 1);
                break;


        }

    }
    void SetArmorTucker(string armor)
    {
        switch (armor)
        {
            case "Leahter":
                ArmourForTucker(1, 0, 0);
                break;
            case "padded":
                ArmourForTucker(0, 1, 0);
                break;
            case "Hide":
                ArmourForTucker(0, 0, 1);
                break;
        }
    }
    void SetHelmetTucker(string helmet)
    {
        switch (helmet)
        {
            case "LeatherHelmet":
                HelmetForTucker(1, 0);
                break;
            case "KettleHelmet":
                HelmetForTucker(0, 1);
                break;
        }
    }
    void SetShieldTucker(string shield)
    {
        switch (shield)
        {
            case "woodenBuckler":
                ShieldForTucker(1, 0, 0, 0, 0);
                break;
            case "WoodenRound":
                ShieldForTucker(0, 1, 0, 0, 0);
                break;
            case "MetalBuckler":
                ShieldForTucker(0, 0, 1, 0, 0);
                break;
            case "MetalSmall":
                ShieldForTucker(0, 0, 0, 1, 0);
                break;
            case "MetalMedium":
                ShieldForTucker(0, 0, 0, 0, 1);
                break;
        }
    }
    public void ProtagnistWeaponSetting(int d, int s, int sa, int c, int sb, int ls, int lb, int m, int w, int sp, int la, int da, int f, int ma, int co, int cb, int ms)
    {
        Globals.inventoryProtagnist.Dragger = d;
        Globals.inventoryProtagnist.ShortSword = s;
        Globals.inventoryProtagnist.ShortAxe = sa;
        Globals.inventoryProtagnist.Club = c;
        Globals.inventoryProtagnist.ShortBow = sb;
        Globals.inventoryProtagnist.LongSword = ls;
        Globals.inventoryProtagnist.LongBow = lb;
        Globals.inventoryProtagnist.Mace = m;
        Globals.inventoryProtagnist.Warhammer = w;
        Globals.inventoryProtagnist.Spear = sp;
        Globals.inventoryProtagnist.LongAxe = la;
        Globals.inventoryProtagnist.DoubleHeadedAxe = da;
        Globals.inventoryProtagnist.Flair = f;
        Globals.inventoryProtagnist.Maul = ma;
        Globals.inventoryProtagnist.CompositeBow = co;
        Globals.inventoryProtagnist.CrossBow = cb;
        Globals.inventoryProtagnist.magicSword = ms;
    }
    void WeaponForJohn(int sSw, int sA, int w, int lA, int sp, int lSw, int d, int m)
    {
        Globals.inventoryJohn.ShortSword = sSw;
        Globals.inventoryJohn.ShortAxe = sA;
        Globals.inventoryJohn.Warhammer = w;
        Globals.inventoryJohn.LongAxe = lA;
        Globals.inventoryJohn.Spear = sp;
        Globals.inventoryJohn.LongSword = lSw;
        Globals.inventoryJohn.Dragger = d;
        Globals.inventoryJohn.Mace = m;
    }
    public void WeaponForMarium(int sSW, int sA, int w, int s, int sB, int lB, int d)
    {
        Globals.inventoryMarium.ShortSword = sSW;
        Globals.inventoryMarium.ShortAxe = sA;
        Globals.inventoryMarium.Warhammer = w;
        Globals.inventoryMarium.Spear = s;
        Globals.inventoryMarium.ShortBow = sB;
        Globals.inventoryMarium.LongBow = lB;
        Globals.inventoryMarium.Dragger = d;
    }
    void WeaponForTucker(int w, int f, int m, int d, int ma)
    {
        Globals.inventoryTucker.Warhammer = w;
        Globals.inventoryTucker.Flair = f;
        Globals.inventoryTucker.Maul = m;
        Globals.inventoryTucker.Dragger = d;
        Globals.inventoryTucker.Mace = ma;
    }
    public void ArmourForProtagnist(int l, int b, int p, int c, int s, int h)
    {
        Globals.inventoryProtagnist.LeatherArmour = l;
        Globals.inventoryProtagnist.BrigadineArmor = b;
        Globals.inventoryProtagnist.PaddedArmour = p;
        Globals.inventoryProtagnist.ChainArmour = c;
        Globals.inventoryProtagnist.ScaleArmour = s;
        Globals.inventoryProtagnist.HideArmour = h;
    }
    void ArmourForJohn(int l, int b, int p, int c, int s, int h)
    {
        Globals.inventoryJohn.LeatherArmour = l;
        Globals.inventoryJohn.BrigadineArmour = b;
        Globals.inventoryJohn.PaddedArmour = p;
        Globals.inventoryJohn.ChainArmour = c;
        Globals.inventoryJohn.ScaleArmour = s;
        Globals.inventoryJohn.HideArmour = h;
    }
    public void ArmourForMarium(int l, int b, int p, int h)
    {
        Globals.inventoryMarium.LeatherArmour = l;
        Globals.inventoryMarium.BrigadineArmour = b;
        Globals.inventoryMarium.PaddedArmour = p;
        Globals.inventoryMarium.HideArmour = h;
    }
    void ArmourForTucker(int l, int p, int h)
    {
        Globals.inventoryTucker.LeatherArmour = l;
        Globals.inventoryTucker.PaddedArmour = p;
        Globals.inventoryTucker.HideArmour = h;
    }
    public void HelmetForProtagnist(int l, int k, int n, int a, int m)
    {
        Globals.inventoryProtagnist.LeatherCap = l;
        Globals.inventoryProtagnist.KettleHat = k;
        Globals.inventoryProtagnist.NesalHelmet = n;
        Globals.inventoryProtagnist.Aventail = a;
        Globals.inventoryProtagnist.MailCoif = m;
    }
    void HelmetForJohn(int l, int k, int n, int a, int m)
    {
        //  Debug.Log("l::" + Globals.inventoryJohn.LeatherCap + " k::" + Globals.inventoryJohn.KettleHat + "  n::" + Globals.inventoryJohn.NasalHelmet);
        Globals.inventoryJohn.LeatherCap = l;
        Globals.inventoryJohn.KettleHat = k;
        Globals.inventoryJohn.NasalHelmet = n;
        Globals.inventoryJohn.Avaintail = a;
        Globals.inventoryJohn.MailCoif = m;
    }
    public void HelmetForMarium(int l, int k, int n)
    {
        Globals.inventoryMarium.LeatherCap = l;
        Globals.inventoryMarium.KettleHat = k;
        Globals.inventoryMarium.NasalHelmet = n;
    }
    void HelmetForTucker(int l, int k)
    {
        Globals.inventoryTucker.LeatherCap = l;
        Globals.inventoryTucker.KettleHat = k;
    }
    public void ShieldForProtagnist(int wB, int wS, int wM, int mB, int mS, int mM)
    {
        Globals.inventoryProtagnist.WoodenBuckler = wB;
        Globals.inventoryProtagnist.WoodenSmallRounded = wS;
        Globals.inventoryProtagnist.WoodenMediumShield = wM;
        Globals.inventoryProtagnist.MetalBuckler = mB;
        Globals.inventoryProtagnist.MetalSmallRounded = mS;
        Globals.inventoryProtagnist.MetalMediumShield = mM;
    }
    void ShieldForJohn(int wB, int wS, int wM, int mB, int mS, int mM)
    {
        Globals.inventoryJohn.WoodenBuckler = wB;
        Globals.inventoryJohn.WoodenSmallRound = wS;
        Globals.inventoryJohn.WoodenMedium = wM;
        Globals.inventoryJohn.metalBuckler = mB;
        Globals.inventoryJohn.metalSmallRound = mS;
        Globals.inventoryJohn.metalMedium = mM;
    }
    void ShieldForTucker(int wB, int wS, int mB, int mS, int mM)
    {
        Globals.inventoryTucker.WoodenBuckler = wB;
        Globals.inventoryTucker.WoodenSmall = wS;
        Globals.inventoryTucker.MetalBuckler = mB;
        Globals.inventoryTucker.MetalSmall = mS;
        Globals.inventoryTucker.MetalMedium = mM;
    }
    public void ShieldForMarium(int wB, int wS, int mB, int mS)
    {
        Globals.inventoryMarium.WoodenBuckler = wB;
        Globals.inventoryMarium.woodenSmall = wS;
        Globals.inventoryMarium.MetalBuckler = mB;
        Globals.inventoryMarium.MetalSmall = mS;
    }
}
