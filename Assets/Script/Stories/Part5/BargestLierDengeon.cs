using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pathfinding;
public class BargestLierDengeon : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, john, marium,blackDog, soldier,hound,wolf,hound1,hound2,wolf1,wolf2;
    [SerializeField]
    Transform playerPos, johnPos,mariumPos,playerInitialPos,backPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform[] houndPos,wolfPos;
    [SerializeField]
    Transform[] SoldierPos;
    [SerializeField]
   public Transform[] targetPos;
    [SerializeField]
    Transform barghestPos;
    public List<GameObject> boundaries = new List<GameObject>();
    string sceneName;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    List<GameObject> Dogs = new List<GameObject>();
    List<GameObject> soldiers = new List<GameObject>();
    [SerializeField]
    GameObject[] obstacles1,obstacles2;
    int value;
    [SerializeField]
    AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.isShop = true;
        Globals.waveCount = 0;
        Globals.barghestDengeon = this;
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        Globals.activePart = "BarghestDengeon";
        if (Globals.isPart1Battle)
        {
            if (sceneName == "Barghest Lair-Dungeon")
            {
                Debug.Log("bool::" + Globals.isBarghest);
                if (Globals.isBarghest)
                {
                    sound.gameObject.SetActive(false);
                    SpawnPlayer();
                    Globals.isBarghest = false;
                }
                else
                    PlayerWithControls();
            }
            else
                PlayerWithControls();

        }
        else
            PlayerWithControls();

    }
    public void ObstacleSetting()
    {
        soldiers.Clear();
        if (Globals.conversationCount == 0)
            GenerateForLiarHounds();
        else
            GenerateWolfForLiar();
    }
    Vector3 pos;
    void GenerateForLiarHounds()
    {
        Globals.isHound = true;
        pos = new Vector3(character.transform.localPosition.x-1.8f, character.transform.localPosition.y, 0);
        for (int i = 0; i < 3; i++)
        {
            GameObject Hond = Resources.Load(("Enemy/Hound"), typeof(GameObject)) as GameObject;
            hound = Instantiate(Hond, pos, Quaternion.identity);
            Globals.ActiveFaces(hound, false, false, false, false);
            soldiers.Add(hound);
          
           
        }
        StartCoroutine(Attack());
    }
    void GenerateWolfForLiar()
    {
        Globals.isWolf = true;
      //  Globals.isHound = false;
        pos = new Vector3(character.transform.localPosition.x-1.8f, character.transform.localPosition.y , 0);
        for (int i = 0; i < 3; i++)
        {
            Debug.Log("inside 222");
            GameObject Hond = Resources.Load(("Enemy/DireWolf"), typeof(GameObject)) as GameObject;
            wolf = Instantiate(Hond, pos, Quaternion.identity);
            Globals.ActiveFaces(wolf, false, false, false, false);
            soldiers.Add(wolf);
        }
        StartCoroutine(Attack());
       // SceneManager.LoadSceneAsync("BattleScene");
    }
    IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.9f);
        foreach (var v in soldiers)
        {
            foreach (Transform child in v.transform)
            {
                if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                {
                    child.GetComponent<Animator>().SetTrigger("Attack");
                }
            }
        }
        Globals.conversationCount++;
        StartCoroutine(Damage());
    }
    IEnumerator AttackOnPartyMember()
    {
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("BattleScene");
    }
    void StartBattle()
    {
        SceneManager.LoadScene("Battle Scene_Barghest  Lair");
    }
    int value1,value2;
    void PlayerWithControls()
    {
        Debug.Log("player with controls");
        Globals.waveCount = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (!Globals.secondVisitMon)
        {
            if(!Globals.isPart1Battle)
               character = Instantiate(Globals.activePlayer, playerInitialPos.position, Quaternion.identity);
            else
                character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
        }
        else
            character = Instantiate(Globals.activePlayer, backPos.position, Quaternion.identity);
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, false, false, true);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
        if (sceneName == "Barghest Lair-Dungeon")
            GenerateDogs();
        else if (sceneName == "Barghest Trail to Dungeon")
        {
            Debug.Log("required scene"+Globals.secondVisitMon);
            if (!Globals.secondVisitMon)
            {
                Debug.Log("count::" + Globals.conversationCount);
                if (Globals.conversationCount == 0)
                {
                    value1 = Random.Range(0, obstacles1.Length);
                    Debug.Log("value1::" + value1);
                    obstacles1[value1].SetActive(true);
                }
                else if (Globals.conversationCount == 1)
                {
                   foreach(var v in obstacles1)
                    {
                        v.SetActive(false);
                    }
                    value2 = Random.Range(0, obstacles2.Length);
                    Debug.Log("value2::" + value2);
                    obstacles2[value2].SetActive(true);
                }
                if (Globals.conversationCount != 0)
                    TagBoundaries();
            }
            else
                TagBoundaries();
        }
    }
    public void AttackOnPlayer()
    {
        for (int i = 0; i < 3; i++)
        {
            Globals.ActiveFaces(soldiers[i], false, false, false, false);
            //if (i == 1)
            //    Globals.ActiveFaces(soldiers[i], false, false, false, false);
            //else if (i == 2)
            //    Globals.ActiveFaces(soldiers[i], false, false, true, false);
            foreach (Transform child in soldiers[i].transform)
            {
                if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                    child.GetComponent<Animator>().SetTrigger("Attack");
            }
        }
    }
    IEnumerator Damage()
    {
        Globals.random = true;
        yield return new WaitForSeconds(0.2f);
        foreach (Transform child in character.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Damage");
        }
        StartCoroutine(AttackOnPartyMember());
    }
    void GenerateDogs()
    {
        GameObject barghest = Resources.Load(("Enemy/MoveableDogs"), typeof(GameObject)) as GameObject;
        blackDog = Instantiate(barghest, barghestPos.position, Quaternion.identity); 
        Globals.ActiveFaces(blackDog.transform.GetChild(0).gameObject, false, false, true, false);
        GenerateHounds();
        GenerateWolf();
    }
    void GenerateHounds()
    {
        if (Globals.lastRandom != "Hound1")
        {
            GameObject Hond = Resources.Load(("Enemy/Hound"), typeof(GameObject)) as GameObject;
            hound1 = Instantiate(Hond, houndPos[0].position, Quaternion.identity);
            Globals.ActiveFaces(hound1, false, false, false, false);
            hound1.name = "Hound1";
        }
        if (Globals.lastRandom != "Hound2")
        {
            GameObject Hond2 = Resources.Load(("Enemy/Hound"), typeof(GameObject)) as GameObject;
            hound2 = Instantiate(Hond2, houndPos[1].position, Quaternion.identity);
            Globals.ActiveFaces(hound2, false, false, false, false);
            hound2.name = "Hound2";
           
        }
    }
    void GenerateWolf()
    {
        if (Globals.lastRandom != "Wolf1")
        {
            GameObject wol = Resources.Load(("Enemy/DireWolf"), typeof(GameObject)) as GameObject;
            wolf1 = Instantiate(wol, wolfPos[0].position, Quaternion.identity);
            Globals.ActiveFaces(wolf1, false, false, false, false);
            wolf1.name = "Wolf1";
        }
        if (Globals.lastRandom != "Wolf2")
        {
            GameObject wol2 = Resources.Load(("Enemy/DireWolf"), typeof(GameObject)) as GameObject;
            wolf2 = Instantiate(wol2, wolfPos[1].position, Quaternion.identity);
            Globals.ActiveFaces(wolf2, false, false, false, false);
            wolf2.name = "Wolf2";
        }
    }
   public void DogsHound(GameObject dog)
    {
        if (sceneName != "Barghest Trail to Dungeon")
        {
            Globals.random = false;
            foreach (Transform child in dog.transform.parent)
            {
                if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                    child.GetComponent<Animator>().SetTrigger("Howl");
            }
            Globals.conversationCount++;
            if (sceneName == "Barghest Lair-Dungeon")
            {
                if (Globals.collideObject.transform.parent.name == "Barghest(Black Hound)")
                    Globals.isBarghest = true;
                else
                  Globals.lastRandom = Globals.collideObject.transform.parent.name;
                StartBattle();
            }
        }
    }
    void SpawnPlayer()
    {
        Globals.waveCount = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
      
        character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
        
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, false, false, true);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
        if (Globals.isPart1Battle && sceneName == "Barghest Lair-Dungeon")
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Barghest Village/Smith/Smith_M_Barghest_Lair_Dialog") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Barghest Village/Smith/Smith_F_Barghest_Lair_Dialog") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Barghest Village/Archer/Archer_M_Barghest_Lair_Dialog") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Barghest Village/Archer/Archer_F_Barghest_Lair_Dialog") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Barghest Village/Acolyte/Acolyte_M_Barghest_Lair_Dialog") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Barghest Village/Acolyte/Acolyte_F_Barghest_Lair_Dialog") as TimelineAsset;
            playble.Play();
            Globals.ActiveControls(character, false);
            Globals.ActiveFaces(character, false, false, true, false);
            GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
            john = Instantiate(jo, johnPos.position, Quaternion.identity);
            Globals.ActiveFaces(john, false, false, false, true);
            GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
            marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
            Globals.ActiveFaces(marium, false, false, false, true);
            Globals.ActiveControls(marium, false);
        }
        else
            TagBoundaries();
      
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
    }
    public void CompleteVideo()
    {
        TagBoundaries();
        john.SetActive(false);
        marium.SetActive(false);
        Globals.ActiveControls(character, true);
        Time.timeScale = 1;
    }
    void TagBoundaries()
    {
        if(Globals.secondVisitMon)
        {
            Globals.conversationCount = 0;
            foreach (var v in boundaries)
            {
                v.gameObject.tag = "BackToBarghest";
            }
            Globals.secondVisitMon = false;
        }
        else
        {
            foreach (var v in boundaries)
            {
                v.tag = "BarghestCave";
            }
        }
    }
}
