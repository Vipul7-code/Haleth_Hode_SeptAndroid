using HelthHolde;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;
using UnityEngine.UI;
public class MotteAndBaileyCastle : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, lordAlfred, sargent,marium,john,tucker,soldier;
    [SerializeField]
    Transform playerPos, SargentPos,sargentNewPos, lordAlfredPos,enemyNew,playerFinalPos,playerinitialPos,afterMottey,playerNewPos,mariumNewPos,johnNewPos,tuckerNewPos,lordAlfredNewPos;
    [SerializeField]
    Camera mainCamera;
    int  dialogCount,specialCount;
    List<GameObject> soldeirs = new List<GameObject>();
    [SerializeField]
   GameObject dialogBox,worldMap,spineChar;
    [SerializeField]
    PlayableDirector playble;
    [SerializeField]
    AudioSource audio;
    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject[] boundaries,hideItems,shieldWall;
    bool isRun;
    float speed = 1f;

    [SerializeField]
    Transform[] soldierPos,supportingGuardPos;
    [SerializeField]
    Transform[] soldierPos2;
    [SerializeField]
    Transform[] soldierPos3;
    [SerializeField]
    GameObject firstGate, secondGate,thirdGate,battlePoint;

    [SerializeField]
    GameObject newPlayble;

    [SerializeField]
    GameObject[] guards;
    bool isRetreat,isRetreat1;
    [SerializeField]
    GameObject canvasGroup;
    GameObject alf;
    // Start is called before the first frame update
    void Start()
    {
        Globals.isEnemyTeam = false;
        Globals.isMyTeam = false;
        Globals.isShop = true;
        Globals.conversationCount = 0;
        Globals.motteBaileyCastle = this;
        Globals.activePart = "motteBailey";
        if (Globals.mbStart)
        {
            Debug.Log("trueeeeeeeeee");
            playble.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("false......");
            playble.transform.parent.gameObject.SetActive(false);
        }
 
        if (!Globals.isSpecial)
            SpawnCharacters();
    }
  void SpawnCharacters()
    {
    
        Debug.Log("spawn character;;;;;;;;;;;;;;");
       Globals.secondVisit = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        Debug.Log("second::" + Globals.secondFight + " third ::" + Globals.thirdFight + " forth::" + Globals.forthFight + "  before mottye::" + Globals.beforeMottey);
        if (Globals.isPart1Battle)
        {
            if (Globals.secondFight)
            {
                playble.transform.parent.gameObject.SetActive(true);
                Debug.Log("111111111111111111111111");
                GateSetting(false, false, true);
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_M_3") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_F_3") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_M_3") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_F_3") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_M_3") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_F_3") as TimelineAsset;
                character = Instantiate(Globals.activePlayer, playerNewPos.position, Quaternion.identity);
                playble.Play();
                Globals.ActiveControls(character, false);
            }
            else if (Globals.thirdFight)
            {
                GateSetting(false, false, true);
                if (this.transform.parent.name == "Motte&Baley_cinematic")
                {
                    Debug.Log("222222222222222");
                    playble.transform.parent.gameObject.SetActive(true);
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Motte&Bailey_Castle_Flee_LordAlfred") as TimelineAsset;
                    this.transform.parent.gameObject.SetActive(true);
                    if (playble.isActiveAndEnabled)
                    {
                        playble.Play();
                    }
                    else
                        Debug.Log("not active::");
                  
                }
                else
                {
                    Debug.Log("3333333333333333333");
                    character = Instantiate(Globals.activePlayer, playerNewPos.position, Quaternion.identity);
                    mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
                    mainCamera.orthographicSize = 5;
                    mainCamera.nearClipPlane = 0.3f;
                    mainCamera.farClipPlane = 1000;
                    mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
                    mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
                    Globals.ActiveControls(character, true);
                }
            }
            else if (Globals.forthFight)
            {
                Debug.Log("44444444444444444");
                Globals.isSpecial = false;
                Globals.first = false;
                GateSetting(false, false, false);
                playble.transform.parent.gameObject.SetActive(true);
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_M_Motte&Bailey_Castle_Last_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_F_Motte&Bailey_Castle_Last_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_M_Motte&Bailey_Castle_Last_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_F_Motte&Bailey_Castle_Last_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_M_Motte&Bailey_Castle_Last_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_F_Motte&Bailey_Castle_Last_CutScene") as TimelineAsset;
                playble.Play();
            }
            else
            {
                if (!Globals.beforeMottey)
                {
                    if (Globals.isMotteyRetreat && !Globals.isRetreate)
                    {
                        Debug.Log("555555555555555555");
                        playble.transform.parent.gameObject.SetActive(true);
                        playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Motte&Bailey_Castle_FleeToCourtyard") as TimelineAsset;
                        playble.Play();
                        isRetreat1 = true;
                        Globals.mbStart = true;
                    }
                    else if (Globals.isRetreate && Globals.isMotteyRetreat)
                    {
                        Debug.Log("6666666666666666");
                        character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
                        mainCamera.orthographicSize = 5;
                        mainCamera.nearClipPlane = 0.3f;
                        mainCamera.farClipPlane = 1000;
                        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
                        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
                        Globals.ActiveControls(character, true);
                        firstGate.SetActive(false);
                        spineChar.SetActive(false);
                      
                    }
                    else if(Globals.isShieldWall)
                    {
                        Debug.Log("777777777777");
                        playble.transform.parent.gameObject.SetActive(true);
                        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
                        FindObjectOfType<BaileyManager>().p1.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Motte and Bailey/Motte&Bailey_Castle_ShieldWall_CutScene") as TimelineAsset;
                        FindObjectOfType<BaileyManager>().p1.transform.GetChild(0).GetComponent<PlayableDirector>().Play();
                        foreach(var v in guards)
                        {
                            v.SetActive(true);
                        }
                        isRetreat1 = false;
                        isRetreat = false;
                    }
                }
                else
                {
                    Debug.Log("88888888888888888");
                    battlePoint.SetActive(false);
                    character = Instantiate(Globals.activePlayer, afterMottey.position, Quaternion.identity);
                    mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
                    mainCamera.orthographicSize = 5;
                    mainCamera.nearClipPlane = 0.3f;
                    mainCamera.farClipPlane = 1000;
                    mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
                    mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
                    Globals.ActiveControls(character, true);
                }
            }
            if (!isRetreat && !Globals.isMotteyRetreat && !Globals.isShieldWall && !Globals.thirdFight)
                SecondWave();
        }
        else
        {
            Debug.Log("9999999999999999999");
            Globals.waveCount = 0;
            character = Instantiate(Globals.activePlayer, playerinitialPos.position, Quaternion.identity);
            Globals.ActiveControls(character, true);
            GameObject sar = Resources.Load(("Enemy/SargentAtArms"), typeof(GameObject)) as GameObject;
           
        }
        if (!Globals.forthFight && !isRetreat && !Globals.isMotteyRetreat && !Globals.isShieldWall && !Globals.thirdFight)
        {
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            Debug.Log("10101010101010"+ character.transform.position+  "camera pos :  "+mainCamera.transform.position);
            mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
            mainCamera.orthographicSize = 5;
            mainCamera.nearClipPlane = 0.3f;
            mainCamera.farClipPlane = 1000;
            mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
            mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
            Globals.ActiveFaces(character, false, false, false, true);
            character.tag = "Player";
            Debug.Log("camera pos :  after" + mainCamera.transform.position);
            SpawnSoldier();
        
        } 
        if(!Globals.isShieldWall)
              alf = Resources.Load(("Enemy/LordAlfredEnemy"), typeof(GameObject)) as GameObject;
        if (Globals.isPart1Battle && !Globals.isShieldWall && !Globals.isRetreate && !Globals.isMotteyRetreat)
        {
            if (!Globals.thirdFight)
            {
                if (!Globals.forthFight)
                {
                    playble.transform.parent.gameObject.SetActive(true);
                    Debug.Log("12 12 12 12 12 12"+ this.transform.parent.name);
                    if (Globals.beforeMottey && this.transform.parent.name == "Motte&Baley_cinematic")
                        return;
                    lordAlfred = Instantiate(alf, lordAlfredNewPos.position, Quaternion.identity);
                    Globals.ActiveFaces(lordAlfred, false, false, true, false);
                   // Globals.ActiveControls(character, true);
                }
            }
            else
            {
                Debug.Log("13 13 13 13 13 13 13 13");
                lordAlfred = Instantiate(alf, enemyNew.position, Quaternion.identity);
                Globals.ActiveFaces(lordAlfred, false, false, true, false);
                SupportingGuard();
            }
            if (Globals.isPart1Battle)
            {
                Debug.Log("part 1 battle............");
                playble.transform.parent.gameObject.SetActive(true);
            }
        }
        Globals.globalChar = character;
    }
    void SupportingGuard()
    {
        for(int i=0;i<4;i++)
        {
            GameObject may = Resources.Load(("Enemy/Guard"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(may, supportingGuardPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(soldier, false, false, true, false);
        }
    }
    void GateSetting(bool f,bool s,bool t)
    {
        firstGate.SetActive(f);
        secondGate.SetActive(s);
        thirdGate.SetActive(t);
    }
    int value;
    void SpawnSoldier()
    {
        if (!Globals.isPart1Battle)
            value = 5;
        else
        {
            if (Globals.beforeMottey)
            {
                value = 6;
                Common();
            }
            else if (Globals.secondFight)
            {
                value = 3;
                Common();
            }
            else
                value = 6;
           
        }
    }
    void Common()
    {
        for (int i = 0; i < value; i++)
        {
            GameObject sol = Resources.Load(("Enemy/Guard"), typeof(GameObject)) as GameObject;
            if (!Globals.isPart1Battle)
                soldier = Instantiate(sol, soldierPos[i].position, Quaternion.identity);
            else
                soldier = Instantiate(sol, soldierPos3[i].position, Quaternion.identity);
            soldeirs.Add(soldier);
            Globals.ActiveFaces(soldier, false, false, true, false);
        }
        if (Globals.secondFight || Globals.beforeMottey)
            SpawnCaptain();
    }
    void SargentAtArms()
    {
        GameObject sar = Resources.Load(("Enemy/SargentAtArms"), typeof(GameObject)) as GameObject;
        sargent = Instantiate(sar, SargentPos.position, Quaternion.identity);
        Globals.ActiveFaces(sargent, false, false, true, false);
    }
    void SpawnCaptain()
    {
        Debug.Log("spawn captain::"+Globals.secondFight+ " ya "+Globals.beforeMottey);
        GameObject sar = Resources.Load(("Enemy/Captain"), typeof(GameObject)) as GameObject;
        sargent = Instantiate(sar, sargentNewPos.position, Quaternion.identity);
        Globals.ActiveFaces(sargent, false, false, true, false);
    }

  public  void GotoGate()
    {
        playble.transform.parent.gameObject.SetActive(true);
        if (!Globals.isPart1Battle)
        {
            Debug.Log("............................................1");
            if (Globals.avatarState.AvatarName == "WarriorMale")
            {
                foreach(var v in hideItems)
                {
                    v.SetActive(false);
                }
                playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_M_Motte&Bailey_Castle_Part_1") as TimelineAsset;
            }
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_F_Motte&Bailey_Castle_Part_1") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_M_Motte&Bailey_Castle_Part_1") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_F_Motte&Bailey_Castle_Part_1") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_M_Motte&Bailey_Castle_Part_1") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_F_Motte&Bailey_Castle_Part_1") as TimelineAsset;
            playble.Play();
           // SargentAtArms();
            character.SetActive(false);
            mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
        }
        else
        {
            if (Globals.thirdFight)
            {
                Debug.Log("............................................2");
                Companion();
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_M_4") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_F_4") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_M_4") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_F_4") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_M_4") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_F_4") as TimelineAsset;
                playble.Play();
            }
            else
            {
                if (!Globals.beforeMottey)
                {
                    Debug.Log("............................................3");
                    battlePoint.SetActive(false);
                    spineChar.SetActive(true);
                    Globals.isRetreate = false;
                    Globals.isMotteyRetreat = false;
                    Globals.isShieldWall = true;
                    FindObjectOfType<BaileyManager>().p1.SetActive(true);
                    FindObjectOfType<BaileyManager>().p1.transform.GetChild(0).GetComponent<MotteAndBaileyCastle>().SpawnCharacters();
                }
                else
                {
                    Debug.Log("............................................4");
                    if (Globals.avatarState.AvatarName == "WarriorMale")
                        playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_M_2") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "WarriorFemale")
                        playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_F_2") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherMale")
                        playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_M_2") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "ArcherFemale")
                        playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_F_2") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestMale")
                        playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_M_2") as TimelineAsset;
                    else if (Globals.avatarState.AvatarName == "PriestFemale")
                        playble.playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_F_2") as TimelineAsset;
                    playble.Play();
                }
            }
        }
        if (!Globals.forthFight)
        {
            if (!Globals.thirdFight)
            {
                Debug.Log("............................................5");
                foreach (var v in soldeirs)
                {
                    Globals.ActiveFaces(v, false, false, true, false);
                }
            }
            if(Globals.isPart1Battle && !Globals.isMotteyRetreat && !Globals.isRetreate && !Globals.isShieldWall)
            Globals.ActiveFaces(lordAlfred, false, false, true, false);
        }
        Globals.ActiveControls(character, false);
        Globals.beforeMottey = false;
    }
    public void StartMotteyBattle()
    {
        Debug.Log("start motte bailey");
        Globals.beforeMottey = true;
        Globals.isShieldWall = false;
        BattleStart();
    }
    public void PlayFirstClip()
    {
        Time.timeScale = 1;
        if (!Globals.isSpecial)
            dialogCount++;
        else
            specialCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (Globals.isPart1Battle)
        {
            if (Globals.secondFight)
            {
                if (dialogCount==3)
                {
                    Debug.Log("Globals.second fight");
                    Globals.secondFight = false;
                    Globals.thirdFight = true;
                    BattleStart();
                }
            }
            else if(Globals.thirdFight)
            {
                if (dialogCount == 9)
                {
                    Debug.Log("Globals.thirdFight");
                    Globals.thirdFight = false;
                    Globals.forthFight = true;
                    BattleStart();
                }
            }
            else
            {
                if (!Globals.forthFight)
                {
                    if (dialogCount == 7 && !Globals.first)
                    {
                        Debug.Log(",,,,,,,,,,here,,,,,,,,,,,,");
                        playble.Pause();
                        newPlayble.SetActive(true);
                        if (Globals.avatarState.AvatarName == "WarriorMale")
                            newPlayble.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_M_GateBreech") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "WarriorFemale")
                            newPlayble.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Motte and Bailey/Smith/Smith_F_GateBreech") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherMale")
                            newPlayble.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_M_GateBreech") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "ArcherFemale")
                            newPlayble.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Motte and Bailey/Archer/Archer_F_GateBreech") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestMale")
                            newPlayble.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_M_GateBreech") as TimelineAsset;
                        else if (Globals.avatarState.AvatarName == "PriestFemale")
                            newPlayble.GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Motte and Bailey/Acolyte/Acolyte_F_GateBreech") as TimelineAsset;
                        newPlayble.transform.GetChild(0).gameObject.SetActive(true);
                        newPlayble.transform.GetChild(0).GetComponent<PlayableDirector>().Play();
                        character.SetActive(false);
                        Globals.isSpecial = true;
                    }
                    else if (dialogCount == 10 && Globals.first)
                    {
                        Globals.thirdFight = false;
                        Globals.isSpecial = false;
                        Globals.first = false;
                        Globals.secondFight = true;
                        BattleStart();
                        Debug.Log(",,,,,,,,,,here,,,,,,,,,,,,33333333333");
                    }
                }
            }
        }
    }
    void RunSoldiers()
    {
        float step = speed * Time.deltaTime;
        for (int i=0;i<4;i++)
        {
            soldeirs[i].transform.position= Vector3.MoveTowards(soldeirs[i].transform.position, target.position, step);
        }
    }
    private void Update()
    {
        if (isRun)
        {
            RunSoldiers();
            isRun = false;
        }
    }
    void BattleStart()
    {
        isRun = false;
        SceneManager.LoadScene("Motte and Baley Battle Scene");
    }
   
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        Time.timeScale = 0;
        Debug.Log("showing...........");
        //ColorBlock col = dialogBox.GetComponent<Button>().colors;
        //col.colorMultiplier = 5;
        //dialogBox.GetComponent<Button>().colors = col;
    }
    void SpawnProtagnist()
    {
        spineChar.SetActive(false);
        playble.transform.parent.GetComponent<MotteAndBaileyCastle>().enabled = true;
        playble.gameObject.SetActive(false);
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerFinalPos.position, Quaternion.identity);
        Globals.ActiveControls(character, true);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;  
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
    }
    public void CompleteVideo()
    {
        if (!Globals.isPart1Battle)
        {
            Debug.Log("not part 1 battle");
            BattleStart();
        }
         
        else if (isRetreat || isRetreat1)
        {
            isRetreat = false;
            Globals.isRetreate = true;
            FindObjectOfType<BaileyManager>().p2.SetActive(true);
            FindObjectOfType<BaileyManager>().p1.SetActive(false);
        }
        else
        {
            if (!Globals.isSpecial)
            {
                if (!Globals.beforeMottey && !Globals.thirdFight)
                {
                    if (Globals.isShieldWall)
                        StartMotteyBattle();
                    else
                    {
                        GateSetting(false, false, false);
                        SpawnProtagnist();
                        Globals.isFirstCompleteStory = true;
                        SetBoundaries();
                    }
                }
                else if(Globals.thirdFight)
                {
                    FindObjectOfType<BaileyManager>().p2.SetActive(true);
                    FindObjectOfType<BaileyManager>().p1.SetActive(false);
                }
            }
            else
            {
                Globals.secondFight = true;
                Globals.isSpecial = false;
                Globals.first = true;
                BattleStart();
                Debug.Log("else fight");
            }
        }
    }
    void SetBoundaries()
    {
        foreach(var v in boundaries)
        {
            v.tag = "LeftHunsville";
        }
    }
    void SecondWave()
    {
        Debug.Log("..............................22222222222222222222222222.......................");
        if (!Globals.secondFight)
        {
            if (Globals.forthFight)
                firstGate.SetActive(false);
            else
                Globals.ActiveControls(character, true);
        }
        else
            Globals.ActiveControls(character, false);
    }
    void Companion()
    {
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        GameObject tuc = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnNewPos.position, Quaternion.identity);
        marium = Instantiate(mar, mariumNewPos.position, Quaternion.identity);
        tucker = Instantiate(tuc, tuckerNewPos.position, Quaternion.identity);
        Globals.ActiveFaces(john, false, false, false, true);
        Globals.ActiveFaces(marium, false, false, false, true);
        Globals.ActiveFaces(tucker, false, false, false, true);
    }
}
