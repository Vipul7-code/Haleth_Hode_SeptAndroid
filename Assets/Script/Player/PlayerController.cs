using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using static Globals;
using HelthHolde;
public class PlayerController : Moveable
{
    public GameObject EnterPopUp, wrongPopUp, loadingScene,loadGamePopUp,leftControl,rightControl;
    DatabaseManager db;
    ChatInteraction chatinteraction;
    Rigidbody2D player;
    EntityGroup entity;
    public List<GameObject> charFriends = new List<GameObject>();
    private float moveSpeed, dirX, dirY;
    [HideInInspector]
    public string message, messageOnText, sceneName;
    public GameObject popUi, closeButton, companionPopUp;
    public textState TextType = textState.Start;
    MessageData messageData;
    Color initialColor, changedColor;
    int interactionId = 1,clickCount=1;
    public enum textState { Completed, Mid,Start }
   public bool isComplete,isSave,isCellar;
    Globals.userData user = new Globals.userData();
    Scene currentScene;
    public ObjectiveLibrary objectiveLibrary;
    Dictionary<Objective, ObjectiveConfiguration> objectives = new Dictionary<Objective, ObjectiveConfiguration>();

    public GameObject collideObject,fogOfWarObject;
    [SerializeField]
    Button load1, load2, load3, load4, load5, load6;
    [HideInInspector]
     public  bool isWalk;
    void Start()
    {
        db = FindObjectOfType<DatabaseManager>();
        player = GetComponent<Rigidbody2D>();
        entity = GetComponent<EntityGroup>();
        chatinteraction = GetComponent<ChatInteraction>();
        moveSpeed = 3f;
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        //if (sceneName == "World Map")
        //    fogOfWarObject.SetActive(true);
        if (sceneName != "BattleScene")
        {
            if (Globals.avatarState.ControlLevel == 0)
                ControlsSetting(false, true);
            else
                ControlsSetting(true, false);
        }
        Globals.playerController = this;
    }
    void ControlsSetting(bool l,bool r)
    {
        leftControl.SetActive(l);
        rightControl.SetActive(r);    
    }
 public void Respawn()
    {
        Debug.Log("respawn player");
        Destroy(Globals.levelManager.character);
        Globals.levelManager.SpawnPlayer(Globals.activePlayer);
    }
    #region
    #endregion
    /// <summary>
    /// UI buttons getting input 
    /// </summary>
    private void Update()
    {
        if (player != null && Globals.isShop)
        {
            RemoteWork();
            dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
            dirY = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;
        }
      
    }


    /// <summary>
    /// moving player 
    /// </summary>
    private void FixedUpdate()
    {
        player.velocity = new Vector2(dirX, dirY);
    }
    
    void RemoteWork()
    {
            if (dirY > 0)
            {
                entity.UpdateFace("back");
                PlayAnimation(1, false);
            }
            else if (dirY < 0)
            {
              entity.UpdateFace("front");
              PlayAnimation(1, false);
            }
            else if (dirX < 0)
            {
              entity.UpdateFace("left");
              PlayAnimation(1, false);
            }
            else if (dirX > 0)
            {
              entity.UpdateFace("right");
              PlayAnimation(1, false);
            }
            else
              PlayAnimation(0, false);
    }

    public void StopPlayer()
    {      
        PlayAnimation(0,false);
    }


