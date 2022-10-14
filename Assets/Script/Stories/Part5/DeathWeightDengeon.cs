using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;

public class DeathWeightDengeon : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, john, marium,tucker,soldier,skeletonArcher,skeletonPriest,skeletonWarrior,zombie,deathWeight;
    [SerializeField]
    Transform playerPos, johnPos, mariumPos, tuckerPos, playerBeforeBattle,backPos,deathWeightPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform[] soldierPos,skeletonPos;
    [SerializeField]
    List<GameObject> allWall = new List<GameObject>();
    [SerializeField]
    PlayableDirector playble;
    [SerializeField]
    GameObject dialogBox;
    int dialogCount,attackValue;
    Scene currentScene;
    //public GameObject obstacle1,obstacle2;
    [SerializeField]
    GameObject[] obstacle1, obstacle2;
    [SerializeField]
    GameObject[] boundaries;
    List<GameObject> soldiers = new List<GameObject>();
    [SerializeField]
    GameObject[] deathwieghtPos;
    int value1, value2;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("conversation count :: "+Globals.conversationCount+ " zombie "+ Globals.iszombie+ " hound : "+Globals.isHound + Globals.isWolf);
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.isShop = true;
        Globals.deathWeightDengeon = this;
        Globals.activePart = "DeathWeightDengeon";
        Globals.waveCount = 0;
        currentScene = SceneManager.GetActiveScene();
        if (Globals.isPart1Battle)
        {
           
            if (Globals.conversationCount >= 4)
                SpawnPlayers();
            else
                PlayerSpawn();
        }
        else
            PlayerSpawn();
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    void PlayerSpawn()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (currentScene.name == "DeathWight Trail to Dungeon")
        {
            if (!Globals.secondVisitMon)
            {
                if (!Globals.isPart1Battle)
                    character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                else
                    character = Instantiate(Globals.activePlayer, Globals.lastposDeathWight, Quaternion.identity);
            }
            else
            {
                character = Instantiate(Globals.activePlayer, backPos.position, Quaternion.identity);
                SetBoundaries();
            }
            
        }
        else
        {
            if (!Globals.isPart1Battle)
                character = Instantiate(Globals.activePlayer, playerBeforeBattle.position, Quaternion.identity);
            else
                character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
        }
        Globals.ActiveFaces(character, false, false, false, true);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        if (currentScene.name == "DeathWight Trail to Dungeon")
        {
            if (!Globals.secondVisitMon)
            {
                if (Globals.conversationCount == 0)
                {
                    value1 = Random.Range(0, obstacle1.Length);
                    obstacle1[value1].SetActive(true);
                }
                else if (Globals.conversationCount == 1)
                {
                    foreach(var v in obstacle1)
                    {
                        v.SetActive(false);
                    }
                    value2 = Random.Range(0, obstacle2.Length);
                    obstacle2[value2].SetActive(true);
                }
                if (Globals.conversationCount != 0)
                    SetBoundaries();
            }

        }
        else
            CommonPart();
    }
  public  void DeathWeightLiarSetting()
    {
        soldiers.Clear();
        if (Globals.conversationCount == 0)
            SpawnZombie();
        else
            SpawnSkeletons();
    }
    Vector3 pos;
    void SpawnZombie()
    {
        Globals.iszombie = true;
        StartCoroutine(Attack());
    }
    void SpawnSkeletons()
    {
        Globals.isHound = true;
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.0f);
        Globals.lastposDeathWight = character.transform.localPosition;
        SceneManager.LoadSceneAsync("BattleScene");
    }
    void CommonPart()
    {
        Debug.Log("hound::" + Globals.isHound + " zombie::" + Globals.iszombie + "  barghest::" + Globals.isBarghest);
        if (!Globals.isBarghest)
        {
            if (!Globals.isHound && !Globals.iszombie)
            {
                Zombie();
                SpawnSkeleton();
                DeathWeightSpawn();
            }
           else if (Globals.isHound && !Globals.iszombie)
            {
                Zombie();
                DeathWeightSpawn();
                Globals.isHound = false;
            }
            else if (!Globals.isHound && Globals.iszombie)
            {
                SpawnSkeleton();
                DeathWeightSpawn();
                Globals.iszombie = false;
            }
        }
        else
            SetBoundaries();
    }
    int value,startPoint;
    void SpawnSkeletonArcher()
    {
        if (Globals.conversationCount != 0)
        {
            value = deathwieghtPos.Length;
            for (int i = 0; i < Globals.barghestAnim.Count; i++)
            {
                if (i == 0)
                    startPoint = 1;
                else
                    startPoint = 0;
                value--;

            }
        }
        else
        {
            startPoint = 0;
            value = 4;
        }
        for (int i = startPoint; i < value; i++)
        {
            GameObject skeletonArch = Resources.Load(("Enemy/Skeleton"+i), typeof(GameObject)) as GameObject;
            skeletonArcher = Instantiate(skeletonArch, deathwieghtPos[i].transform.position, Quaternion.identity);
        }
        DeathWeightSpawn();
    }
    void DeathWeightSpawn()
    {
        GameObject skeletonArch = Resources.Load(("Enemy/DeathWeight"), typeof(GameObject)) as GameObject;
        deathWeight = Instantiate(skeletonArch, deathWeightPos.transform.position, Quaternion.identity);
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if(currentScene.name== "DeathWight Trail to Dungeon")
        {
            if (dialogCount == 3)
                OnCompleteVideo();
        }
        else if(currentScene.name== "Death WIght Lair")
        {
            if (dialogCount == 2)
                OnCompleteVideo();
        }
    }
    private void Update()
    {
        if(Globals.PlayNow)
        {
            Globals.indexCount = Globals.collideObject.transform.parent.GetComponent<EntityGroup>().indexOfPlayer;
            foreach(Transform v in Globals.collideObject.transform)
            {
                if (v.gameObject.activeInHierarchy && v.GetComponent<Animator>() != null)
                    v.GetComponent<Animator>().SetTrigger("Attack");
            }
           Damage();
            Globals.PlayNow = false;
        }
    }
    void Zombie()
    {
        for(int i=0;i<3;i++)
        {
            GameObject sol = Resources.Load(("Enemy/Skeleton3"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(sol, soldierPos[i].position, Quaternion.identity);
            soldiers.Add(soldier);
        }
    }
    void SpawnSkeleton()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject sol = Resources.Load(("Enemy/Skeleton1"), typeof(GameObject)) as GameObject;
            skeletonArcher = Instantiate(sol, skeletonPos[i].position, Quaternion.identity);
        }
    }

    public void AttackOnPlayer()
    {
        if (currentScene.name == "DeathWight Trail to Dungeon")
            attackValue = 3;
        else
            attackValue = value;
        for(int i=0;i<attackValue;i++)
        {
            if (i %2 !=0)
                Globals.ActiveFaces(soldiers[i], false, false, true, false);
            else
                Globals.ActiveFaces(soldiers[i], true, false, false, false);
            foreach (Transform child in soldiers[i].transform)
            {
                if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                    child.GetComponent<Animator>().SetTrigger("Attack");
            }
        }
        Damage();
    }
    void Damage()
    {
        foreach (Transform child in character.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Damage");
        }
        StartCoroutine(AttackByDeathWeight());
    }
    void OnCompleteVideo()
    {
        tucker.SetActive(false);
        Globals.conversationCount = 0;
        Globals.barghestAnim.Clear();
        Globals.isHound = false;
        Globals.iszombie = false;
        Globals.ActiveControls(character, true);
        Globals.isFirstCompleteStory = false;
    }
    IEnumerator AttackByDeathWeight()
    {
        Globals.barghestAnim.Add(Globals.collideObject.transform.parent.gameObject.name);
        if (Globals.collideObject.transform.parent.gameObject.name == "Skeleton0(Clone)")
            Globals.isWolf = true;
        else if (Globals.collideObject.transform.parent.gameObject.name == "Skeleton1(Clone)")
            Globals.isHound = true;
        else if (Globals.collideObject.transform.parent.gameObject.name == "Skeleton2(Clone)")
            Globals.isskelton = true;
        else if (Globals.collideObject.transform.parent.gameObject.name == "Skeleton3(Clone)")
            Globals.iszombie = true;
        else if (Globals.collideObject.transform.parent.gameObject.name == "DeathWeight(Clone)")
            Globals.isBarghest = true;
        yield return new WaitForSeconds(1.2f);
        if (currentScene.name == "DeathWight Trail to Dungeon")
        {
            Globals.deathWightCount = 1;
            Globals.lastposDeathWight = character.transform.localPosition;
            Debug.Log("last pos::" + Globals.lastposDeathWight);
            SceneManager.LoadSceneAsync("BattleScene");
        }
        else if (currentScene.name == "Death WIght Lair")
        {
            Globals.deathWightCount = 2;
            SceneManager.LoadSceneAsync("Battle Scene_Death Wight Lair");
        }
    }

    void SpawnPlayers()
    {
        if (currentScene.name == "DeathWight Trail to Dungeon")
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Smith/Smith_M_DeathWights_ToLair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Smith/Smith_F_DeathWights_ToLair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Archer/Archer_M_DeathWights_ToLair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Archer/Archer_F_DeathWights_ToLair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Acolyte/Acolyte_M_DeathWights_ToLair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Acolyte/Acolyte_F_DeathWights_ToLair_Dialog_01") as TimelineAsset;
        }
        else if (currentScene.name == "Death WIght Lair")
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Smith/Smith_M_DeathWights_Lair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Smith/Smith_F_DeathWights_Lair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Archer/Archer_M_DeathWights_Lair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Archer/Archer_F_DeathWights_Lair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Acolyte/Acolyte_M_DeathWights_Lair_Dialog_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Death Wight's Village/Acolyte/Acolyte_F_DeathWights_Lair_Dialog_01") as TimelineAsset;
        }
        playble.Play();
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if(Globals.conversationCount==5)
           character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
        else
            character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
        Globals.ActiveFaces(character, false, false, false, true);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
        Globals.ActiveControls(character, false);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(jo, new Vector3(Globals.enterPos.x+2,Globals.enterPos.y,0), Quaternion.identity);
        Globals.ActiveControls(tucker, false);
        SetBoundaries();
    }
    void SetBoundaries()
    {
        foreach(var v in boundaries)
        {
            v.tag = "BackToDeath";
        }
    }
  
    void SetControls()
    {
        character.GetComponent<PlayerController>().popUi.SetActive(false);
        john.SetActive(false);
    }
}
