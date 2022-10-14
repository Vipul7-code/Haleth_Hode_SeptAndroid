using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class MonestryExt : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    GameObject character, marium,john,soldier;
    [SerializeField]
    Transform playerPos, mariumPos,johnPos,returnPos,returnFrontDoor;
    [SerializeField]
    Camera mainCamera;
    int clickCount = 1, interactionId = 1;
    public GameObject[] wall;
    [SerializeField]
    GameObject GoToHome,goHomeNext;
    [SerializeField]
   public AudioSource openGate;

    public PlayableDirector playble;
    public GameObject dialogBox,worldMap;
    int dialogCount;

    [SerializeField]
    Transform[] soldierPos;
    [SerializeField]
    GameObject[] fight;
    List<GameObject> soldiers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Globals.isMyTeam = false;
        //Globals.isPart1Battle = true;
        Globals.isEnemyTeam = false;
      //  Globals.secondVisitMon = true;
     //   if (Globals.currentObjective == "monestry")
        {
            Globals.isShop = true;
            Globals.monestryExt = this;
            Globals.activePart = "MonestryExt";
            Globals.atWaterCount = 6;
            if (Globals.secondVisitMon == false)
            {
                if (Globals.isPart1Battle)
                {
                   // Globals.objectiveScene = "Monastery2ndFloor_int";
                    SpawnCharacter();
                }
                else
                    SpawnCharacters();

            }
            else
            {
                GoToHome.transform.tag = "Untagged";
                goHomeNext.tag = "Untagged";
                SpawnCharacter();
                foreach (var v in wall)
                {
                    v.transform.tag = "LeftMonestry";
                }
                Globals.ActiveControls(character, true);
                Globals.secondVisit = 1;
            }
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
       //if(dialogCount==10)
       // {
          
       // }
    }
    public void OnComplete()
    {
        marium.SetActive(false);
        john.SetActive(false);
        Globals.ActiveControls(character, true);
        Globals.isFirstCompleteStory = true;
    }
    public void SpawnCharacters()
    {
        Globals.waveCount = 0;
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_M_Monastery_Exterior_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Smith/Smith_F_Monastery_Exterior_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_M_Monastery_Exterior_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Archer/Archer_F_Monastery_Exterior_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_M_Monastery_Exterior_Dialog") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Monsestery/Acolyte/Acolyte_F_Monastery_Exterior_Dialog") as TimelineAsset;
        playble.Play();
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        character.tag = "Player";
        Globals.ActiveFaces(character, true, false, false, false);
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        Globals.ActiveFaces(marium, false, false, true, false);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
        Globals.ActiveFaces(john, false, false, false, true);
        SetControls();
        SpawnSoldier();
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
    }
    void SpawnSoldier()
    {
        for(int i=0;i<7;i++)
        {
            GameObject sol= Resources.Load(("Enemy/Soldier"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(sol, soldierPos[i].position, Quaternion.identity);
            soldiers.Add(soldier);
            if (i == 0 || i == 1 || i == 2)
                Globals.ActiveFaces(soldier, false, true, false, false);
            else if (i == 3)
                Globals.ActiveFaces(soldier, false, false, false, true);
            else if (i == 4 || i == 5)
                Globals.ActiveFaces(soldier, true, false, false, false);
            else if (i == 6)
                Globals.ActiveFaces(soldier, false, false, true, false);

        }
    }
    public void turnSoldier()
    {
       foreach(var v in soldiers)
        {
            Globals.ActiveFaces(v.gameObject, false, false, false, true);
        }
        StartCoroutine(startBattle());
    }
    IEnumerator startBattle()
    {
        Globals.enterPos = character.transform.localPosition;
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("BattleScene");
    }
   
    void SpawnCharacter()
    {
        foreach (var v in fight)
        {
            v.SetActive(false);
        }
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (!Globals.secondVisitMon)
            character = Instantiate(Globals.activePlayer, Globals.enterPos, Quaternion.identity);
        else
        {
            if(Globals.monFrontDoor)
                character = Instantiate(Globals.activePlayer, returnFrontDoor.position, Quaternion.identity);
            else
              character = Instantiate(Globals.activePlayer, returnPos.position, Quaternion.identity);
        }
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
    }
    void SetControls()
    {
        Globals.ActiveControls(character, false);
    }
}
