using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pathfinding;
public class BarghestVill : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, villagers,marchentFemale;
    [SerializeField]
    Transform playerPos,marchentPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform[] villPos,newPos;
    int dialogCount;
    List<GameObject> allMember = new List<GameObject>();
    List<GameObject> vil = new List<GameObject>();
    [SerializeField]
    GameObject Inn;
    [SerializeField]
    PlayableDirector playble;
    [SerializeField]
    GameObject dialogBox;
    public GameObject[] targetPos;
    public GameObject aStar;
    public GameObject[] boundaries;
    [SerializeField]
    AudioSource bg;
    bool merchantComplete = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("play now..."+ Globals.PlayNow+ " shop dialog : "+Globals.isShopDialog+" second visit :  "+Globals.secondVisit+ " Globals is church complete "+Globals.isChurchComplete+ "first complete story : "+Globals.isFirstCompleteStory);
        Time.timeScale = 1;
        Globals.secondVisit = 1;
       // Globals.againVisit = 0;
        Globals.isChurchComplete = false;
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.waveCount = 0;
        Globals.isShop = true;
        Globals.barghestVill = this;
        Globals.objectiveScene = "Barghest Village";
        Globals.activePart = "BarghestVill";
        SpawnCharacters();
    }
void SpawnCharacters()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterInn||Globals.enterFarmhouse||Globals.enterBarghestShop)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.8f), 0), Quaternion.identity) as GameObject;
            Globals.enterInn = false;
            Globals.enterFarmhouse = false;
            Globals.enterBarghestShop = false;
        }
        else
            character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        allMember.Add(character);
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, false, false, true);
        if(Globals.againVisit==0)
            bg.clip= Resources.Load("Sound/Haunted/Haunted Town Music mix") as AudioClip;
        else
            bg.clip= Resources.Load("Sound/TownAttack/Peaceful Town Music mix") as AudioClip;
        bg.Play();
        OtherCharacters();
    }
    void SpawnMerchant()
    {
        GameObject mar = Resources.Load(("Others/FemaleMarchent"), typeof(GameObject)) as GameObject;
        marchentFemale = Instantiate(mar, marchentPos.position, Quaternion.identity);
        Globals.ActiveFaces(marchentFemale, true, false, false, false);
    }
    void OtherCharacters()
    {
        for (int i = 0; i <4; i++)
        {
            GameObject vill = Resources.Load(("Others/Moveable" + (i + 1)), typeof(GameObject)) as GameObject;
            villagers = Instantiate(vill, villPos[i].position, Quaternion.identity);
            vil.Add(villagers);
        }
        if (Globals.againVisit == 1)
            SpawnMerchant();
        if(Globals.conversationCount>3)
        {
            foreach(var v in vil)
            {
               foreach(var s in v.transform.GetChild(0).GetComponent<EntityGroup>().allSides)
                {
                    s.gameObject.SetActive(false);
                }
            }
        }
        if (Globals.isShopDialog)
            SetBoundaries();
    }
    public void PlayFirstClip()
    {
        Debug.Log("dialog count :: "+dialogCount+" conversation count :: "+Globals.conversationCount);
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        Globals.conversationCount++;
        if(!isMerchant)
          Globals.ActiveControls(character, true);
        else
        {
            Debug.Log("count::" + dialogCount);
            if (dialogCount == 2 || dialogCount == 5)
                Globals.ActiveControls(character, true);
        }
        //if (Globals.conversationCount == 4)
        //    SetBoundaries();
        if(Globals.conversationCount == 6 && dialogCount == 6 || (Globals.conversationCount == 4 && Globals.againVisit == 0))
        {
            Debug.Log("set boundries");
            SetBoundaries();
        }
        if (isMerchant)
        {
            Debug.Log("added 250 gold.........");
            Globals.shopMerchant.Gold += 250;
            FindObjectOfType<DatabaseManager>().UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant);
            isMerchant = false;
            merchantComplete = true;
        }
    }
    bool isMerchant;
    private void Update()
    {
        if (Globals.PlayNow)
        {
            Debug.Log("play now................");
            foreach (var v in Globals.collideObject.transform.parent.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            if (Globals.againVisit == 0)
            {
                if (Globals.collideObject.transform.parent.name== "atwaterTown1")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/Barghest_Villager1_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name== "atwaterTown2")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/Barghest_Villager2_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name== "atwaterTown3")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/Barghest_Villager3_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name== "atwaterTown5")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/Barghest_Villager4_Dialog") as TimelineAsset;
            }
            else
            {
                if (Globals.collideObject.transform.parent.name == "atwaterTown1")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/After Defeating Hound/Barghest2_Villager1_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name == "atwaterTown2")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/After Defeating Hound/Barghest2_Villager2_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name == "atwaterTown3")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/After Defeating Hound/Barghest2_Villager3_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name == "atwaterTown5")
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/After Defeating Hound/Barghest2_Villager4_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name == "FemaleMarchent(Clone)")
                {
                    isMerchant = true;
                    playble.playableAsset = Resources.Load("Playables/Barghest Village/After Defeating Hound/Barghest2_Merchant_Dialog") as TimelineAsset;
                }

            }
            Globals.ActiveControls(character, false);
            playble.Play();
            Globals.PlayNow = false;
            Globals.collideObject = null;
        }
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        Debug.Log("complete video...");
        Globals.ActiveControls(character, true);
        SetBoundaries();
        Globals.secondVisit = 0;
        Globals.isChurchComplete = true;
        Globals.isFirstCompleteStory = true;
        if (Globals.againVisit == 1)
            Globals.avatarState.TotalXp += 9160;
        Globals.storyCount = 1;
    }
    void SetBoundaries()
    {
        foreach(var v in boundaries)
        {
            v.transform.tag = "LeaveBarghest";
        }
    }
   
    void SecondWave()
    {
        GameObject mar = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
        marchentFemale = Instantiate(mar, marchentPos.position, Quaternion.identity);
        Globals.ActiveFaces(marchentFemale, false, false, true, false);
        MarchenSetting(true);
    }
    void MarchenSetting(bool set)
    {
        foreach(var v in marchentFemale.GetComponent<EntityGroup>().allSides)
        {
            v.gameObject.SetActive(set);
        }
    }
}
