using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class MonestryTuckerJoin : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    GameObject character,nun,monk, abott, tucker, marium, john,FistNun,secondNun,thirdNun, forthNun,fifthNun,sixthNun,seventhNun,firstMonk,secondMonk,thirdMonk,forthMonk,fifthMonk,sixthMonk,seventhMonk;
    [SerializeField]
    Transform nunPos,monkPos,playerNewPos, abottPos, tuckerPos, playerPos2, mariumPos2, johnPos2;
    [SerializeField]
    GameObject travelPoint,secondTravel;
    [SerializeField]
    Camera mainCamera;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    [SerializeField]
    GameObject first,second;
    [SerializeField]
    GameObject[] FirstNonPos,secondNonPos,thirdNonPos,forthNonPos,fifthNonPos,sixNunPos,sevenNunPos;
    [SerializeField]
    GameObject[] firstMonkPos,secondMonkPos,thirdMonkPos,forthMonkPos,fifthMonkPos,sixthMonkPos;
    bool isNunConvo,finalNun;
    [SerializeField]
    AudioSource partyMember;
    [SerializeField]
    GameObject door1, door2;
    GameObject halfWayNun, halfWayMonk;
    // Start is called before the first frame update
    void Start()
    {
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.isShop = true;
        Globals.monestryTuckerJoin = this;
        Globals.activePart = "MonestryTuckerJoin";
        door1.tag = "Untagged";
        door2.tag = "Untagged";
        if (!Globals.secondVisitMon)
        {
            if (Globals.isPart1Battle && Globals.activeScene == Globals.CurrentScene.CellarTucker)
            {
                GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
                halfWayNun = Instantiate(non, nunPos.transform.position, Quaternion.identity);
                halfWayNun.tag = "nun8";
                Globals.ActiveFaces(halfWayNun, false, false, true, false);
                GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
                 halfWayMonk = Instantiate(monk, monkPos.transform.position, Quaternion.identity);
                halfWayMonk.tag = "monk8";
                Globals.ActiveFaces(halfWayMonk, false, false, true, false);
                SecondWave();
            }
        }
        else
            SecondWave();
    }
    void ThirdWave()
    {
        finalNun = false;
        travelPoint.SetActive(false);
        GameObject ab = Resources.Load(("AbbotGregory"), typeof(GameObject)) as GameObject;
        abott = Instantiate(ab, abottPos.position, Quaternion.identity);
        Globals.ActiveFaces(abott, false, false, true, false);
        FirstRoomNun();
    }
    void SecondWave()
    {
        first.SetActive(false);
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerNewPos.position, Quaternion.identity);
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, false, false, true);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.farClipPlane = 1000;
        if (!Globals.secondVisitMon)
            ThirdWave();
    }
    void FirstRoomNun()
    {
        GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
        FistNun = Instantiate(non, FirstNonPos[0].transform.position, Quaternion.identity);
        FistNun.tag = "nun1";
        Globals.ActiveFaces(FistNun, false, false, false, true);
        foreach (var v in FistNun.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(true);
        }
        FirstRoomMonk();
        SecondRoomNun();

    }
    void SecondRoomNun()
    {
        GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
        secondNun = Instantiate(non, secondNonPos[0].transform.position, Quaternion.identity);
        secondNun.tag = "nun2";
        Globals.ActiveFaces(secondNun, false, false, false, true);
        foreach (var v in secondNun.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(true);
        }
        SecondRoomMonk();
        ThirdRoomNun();
    }
    void ThirdRoomNun()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
            thirdNun = Instantiate(non, thirdNonPos[i].transform.position, Quaternion.identity);
            thirdNun.tag = "nun3";
            Globals.ActiveFaces(thirdNun, false, false, false, true);
            foreach (var v in thirdNun.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
        ThirdRoomMonk();
        ForthRoomNun();
    }
    void ForthRoomNun()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
            forthNun = Instantiate(non, forthNonPos[i].transform.position, Quaternion.identity);
            forthNun.tag = "nun4";
            Globals.ActiveFaces(forthNun, false, false, false, true);
            foreach (var v in forthNun.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
        ForthRoomMonk();
        FifthRoomNun();
    }
    void FifthRoomNun()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
            fifthNun = Instantiate(non, fifthNonPos[i].transform.position, Quaternion.identity);
            fifthNun.tag = "nun5";
            Globals.ActiveFaces(fifthNun, false, false, false, true);
            foreach (var v in fifthNun.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
        FifthRoomMonk();
        SixthRoomNun();
    }
    void SixthRoomNun()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
            sixthNun = Instantiate(non, sixNunPos[i].transform.position, Quaternion.identity);
            sixthNun.tag = "nun6";
            Globals.ActiveFaces(sixthNun, false, false, false, true);
            foreach (var v in sixthNun.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
        SixthRoomMonk();
        SevenRoomNun();
    }
    void SevenRoomNun()
    {
        GameObject non = Resources.Load(("Others/Nun"), typeof(GameObject)) as GameObject;
        seventhNun = Instantiate(non, sevenNunPos[0].transform.position, Quaternion.identity);
        seventhNun.tag = "nun7";
        Globals.ActiveFaces(seventhNun, false, false, false, true);
        foreach (var v in seventhNun.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(true);
        }
        SeventhRoomMonk();
    }
    void FirstRoomMonk()
    {
        GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
        firstMonk = Instantiate(monk, firstMonkPos[0].transform.position, Quaternion.identity);
        firstMonk.transform.tag = "monk1";
        Globals.ActiveFaces(monk, false, false, true, false);
        foreach (var v in firstMonk.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(true);
        }
    }
    void SecondRoomMonk()
    {
        GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
        secondMonk = Instantiate(monk, secondMonkPos[0].transform.position, Quaternion.identity);
        secondMonk.transform.tag = "monk2";
        Globals.ActiveFaces(monk, false, false, true, false);
        foreach (var v in secondMonk.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(true);
        }
    }
    void ThirdRoomMonk()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
            thirdMonk = Instantiate(monk, thirdMonkPos[i].transform.position, Quaternion.identity);
            thirdMonk.transform.tag = "monk3";
            Globals.ActiveFaces(monk, false, false, true, false);
            foreach (var v in thirdMonk.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
    }
    void ForthRoomMonk()
    {
        GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
        forthMonk = Instantiate(monk, forthMonkPos[0].transform.position, Quaternion.identity);
        forthMonk.transform.tag = "monk4";
        Globals.ActiveFaces(monk, false, false, true, false);
        foreach (var v in forthMonk.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(true);
        }
    }
    void FifthRoomMonk()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
            fifthMonk = Instantiate(monk, fifthMonkPos[i].transform.position, Quaternion.identity);
            fifthMonk.transform.tag = "monk5";
            Globals.ActiveFaces(monk, false, false, true, false);
            foreach (var v in fifthMonk.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
    }
    void SixthRoomMonk()
    {
        for (int i = 0; i < 1; i++)
        {
            GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
            sixthMonk = Instantiate(monk, sixthMonkPos[i].transform.position, Quaternion.identity);
            sixthMonk.transform.tag = "monk6";
            Globals.ActiveFaces(monk, false, false, true, false);
            foreach (var v in sixthMonk.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
    }
    void SeventhRoomMonk()
    {
      //  for (int i = 0; i < 1; i++)
        {
            GameObject monk = Resources.Load(("Others/MonkMon"), typeof(GameObject)) as GameObject;
            seventhMonk = Instantiate(monk, sixNunPos[1].transform.position, Quaternion.identity);
            seventhMonk.transform.tag = "monk7";
            Globals.ActiveFaces(monk, false, false, true, false);
            foreach (var v in seventhMonk.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
    }
    public void StartConversation()
    {
        Globals.ActiveControls(character, false);
        finalNun = true;
        playble.playableAsset = Resources.Load("Playables/Monsestery/MonasteryCellar_Monk&Nun_Dialog") as TimelineAsset;
        playble.Play();
    }
    public void StartOtherConversation()
    {
        secondTravel.SetActive(false);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos2.position, Quaternion.identity);
        Globals.ActiveFaces(john, true, false, false, false);
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos2.position, Quaternion.identity);
        Globals.ActiveFaces(marium, true, false, false, false);
        GameObject tuck = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(tuck, tuckerPos.position, Quaternion.identity);
        Globals.ActiveFaces(tucker, false, false, false, true);
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_M_MonasteryCellar_Abbott_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_F_MonasteryCellar_Abbott_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_M_MonasteryCellar_Abbott_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_F_MonasteryCellar_Abbott_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_M_MonasteryCellar_Abbott_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_F_MonasteryCellar_Abbott_Dialog") as TimelineAsset;
        playble.Play();
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        Globals.noOfCompanions = 3;
        Globals.UpdateDefaultEquipment();
        DatabaseManager.instance.UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
        DatabaseManager.instance.UpdateRecord<Globals.TuckerInventory>(Globals.inventoryTucker);
        Debug.Log("tucker aa gya/.............");
        partyMember.Play();
        door1.tag = "LeftMonestry";
        door2.tag = "LeftMonestry";
        CompleteVideoSetting();
        Globals.isFirstCompleteStory = true;
        StartCoroutine(FinalControls());
    }
    IEnumerator FinalControls()
    {
        yield return new WaitForSeconds(0.7f);
        Globals.ActiveControls(character, true);
    }
    private void Update()
    {
        if(Globals.PlayNow)
        {
            foreach (var v in Globals.collideObject.transform.parent.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            OnNunMonkConversationComplete();
            isNunConvo = true;
            playble.playableAsset = Resources.Load("Playables/Monsestery/MonasteryCellar_Monk&Nun_Dialog") as TimelineAsset;
            Globals.ActiveControls(character, false);
            playble.Play();     
            Globals.collideObject = null;
            Globals.PlayNow = false;
        }
    }
    void OnNunMonkConversationComplete()
    {
        if(Globals.collideObject.transform.parent.tag== "nun1" || Globals.collideObject.transform.parent.tag == "monk1")
        {
            foreach (var v in FistNun.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            foreach (var v in firstMonk.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
        }
       else if (Globals.collideObject.transform.parent.tag == "nun2" || Globals.collideObject.transform.parent.tag == "monk2")
        {
            foreach (var v in secondNun.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            foreach (var v in secondMonk.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
        }
        else if (Globals.collideObject.transform.parent.tag == "nun3" || Globals.collideObject.transform.parent.tag == "monk3")
        {
            foreach (var v in thirdNun.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            foreach (var v in thirdMonk.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
        }
        else if (Globals.collideObject.transform.parent.tag == "nun4" || Globals.collideObject.transform.parent.tag == "monk4")
        {
            foreach (var v in forthNun.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            foreach (var v in forthMonk.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
        }
        else if (Globals.collideObject.transform.parent.tag == "nun5" || Globals.collideObject.transform.parent.tag == "monk5")
        {
            foreach (var v in fifthNun.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            foreach (var v in fifthMonk.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
        }
        else if (Globals.collideObject.transform.parent.tag == "nun6" || Globals.collideObject.transform.parent.tag == "monk6")
        {
            foreach (var v in sixthNun.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            foreach (var v in sixthMonk.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
        }
        else if (Globals.collideObject.transform.parent.tag == "nun7" || Globals.collideObject.transform.parent.tag == "monk7")
        {
            foreach (var v in seventhNun.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            foreach (var v in seventhMonk.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
        }
        else if (Globals.collideObject.transform.parent.tag == "nun8" || Globals.collideObject.transform.parent.tag == "monk8")
        {
            halfwayNunMonk = true;
        }
    }
    bool halfwayNunMonk;
    void CompleteVideoSetting()
    {
        tucker.SetActive(false);
        john.SetActive(false);
        marium.SetActive(false);
        secondTravel.SetActive(false);
        
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (isNunConvo)
        {
            if(halfwayNunMonk)
            {
                halfWayNun.SetActive(false);
                halfWayMonk.SetActive(false);
                halfwayNunMonk = false;
            }
            Globals.ActiveControls(character, true);
            isNunConvo = false;
        }
        else
        {
            if(finalNun)
              ThirdWave();
        }

    }
}
