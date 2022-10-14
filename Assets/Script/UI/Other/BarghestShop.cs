using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class BarghestShop : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [SerializeField]
    Transform playerSpawnPos, merchantPos;
    [SerializeField]
    public Camera mainCamera;
    [HideInInspector]
    public GameObject character, marchentFemale;
    public GameObject frontDoor,counter;
    public PlayableDirector playble;
    public GameObject dialogBox;
    [HideInInspector]
    public int dialogCount;
    // Start is called before the first frame update
    void Start()
    {
        Globals.barghestShop = this;
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Globals.waveCount = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerSpawnPos.position, Quaternion.identity) as GameObject;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        Globals.ActiveFaces(character, false, true, false, false);
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player";
        SpawnMerchant();
    }
    void SpawnMerchant()
    {
        GameObject mar = Resources.Load(("Others/FemaleMarchent"), typeof(GameObject)) as GameObject;
        marchentFemale = Instantiate(mar, merchantPos.position, Quaternion.identity);
        Globals.ActiveFaces(marchentFemale, true, false, false, false);
        
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
        if(!Globals.isShopDialog)
        {
            if(Globals.againVisit==0)
            {
                if (dialogCount == 3)
                    OnComplete();
            }
            else
            {
                if (dialogCount == 1)
                    OnComplete();
            }
            Globals.isShopDialog = true;
        }
        else
        {
            if (dialogCount == 1)
                OnComplete();
        }
    }
 public   void OnComplete()
    {
        counter.tag = "Untagged";
        frontDoor.tag = "LeaveChurch";
        Globals.ActiveControls(character, true);
        Globals.uiHandler.merchantPanel.SetActive(true);
      //  Globals.ActiveControls(character, false);
    }
    private void Update()
    {
        if(Globals.shopCounter)
        {
            if (Globals.againVisit == 0)
            {
                if (!Globals.isShopDialog)
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/Barghest_Merchant_Once_Dialog") as TimelineAsset;
                else
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/Barghest_Merchant_Everytime_Dialog") as TimelineAsset;
                playble.Play();
            }
            else
                OnComplete();
            Globals.conversationCount++;
            
            Globals.ActiveControls(character, false);
            Globals.shopCounter = false;
        }
    }
}
