using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class DeathWeightVill : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, oldMan,marium,john,tucker,villager;
    [SerializeField]
    Transform playerPos, oldManPos,mariumPos,johnPos,tuckerPos;
    [SerializeField]
    Transform[] villagersPos;
    [SerializeField]
    Camera mainCamera;
    List<GameObject> villagers = new List<GameObject>();
    [HideInInspector]
    public bool leaveDeath;
    [SerializeField]
    GameObject inn;
    [SerializeField]
    PlayableDirector playble;
    [SerializeField]
    GameObject dialogBox,worldMap;
    int dialogCount;
    [SerializeField]
    List<GameObject> boundaries = new List<GameObject>();
    public GameObject[] targetPos;
    [SerializeField]
    AudioSource bg;
    // Start is called before the first frame update
    void Start()
    {
      //  Debug.Log("is promotion::" + Globals.afterPromotion + "  is lightning::" + Globals.isLightening);
        Globals.secondVisit = 1;
        Globals.deathWightCount = 0;
        Globals.isChurchComplete = false;
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
      //  Globals.againVisit = 0;
        Globals.isShop = true;
        Globals.objectiveScene = "Death Wight Village";
        Globals.activePart = "DeathWeight";
        Globals.deathWeight = this;
        // SpawnVillagers();
        if (Globals.againVisit == 0)
        {
            //   bg.clip = Resources.Load("Sound/TownAttack/BusyTownBg") as AudioClip; charu
            Debug.Log("here visit............");
            bg.gameObject.SetActive(true);
            bg.Play();
            if (Globals.isPart1Battle)
                AfterBattle();
            else
                SpawnCharacter();
        }
        else if (Globals.againVisit == 1)
        {
            bg.gameObject.SetActive(true);
            bg.clip = Resources.Load("Sound/TownAttack/Peaceful Town Music mix") as AudioClip;
            bg.Play();
            SpawnVillagers();
        }
       // playble.Play();
    }
    void AfterBattle()
    {
        SpawnProtagnist();
        GameObject mar= Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        Globals.ActiveFaces(marium, false, false, false, true);
        GameObject jo= Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
        Globals.ActiveFaces(john, false, false, true, false);
        GameObject tuc= Resources.Load(("PriestMale"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
        Globals.ActiveFaces(tucker, true, false, false, false);
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
        Debug.Log("dialog count :: " + dialogCount);

        if (Globals.againVisit == 0)
        {
            if (dialogCount == 8)
            {
                Debug.Log("here............11111111111");
                SetBoundaries();
                SettingOfOldMan(false);
                Globals.isFirstCompleteStory = true;
                Globals.ActiveControls(character, true);
                Globals.storyCount = 1;
            }
        }
        else 
        {
            Debug.Log("here............222222222222222");
            //if(dialogBox.GetComponent<Button>().enabled)
            //    Globals.ActiveControls(character, false);
            //else
                Globals.ActiveControls(character, true);
            if(playble.playableAsset.name  == "DeathWights_OldMan_Dialog_02")
            {
                Debug.Log("here............33333333333333333333");
                Globals.ActiveControls(character, false);
            }
            if(!Globals.isSargent)
               Globals.conversationCount++;
        }
    }
   
    void SpawnProtagnist()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterInn || Globals.enterFarmhouse || Globals.enterShop || Globals.enterChurch)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.8f), 0), Quaternion.identity) as GameObject;
            Globals.enterInn = false;
            Globals.enterFarmhouse = false;
            Globals.enterShop = false;
            Globals.enterChurch = false;
            Globals.ActiveFaces(character, true, false, false, false);
            if (Globals.isFirstCompleteStory)
                SetBoundaries();
        }
        else
        {
            character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
            Globals.ActiveFaces(character, false, true, false, false);
        }
        Globals.ActiveControls(character, true);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
    }
    void SpawnVillagers()
    {
        SpawnProtagnist();
        for(int i=0;i<3;i++)
        {
            GameObject vill = Resources.Load(("Others/Moveable" + (i + 1)), typeof(GameObject)) as GameObject;
            villager = Instantiate(vill, villagersPos[i].transform.position, Quaternion.identity);
            villagers.Add(villager);
        }
        if (Globals.conversationCount > 3)
        {
            foreach (var v in villagers)
            {
                foreach (var s in v.transform.GetChild(0).GetComponent<EntityGroup>().allSides)
                {
                    s.gameObject.SetActive(false);
                }
            }
        }
        OldMan();

    }
  public  void OldMan()
    {
        GameObject old = Resources.Load(("Others/AtwaterSage"), typeof(GameObject)) as GameObject;
        oldMan = Instantiate(old, oldManPos.position, Quaternion.identity);
        Globals.ActiveFaces(oldMan, false, false, false, true);
        SettingOfOldMan(true);
        if (Globals.againVisit == 1)
            Globals.avatarState.TotalXp += 6180;
    }
    public void CompleteVideo()
    {
        Debug.Log("complete .............." + Globals.shopMerchant.Gold);
        Globals.ActiveControls(character, true);
        Globals.secondVisit = 0;
        Globals.isSargent = false;
        Globals.isChurchComplete = true;
        Globals.isFirstCompleteStory = true;
        SetBoundaries();
        Globals.storyCount = 1;
        Globals.shopMerchant.Gold += 250; // charu
        FindObjectOfType<DatabaseManager>().UpdateRecord<Globals.MerchantShop>(Globals.shopMerchant); // charu
    }
    void SpawnCharacter()
    {
        SpawnProtagnist();
        GameObject old = Resources.Load(("Others/AtwaterSage"), typeof(GameObject)) as GameObject;
        oldMan = Instantiate(old, oldManPos.position, Quaternion.identity);
        SettingOfOldMan(true);
    }
    void SettingOfOldMan(bool s)
    {
        foreach(var v in oldMan.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(s);
        }
    }
    private void Update()
    {
        if (Globals.PlayNow)
        {
            foreach (var v in Globals.collideObject.transform.parent.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            if (Globals.againVisit == 1)
            {
                if (Globals.collideObject.transform.parent.name == "atwaterTown3")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/DeathWights_Villager1_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name == "atwaterTown1")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/DeathWights_Villager2_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name == "atwaterTown2")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/DeathWights_Villager3_Dialog") as TimelineAsset;
                else if (Globals.collideObject.transform.parent.name == "AtwaterSage(Clone)")
                {
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/DeathWights_OldMan_Dialog_02") as TimelineAsset;
                    Globals.isSargent = true;
                }
            }
            else
            {
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Smith/Smith_M_DeathWights_OldMan_Dialog_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Smith/Smith_F_DeathWights_OldMan_Dialog_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Archer/Archer_M_DeathWights_OldMan_Dialog_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Archer/Archer_F_DeathWights_OldMan_Dialog_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Acolyte/Acolyte_M_DeathWights_OldMan_Dialog_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Acolyte/Acolyte_F_DeathWights_OldMan_Dialog_01") as TimelineAsset;
            }
            Globals.ActiveControls(character, false);
            playble.Play();
            Globals.PlayNow = false;
            Globals.collideObject = null;
        }
    }

   void SetBoundaries()
    {
        foreach(var v in boundaries)
        {
            v.tag = "LeaveDeathVill";
        }
    }
}
