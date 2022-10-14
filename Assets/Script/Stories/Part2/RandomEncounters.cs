using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class RandomEncounters : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, marium, john, sargent, sage, soldier,sm,ml,cons,hn,sc,gua,militia,conscript,hunter,Guard,scout;
    [SerializeField]
    Transform playerPos, sargentPos,mariumPos,johnPos,afterBattle;
    [SerializeField]
    Transform[] soldierPos;
    [SerializeField]
    Camera mainCamera;
    public PlayableDirector playble;
    public GameObject dialogBox,soldierPanel;
    int dialogCount;
    [SerializeField]
    GameObject cube, fAcolyte,soldierFace,conscriptFace;
    [SerializeField]
    GameObject[] boundries;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        Debug.Log("atwater count  : "+Globals.atWaterCount + "globals play now :: "+Globals.PlayNow + "is random attack :: "+Globals.isRandomAttack);
        Debug.Log("current scene :: "+Globals.activeScene+ " current objective :: "+Globals.currentObjective+ " objective scene :: "+ Globals.objectiveScene+ " active random:: "+Globals.activeRandom);
        Debug.Log(" "+ Globals.petrol1+" "+Globals.petrol2+ " "+Globals.petrol3+ "part 1 bATTLE "+ Globals.isPart1Battle + "PETROL COUNT :: "+Globals.petrolCount+" CARAVAN COUNT "+Globals.caravnCount + " is caravan1 :: "+Globals.carvan1+ " is carvan2 :: "+Globals.caravan2);
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (!Globals.isPart1Battle)
        {
            character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
            Globals.ActiveControls(character, true);
            Globals.ActiveFaces(character, false, true, false, false); // f t f f
            if (Globals.atWaterCount < 6)
            {
                if (Globals.petrol1 || Globals.petrol2|| Globals.petrol3)
                    EnemyForStartPetrol();
                else if (Globals.petrol3)
                    EnemyForLastPetrol();
                if (Globals.carvan1 || Globals.caravan2)
                    EnemyForCaravan();
            }
            else
            {
                if (Globals.petrol1 || Globals.petrol2 || Globals.petrol3)
                    EnemyForOtherPetrols();
                else
                    EnemyForOtherCaravan();
            }
            Debug.Log("time scale :: "+ Time.timeScale);
        }
        else
        {
            soldierPanel.SetActive(false);
            cube.SetActive(false);
            character = Instantiate(Globals.activePlayer, afterBattle.position, Quaternion.identity);
            Globals.ActiveControls(character, false);
            Globals.ActiveFaces(character, false, false, false, true);
            SpawnPartyMember();
        }
        character.tag = "Player";
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        StartCoroutine(CameraZoomIn());

    }
    void EnemyForOtherPetrols()
    {
        for (int i = 0; i < 3; i++)
        {
            cons = Resources.Load(("Enemy/Hunter"), typeof(GameObject)) as GameObject;
            conscript = Instantiate(cons, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(conscript, true, false, false, false);
        }
        for (int i = 3; i < 5; i++)
        {
            sm = Resources.Load(("Enemy/Soldier"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(sm, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(soldier, true, false, false, false);
        }
      GameObject sar = Resources.Load(("Enemy/SargentAtArms"), typeof(GameObject)) as GameObject;
        sargent = Instantiate(sar, soldierPos[6].position, Quaternion.identity);
        Globals.ActiveFaces(sargent, true, false, false, false);
    }
    void EnemyForOtherCaravan()
    {
        for (int i = 0; i < 3; i++)
        {
            cons = Resources.Load(("Enemy/Conscript"), typeof(GameObject)) as GameObject;
            conscript = Instantiate(cons, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(conscript, true, false, false, false);
        }
        GameObject sar = Resources.Load(("Enemy/Soldier"), typeof(GameObject)) as GameObject;
        soldier = Instantiate(sar, soldierPos[4].position, Quaternion.identity);
        Globals.ActiveFaces(soldier, true, false, false, false);
    }
    void EnemyForStartPetrol()
    {
        for (int i = 0; i < 3; i++)
        {
            ml = Resources.Load(("Enemy/Militia"), typeof(GameObject)) as GameObject;
            militia = Instantiate(ml, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(militia, true, false, false, false);
        }
        for(int i=3;i<6;i++)
        {
            cons = Resources.Load(("Enemy/Conscript"), typeof(GameObject)) as GameObject;
            conscript = Instantiate(cons, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(conscript, true, false, false, false);
        }
        sm = Resources.Load(("Enemy/Soldier"), typeof(GameObject)) as GameObject;
        soldier = Instantiate(sm, soldierPos[7].position, Quaternion.identity);
        Globals.ActiveFaces(soldier, true, false, false, false);
    }
    void EnemyForLastPetrol()
    {
        for (int i = 0; i < 3; i++)
        {
            sc = Resources.Load(("Enemy/Scout"), typeof(GameObject)) as GameObject;
            scout = Instantiate(sc, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(scout, true, false, false, false);
        }
        for (int i = 3; i < 6; i++)
        {
            hn = Resources.Load(("Enemy/Hunter"), typeof(GameObject)) as GameObject;
            hunter = Instantiate(hn, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(hunter, true, false, false, false);
        }
        sm = Resources.Load(("Enemy/Soldier"), typeof(GameObject)) as GameObject;
        soldier = Instantiate(sm, soldierPos[7].position, Quaternion.identity);
        Globals.ActiveFaces(soldier, true, false, false, false);
    }
    void EnemyForCaravan()
    {
        for (int i = 0; i < 3; i++)
        {
            ml = Resources.Load(("Enemy/Militia"), typeof(GameObject)) as GameObject;
            militia = Instantiate(ml, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(militia, true, false, false, false);
        }
        for (int i = 3; i < 6; i++)
        {
            cons = Resources.Load(("Enemy/Conscript"), typeof(GameObject)) as GameObject;
            conscript = Instantiate(cons, soldierPos[i].position, Quaternion.identity);
            Globals.ActiveFaces(conscript, true, false, false, false);
        }
    }
    IEnumerator CameraZoomIn()
    {
        if(!Globals.isPart1Battle)
          yield return new WaitForSeconds(1f);
        else
            yield return new WaitForSeconds(0f);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
    }
    void SpawnPartyMember()
    {
        Globals.ActiveControls(character, false);
        if (Globals.activeRandom == Globals.CurrentRandom.caravans)
        {
            fAcolyte.SetActive(false);
            if (Globals.caravnCount == 0)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Caravan1_Dialog(End)") as TimelineAsset;
            else if (Globals.caravnCount == 1)
            {
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Smith/Smith_M_WorldMap_Caravan2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Smith/Smith_F_WorldMap_Caravan2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Archer/Archer_M_WorldMap_Caravan2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Archer/Archer_F_WorldMap_Caravan2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Acolyte/Acolyte_M_WorldMap_Caravan2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                {
                    fAcolyte.SetActive(true);
                    playble.playableAsset = Resources.Load("Playables/World Map/Acolyte/Acolyte_F_WorldMap_Caravan2_Dialog(End)") as TimelineAsset;
                }
            }
            else if (Globals.caravnCount == 2)
            {
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Smith/Smith_M_WorldMap_SupplyWagon_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Smith/Smith_F_WorldMap_SupplyWagon_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Archer/Archer_M_WorldMap_Caravan2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Archer/Archer_F_WorldMap_SupplyWagon_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Acolyte/Acolyte_M_WorldMap_SupplyWagon_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                {
                    fAcolyte.SetActive(true);
                    playble.playableAsset = Resources.Load("Playables/World Map/Acolyte/Acolyte_F_WorldMap_SupplyWagon_Dialog(End)") as TimelineAsset;
                }
            }
        }
        else if (Globals.activeRandom == Globals.CurrentRandom.petrols)
        {
            if (Globals.petrolCount == 0)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Patrol3_Dialog(End)") as TimelineAsset;
            else if (Globals.petrolCount == 1)
            {
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Smith/Smith_M_WorldMap_Patrol2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Smith/Smith_F_WorldMap_Patrol2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Archer/Archer_M_WorldMap_Patrol2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Archer/Archer_F_WorldMap_Patrol2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/World Map/Acolyte/Acolyte_M_WorldMap_Patrol2_Dialog(End)") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                {
                    fAcolyte.SetActive(true);
                    playble.playableAsset = Resources.Load("Playables/World Map/Acolyte/Acolyte_F_WorldMap_Patrol2_Dialog(End)") as TimelineAsset;
                }
            }
            else if (Globals.petrolCount == 2)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Patrol1_Dialog(End)") as TimelineAsset;
        }
        playble.Play();
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
        if (!Globals.isPart1Battle)
        {
            if (dialogCount == 1)
                StartCoroutine(startBatlle());
        }
    }
    public void StartConversation()
    {
        Globals.ActiveControls(character, false);
        fAcolyte.SetActive(false);
        if (Globals.activeRandom == Globals.CurrentRandom.caravans)
        {
            Debug.Log("caravan");
            soldierFace.GetComponent<Image>().sprite = Resources.Load<Sprite>("Faces/Conscript");
            fAcolyte.SetActive(false);
            if(Globals.caravnCount == 0)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Caravan1_Dialog(Start)") as TimelineAsset;
            else if (Globals.caravnCount ==1)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Caravan2_Dialog_01(Start)") as TimelineAsset;
            else if(Globals.caravnCount==2)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_SupplyWagon_Dialog(Start)") as TimelineAsset;
        }
        else if(Globals.activeRandom==Globals.CurrentRandom.petrols)
        {
            Debug.Log("petrols "+Globals.atWaterCount);
            if (Globals.atWaterCount>=5)
                conscriptFace.GetComponent<Image>().sprite = Resources.Load<Sprite>("Faces/Sargent_at_Arms");
            else
               conscriptFace.GetComponent<Image>().sprite = Resources.Load<Sprite>("Faces/Soldier_1");
            if (Globals.petrolCount==0)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Patrol3_Dialog(Start)") as TimelineAsset;
            else if(Globals.petrolCount==1)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Patrol2_Dialog(Start)") as TimelineAsset;
            else if(Globals.petrolCount==2)
                playble.playableAsset = Resources.Load("Playables/World Map/WorldMap_Patrol1_Dialog(Start)") as TimelineAsset;
        }
        playble.Play();
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);

    }
    public void CompleteVideo()
    {
        if (Globals.activeRandom == Globals.CurrentRandom.caravans)
            Globals.caravnCount++;
        else
            Globals.petrolCount++;
        Globals.atWaterCount++;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
        SettingOfMemebers();
        Globals.isPart1Battle = false;
        Globals.ActiveControls(character, true);
        TagBoundries();
    }
    void TagBoundries()
    {
        foreach(var v in boundries)
        {
            v.tag = "LeftCampsite";
        }
    }
    void SettingOfMemebers()
    {
        marium.SetActive(false);
        john.SetActive(false);
    }
    IEnumerator startBatlle()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("BattleScene");
    }
}
