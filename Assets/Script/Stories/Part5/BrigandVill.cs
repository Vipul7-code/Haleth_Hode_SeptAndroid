using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class BrigandVill : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, marium, john, tucker, villager1,villager2,villager3,enemy,enemy1,enemy2,helena;
    [SerializeField]
    Transform playerPos, mariumPos, johnPos, tuckerPos;
    [SerializeField]
    Transform[] villagersPos;
    [SerializeField]
    Transform[] enemyPos;
    [SerializeField]
    Camera mainCamera;
    List<GameObject> villagers = new List<GameObject>();

    [SerializeField]
    GameObject inn;
    bool isPlay,villPeople,random;

    [SerializeField]
    PlayableDirector playble;
    [SerializeField]
    GameObject dialogBox,worldMap;
    int dialogCount;

    [SerializeField]
    List<GameObject> boundaries = new List<GameObject>();
    int value;
    [SerializeField]
    AudioSource bgSound;
    [SerializeField]
    GameObject secondPlayble;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("last pos :: "+Globals.lastPos);
        Debug.Log("enter church : "+Globals.enterChurch + " "+Globals.enterFarmhouse+ " "+Globals.isFirstCompleteStory);
        Globals.activeScene = Globals.CurrentScene.TheBrigand;
        Globals.secondVisit = 1;
       // Globals.againVisit = 1;
        Globals.isChurchComplete = false;
        Globals.isEnemyTeam = false;
        Globals.isMyTeam = false;
        Globals.isShop = true;
        Globals.brigandVill = this;
        Globals.objectiveScene = "Brigand Village";
        Globals.activePart = "BrigandVill";
        bgSound.gameObject.SetActive(true);
        if (Globals.againVisit == 0)
        {
            if (playble.transform.parent.name == "Brigand_Village_Dialogs")
                SpawnProtagnist();
            else
            {
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_M_BrigandVillage_BrigandVeteran_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_F_BrigandVillage_BrigandVeteran_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_M_BrigandVillage_BrigandVeteran_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_F_BrigandVillage_BrigandVeteran_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_M_BrigandVillage_BrigandVeteran_CutScene_01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_F_BrigandVillage_BrigandVeteran_CutScene_01") as TimelineAsset;
                playble.Play();
            }
        }
        else if (Globals.againVisit == 1)
            BackVillage();

        if (Globals.isFirstCompleteStory)
        {
            Debug.Log("complete story leave.....");
            SetBoundaries();
        }
    }
    void SpawnProtagnist()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterChurch)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.3f), 0), Quaternion.identity) as GameObject;
            Globals.enterChurch = false;
        }
        else if (Globals.enterInn)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.3f), 0), Quaternion.identity) as GameObject;
            Globals.enterInn = false;
        }
        else if (Globals.enterShop)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.3f), 0), Quaternion.identity) as GameObject;
            Globals.enterShop = false;
        }
        else if (Globals.enterFarmhouse)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.3f), 0), Quaternion.identity) as GameObject;
            Globals.enterFarmhouse = false;
        }
        else
        {
            if(!Globals.isPart1Battle)
              character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
            else
                character = Instantiate(Globals.activePlayer, Globals.lastbrigandpos, Quaternion.identity);
        }
        Globals.ActiveControls(character, false);
        Globals.ActiveFaces(character, false, true, false, false);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        if (Globals.againVisit == 0)
        {
            if (!Globals.isPart1Battle)
            {
                bgSound.clip = Resources.Load("Sound/TownAttack/TownUnderAttack") as AudioClip;
                bgSound.Play();
              
            }
            else
                SetBoundaries();
            SpawnOthers();
        }
        else
        {
            bgSound.clip = Resources.Load("Sound/TownAttack/Peaceful Town Music mix") as AudioClip;
            bgSound.Play();
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
                SpawnMarium();
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_M_Brigand_Village_HelenaJoins_Dialog(Skip)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_F_Brigand_Village_HelenaJoins_Dialog(Skip)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_M_Brigand_Village_HelenaJoins_Dialog(Skip)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_F_Brigand_Village_HelenaJoins_Dialog(Skip)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_M_Brigand_Village_HelenaJoins_Dialog(Skip)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_F_Brigand_Village_HelenaJoins_Dialog(Skip)") as TimelineAsset;
                playble.Play();
            }
            else
            {
                if (Globals.collideObject.transform.parent.name == "InnkeeperWife(Clone)")
                {
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Brigand_Village_Villager1_Dialog") as TimelineAsset;
                    playble.Play();
                    villPeople = true;
                }
                else if (Globals.collideObject.transform.parent.name == "TownPeople2(Clone)")
                {
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Brigand_Village_Villager2_Dialog") as TimelineAsset;
                    playble.Play();
                    villPeople = true;
                }
                else if (Globals.collideObject.transform.parent.name == "atwaterTown3(Clone)")
                {
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Brigand_Village_Villager4_Dialog") as TimelineAsset;
                    playble.Play();
                    villPeople = true;
                }
            }
            Globals.ActiveControls(character, false);
            Globals.PlayNow = false;
            Globals.collideObject = null;
        }
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
        if (Globals.againVisit==0)
        {
            if (villPeople)
            {
                Globals.ActiveControls(character, true);
                villPeople = false;
            }
            Globals.conversationCount++;
            if (Globals.conversationCount == 3)
            {
                Globals.lastbrigandpos = character.transform.localPosition;
                character.SetActive(false);
                enemy.SetActive(false);
                enemy1.SetActive(false);
                enemy2.SetActive(false);
                villager3.SetActive(false);
                secondPlayble.SetActive(true);
            }
        }
    }
    void SpawnMarium()
    {
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        Globals.ActiveFaces(marium, false, false, false, true);
    }
  public  void OnCompleteVideo()
    {
        if (Globals.againVisit == 1)
        {
            tucker.SetActive(false);
            marium.SetActive(false);
            Globals.isFirstCompleteStory = true;
            Globals.avatarState.TotalXp += 3160;
            SetBoundaries();
        }
        else
            StartCoroutine(StartBattle());
        Globals.storyCount = 1;
        Globals.ActiveControls(character, true);
    }
    IEnumerator StartBattle()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadSceneAsync("Battle Scene_CobbleStone");
    }
    void BackVillage()
    {
        SpawnProtagnist();
        if(!Globals.isFirstCompleteStory)
           SpawnPartyMember();
    }
    void SpawnPartyMember()
    {
        GameObject tuc = Resources.Load(("Companion/Helena"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
        HelenaSetting(true);
        Globals.ActiveFaces(tucker, true, false, false, false);
        Globals.ActiveControls(character, true);
    }
    void HelenaSetting(bool set)
    {
        foreach(var v in tucker.GetComponent<EntityGroup>().allSides)
        {
            v.SetActive(set);
        }
    }
  
    void PlayEnemyClip()
    {
        if (!isPlay)
        {
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Brigand_Village_VeteranBrigand_Dialog") as TimelineAsset;
            playble.Play();
            foreach (var v in enemy.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(false);
            }
            isPlay = true;
            Globals.PlayNow = true;
        }
    }
    void SpawnOthers()
    {
        GameObject vill1 = Resources.Load(("Others/InnkeeperWife"), typeof(GameObject)) as GameObject;
        villager1 = Instantiate(vill1, villagersPos[0].position, Quaternion.identity);
        villagers.Add(villager1);
        GameObject vill2= Resources.Load(("Others/TownPeople2"), typeof(GameObject)) as GameObject;
        villager2 = Instantiate(vill2, villagersPos[1].position, Quaternion.identity);
        villagers.Add(villager2);
        GameObject vill3= Resources.Load(("Others/atwaterTown3"), typeof(GameObject)) as GameObject;
        villager3 = Instantiate(vill3, villagersPos[2].position, Quaternion.identity);
        villagers.Add(villager3);
        if (!Globals.isPart1Battle)
        {
            SpawnEnemy();
            foreach (var v in villager1.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            foreach (var v in villager2.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            foreach (var v in villager3.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
    }
    int count;
    string compareElement1,compareElement2,compareElement3;
    void SpawnEnemy()
    {
        if (compareElement1 != "BrigandVeteran(Clone)" && compareElement2!= "BrigandVeteran(Clone)" && compareElement3!= "BrigandVeteran(Clone)")
        {
            GameObject vill3 = Resources.Load(("Enemy/BrigandVeteran"), typeof(GameObject)) as GameObject;
            enemy2 = Instantiate(vill3, enemyPos[0].position, Quaternion.identity);
        }
        if (compareElement1 != "BrigandArcher(Clone)"&& compareElement2 != "BrigandArcher(Clone)" && compareElement3 != "BrigandArcher(Clone)")
        {
            GameObject vill1 = Resources.Load(("Enemy/BrigandArcher"), typeof(GameObject)) as GameObject;
            enemy = Instantiate(vill1, enemyPos[5].position, Quaternion.identity);
        }
        if (compareElement1 != "Brigand(Clone)" && compareElement2 != "Brigand(Clone)" && compareElement3 != "Brigand(Clone)")
        {
            GameObject vill2 = Resources.Load(("Enemy/Brigand"), typeof(GameObject)) as GameObject;
            enemy1 = Instantiate(vill2, enemyPos[4].position, Quaternion.identity);
        }
    }
    void SetBoundaries()
    {
        foreach(var v in boundaries)
        {
            v.tag = "LeaveBrigand";
        }
    }
}
