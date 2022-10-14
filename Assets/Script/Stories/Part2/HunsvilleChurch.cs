using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HelthHolde;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class HunsvilleChurch : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    GameObject character, priest, townP1, townP2, townp3;
    [SerializeField]
    Transform playerPos, priestPos,p1,p2,p3,exitChurch;
    [SerializeField]
    Camera mainCamera;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
    DatabaseManager db;
    [SerializeField]
    GameObject ruinedIcon,ruinedCube;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Debug.Log("back vill::" + Globals.backToVill + "  church complete::" + Globals.isChurchComplete);
        Globals.isEnemyTeam = false;
        Globals.isMyTeam = false;
        db = FindObjectOfType<DatabaseManager>();
        if (Globals.secondVisit == 1)
            Common();
        else if (Globals.secondVisit == 2 && Globals.isChurchComplete)
            Common();
    }
    void Common()
    {
        Globals.isShop = true;
        Globals.hunsChurch = this;
        Globals.activePart = "HunsChurch";
        ruinedIcon.SetActive(false);
        ruinedCube.tag = "EnterBlackSmit";
        SpawnCharacters();
    }
    void SpawnCharacters()
    {
        Globals.waveCount = 0;
        if (Globals.avatarState.AvatarName == "WarriorMale")
            playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Smith/Smith_M_Huntsville_Church_Exterior_Dialogue_01") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
            playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Smith/Smith_F_Huntsville_Church_Exterior_Dialogue_01") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherMale")
            playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Archer/Archer_M_Huntsville_Church_Exterior_Dialogue_01") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "ArcherFemale")
            playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Archer/Archer_F_Huntsville_Church_Exterior_Dialogue_01") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestMale")
            playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Acolyte/Acolyte_M_Huntsville_Church_Exterior_Dialogue_01") as TimelineAsset;
        else if (Globals.avatarState.AvatarName == "PriestFemale")
            playble.playableAsset = Resources.Load("Playables/Huntsville/HuntsvilleVillageExterior/Acolyte/Acolyte_F_Huntsville_Church_Exterior_Dialogue_01") as TimelineAsset;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if (Globals.enterChurch || Globals.enterInn || Globals.enterShop || Globals.enterFarmhouse || Globals.enterMayor || Globals.enterBlackSmith)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.8f), 0), Quaternion.identity) as GameObject;
            Globals.enterChurch = false;
            Globals.enterInn = false;
            Globals.enterShop = false;
            Globals.enterFarmhouse = false;
            Globals.enterMayor = false;
            Globals.enterBlackSmith = false;
            Globals.ActiveFaces(character, true, false, false, false);
            if (Globals.secondVisit == 2)
                SpawnVillagePeople();
        }
        else
        {
            if(Globals.secondVisit==2)
                character = Instantiate(Globals.activePlayer, exitChurch.position, Quaternion.identity);
            else
               character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
            Globals.ActiveFaces(character, false, true, false, false);
        }
        character.tag = "Player";
        
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.farClipPlane = 1000;
        if (Globals.secondVisit == 1)
            PriestSpawn();
        else
            SpawnVillagePeople();
    }
    void SpawnVillagePeople()
    {
        if (!Globals.isT1)
        {
            GameObject t1 = Resources.Load(("Others/Moveable2"), typeof(GameObject)) as GameObject;
            townP1 = Instantiate(t1, p1.position, Quaternion.identity);
        }
         if (!Globals.isT2)
        {
            GameObject t2 = Resources.Load(("Others/Moveable4"), typeof(GameObject)) as GameObject;
            townP2 = Instantiate(t2, p2.position, Quaternion.identity);
        }
         if (!Globals.isT3)
        {
            GameObject t3 = Resources.Load(("Others/FemaleMarchent"), typeof(GameObject)) as GameObject;
            townp3 = Instantiate(t3, p3.position, Quaternion.identity);
        }
    }
    void PriestSpawn()
    {
        GameObject mar = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
        priest = Instantiate(mar, priestPos.position, Quaternion.identity);
        PriestSetting(true);
        Globals.ActiveFaces(priest, false, false, true, false);
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        if (Globals.secondVisit == 1)
        {
            PriestSetting(false);
            Globals.ActiveControls(character, true);
            Globals.avatarState.TotalXp += 800;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
        else if (Globals.secondVisit == 2)
        {
            Globals.isT1 = false;
            Globals.isT2 = false;
            Globals.isT3 = false;
            Globals.ActiveControls(character, true);
        }
        else
        {
            Globals.ActiveControls(Globals.hunsExt.character, true);
            //  Globals.hunsExt.Others();
            Globals.avatarState.TotalXp += 1250;
            db.UpdateRecord<Globals.SelectedAvatar>(Globals.avatarState);
        }
       
        Globals.enterChurch = true;
        Globals.isFirstCompleteStory = true;
    }
    private void Update()
    {
        if (Globals.PlayNow)
        {
             if(Globals.secondVisit==2)
            {
                if (Globals.collideObject.transform.parent.name == "FemaleMarchent(Clone)")
                {
                    Globals.isT3 = true;
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Town People Dialogs/Huntsville_Villager2_Dialog") as TimelineAsset;
                    foreach (var v in Globals.collideObject.transform.parent.GetComponent<EntityGroup>().allSides)
                    {
                        v.SetActive(false);
                    }
                }
                else if (Globals.collideObject.transform.parent.name == "atwaterTown2")
                {
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Town People Dialogs/Huntsville_Villager3_Dialog") as TimelineAsset;
                    Globals.isT1 = true;
                }
                else if (Globals.collideObject.transform.parent.name == "atwaterTown5")
                {
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Town People Dialogs/Huntsville_Villager1_Dialog") as TimelineAsset;
                    Globals.isT2 = true;
                }
            }
            playble.Play();
            Globals.collideObject = null;
            Globals.ActiveControls(character, false);
            Globals.PlayNow = false;
        }
    }
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogCount++;
        dialogBox.GetComponent<Button>().enabled = false;
      
    }
 void PriestSetting(bool set)
    {
        foreach(var v in priest.GetComponent<EntityGroup>().allSides)
        {
            v.gameObject.SetActive(set);
        }
        priest.SetActive(set);
    }
}
