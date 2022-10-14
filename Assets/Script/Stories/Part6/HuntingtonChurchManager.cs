using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
public class HuntingtonChurchManager : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [SerializeField]
    Transform playerSpawnPos, priestPos,mariumPos,johnPos,tuckerPos,afterBattlePos,afterInnKeeper;
    [SerializeField]
    Transform[] barmaidPos,guardPos;
    [SerializeField]
    public Camera mainCamera;
    [HideInInspector]
    public GameObject character, priest, mar,marium,john,tucker, barmaid1, barmaid2, barmaid3,guard1,guard2;
    public GameObject frontDoor;
    public PlayableDirector playble;
    public GameObject dialogBox, conversationBox1,conversationBox2,conversationBox3;
    [HideInInspector]
   public int dialogCount;
    [SerializeField]
    GameObject fakeWall,castleEscape,obstacle;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("inn count here::" + Globals.huntingtonVill.clickCount);
        Globals.huntingtonChurch = this;
     ///   Globals.huntingtonVill.clickCount = 6;
      //  Globals.isPart1Battle = true;
        if (!Globals.isPart1Battle)
            SpawnPlayer();
        else
        {
            if(Globals.enterInn)
              SpawnPartyMember();
        }
    }
void ForConversation()
    {
        if (Globals.completeEfforts)
            conversationBox3.SetActive(true);
        else
            conversationBox3.SetActive(false);
    }
  public  void SpawnPlayer()
    {
        Globals.waveCount = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterInn)
        {
            ForConversation();
            mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        }
        if (!Globals.isPart1Battle)
        {
            if (Globals.enterInn)
            {
                if (Globals.innController.clickCount == 1)
                    character = Instantiate(Globals.activePlayer, afterInnKeeper.position, Quaternion.identity) as GameObject;
                else
                    character = Instantiate(Globals.activePlayer, playerSpawnPos.position, Quaternion.identity) as GameObject;
            }
            else
                character = Instantiate(Globals.activePlayer, playerSpawnPos.position, Quaternion.identity) as GameObject;
            SpawnPriest();
        }
        else
        {
            character = Instantiate(Globals.activePlayer, afterBattlePos.position, Quaternion.identity) as GameObject;
            character.GetComponent<EntityGroup>().prespFaceL.GetComponent<MeshRenderer>().sortingOrder = 5;
        }
        Globals.ActiveFaces(character, false, true, false, false);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player"; 
    }
    void SpawnPartyMember()
    {
        SpawnPlayer();
        Globals.ActiveControls(character, false);
        Globals.ActiveFaces(character, false, false, true, false);
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_M_Huntington_Fakewall_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Smith/Smith_F_Huntington_Fakewall_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_M_Huntington_Fakewall_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Archer/Archer_F_Huntington_Fakewall_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_M_Huntington_Fakewall_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Inn/Acolyte/Acolyte_F_Huntington_Fakewall_Dialog") as TimelineAsset;
        playble.Play();
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
        if (!Globals.isPart1Battle)
        {
            if (dialogCount >= 1)
                OnCompleteVideo();
            
        }
        else
        {
            if (dialogCount == 2)
            {
                fakeWall.SetActive(false);
                castleEscape.tag = "CastleEscape";
                Globals.ActiveControls(character, true);
                Globals.completeEfforts = false;
                Globals.isPart1Battle = false;
                Globals.activeScene = Globals.CurrentScene.CastleEscapeTunnel;
            }
                
        }
    }
   public void SpawnBarmaidMale()
    {
        GameObject gua1 = Resources.Load(("Enemy/GuardBarmaidBlonde"), typeof(GameObject)) as GameObject;
        guard1 = Instantiate(gua1, guardPos[0].position, Quaternion.identity);
        GameObject gua2 = Resources.Load(("Enemy/GuardBarmaidDark"), typeof(GameObject)) as GameObject;
        guard2 = Instantiate(gua2, guardPos[1].position, Quaternion.identity);
    }
    void SpawnPriest()
    {
        if (Globals.isArmorer)
            Globals.ActiveControls(character, false);
        else 
          Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, true, false, false);
        if (Globals.enterChurch)
            mar = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
        else if (Globals.enterInn)
        {
            mar = Resources.Load(("Others/HunsvilleInnKeeper"), typeof(GameObject)) as GameObject;
            GameObject bar1 = Resources.Load(("Others/FemaleBarmaid"), typeof(GameObject)) as GameObject;
            barmaid1 = Instantiate(bar1, barmaidPos[0].position, Quaternion.identity);
            GameObject bar2 = Resources.Load(("Others/FemaleBarmaid2 Variant"), typeof(GameObject)) as GameObject;
            barmaid2 = Instantiate(bar2, barmaidPos[1].position, Quaternion.identity);
            GameObject bar3 = Resources.Load(("Others/FemaleBarmaid3 Variant"), typeof(GameObject)) as GameObject;
            barmaid3 = Instantiate(bar3, barmaidPos[2].position, Quaternion.identity);
           
        }
        else if (Globals.enterShop)
            mar = Resources.Load(("HuntsvilleMerchant"), typeof(GameObject)) as GameObject;
        else if (Globals.isArmorer)
            mar = Resources.Load(("Others/atwaterTown1"), typeof(GameObject)) as GameObject;
        else if (Globals.enterBlackSmith)
            mar = Resources.Load(("Others/HunsvilleBlackSmith"), typeof(GameObject)) as GameObject;
        priest = Instantiate(mar, priestPos.position, Quaternion.identity);
        if (Globals.enterShop)
            Globals.ActiveFaces(priest, true, false, false, false);
        else if (Globals.enterInn)
            Globals.ActiveFaces(priest, false, false, true, false);
        else if (Globals.enterBlackSmith)
            Globals.ActiveFaces(priest, true, false, false, false);
        else
            Globals.ActiveFaces(priest, true, false, false, false);
    }
  public  void DialogSetUp()
    {
        if(Globals.enterChurch)
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington_PriestMale_Dialog") as TimelineAsset;
        else if(Globals.enterInn)
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington_InnkeeperFemale_Dialog") as TimelineAsset;
        else if(Globals.enterShop)
            playble.playableAsset = Resources.Load("Playables/Huntington Village/Huntington_MerchantFemale_Dialog") as TimelineAsset;
        playble.Play();
        Globals.ActiveControls(character, false);
    }
    void OnCompleteVideo()
    {
        if (Globals.enterInn)
        {
            conversationBox1.SetActive(false);
            conversationBox2.SetActive(false);
        }
        else if (Globals.enterShop)
        {
            Globals.uiHandler.merchantPanel.SetActive(true);
            Globals.ActiveControls(character, false);
        }
        else if (Globals.enterChurch)
            obstacle.SetActive(false);
        Globals.ActiveControls(character, true);
        frontDoor.tag = "LeaveChurch";
    }
}