    /// <summary>
    /// setting the animation from our remote control
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="attact"></param>
    public void PlayAnimation(float speed, bool attact)
    {        
        foreach (Transform child in player.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>()!=null)
            {
                child.GetComponent<Animator>().SetFloat("Speed", speed);      
            }
        }
    }
    GameObject par;
    private void OnCollisionEnter2D(Collision2D col)
    {
       // Debug.Log("name::" + col.gameObject.name + "  tag::" + col.gameObject.tag);
      //  Debug.Log("character..........??????" + Globals.tutorialPart.character);
        if (col.gameObject.tag == "downside" && this.tag == "Player")
        {
            Globals.sideName = "down";
            GameObject coll = col.transform.parent.gameObject;
            if (entity.controlPanel != null)
                entity.controlPanel.SetActive(false);
            Globals.ActiveFaces(coll, true, false, false, false);
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Globals.PlayNow = false;
            Globals.collideObject = col.gameObject;
            if (sceneName == "Blacksmith Shop_int_Huntsville")
                Globals.uiHandler.merchantPanel.SetActive(true);
            else
              Globals.enterPos = this.transform.localPosition;

            if ((Globals.activeScene == Globals.CurrentScene.Huntsville) || Globals.activeScene == Globals.CurrentScene.AtwaterVillage || activePart == "BarghestVill" || activePart == "DeathWeight" || activePart == "DeathWeightDengeon" || activePart == "BrigandVill" || Globals.activeScene == CurrentScene.CellarTucker)
                Globals.PlayNow = true;
            else if (Globals.activePart == "BarghestDengeon")
            {
                if (sceneName == "Barghest Lair-Dungeon")
                    Globals.barghestDengeon.DogsHound(col.gameObject);
            }
            else if (activePart == "BrigandLierDengeon")
            {
                if (sceneName == "Brigand Trail to Dungeon")
                    Globals.brigandLierDengeon.AttackOnPlayer();
                else
                    Globals.PlayNow = true;
            }
        }
        else if (col.gameObject.tag == "upside" && this.tag == "Player")
        {
            Globals.sideName = "up";
            GameObject coll = col.transform.parent.gameObject;
            if (entity.controlPanel != null)
                entity.controlPanel.SetActive(false);
            Globals.ActiveFaces(coll, false, true, false, false);
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Globals.PlayNow = false;
            Globals.collideObject = col.gameObject;
            Globals.enterPos = this.transform.localPosition;
            if ((Globals.activeScene == Globals.CurrentScene.Huntsville) || Globals.activeScene == Globals.CurrentScene.AtwaterVillage || activePart == "BarghestVill" || activePart == "DeathWeight" || activePart == "DeathWeightDengeon" || activePart == "BrigandVill" || Globals.activeScene == CurrentScene.CellarTucker)
                Globals.PlayNow = true;
            else if (Globals.activePart == "BarghestDengeon")
            {
                if (sceneName == "Barghest Lair-Dungeon")
                    Globals.barghestDengeon.DogsHound(col.gameObject);
            }
            else if (activePart == "BrigandLierDengeon")
            {
                if (sceneName == "Brigand Trail to Dungeon")
                    Globals.brigandLierDengeon.AttackOnPlayer();
                else
                    Globals.PlayNow = true;
            }

        }
        else if (col.gameObject.tag == "RSide" && this.tag == "Player")
        {
            Globals.sideName = "rSide";
            GameObject coll = col.transform.parent.gameObject;
            if (entity.controlPanel != null)
                entity.controlPanel.SetActive(false);
            Globals.ActiveFaces(coll, false, false, false, true);
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Globals.PlayNow = false;
            Globals.collideObject = col.gameObject;
            Globals.enterPos = this.transform.localPosition;
            if ((Globals.activeScene == Globals.CurrentScene.Huntsville) || Globals.activeScene == Globals.CurrentScene.AtwaterVillage || activePart == "BarghestVill" || activePart == "DeathWeight" || activePart == "DeathWeightDengeon" || activePart == "BrigandVill" || Globals.activeScene == CurrentScene.CellarTucker)
                Globals.PlayNow = true;
            else if (Globals.activePart == "BarghestDengeon")
            {
                if (sceneName == "Barghest Lair-Dungeon")
                    Globals.barghestDengeon.DogsHound(col.gameObject);
            }
            else if (activePart == "BrigandLierDengeon")
            {
                if (sceneName == "Brigand Trail to Dungeon")
                    Globals.brigandLierDengeon.AttackOnPlayer();
                else
                    Globals.PlayNow = true;
            }

        }
        else if (col.gameObject.tag == "LSide" && this.tag == "Player")
        {
            Globals.sideName = "lSide";
            GameObject coll = col.transform.parent.gameObject;
            entity.controlPanel.SetActive(false);
            Globals.ActiveFaces(coll, false, false, true, false);
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Globals.PlayNow = false;
          Globals.collideObject = col.gameObject;
            Globals.enterPos = this.transform.localPosition;
            if ((Globals.activeScene == Globals.CurrentScene.Huntsville) || Globals.activeScene == Globals.CurrentScene.AtwaterVillage || activePart == "BarghestVill" || activePart == "DeathWeight" || activePart == "DeathWeightDengeon" || activePart == "BrigandVill" || Globals.activeScene == CurrentScene.CellarTucker)
                Globals.PlayNow = true;
            else if (Globals.activePart == "BarghestDengeon")
            {
                if (sceneName == "Barghest Lair-Dungeon")
                    Globals.barghestDengeon.DogsHound(col.gameObject);
            }
            else if (activePart == "BrigandLierDengeon")
            {
                if (sceneName == "Brigand Trail to Dungeon")
                    Globals.brigandLierDengeon.AttackOnPlayer();
                else
                    Globals.PlayNow = true;
            }
        }
        if (col.gameObject.tag == "ChurchGate")//&& Globals.enterChurch && Globals.backToVill == false)
        {
            if (!Globals.completeIntro)
            {
                Globals.enterChurch = true;
                Globals.enterPos = this.transform.localPosition;
                if (Globals.activeScene == CurrentScene.HuntingtonVillage)
                    SceneManager.LoadSceneAsync("Huntington Chruch_int");
                else
                {
                    if (sceneName == "Huntsville")
                        SceneManager.LoadSceneAsync("Huntsville Chruch_int");
                    else
                    {
                        if (sceneName == "Atwater Village" && Globals.atWaterCount == 0)
                            return;
                        else
                            SceneManager.LoadSceneAsync("AtwaterChurch");
                    }
                }
            }
            else
                Globals.uiHandler.ClickOnButton("QuestLog");
        }
        else if (col.gameObject.tag == "obstacle")
        {
            Globals.ActiveControls(this.gameObject, false);
            if (sceneName == "Barghest Trail to Dungeon")
                Globals.barghestDengeon.ObstacleSetting();
            else if (sceneName == "DeathWight Trail to Dungeon")
                Globals.deathWeightDengeon.DeathWeightLiarSetting();
            else
            {
                Globals.InnVisit++;
                SceneManager.LoadSceneAsync("BattleScene");
            }
            Globals.enterPos = this.transform.localPosition;

        }
        else if (col.gameObject.tag == "ChurchConvo" && this.tag=="Player")
            Globals.churchHandler.ConvoStart();
        else if (col.gameObject.tag == "BackToTrail")
            SceneManager.LoadSceneAsync("Barghest Trail to Dungeon");
        else if (col.gameObject.tag == "EnterInn")
        {
            if (Globals.completeIntro)
            {
                if (Globals.conversationCount == 7 || Globals.conversationCount == 8)
                {
                    Globals.enterInn = true;
                    Globals.enterPos = this.transform.localPosition;
                    if (Globals.activeScene == CurrentScene.HuntingtonVillage)
                        SceneManager.LoadSceneAsync("Huntington_Inn_1stFloor");
                    else
                    {
                        if (sceneName == "Atwater Village" && Globals.atWaterCount == 0)
                            return;
                        else
                            SceneManager.LoadSceneAsync("Huntsville_Inn_1stFloor");
                    }
                }
                else
                    Globals.uiHandler.ClickOnButton("QuestLog");
            }
            else
            {
                Globals.enterInn = true;
                Globals.enterPos = this.transform.localPosition;
                if (Globals.activeScene == CurrentScene.HuntingtonVillage)
                    SceneManager.LoadSceneAsync("Huntington_Inn_1stFloor");
                else
                {
                    if (sceneName == "Atwater Village" && Globals.atWaterCount == 0)
                        return;
                    else
                        SceneManager.LoadSceneAsync("Huntsville_Inn_1stFloor");
                }
            }
        }
        else if(col.gameObject.tag=="Part1")
        {
            Globals.enterInn = true;
            Globals.isSecondInn = true;
            Globals.enterPos = this.transform.localPosition;
            SceneManager.LoadSceneAsync("Huntsville_Inn_1stFloor");
        }
        else if (col.gameObject.tag == "Finish")
        {
            Globals.enterInn = true;
            Globals.enterPos = this.transform.localPosition;
            SceneManager.LoadSceneAsync("Huntsville_Inn_1stFloor");
        }
        else if (col.gameObject.tag == "EnterMayor")
        {
            if (Globals.completeIntro)
            {
                if (Globals.conversationCount == 5 || Globals.conversationCount == 8)
                {
                    Globals.enterMayor = true;
                    Globals.enterPos = this.transform.localPosition;
                    SceneManager.LoadSceneAsync("Huntsville_Mayor_Int");
                }
                else
                    Globals.uiHandler.ClickOnButton("QuestLog");
            }
            else
            {
                Globals.enterMayor = true;
                Globals.enterPos = this.transform.localPosition;
                SceneManager.LoadSceneAsync("Huntsville_Mayor_Int");
            }

        }
        else if (col.gameObject.tag == "ShopCounter")
            shopCounter = true;
        else if (col.gameObject.tag == "EnterShop")
        {
            if (Globals.completeIntro)
            {
                if (Globals.conversationCount == 6 || Globals.conversationCount == 8)
                {
                    Globals.enterShop = true;
                    Globals.enterPos = this.transform.localPosition;
                    if (Globals.activeScene == CurrentScene.HuntingtonVillage)
                        SceneManager.LoadSceneAsync("MerchantShop_Int_Huntington");
                    else
                    {
                        if (sceneName == "Atwater Village")
                        {
                            if (atWaterCount == 0)
                                return;
                            else
                                SceneManager.LoadSceneAsync("AtwaterMerchantShop");
                        }
                        else
                            SceneManager.LoadSceneAsync("Huntsville_MerchantShop_Int");
                    }
                }
                else
                    Globals.uiHandler.ClickOnButton("QuestLog");

            }
            else
            {
                Globals.enterShop = true;
                Globals.enterPos = this.transform.localPosition;
                if (Globals.activeScene == CurrentScene.HuntingtonVillage)
                    SceneManager.LoadSceneAsync("MerchantShop_Int_Huntington");
                else
                {
                    if (sceneName == "Atwater Village")
                    {
                        if (Globals.atWaterCount == 0)
                            return;
                        else
                            SceneManager.LoadSceneAsync("AtwaterMerchantShop");
                    }
                    else
                        SceneManager.LoadSceneAsync("Huntsville_MerchantShop_Int");
                }
            }
        }
        else if (col.gameObject.tag == "SecondInn")
        {
            Globals.leavingsecondInn = true;
            if (Globals.activeScene == CurrentScene.HuntingtonVillage)
            {
                if(Globals.isSecondInn)
                   SceneManager.LoadSceneAsync("Huntsville_Inn_1stFloor");
                else
                    SceneManager.LoadSceneAsync("Huntington_Inn_1stFloor");
            }
            else
                SceneManager.LoadSceneAsync("Huntsville_Inn_1stFloor");
            Globals.isSecondInn = false;
        }
        else if (col.gameObject.tag == "EnterBlackSmit")
        {
            Debug.Log("second visit::" + Globals.secondVisit);
           // if (Globals.secondVisit != 0)
            {
                enterBlackSmith = true;
                Globals.enterPos = this.transform.localPosition;
                Debug.Log("enter pos::" + Globals.enterPos);
                SceneManager.LoadSceneAsync("Blacksmith Shop_int_Huntsville");
            }
        }
        else if (col.gameObject.tag == "EnterFarmhouse")
        {
            Globals.enterFarmhouse = true;
            Globals.enterPos = this.transform.localPosition;
            Debug.Log("enter farmhouse pos::" + Globals.enterPos);
            if (sceneName == "Atwater Village" && Globals.atWaterCount == 0)
                return;
            else
                SceneManager.LoadSceneAsync("Farm House Interior");
        }

        else if (col.gameObject.tag == "LeftChurch")
        {
            Globals.backToVill = true;
            Globals.secondVisit = 1;
            SceneManager.LoadSceneAsync(Globals.objectiveScene);
        }
        else if (col.gameObject.tag == "gate")
            Globals.motteBaileyCastle.GotoGate();
        else if (col.gameObject.tag == "ShopOpen")
        {
            if (!Globals.completeIntro)
                Globals.uiHandler.merchantPanel.SetActive(true);
            else
            {
                if (Globals.merchantVisit == 0)
                {
                    Debug.Log("character.........."+ FindObjectOfType<TutorialPart>().character);
                    Globals.ActiveControls(Globals.tutorialPart.character, false);
                    Globals.tutorialPart.CompleteMerchantDialo();
                }
                 
                else
                    Globals.uiHandler.merchantPanel.SetActive(true);
            }
        }
        else if (col.gameObject.tag == "MotteyBattle")
            Globals.motteBaileyCastle.StartMotteyBattle();
        else if (col.gameObject.tag == "EnterDengeon")
            SceneManager.LoadSceneAsync("Huntsville_Well_Dungeon_New");
        else if (col.gameObject.tag == "LeaveChurch")
        {
            if (sceneName != "Huntington_Inn_1stFloor")
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
            else
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
            Debug.Log("leave church::"+Globals.enterPos);
        }
        else if (col.gameObject.tag == "BarghestShop")
        {
            Globals.enterBarghestShop = true;
            Globals.enterPos = this.transform.localPosition;
            SceneManager.LoadSceneAsync("Berghest_MerchantShop_Potions");
        }
        else if (col.gameObject.tag == "leftCellar")
        {
            Globals.secondVisitMon = true;
            Globals.activeScene = Globals.CurrentScene.CellarInt;
            SceneManager.LoadSceneAsync("Monastery2ndFloor_int");
        }
        else if (col.gameObject.tag == "exitDengeon")
        {
            Globals.tutorialPart.StairsSoundPlay();
            SceneManager.LoadSceneAsync("Huntsville Chruch_int");
        }
        else if (col.gameObject.tag == "LeaveDeath")
        {
            if (Globals.deathWeight.leaveDeath)
            {
                Globals.isFirstCompleteStory = false;
                PopUpForLeave();
            }
            else
                SceneManager.LoadSceneAsync("BattleScene");
        }
        else if (col.gameObject.tag == "InnSecond")
        {
            Globals.enterPos = this.transform.localPosition;
            Globals.innSecond = true;
            if (sceneName == "Huntsville_Inn_1stFloor")
                SceneManager.LoadSceneAsync("Huntsville_Inn");
            else
                SceneManager.LoadSceneAsync("Huntington_Inn_second");
        }
        else if (col.gameObject.tag == "ChurchToVillage")
        {
            Globals.isHome = true;
            SceneManager.LoadSceneAsync(0);
        }
        else if (col.gameObject.tag == "CastleEscape")
        {
            if (sceneName == "Huntington_Inn_1stFloor")
                SceneManager.LoadSceneAsync("Huntington Castle Escape Tunnel");
        }
        else if (col.gameObject.tag == "LeaveSacredDengeon")//&& Globals.sacretDengeon.leaveDengeon)
        {
            Globals.againVisit = 1;
            Globals.storyCount = 0;
            Globals.activeScene = CurrentScene.SacredPlace;
            currentObjective = "Sacred Place";
            SceneManager.LoadSceneAsync("Sacred Place Exterior_New");
        }
        else if (col.gameObject.tag == Objective.Huntsville.ToString())
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Huntsville";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Soldier Campsite")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                if (Globals.soldierCampsiteVisit == 0)
                    Globals.objectiveScene = "Soldier Campsite";
                else
                    Globals.objectiveScene = "Second Soldier Campsite";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Huntington")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Huntington Town_Alley Scenes";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Wagon Caravan")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Wagon Carvan";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Monastery")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Monastery_ext";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "SecondSoldierCaravan")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Second Soldier Campsite";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Atwater Village")
        {

            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Atwater Village";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Sacred Place")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Sacred Place Exterior_New";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "EnteringSacredDeng")
        {
            Globals.enterPos = this.transform.localPosition;
            Globals.storyCount = 0;
            SceneManager.LoadSceneAsync("Sacred Place Interior");
        }
        else if (col.gameObject.tag == "Campsite")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Monk Campsite";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Motte and Bailey Castle")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Motte and Baley Castle";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "BarghestVillage")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Barghest Village";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "TheDeathWeight")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Death Wight Village";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "RandomAttack")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                par = col.gameObject.transform.parent.gameObject;
                Globals.isRandomAttack = true;
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "Brigand Village")
        {
            if (col.gameObject.tag == Globals.currentObjective.ToString())
            {
                Globals.objectiveScene = "Brigand Village";
                PopUpShow();
            }
            else
                AnotherPopUp();
        }
        else if (col.gameObject.tag == "startConversationSacret")
        {
            Globals.storyCount = 1;
            FindObjectOfType<GameManager>().OnConversationStart();
        }
        else if (col.gameObject.tag == "Shrine" && this.tag == "Player")
        {
            // if (!Globals.completeIntro)
            {
                if (!Globals.isShrine)
                {

                    if (!Globals.completeIntro)
                    {
                        isSave = true;
                        SaveState();
                    }
                    else
                    {
                        if (Globals.dengeonTreasure == 1)
                            Globals.tutorialPart.ChurchDialog();
                        else
                        {
                            //isSave = true;
                            //SaveState();
                        }
                    }
                }
            }
        }
        else if (col.gameObject.tag == "EditorOnly")
            col.gameObject.GetComponent<TreasureFunctionality>().TreatureOpen();
        else if (col.gameObject.tag == "side")
            col.gameObject.GetComponent<TreasureFunctionality>().TreasureSecond();
        else if (col.gameObject.tag == "Respawn")
            Globals.tutorialPart.TreasureDialogs();
        else if (col.gameObject.tag == "CellarInt")
        {
            Globals.activeScene = CurrentScene.CellarInt;
            currentObjective = "Monastary Crypt";
            objectiveScene = "Monastery2ndFloor_int";
            isCellar = true;
            PopUpShow();
        }
        else if (col.gameObject.tag == "CellarTucker")
        {

            Globals.activeScene = CurrentScene.CellarTucker;
            currentObjective = "Monastery";
            objectiveScene = "Monastery1stFloor_int";
            isCellar = true;
            PopUpShow();
        }
        else if (col.gameObject.tag == "LeftMonestry" || col.gameObject.tag == "LeftCampsite" || col.gameObject.tag == "LeftWagon" || col.gameObject.tag == "LeftHunsville")
        {
            isCellar = false;
            if (sceneName == "Atwater Village" && atWaterCount == 5)
                atWaterCount++;
            PopUpForLeave();
        }
        else if (col.gameObject.tag == "BarghestCave")
        {
            Debug.Log("active part" + activePart);
            Globals.conversationCount = 0;
            Globals.barghestAnim.Clear();
            Globals.lastRandom = "";
            Globals.isHound = false;
            Globals.isWolf = false;
            Globals.random = false;
            Globals.isPart1Battle = false;
            Globals.isFirstCompleteStory = false;
            if (activePart == "DeathWeightDengeon")
                SceneManager.LoadSceneAsync("Death WIght Lair");
            else if (activePart == "BrigandLierDengeon")
                SceneManager.LoadSceneAsync("Brigand Lair");
            else
            {
                if (sceneName == "Barghest Trail to Dungeon")
                    SceneManager.LoadSceneAsync("Barghest Lair-Dungeon");
                else if (sceneName == "Barghest Lair-Dungeon")
                {
                    Globals.secondVisitMon = true;
                    SceneManager.LoadSceneAsync("Barghest Trail to Dungeon");
                }
            }
        }
        else if (col.gameObject.tag == "LeaveBarghest")
        {
            if (Globals.againVisit == 0)
            {
                Globals.conversationCount = 0;
                objectiveScene = "Barghest Trail to Dungeon";
                SceneManager.LoadSceneAsync(objectiveScene);
            }
            else if (Globals.againVisit == 1)
            {
                Globals.isBarghest = false;
                PopUpForLeave();
            }
        }
        else if (col.gameObject.tag == "LeaveDeathVill")
        {
            Globals.conversationCount = 0;
            if (Globals.againVisit == 0)
            {
                Globals.deathWightCount = 1;
                SceneManager.LoadSceneAsync("DeathWight Trail to Dungeon");
            }
            else if (Globals.againVisit == 1)
            {
                Globals.isBarghest = false;
                Globals.lastRandom = "";
                PopUpForLeave();
            }
        }
        else if (col.gameObject.tag == "completeRound")
        {
            Globals.ActiveControls(this.gameObject, false);
            Globals.monestryTuckerJoin.StartOtherConversation();
        }
        else if (col.gameObject.tag == "LeaveBrigand")
        {
            if (Globals.againVisit == 0)
            {
                Globals.isPart1Battle = false;
                //  Globals.isSargent = false;
                Globals.barghestAnim.Clear();
                Globals.brigandCount = 1;
                SceneManager.LoadSceneAsync("Brigand Trail to Dungeon");
            }
            else if (Globals.againVisit == 1)
                PopUpForLeave();
        }
        else if (col.gameObject.tag == "fadeIn")
            Globals.tutorialPart.SeventhPart();
        else if (col.gameObject.tag == "LeaveCave")
            SceneManager.LoadSceneAsync("Barghest Trail to Dungeon");
        else if (col.gameObject.tag == "BackToBarghest")
        {
            Globals.currentObjective = "BarghestVillage";
            conversationCount = 0;
            Globals.againVisit = 1;
            Globals.storyCount = 0;
            SceneManager.LoadSceneAsync("Barghest Village");
        }
        else if (col.gameObject.tag == "BackToBrigand")
        {
            Globals.isFirstCompleteStory = false;
            conversationCount = 0;
            Globals.isPart1Battle = false;
            if (Globals.brigandCount == 1)
            {
                Globals.brigandCount = 2;
                if (!Globals.secondVisitMon)
                    SceneManager.LoadSceneAsync("Brigand Lair");
                else
                    SceneManager.LoadSceneAsync("Brigand Village");
            }
            else
            {
                Globals.secondVisitMon = true;
                Globals.brigandCount = 1;
                SceneManager.LoadSceneAsync("Brigand Trail to Dungeon");
            }
            Globals.againVisit = 1;
        }
        else if (col.gameObject.tag == "FightInMonestry")
        {
            Globals.sneakMon = true;
            Globals.monestryExt.turnSoldier();
        }
        else if (col.gameObject.tag == "checkPoint")
        {
            Globals.enterPos = this.transform.localPosition;
            Globals.isExploringTunnel = true;
            Globals.isTunnel = true;
            SceneManager.LoadSceneAsync("Huntington Caved-In Tunnel Entrance Interior");
        }
        else if (col.gameObject.tag == "EnterArmorer")
        {
            Globals.isArmorer = true;
            Globals.enterPos = this.transform.localPosition;
            SceneManager.LoadSceneAsync("Armour_Huntington");
        }
        else if (col.gameObject.tag == "LeftTucker" && this.tag == "Player")
        {
            Globals.atWaterCount = 6;
            // currentObjective = "RandomAttack";
            objectiveScene = "Monastery_ext";
            //  isCellar = true;
            secondVisitMon = true;
            if (col.gameObject.name == "mainDoor")
                Globals.monFrontDoor = true;
            PopUpShow();
        }
        else if (col.gameObject.tag == "GameController")
        {
            Globals.secondFight = false;
            Globals.huntingtonCastle.ThroneEntryDialog();
        }
        else if (col.gameObject.tag == "BackToDeath")
        {
            Globals.againVisit = 1;
            Globals.storyCount = 1;
            conversationCount = 0;
            Globals.isPart1Battle = false;
            if (sceneName == "DeathWight Trail to Dungeon")
            {
                Globals.isBarghest = false;
                if (Globals.secondVisitMon)
                {
                    SceneManager.LoadSceneAsync("Death Wight Village");
                    Globals.secondVisitMon = false;
                }
                else
                    SceneManager.LoadSceneAsync("Death WIght Lair");
            }
            else if (sceneName == "Death WIght Lair")
            {
                Globals.secondVisitMon = true;
                SceneManager.LoadSceneAsync("DeathWight Trail to Dungeon");
            }

        }
        else if (col.gameObject.tag == "castleTunnel")
        {
            isPart1Battle = false;
          //  SceneManager.LoadSceneAsync("Huntigton Castle Throne Room");
            SceneManager.LoadSceneAsync("Huntington Castle Interior");
        }
        else if (col.gameObject.tag == "DamageHuns")
        {
            Globals.isHome = true;
            SceneManager.LoadSceneAsync("World Map");
        }
        if(sceneName == "World Map")
        {
            if(currentObjective != "Soldier Campsite")
            {
                Debug.Log("world map but not soldier campsite");
                Globals.uiManager.soldier.SetActive(false);
                Globals.uiManager.soldierCol.SetActive(false);
                Globals.uiManager.secondSoldier.SetActive(false);
                Globals.uiManager.secondSoldierCol.SetActive(false);
            }
        }
    }
    void SettingOfCaravan()
    {
        if (par.name == "Caravans1" || par.name== "Caravan1")
            Globals.carvan1 = true;
        else if (par.name == "Caravans2" || par.name== "Caravan2")
            Globals.caravan2 = true;
        else if (par.name == "Caravans3"|| par.name== "Caravan3")
            Globals.caravan3 = true;
    }
    void SettingOfPetrols()
    {
        if (par.name == "Petrol1" || par.name == "Petrols1")
            Globals.petrol1 = true;
        else if (par.name == "Petrol2" || par.name == "Petrols2")
            Globals.petrol2 = true;
        else if (par.name == "Petrol3" || par.name== "Petrols3")
            Globals.petrol3 = true;
    }
    void ClearData()
    {
        Globals.playerState.companion1 = "";
        Globals.playerState.companion2 = "";
        Globals.playerState.companion3 = "";
        db.UpdateRecord<Globals.Player>(Globals.playerState);
    }
    public void SaveLoadGames(int count)
    {
        switch(count)
        {
            case 1:
                Globals.loadGame.Xp1 = Globals.avatarState.TotalXp;
                Globals.loadGame.SaveState1 = Globals.objectiveScene;
                Globals.loadGame.secondVisit1 = Globals.secondVisit;
                Globals.loadGame.SaveObjective1 = Globals.currentObjective;
                Globals.loadGame.savePlayer1 = Globals.avatarState.AvatarName;
                Globals.loadGame.companionCount1 = Globals.noOfCompanions;
                Globals.loadGame.gold1 = Globals.shopMerchant.Gold;
                Globals.loadGame.lastpos1x = Globals.lastPos.x;
                Globals.loadGame.lastpos1y = Globals.lastPos.y;
                Globals.loadGame.atwater1 = Globals.atWaterCount;
                SaveGameData(1);
                break;
            case 2:
                Globals.loadGame.Xp2 = Globals.avatarState.TotalXp;
                Globals.loadGame.SaveState2 = Globals.objectiveScene;
                Globals.loadGame.secondVisit2 = Globals.secondVisit;
                Globals.loadGame.SaveObjective2 = Globals.currentObjective;
                Globals.loadGame.savePlayer2 = Globals.avatarState.AvatarName;
                Globals.loadGame.companionCount2 = Globals.noOfCompanions;
                Globals.loadGame.gold2 = Globals.shopMerchant.Gold;
                Globals.loadGame.lastpos2x = Globals.lastPos.x;
                Globals.loadGame.lastpos2y = Globals.lastPos.y;
                Globals.loadGame.atwater2 = Globals.atWaterCount;
                SaveGameData(2);
                break;
            case 3:
                Globals.loadGame.Xp3 = Globals.avatarState.TotalXp;
                Globals.loadGame.SaveState3 = Globals.objectiveScene;
                Globals.loadGame.secondVisit3 = Globals.secondVisit;
                Globals.loadGame.SaveObjective3 = Globals.currentObjective;
                Globals.loadGame.savePlayer3 = Globals.avatarState.AvatarName;
                Globals.loadGame.companionCount3 = Globals.noOfCompanions;
                Globals.loadGame.gold3 = Globals.shopMerchant.Gold;
                Globals.loadGame.lastpos3x = Globals.lastPos.x;
                Globals.loadGame.lastpos3y = Globals.lastPos.y;
                Globals.loadGame.atwater3 = Globals.atWaterCount;
                SaveGameData(3);
                break;
            case 4:
                Globals.loadGame.Xp4 = Globals.avatarState.TotalXp;
                Globals.loadGame.SaveState4 = Globals.objectiveScene;
                Globals.loadGame.secondVisit4 = Globals.secondVisit;
                Globals.loadGame.SaveObjective4 = Globals.currentObjective;
                Globals.loadGame.savePlayer4 = Globals.avatarState.AvatarName;
                Globals.loadGame.companionCount4 = Globals.noOfCompanions;
                Globals.loadGame.gold4 = Globals.shopMerchant.Gold;
                Globals.loadGame.lastpos4x = Globals.lastPos.x;
                Globals.loadGame.lastpos4y = Globals.lastPos.y;
                Globals.loadGame.atwater4 = Globals.atWaterCount;
                SaveGameData(4);
                break;
            case 5:
                Globals.loadGame.Xp5 = Globals.avatarState.TotalXp;
                Globals.loadGame.SaveState5 = Globals.objectiveScene;
                Globals.loadGame.secondVisit5 = Globals.secondVisit;
                Globals.loadGame.SaveObjective5 = Globals.currentObjective;
                Globals.loadGame.savePlayer5 = Globals.avatarState.AvatarName;
                Globals.loadGame.companionCount5 = Globals.noOfCompanions;
                Globals.loadGame.gold5 = Globals.shopMerchant.Gold;
                Globals.loadGame.lastpos5x = Globals.lastPos.x;
                Globals.loadGame.lastpos5y = Globals.lastPos.y;
                Globals.loadGame.atwater5 = Globals.atWaterCount;
                SaveGameData(5);
                break;
            case 6:
                Globals.loadGame.Xp6 = Globals.avatarState.TotalXp;
                Globals.loadGame.SaveState6 = Globals.objectiveScene;
                Globals.loadGame.secondVisit6 = Globals.secondVisit;
                Globals.loadGame.SaveObjective6 = Globals.currentObjective;
                Globals.loadGame.savePlayer6 = Globals.avatarState.AvatarName;
                Globals.loadGame.companionCount6 = Globals.noOfCompanions;
                Globals.loadGame.gold6 = Globals.shopMerchant.Gold;
                Globals.loadGame.lastpos6x = Globals.lastPos.x;
                Globals.loadGame.lastpos6y = Globals.lastPos.y;
                Globals.loadGame.atwater6 = Globals.atWaterCount;
                SaveGameData(6);
                break;
        }
        db.UpdateRecord<Globals.LoadGame>(Globals.loadGame);
        loadGamePopUp.SetActive(false);
    }
    void PopUpShow()
    {

        Globals.isPart1Battle = false;
        if (currentObjective != "RandomAttack")
        {
            if(Globals.activeScene==CurrentScene.BarghestVillage || Globals.activeScene==CurrentScene.TheDeathWeight)
                EnterMessage("Do you want to enter the Mysterious Village?");
            else
              EnterMessage("Do you want to enter " + Globals.currentObjective + " ?");
        }
        else
        {
            if (par.name == "Caravan1" || par.name == "Caravan2"||par.name== "Caravan3" || par.name == "Caravans1" || par.name == "Caravans2" || par.name == "Caravans3")
            {
                if (atWaterCount < 6)
                    EnterMessage("Do you want to enter Atwater Enemy Caravan?");
                else
                    EnterMessage("Do you want to enter Motte and Bailey Castle Enemy Caravan?");
            }
            else
            {
                if (atWaterCount < 6)
                    EnterMessage("Do you want to enter Atwater Enemy Petrol?");
                else
                    EnterMessage("Do you want to enter Motte and Bailey Castle Enemy Petrol?");
            }
        }
    }
   public void PopUpForLeave()
    {
        if(currentObjective == "TheDeathWeight")
        {
            EnterMessage("Do you want to leave Death Weight" + " and enter the world map? ");
        }
        else if (currentObjective != "RandomAttack")
            EnterMessage("Do you want to leave " + Globals.currentObjective + " and enter the world map? ");
        else
            EnterMessage("Do you want to exit to the World Map?");
    }
    void SaveState()
    {
        if (sceneName == "Huntsville Chruch_int" || sceneName == "Huntington Chruch_int")
            EnterMessage("Do you want to pray?");
        else
            EnterMessage("Do you want to save your state?");
    }
    void EnterMessage(string msg)
    {
        EnterPopUp.SetActive(true);
        if (isSave)
            ActiveControls(false);
         else
            GetComponent<EntityGroup>().controlPanel.SetActive(false);
        EnterPopUp.transform.GetChild(0).GetComponent<Text>().text = msg;
    }
    void ActiveControls(bool set)
    {
        GetComponent<EntityGroup>().controlPanel.SetActive(set);
    }
   void AnotherPopUp()
    {
        wrongPopUp.SetActive(true);
        GetComponent<EntityGroup>().controlPanel.SetActive(false);
        if (currentObjective != "RandomAttack")
            wrongPopUp.transform.GetChild(0).GetComponent<Text>().text = "Please complete " + Globals.currentObjective + " first to enter this location";
        else
            wrongPopUp.transform.GetChild(0).GetComponent<Text>().text = "Please destroy 2 Caravans and 3 Petrols first";
    }
    public void Yes()
    {

        EnterPopUp.SetActive(false);
        if (!isSave)
        {
            loadingScene.SetActive(true);
            if (sceneName == "World Map" ||isCellar)
            {
                if (Globals.isRandomAttack)
                {
                    Debug.Log("is random attack...................");
                    if (par.name == "Caravan1" || par.name == "Caravan2"|| par.name== "Caravan3" || par.name == "Caravans1" || par.name == "Caravans2" || par.name == "Caravans3")
                    {
                        activeRandom = CurrentRandom.caravans;
                        lastPos = this.transform.localPosition;
                        Globals.objectiveScene = "Caravan";
                        Globals.caravanName.Add(par.name);
                        SettingOfCaravan();
                    }
                    else
                    {
                        activeRandom = CurrentRandom.petrols;
                        lastPos = this.transform.localPosition;
                        Globals.objectiveScene = "Petrols";
                        Globals.petrolsName.Add(par.name);
                        SettingOfPetrols();
                    }
                }
                else
                {
                    if (isCellar && Globals.activeScene == CurrentScene.monastery)
                        Globals.monestryExt.openGate.Play();
                    if (activeScene != CurrentScene.CellarInt && activeScene != CurrentScene.CellarTucker)
                        Globals.lastPos = this.transform.localPosition;
                }
                if (Globals.activeScene == CurrentScene.TheDeathWeight || Globals.activeScene == CurrentScene.BarghestVillage)
                    loadingScene.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "Loading Mysterious Village";
                else
                {
                    Globals.barghestAnim.Clear();
                    if(!Globals.isRandomAttack)
                      loadingScene.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "Loading " + Globals.currentObjective + ".............";
                }
                SceneManager.LoadSceneAsync(Globals.objectiveScene);
            }
            else
            {
                Globals.carvan1 = false;
                Globals.caravan2 = false;
                Globals.caravan3 = false;
                Globals.petrol1 = false;
                Globals.petrol2 = false;
                Globals.petrol3 = false;

                Globals.isHome = true;
                loadingScene.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "Loading world map " + "............";
                SetObjectives();
                Globals.againVisit = 0;
                Globals.conversationCount = 0;
                SceneManager.LoadSceneAsync("World Map");
            }
        }
        else
        {
            Debug.Log("objective scene :: "+Globals.objectiveScene + " current objective :: "+ Globals.currentObjective+" second visit :: "+ Globals.secondVisit);
            ReadLoadDB();
            Globals.avatarState.SaveState = Globals.objectiveScene;
            Globals.avatarState.SaveObjective = Globals.currentObjective;
            Globals.avatarState.secondVisit = Globals.secondVisit;
            Globals.avatarState.companionCount = Globals.noOfCompanions;
            Globals.uiHandler.SaveContinueGameData();
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
            ActiveControls(true);
            isSave = false;
            Globals.isShrine = true;
            loadGamePopUp.SetActive(true);
        }
    }
    Color32 loadColor = new Color32(171, 109, 109, 255);
    void ReadLoadDB()
    {
        if (Globals.loadGame.SaveObjective1 != "")
            load1.GetComponent<Image>().color = loadColor;
        if (Globals.loadGame.SaveObjective2 != "")
            load2.GetComponent<Image>().color = loadColor;
        if (Globals.loadGame.SaveObjective3 != "")
            load3.GetComponent<Image>().color = loadColor;
        if (Globals.loadGame.SaveObjective4 != "")
            load4.GetComponent<Image>().color = loadColor;
        if (Globals.loadGame.SaveObjective5 != "")
            load5.GetComponent<Image>().color = loadColor;
        if (Globals.loadGame.SaveObjective6 != "")
            load6.GetComponent<Image>().color = loadColor;

    }
   void SetObjectives()
    {
        switch(currentObjective)
        {
            case "Soldier Campsite":
                Debug.Log("set objective :: soldier campsite");
                previosObjective = null;
                Globals.isLightening = false;
                Globals.afterPromotion = false;
                if (Globals.soldierCampsiteVisit == 0)
                {
                    currentObjective = "Wagon Caravan";
                    activeScene = CurrentScene.WagonCaravan;
                }
                else
                {
                    secondVisit = 0;
                    currentObjective = "Huntsville";  //TheDeathWeight
                    activeScene = CurrentScene.Huntsville;  //TheDeathWeight
                }
                break;
            case "Wagon Caravan":
                currentObjective = "Soldier Campsite";
                previosObjective = null;
                Globals.isLightening = false;
                Globals.afterPromotion = false;
                Globals.soldierCampsiteVisit = 1;
                activeScene = CurrentScene.SoldierCampsite;
                break;
            case "SecondSoldierCaravan":
                secondVisit = 0;
               
                previosObjective = null;
                Globals.isLightening = false;
                Globals.afterPromotion = false;
                break;
            case "Huntsville":
                previosObjective = "Huntsville";
                
                if (secondVisit == 0)
                {
                    Globals.InnVisit = 0;
                    Globals.isLightening = false;
                    Globals.afterPromotion = false;
                    currentObjective = "Atwater Village";
                    activeScene = CurrentScene.AtwaterVillage;
                }
                else if(secondVisit == 1)
                {
                    currentObjective = "Campsite";
                    Globals.isLightening = true;
                    Globals.afterPromotion = false;
                    activeScene = CurrentScene.MonkCampsite;
                }
                else if(secondVisit==2)
                {
                    currentObjective = "BarghestVillage";
                    Globals.isLightening = true;
                    Globals.afterPromotion = true;
                    activeScene = CurrentScene.BarghestVillage;
                }
                break;
            case "Atwater Village":
                previosObjective = "Atwater Village";
                Globals.afterPromotion = false;
                if (atWaterCount == 0)
                {
                    currentObjective = "RandomAttack";
                    Globals.isLightening = false;
                    activeScene = CurrentScene.RandomAttack;
                }
                else
                {
                    caravnCount = 0;
                    petrolCount = 0;
                    conversationCount = 0;
                    currentObjective = "Sacred Place";
                    Globals.isLightening = false;
                    activeScene = CurrentScene.SacredPlace;
                }
                Globals.InnVisit = 0;
                break;
            case "RandomAttack":
                Globals.afterPromotion = false;
                if (atWaterCount == 5)
                {
                    Globals.sargentKill = false;
                    Globals.conversationCount = 0;
                    Globals.InnVisit = 0;
                    Globals.isRandomAttack = false;
                    Globals.isLightening = false;
                    previosObjective = null;
                    currentObjective = "Atwater Village";
                    activeScene = CurrentScene.AtwaterVillage;
                }
                else if (atWaterCount < 12 && atWaterCount >= 6)
                    previosObjective = "Motte and Bailey Castle";
                else if (atWaterCount == 12)
                {
                    caravnCount = 0;
                    petrolCount = 0;
                    Globals.isRandomAttack = false;
                    previosObjective = null;
                    Globals.isLightening = true;
                    currentObjective = "Motte and Bailey Castle";
                    activeScene = CurrentScene.MotteAndBaileyCastle;
                }
                break;
            case "Sacred Place":
                secondVisit = 1;
                Globals.caravanName.Clear();
                Globals.petrolsName.Clear();
                Globals.isLightening = true;
                Globals.afterPromotion = false;
                previosObjective = null;
                currentObjective = "Huntsville";
                activeScene = CurrentScene.Huntsville;
                break;
            case "Campsite":
                previosObjective = null;
                Globals.isLightening = true;
                Globals.afterPromotion = false;
                currentObjective = "Monastery";
                activeScene = CurrentScene.monastery;
                break;
            case "Monastery":
                previosObjective = null;
                Globals.isLightening = true;
                Globals.afterPromotion = false;
                previosObjective = "Monastery";
                currentObjective = "RandomAttack";
                activeScene = CurrentScene.RandomAttack;
                break;
            case "CellarInt":
                previosObjective = null;
                currentObjective = "CellarTucker";
                Globals.afterPromotion = false;
                activeScene = CurrentScene.CellarTucker;
                break;
            case "CellarTucker":
                secondVisit = 1;
                previosObjective = null;
                Globals.isLightening = true;
                Globals.afterPromotion = false;
                currentObjective = "Monastery";
                activeScene = CurrentScene.monastery;
                break;
            case "Motte and Bailey Castle":
                Globals.secondVisitMon = false;
                secondVisit = 2;
                previosObjective = null;
                Globals.isLightening = true;
                Globals.afterPromotion = false;
                currentObjective = "Huntsville";
                activeScene = CurrentScene.Huntsville;
                break;
            case "BarghestVillage":
                conversationCount = 0;
                previosObjective = null;
                Globals.afterPromotion = true;
                Globals.isLightening = true;
                currentObjective = "TheDeathWeight";
                activeScene = CurrentScene.TheDeathWeight;
                break;
            case "TheDeathWeight":
                conversationCount = 0;
                previosObjective = null;
                Globals.afterPromotion = true;
                Globals.isLightening = true;
                currentObjective = "Brigand Village";
                activeScene = CurrentScene.TheBrigand;
                break;
            case "Brigand Village":
                previosObjective = null;
                Globals.afterPromotion = true;
                Globals.isLightening = true;
                currentObjective = "Huntington";
                activeScene = CurrentScene.HuntingtonVillage;
                break;
            case "Huntington":
                previosObjective = null;
                Globals.afterPromotion = true;
                Globals.isLightening = true;
                currentObjective = "Soldier Campsite";
                activeScene = CurrentScene.SoldierCampsite;
                Globals.soldierCampsiteVisit = 0;
                break;
        }
        Debug.Log("current here" + Globals.currentObjective);
     
    }
    public void No()
    {
       EnterPopUp.SetActive(false);
       GetComponent<EntityGroup>().controlPanel.SetActive(true);
    }
    public void Okay()
    {
      wrongPopUp.SetActive(false);
      GetComponent<EntityGroup>().controlPanel.SetActive(true);
    }
    void UpdateLibraty(GameObject _player,DialogData library)
    {
        _player.GetComponent<ChatInteraction>().dialogLibrary = library;
    }
    public void myPlayerChat(string message)
    {
        popUi.SetActive(true);
        Text playerText = popUi.transform.GetChild(0).GetComponent<Text>();
        Image pic = popUi.transform.GetChild(1).GetComponent<Image>();
        popUi.transform.GetChild(1).GetComponent<Image>().sprite = Globals.message.dialogData.playerData.characterImage;
        playerText.text = "";
        playerText.text = message;
        if (Globals.message.dialogData.playerType != PlayerType.Player)
            pic.sprite = Globals.message.dialogData.pic;
        popUi.transform.GetChild(0).GetComponent<TextTyper>().StartWords();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "completeRound")
            isComplete = true;
        else if (collision.gameObject.tag == "SecludedArea")
        {
            if (sceneName == "Huntsville_Well_Dungeon")
                Globals.tutorialPart.OnDoorOpen();
            else
                Globals.controller.door.Play();
        }
        else if (collision.gameObject.tag == "randomConversation")
        {
            if (sceneName == "Monastery1stFloor_int")
                Globals.monestryTuckerJoin.StartConversation();
            else
                FindObjectOfType<RandomEncounters>().StartConversation();
        }
        else if (collision.gameObject.tag == "obstacle")
        {
            if (activeScene == CurrentScene.HuntingtonVillage)
            {
                if (sceneName == "Huntington_Inn_1stFloor")
                {
                    if (Globals.completeEfforts)
                    {
                        Globals.innController.clickCount = 1;
                        Globals.innController.SecondSetting();
                    }
                }
                else if (sceneName == "MerchantShop_Int_Huntington" && Globals.completeEfforts)
                {
                    Globals.uiHandler.merchantPanel.SetActive(true);
                    Globals.ActiveControls(this.gameObject, false);
                }
                else
                    Globals.huntingtonChurch.DialogSetUp();
            }
            else
            {
                Globals.enterPos = this.transform.localPosition;
                if (sceneName == "Death WIght Lair")
                    Globals.deathWeightDengeon.AttackOnPlayer();
                else if (sceneName == "Brigand Trail to Dungeon" || sceneName == "Brigand Lair")
                    Globals.brigandLierDengeon.AttackOnPlayer();
                else
                    Globals.barghestDengeon.AttackOnPlayer();
            }
        }
        else if (collision.gameObject.tag == "Part1")
        {
            Globals.enterPos = this.transform.localPosition;
            if (sceneName == "Huntington_Inn_1stFloor")
            {
                Globals.innController.clickCount = 2;
                Globals.innController.ThirdSetting();
            }
            else
                Globals.castleTunnel.Attaintion();
        }
        else if (collision.gameObject.tag == "Door1")
               Globals.tutorialPart.ChurchSetting();
        else if (collision.gameObject.tag == "BarghestCave")
            FindObjectOfType<HuntingtonController>().DialogPart();
        else if (collision.gameObject.tag == "Caravan1")
            Globals.huntingtonVill.SecondCutScene();
        else if (collision.gameObject.tag == "checkPoint")
            Globals.huntingtonVill.ThirdCutScene();
        else if (collision.gameObject.tag == "Hunsville")
            Globals.huntingtonVill.ForthCutScene();
        else if (collision.gameObject.tag == "ThroneRoom")
        {
            CommonPart();
            SceneManager.LoadSceneAsync("Huntigton Castle Throne Room");
        }
        else if (collision.gameObject.tag == "leavingThrone")
        {
            CommonPart();
            Globals.leavingThrone = true;
            SceneManager.LoadSceneAsync("Huntington Castle Interior");
        }
        else if (collision.gameObject.tag == "huntingtonCourtyard")
        {
            Globals.ActiveControls(this.gameObject, false);
            SceneManager.LoadSceneAsync("Huntington Town_Alley Scenes");
        }
        else if (collision.gameObject.tag == "gate")
            FindObjectOfType<HuntingtonThroneRoom>().ThronDialogs();
        else if (collision.gameObject.tag == "fadeIn")
        {
            if (sceneName == "Huntsville_Well_Dungeon")
                Globals.tutorialPart.BattleStart();
        }
        else if (collision.gameObject.tag == "Inn")
            Globals.tutorialPart.CompleteMayorDialog();

    }
    void CommonPart()
    {
        loadingScene.SetActive(true);
        Globals.secondFight = false;
        Globals.isPart1Battle = false;
    }

    public void SaveGameData(int save)
    {
        Globals.loadData = false;
        switch(save)
        {
            case 1:
                Globals.SaveGame1 saveGame = new Globals.SaveGame1();
                db.UpdateRecord<Globals.SaveGame1>(saveGame);
                break;
            case 2:
                Globals.SaveGame2 saveGame2 = new Globals.SaveGame2();
                db.UpdateRecord<Globals.SaveGame2>(saveGame2);
                break;
            case 3:
                Globals.SaveGame3 saveGame3 = new Globals.SaveGame3();
                db.UpdateRecord<Globals.SaveGame3>(saveGame3);
                break;
            case 4:
                Globals.SaveGame4 saveGame4 = new Globals.SaveGame4();
                db.UpdateRecord<Globals.SaveGame4>(saveGame4);
                break;
            case 5:
                Globals.SaveGame5 saveGame5 = new Globals.SaveGame5();
                db.UpdateRecord<Globals.SaveGame5>(saveGame5);
                break;
            case 6:
                Globals.SaveGame6 saveGame6 = new Globals.SaveGame6();
                db.UpdateRecord<Globals.SaveGame6>(saveGame6);
                break;

        }
    }
}

