using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Pathfinding;
public class BrigandLierDengeon : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, marium, john, tucker, villager, enemy,soldier,helena,archer1,archer2,archer3,veteren1,veteran2,veteran3;
    [SerializeField]
    Transform playerPos, enemyPos, mariumPos, johnPos, tuckerPos,playernewPos,HelenaPos,lastPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    PlayableDirector playble;
    [SerializeField]
    GameObject dialogBox;
    int dialogCount;
    Scene currentScene;
    [SerializeField]
   public Transform[] soldierPos,targetPos,archerPos,veterenPos;
    List<GameObject> soldiers = new List<GameObject>();
    [SerializeField]
    Transform[] brigandPos;
    [SerializeField]
    GameObject[] boundaries;
    [SerializeField]
    GameObject obstacle,obstacle1;
    int random;
    bool obsrandom = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Globals.isPart1Battle);
        Debug.Log( Globals.secondVisitMon);
        Debug.Log(Globals.thirdFight);
        Debug.Log(Globals.lastRandom);
        Debug.Log(Globals.secondFight);
        Debug.Log(Globals.PlayNow );
        Debug.Log(Globals.isArcherBrigand );
        Debug.Log(Globals.isVeteran);


        Globals.activeScene = Globals.CurrentScene.BrigandLairDengeon;
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        Globals.isShop = true;
       // Globals.secondFight = true;
        Globals.activePart = "BrigandLierDengeon";
        Globals.brigandLierDengeon = this;
        currentScene = SceneManager.GetActiveScene();
        SpawnProtagnist();

        if (!Globals.isPart1Battle)
        {
            random = Random.Range(0, brigandPos.Length - 1); // for random brigand attack
            Debug.Log("brigand active :: " + random);
            brigandPos[random].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("here............ ");
            foreach(Transform t in brigandPos)
            {
                t.gameObject.SetActive(false);
            }
        }

 
    }
    void SpawnProtagnist()
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (currentScene.name == "Brigand Trail to Dungeon")
        {
            if (!Globals.isPart1Battle)
            {
                if (!Globals.secondVisitMon)
                {
                    obstacle.SetActive(true);
                    character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                }
                else
                {
                    character = Instantiate(Globals.activePlayer, lastPos.position, Quaternion.identity);
                    SetBoundaries();
                }
            }
            else
            {
                if (Globals.InnVisit < 2)
                {
                    obstacle.SetActive(false);
                    obstacle1.SetActive(true);
                }
                character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                SetBoundaries();
            }
            Globals.ActiveControls(character, true);
            Globals.ActiveFaces(character, false, true, false, false);
        }
        else
        {
            if (!Globals.isPart1Battle)
            {
                character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
                // SpawnBrigand();
            }
            else
            {
                character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                if (!Globals.thirdFight)
                {
                    SpawnArcher();
                    SpanwVeteran();
                }
                SpawnBossBrigand();
                //if (!Globals.isArcherBrigand && !Globals.isVeteran)
                //{
                //    character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                //    if (!Globals.thirdFight)
                //    {
                //        SpawnArcher();
                //        SpanwVeteran();
                //    }
                //    SpawnBossBrigand();
                //}
                //else if (Globals.isArcherBrigand && !Globals.isVeteran)
                //{
                //    character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                //    if (!Globals.thirdFight)
                //        SpanwVeteran();
                //    SpawnBossBrigand();
                //}
                //else if (!Globals.isArcherBrigand && Globals.isVeteran)
                //{
                //    character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                //    if (!Globals.thirdFight)
                //        SpawnArcher();
                //    SpawnBossBrigand();
                //}
                //if (Globals.isArcherBrigand && Globals.isVeteran)
                //{
                //    character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
                //    SpawnBossBrigand();
                //}
            }
            Globals.ActiveControls(character, true);
            Globals.ActiveFaces(character, false, false, false, true);
        }
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        character.tag = "Player";
    }

