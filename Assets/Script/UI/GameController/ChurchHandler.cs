using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;
public class ChurchHandler : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    public GameObject[] fadedObject;
    [SerializeField]
    Transform playerSpawnPos,priestPos,priestNewPos,marchentPos,abottPos,mariumPos, johnPos,tuckerPos;
    [SerializeField]
    public Camera mainCamera;
    [HideInInspector]
    public GameObject character, priest,abott,marchent,marium,john,tucker;
    public GameObject[] wall,disableItems;
    public PlayableDirector playble;
    public GameObject dialogBox,convoBox;
    // Start is called before the first frame update
    void Start()
    {
        if (Globals.isFirstCompleteStory && Globals.secondVisit == 1 && !Globals.isChurchComplete)
        {
            CommonPart();
           // playble.transform.parent.GetComponent<ChurchHandler>().enabled = false;
            SpawnPlayer();
        }
        else if (Globals.isFirstCompleteStory && Globals.secondVisit == 2 && !Globals.isChurchComplete)
        {
            if (this.name == "Huntsville_Church_Int_CutScene")
            {
                convoBox.SetActive(true);
                SpawnCompanion();
                CommonPart();
                Protagnist();
            }
            else
            {
               
                if (Globals.avatarState.AvatarName == "WarriorMale")
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Smith/Smith_M_Huntsville_Church_Int_Upgrade_Smith_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "WarriorFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Smith/Smith_F_Huntsville_Church_Int_Upgrade_Smith_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherMale")
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Archer/Archer_M_Huntsville_Church_Int_Upgrade_Smith_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "ArcherFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Archer/Archer_F_Huntsville_Church_Int_Upgrade_Smith_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestMale")
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Acolyte/Acolyte_M_Huntsville_Church_Int_Upgrade_Smith_CutScene") as TimelineAsset;
                else if (Globals.avatarState.AvatarName == "PriestFemale")
                    playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Acolyte/Acolyte_F_Huntsville_Church_Int_Upgrade_Smith_CutScene") as TimelineAsset;
                playble.Play();
            }
        }
    }
    void SpawnCompanion()
    {
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        GameObject tuc = Resources.Load(("Companion/Tucker"), typeof(GameObject)) as GameObject;
        tucker = Instantiate(tuc, tuckerPos.position, Quaternion.identity);
        Globals.ActiveFaces(john, false, false, true, false);
        Globals.ActiveFaces(tucker, false, false, true, false);
        Globals.ActiveFaces(marium, false, false, false, true);
    }
    void CommonPart()
    {
        Globals.isShop = true;
        Globals.churchHandler = this;
        Globals.activePart = "Church";
        Globals.isBattle = true;
    }
    void SpawnPlayer()
    {
        Globals.waveCount = 0;
        convoBox.SetActive(true);
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerSpawnPos.position, Quaternion.identity) as GameObject;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        if (Globals.enterChurch)
        {
            ActiveControls(character, true);
            character.tag = "Player";
            SetCharacter(character, false, false, true, false);
            GameObject mar = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
            priest = Instantiate(mar, priestPos.position, Quaternion.identity);
            SetCharacter(priest, false, false, false, true);
        }
        else
            Globals.ActiveControls(character, true);
       
    }
    public void ConvoStart()
    {
        Debug.Log("inside this");
        ActiveControls(character, false);
        if (Globals.secondVisit == 1)
        {
            if (Globals.avatarState.AvatarName == "WarriorMale")
                playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Smith/Smith_M_Huntsville_Church_Interior_Dialogue_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "WarriorFemale")
                playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Smith/Smith_F_Huntsville_Church_Interior_Dialogue_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherMale")
                playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Archer/Archer_M_Huntsville_Church_Interior_Dialogue_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "ArcherFemale")
                playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Archer/Archer_F_Huntsville_Church_Interior_Dialogue_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestMale")
                playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Acolyte/Acolyte_M_Huntsville_Church_Interior_Dialogue_01") as TimelineAsset;
            else if (Globals.avatarState.AvatarName == "PriestFemale")
                playble.playableAsset = Resources.Load("Playables/Huntsville/Huntsville Chuch Interior/Acolyte/Acolyte_F_Huntsville_Church_Interior_Dialogue_01") as TimelineAsset;
        }
        else if (Globals.secondVisit == 2)
        {
            character.SetActive(false);
            ActiveOthers(false);
            marium.SetActive(false);
            john.SetActive(false);
            tucker.SetActive(false);
            playble.gameObject.SetActive(true);
            this.GetComponent<ChurchHandler>().enabled = false;
        }
        playble.Play();
    }
    void ActiveOthers(bool active)
    {
        abott.SetActive(active);
        marchent.SetActive(active);
        priest.SetActive(active);
    }
    void SetCharacter(GameObject character, bool face1,bool face2,bool face3,bool face4)
    {
        character.GetComponent<EntityGroup>().prespFaceL.SetActive(face1);
        character.GetComponent<EntityGroup>().prespFaceR.SetActive(face2);
        character.GetComponent<EntityGroup>().backFace.SetActive(face3);
        character.GetComponent<EntityGroup>().frontFaces.SetActive(face4);
    }
    void ActiveControls(GameObject player,bool set)
    {
        player.GetComponent<EntityGroup>().controlPanel.SetActive(set);
    }
  
    public void PlayFirstClip()
    {
        playble.playableGraph.GetRootPlayable(0).SetSpeed(1);
        dialogBox.GetComponent<Button>().enabled = false;
    }
    public void PauseClip()
    {
        dialogBox.GetComponent<Button>().enabled = true;
        playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    public void CompleteVideo()
    {
        Globals.isChurchComplete = true;
        convoBox.SetActive(false);
        foreach (var v in wall)
        {
            v.gameObject.tag = "LeftChurch";
        }
        if (Globals.secondVisit == 2)
        {
            playble.playableGraph.GetRootPlayable(0).SetSpeed(0);
            Protagnist();
        }
        else if (Globals.secondVisit == 1)
            ActiveControls(character, true);
       
       
    }
    void Protagnist()
    {
        Globals.isShop = true;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerSpawnPos.position, Quaternion.identity);
        character.tag = "Player";
        mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        if (Globals.isChurchComplete)
        {
            foreach(var v in disableItems)
            {
                v.gameObject.SetActive(false);
            }
        }
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, true, false, false, false);
        SpawnMarchenPriest();
    }
    void SpawnMarchenPriest()
    {
        GameObject ab = Resources.Load(("Others/AbbotGregory"), typeof(GameObject)) as GameObject;
        abott = Instantiate(ab, abottPos.position, Quaternion.identity);
        GameObject pr = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
        priest = Instantiate(pr, priestNewPos.position, Quaternion.identity);
        GameObject mar = Resources.Load(("HuntsvilleMerchant"), typeof(GameObject)) as GameObject;
        marchent = Instantiate(mar, marchentPos.position, Quaternion.identity);
        Globals.ActiveFaces(abott, true, false, false, false);
        Globals.ActiveFaces(priest, true, false, false, false);
        Globals.ActiveFaces(marchent, true, false, false, false);
    }
}
