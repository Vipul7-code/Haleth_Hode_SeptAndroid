using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class HuntigtonVill : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, marium, john, tucker, helena,guard,customer;
    [SerializeField]
    Transform playerPos,playerPosFinal,afterCutScene,afterGuard,afterHook,mariumPos,johnPos,tuckerPos,johnPos1,tuckerPos1,mariumPos1;
    [SerializeField]
    Camera mainCamera;
    [HideInInspector]
   public int interactionId = 1, clickCount = 1;
    List<GameObject> allMember = new List<GameObject>();
    public GameObject conversationBox1,conversationBox2,conversationBox3,conversationBox4,conversationBox5,conversationBox6;
    int dialogCount;

    [SerializeField]
  public  GameObject playble1, playble2, playble3, playble4;
    // Start is called before the first frame update
    void Start()
    {
        Globals.isEnemyTeam = false;
        Globals.isMyTeam = false;
        Globals.isShop = true;
        Globals.huntingtonVill = this;
        Globals.activePart = "HuntingtonVill";
        if (!Globals.leavingThrone)
        {
            if(Globals.isExploringTunnel)
            {
                if (!Globals.drunkenGuy && !Globals.grappingHook)
                {
                    conversationBox1.tag = "Untagged";
                    conversationBox3.tag = "Caravan1";
                }
                else if (Globals.drunkenGuy && !Globals.grappingHook)
                {
                    conversationBox1.tag = "Untagged";
                    conversationBox4.tag = "Hunsville";
                    conversationBox5.tag = "Hunsville";
                }
            }
            SpawnProtagnist();
            PlaybleSetting(false, true, false, false);
        }
        else
        {
            PlaybleSetting(false, false, true, false);
            if (Globals.avatarState.AvatarName == "WarriorMale")
               playble3.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_M_Huntington_Courtyard_Messenger_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble3.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Smith/Smith_F_Huntington_Courtyard_Messenger_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble3.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_M_Huntington_Courtyard_Messenger_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble3.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Archer/Archer_F_Huntington_Courtyard_Messenger_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble3.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_M_Huntington_Courtyard_Messenger_CutScene") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble3.transform.GetChild(0).GetComponent<PlayableDirector>().playableAsset = Resources.Load("Playables/Huntington Village/Acolyte/Acolyte_F_Huntington_Courtyard_Messenger_CutScene") as TimelineAsset;
            playble3.transform.GetChild(0).GetComponent<PlayableDirector>().Play();
        }
    }
   public void PlaybleSetting(bool p1,bool p2,bool p3,bool p4)
    {
        playble1.SetActive(p1);
        playble2.SetActive(p2);
        playble3.SetActive(p3);
        playble4.SetActive(p4);
    }
    public void SecondCutScene()
    {
        clickCount = 2;
        Debug.Log("click count::" + clickCount);
       // clickCount++;
        conversationBox6.GetComponent<BoxCollider2D>().enabled = false;
        conversationBox2.transform.tag = "checkPoint";
        CommonPart();
    }
    public void ThirdCutScene()
    {
         clickCount = 3;
        Debug.Log("click count::" + clickCount);
       // clickCount++;
        CommonPart();
    }
    public void ForthCutScene()
    {
         clickCount = 5;
        Debug.Log("click count::" + clickCount);
        //clickCount++;
        CommonPart();
    }
    void CommonPart()
    {
        Globals.completeEfforts = true;
        PlaybleSetting(true, false, false, false);
        FindObjectOfType<HuntingtonController>().PlaybleFile();
        Destroy(character.gameObject);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = true;
    }
    public void OnCompleteCutScene()
    {
        // clickCount = 2;
        conversationBox3.SetActive(false);
        CompleteCommon();
    }
    void CompleteCommon()
    {
        PlaybleSetting(false, true, false, false);
        SpawnProtagnist();
    }
    public void OnCompleteThirdScene()
    {
         clickCount = 4;
        Debug.Log("click count::" + clickCount);
       // clickCount++;
        conversationBox2.SetActive(false);
        CompleteCommon();
        FindObjectOfType<HuntingtonController>().AfterCutsceneDialog();
    }
    public void OnCompleteFifthScene()
    {
          clickCount = 6;
        Debug.Log("click count::" + clickCount);
       // clickCount++;
        conversationBox4.SetActive(false);
        conversationBox5.SetActive(false);
        CompleteCommon();
    }
  public  void SpawnFinalProtagnist()
    {
        conversationBox2.SetActive(false);
        conversationBox3.SetActive(false);
        conversationBox4.SetActive(false);
        conversationBox5.SetActive(false);
        conversationBox6.SetActive(false);
        playble4.SetActive(false);
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterChurch || Globals.enterInn|| Globals.enterShop||Globals.isArmorer||Globals.enterBlackSmith || Globals.isTunnel)
        {
            Debug.Log("arm::" + Globals.isArmorer + "  shop::" + Globals.enterShop);
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.4f), 0), Quaternion.identity);
            Globals.enterChurch = false;
            Globals.enterInn = false;
            Globals.enterShop = false;
            Globals.isArmorer = false;
            Globals.enterBlackSmith = false;
            Globals.isTunnel = false;
        }
        else
            character = Instantiate(Globals.activePlayer, playerPosFinal.position, Quaternion.identity);
        Globals.ActiveControls(character, false);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        // allMember.Add(character);
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, true, false, false);
    }
    void SpawnProtagnist()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterChurch || Globals.enterInn || Globals.enterShop||Globals.isArmorer||Globals.enterBlackSmith||Globals.isTunnel|| Globals.enterFarmhouse)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x,(Globals.enterPos.y-0.6f),0), Quaternion.identity);
            Globals.enterChurch = false;
            Globals.enterInn = false;
            Globals.enterShop = false;
            Globals.isArmorer = false;
            Globals.enterBlackSmith = false;
            Globals.isTunnel = false;
        }
        else if (clickCount == 2)
            character = Instantiate(Globals.activePlayer, afterCutScene.position, Quaternion.identity);
        else if (clickCount == 4)
        {
            character = Instantiate(Globals.activePlayer, afterGuard.position, Quaternion.identity);
            conversationBox4.tag = "Hunsville";
            conversationBox5.tag = "Hunsville";
            SpawnPartyMember();
        }
        else if (clickCount == 6)
            character = Instantiate(Globals.activePlayer, afterHook.position, Quaternion.identity);
        else
            character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        Globals.ActiveControls(character, false);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        // allMember.Add(character);
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, true, false, false);
    }
  public  void SpawnPartyMember()
    {
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        GameObject tuc = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        if (clickCount == 1)
        {
            marium = Instantiate(mar, mariumPos1.position, Quaternion.identity);
            john = Instantiate(jo, johnPos1.position, Quaternion.identity);
            tucker = Instantiate(tuc, tuckerPos1.position, Quaternion.identity);
            Globals.ActiveFaces(marium, false, false, false, true);
            Globals.ActiveFaces(john, false, false, false, true);
            Globals.ActiveFaces(tucker, false, false, false, true);
        }
        else
        {
            marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
           john = Instantiate(jo, johnPos.position, Quaternion.identity);
           tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
            Globals.ActiveFaces(marium, false, true, false, false);
            Globals.ActiveFaces(john, false, true, false, false);
            Globals.ActiveFaces(tucker, false, true, false, false);
        }
       
    }
   
}