void SpawnBrigand()
    {
        Globals.ActiveControls(character, true);
        for (int i = 0; i < 3; i++)
        {
            GameObject sol = Resources.Load(("Enemy/Brigand"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(sol, brigandPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(soldier, false, false, true, false);
            soldiers.Add(soldier);
        }
    }
    void SpawnArcher()
    {
        if(Globals.lastRandom!="archer1")
        {
            GameObject sol = Resources.Load(("Enemy/BrigandArcher"), typeof(GameObject)) as GameObject;
            archer1 = Instantiate(sol, archerPos[0].position, Quaternion.identity);
            Globals.ActiveFaces(archer1, false, false, true, false);
            foreach (var v in archer1.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            archer1.name = "archer1";
        }
        if (Globals.lastRandom != "archer2")
        {
            GameObject sol = Resources.Load(("Enemy/BrigandArcher"), typeof(GameObject)) as GameObject;
            archer2 = Instantiate(sol, archerPos[1].position, Quaternion.identity);
            Globals.ActiveFaces(archer2, false, false, true, false);
            foreach (var v in archer2.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            archer2.name = "archer2";
        }
        if (Globals.lastRandom != "archer3")
        {
            GameObject sol = Resources.Load(("Enemy/BrigandArcher"), typeof(GameObject)) as GameObject;
            archer3 = Instantiate(sol, archerPos[2].position, Quaternion.identity);
            Globals.ActiveFaces(archer3, false, false, true, false);
            foreach (var v in archer3.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            archer3.name = "archer3";
        }
        //for (int i = 0; i < 3; i++)
        //{
        //    GameObject sol = Resources.Load(("Enemy/BrigandArcher"), typeof(GameObject)) as GameObject;
        //    archer = Instantiate(sol, archerPos[i].position, Quaternion.identity);
        //    Globals.ActiveFaces(archer, false, false, true, false);
        //    foreach (var v in archer.GetComponent<EntityGroup>().allSides)
        //    {
        //        v.SetActive(true);
        //    }
        //    soldiers.Add(archer);
        //}
    }
    void SpanwVeteran()
    {
        Debug.Log("last name::" + Globals.lastRandom);
        if (Globals.lastRandom != "veteren1")
        {
            GameObject vet = Resources.Load(("Enemy/BrigandVeteran"), typeof(GameObject)) as GameObject;
            veteren1 = Instantiate(vet, veterenPos[0].position, Quaternion.identity);
            foreach (var v in veteren1.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            Globals.ActiveFaces(veteren1, false, false, false, true);
            veteren1.name = "veteren1";
        }
        if (Globals.lastRandom != "veteren2")
        {
            GameObject vet = Resources.Load(("Enemy/BrigandVeteran"), typeof(GameObject)) as GameObject;
            veteran2 = Instantiate(vet, veterenPos[1].position, Quaternion.identity);
            foreach (var v in veteran2.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            Globals.ActiveFaces(veteran2, false, false, true, false);
            veteran2.name = "veteren2";
        }
        if (Globals.lastRandom != "veteren3")
        {
            GameObject vet = Resources.Load(("Enemy/BrigandVeteran"), typeof(GameObject)) as GameObject;
            veteran3 = Instantiate(vet, veterenPos[2].position, Quaternion.identity);
            foreach (var v in veteran3.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
            Globals.ActiveFaces(veteran3, false, false, true, false);
            veteran3.name = "veteren3";
        }
        //for (int i = 0; i < 3; i++)
        //{
        //    GameObject vet = Resources.Load(("Enemy/BrigandVeteran"), typeof(GameObject)) as GameObject;
        //    veteren = Instantiate(vet, veterenPos[i].position, Quaternion.identity);
        //    foreach(var v in veteren.GetComponent<EntityGroup>().allSides)
        //    {
        //        v.SetActive(true);
        //    }
        //    Globals.ActiveFaces(veteren, false, false, true, false);
        //    soldiers.Add(veteren);
        //}
    }
    public void HelenaDialogs()
    {
        helena.GetComponent<EntityGroup>().helenaRope1.SetActive(false);
        helena.GetComponent<EntityGroup>().helenaRope2.SetActive(false);
        playble.playableAsset = Resources.Load("Playables/Brigand Village/Brigand_Lair_Dialog04") as TimelineAsset;
        playble.Play();
    }
    void SpawnBossBrigand()
    {
        obstacle.SetActive(false);
        GameObject en = Resources.Load(("Enemy/BrigandLeader"), typeof(GameObject)) as GameObject;
        enemy = Instantiate(en, enemyPos.position, Quaternion.identity);
        if (!Globals.secondFight && !Globals.thirdFight)
        {
            Debug.Log("frist fight????????????????????????");
            foreach (var v in enemy.transform.GetComponent<EntityGroup>().allSides)
            {
                v.SetActive(true);
            }
        }
        else if(Globals.secondFight)
        {
            Debug.Log("second ????????????????????????");
            Globals.ActiveControls(character, false);
            SpawnPartyMember();
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Brigand_Lair_Dialog02") as TimelineAsset;
            playble.Play();
        }
        else if (Globals.thirdFight)
        {
            //  StopMovement();
            Debug.Log("third fight????????????????????????");
            Globals.ActiveControls(character, false);
            StartCoroutine(DiesEnemy());
        }
        SpawnHelena();
    }
    void StopMovement()
    {
        enemy.transform.GetChild(0).GetComponent<NPCMovement>().enabled = false;
        enemy.GetComponent<AIDestinationSetter>().enabled = false;
        enemy.GetComponent<AIPath>().enabled = false;
        enemy.GetComponent<Seeker>().enabled = false;
    }

    private void Update()
    {
        if (Globals.PlayNow)
        {
            if ( Globals.collideObject.transform.parent.name == "veteren3" || Globals.collideObject.transform.parent.name == "veteren2" || Globals.collideObject.transform.parent.name == "veteren1")//(Globals.collideObject.transform.parent.name == "BrigandVeteran(Clone)")
            {
                Globals.isVeteran = true;
                Globals.enterPos = character.transform.localPosition;
                Globals.lastRandom = Globals.collideObject.transform.parent.name;
                SceneManager.LoadSceneAsync("Battle Scene_Brigand Lair");
            }
            else if(Globals.collideObject.transform.parent.name == "archer1" || Globals.collideObject.transform.parent.name == "archer2" || Globals.collideObject.transform.parent.name == "archer3")
            {
                Globals.isArcherBrigand = true;
                Globals.enterPos = character.transform.localPosition;
                Globals.lastRandom = Globals.collideObject.transform.parent.name;
                SceneManager.LoadSceneAsync("Battle Scene_Brigand Lair");
            }
            //else if (Globals.collideObject.transform.parent.name == "BrigandArcher(Clone)")
            //{
            //    Globals.isArcherBrigand = true;
            //    Globals.enterPos = character.transform.localPosition;
            //    SceneManager.LoadSceneAsync("Battle Scene_Brigand Lair");
            //}
            else if (Globals.collideObject.transform.parent.name == "BrigandLeader(Clone)")
            {
                Globals.isArcherBrigand = false;
                Globals.isVeteran = false;
                Globals.enterPos = character.transform.localPosition;
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_M_Brigand_Lair_Dialog01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_F_Brigand_Lair_Dialog01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_M_Brigand_Lair_Dialog01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_F_Brigand_Lair_Dialog01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_M_Brigand_Lair_Dialog01") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_F_Brigand_Lair_Dialog01") as TimelineAsset;
                playble.Play();
                Globals.ActiveControls(character, false);
            }
            Globals.PlayNow = false;
        }
    }
    void SetBoundaries()
    {
        Globals.isVeteran = false;
        Globals.isArcherBrigand = false;
        foreach(var v in boundaries)
        {
            v.tag = "BackToBrigand";
        }
    }
    void SpawnSoldiers()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject sol = Resources.Load(("Enemy/MoveableSoldiers"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(sol, soldierPos[i].position, Quaternion.identity);
            soldier.GetComponent<AIDestinationSetter>().isStart = true;
            soldier.GetComponent<AIDestinationSetter>().target = targetPos[i].transform;
            soldiers.Add(soldier);
        }
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void PlayFirstClip()
    {
        Debug.Log("second fight : "+Globals.secondFight+" third fight : "+Globals.thirdFight + " dialog count : "+dialogCount);
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (Globals.secondFight)
        {
            if (dialogCount == 1)
            {
                Globals.secondFight = false;
                Globals.thirdFight = true;
                SceneManager.LoadSceneAsync("Battle Scene_Brigand Lair");
            }
        }
        else if(Globals.thirdFight)
        {
            if(dialogCount==5)
            {
              
               // SpawnHelena();
            }
        }
        else
        {
            if (dialogCount == 9)
            {
                Globals.secondFight = true;
                SceneManager.LoadSceneAsync("Battle Scene_Brigand Lair");
            }
        }
    }
    public void CompleteVideo()
    {
        Debug.Log("complete video :: "+Globals.thirdFight);
        if (Globals.thirdFight)
        {
            marium.transform.localPosition = new Vector3(helena.transform.localPosition.x + 1.5f, helena.transform.localPosition.y, 0);
            Globals.ActiveFaces(marium, false, false, true, false);
            HelenaDialogs();
            Globals.thirdFight = false;
        }
        else
        {
            SetBoundaries();
            DisableOthers();
        }
    }
    void SpawnHelena()
    {
        GameObject Hel = Resources.Load(("Companion/Helena"), typeof(GameObject)) as GameObject;
        helena = Instantiate(Hel, HelenaPos.position, Quaternion.identity);
        helena.GetComponent<EntityGroup>().helenaRope1.SetActive(true);
        helena.GetComponent<EntityGroup>().helenaRope2.SetActive(true);
        Globals.ActiveFaces(helena, false, false, true, false);
        //playble.playableAsset = Resources.Load("Playables/Brigand Village/Brigand_Lair_Dialog02") as TimelineAsset;
        //playble.Play();
    }
    void SpawnPartyMember()
    {
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        Globals.ActiveFaces(marium, false, false, false, true);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
     //   Globals.ActiveControls(john, false);
        Globals.ActiveFaces(john, false, false, false, true);
        GameObject tuc = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
      //  Globals.ActiveControls(tucker, false);
        Globals.ActiveFaces(tucker, false, false, false, true);
    }
    public void AttackOnPlayer()
    {
        Debug.Log("solderis list length :: "+soldiers.Count);
        Globals.enterPos = character.transform.localPosition;
        for (int i = 0; i < 3; i++)
        {
            if(soldiers.Count != 0)
            {
                foreach (Transform child in soldiers[i].transform)
                {
                    if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                        child.GetComponent<Animator>().SetTrigger("Attack");
                }
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
    IEnumerator AttackByDeathWeight()
    {
        yield return new WaitForSeconds(0.5f);
        if (currentScene.name == "Brigand Trail to Dungeon")
        {
            Globals.brigandCount = 1;
            SceneManager.LoadSceneAsync("BattleScene");
        }
        else
        {
            Globals.brigandCount = 2;
            SceneManager.LoadSceneAsync("Battle Scene_Brigand Lair");
        }
        
    }
    IEnumerator DiesEnemy()
    {
        SpawnPartyMember();
        Globals.ActiveFaces(enemy, false, false, true, false);
       // SpawnHelena();
        foreach (Transform child in enemy.transform)
        {
            if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                child.GetComponent<Animator>().SetTrigger("Death");
        }
        yield return new WaitForSeconds(0.3f);
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_M_Brigand_Lair_Dialog03") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Smith/Smith_F_Brigand_Lair_Dialog03") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_M_Brigand_Lair_Dialog03") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Archer/Archer_F_Brigand_Lair_Dialog03") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_M_Brigand_Lair_Dialog03") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Brigand Village/Acolyte/Acolyte_F_Brigand_Lair_Dialog03") as TimelineAsset;
        playble.Play();
    }
    void DisableOthers()
    {
        john.SetActive(false);
        marium.SetActive(false);
        tucker.SetActive(false);
        helena.SetActive(false);
        enemy.SetActive(false);
        Globals.ActiveControls(character,true);
    }

}
