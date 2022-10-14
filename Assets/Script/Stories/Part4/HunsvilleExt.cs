using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HelthHolde;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;
public class HunsvilleExt : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
    public GameObject character, mayor, priest, abbott;
    [SerializeField]
    Transform playerPos, mayorPos, priestPos, abbottPos;
    [SerializeField]
    Camera mainCamera;
    public PlayableDirector playble;
    public GameObject dialogBox;
    int dialogCount;
   public bool isAbbot, isMarchent, isPriest;
    [SerializeField]
    GameObject ruinedIcon, ruinedCube;
    // Start is called before the first frame update
    void Start()
    {
        Globals.isMyTeam = false;
        Globals.isEnemyTeam = false;
        if (Globals.secondVisit == 2)
        {
            ruinedIcon.SetActive(false);
            ruinedCube.tag = "EnterBlackSmit";
            //playble.playableAsset = Resources.Load("Playables/HuntsvilleVillageExterior/Huntsville_Exterior_Part4_Dialogue_01") as TimelineAsset;
            //playble.Play();
            Globals.isShop = true;
            Globals.conversationCount = 0;
            Globals.hunsExt = this;
            Globals.activePart = "HunsPart4";
           // SpawnCharacters();
        }
    }
    void SpawnCharacters()
    {
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
        }
        else
            character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, true, false, false);
        OtherCharacters();
    }
    void OtherCharacters()
    {
        Debug.Log("here");
        GameObject may = Resources.Load(("HuntsvilleMayor"), typeof(GameObject)) as GameObject;
        mayor = Instantiate(may, mayorPos.position, Quaternion.identity);
        Globals.ActiveFaces(mayor, false, false, true, false);
        OthersSetting(mayor,true);
        GameObject pri = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
        priest = Instantiate(pri, priestPos.position, Quaternion.identity);
        Globals.ActiveFaces(priest, false, false, false, true);
        OthersSetting(priest, true);
        GameObject abb = Resources.Load(("Enemy/AbbotChesterEnemy"), typeof(GameObject)) as GameObject;
        abbott = Instantiate(abb, abbottPos.position, Quaternion.identity);
        Globals.ActiveFaces(abbott, false, false, false, true);
        OthersSetting(abbott, true);
    }
    private void Update()
    {
        if(Globals.PlayNow)
        {
            Debug.Log("inside here"+Globals.collideObject.transform.parent.name);

            foreach (var v in Globals.collideObject.transform.parent.GetComponent<EntityGroup>().allSides)
            {
                v.gameObject.SetActive(false);
            }
            if (Globals.collideObject.transform.parent.name == "HuntsvilleMayor(Clone)")
            {
                playble.playableAsset = Resources.Load("Playables/HuntsvilleVillageExterior/Huntsville_Exterior_Part4_Mayor_Dialogue_01") as TimelineAsset;
                isMarchent = true;
            }
            else if (Globals.collideObject.transform.parent.name == "HuntsvillePriest(Clone)")
            {
                playble.playableAsset = Resources.Load("Playables/HuntsvilleVillageExterior/Huntsville_Exterior_Part4_Priest_Dialogue_02") as TimelineAsset;
                isPriest = true;
            }
            else if (Globals.collideObject.transform.parent.name == "AbbotChesterEnemy(Clone)")
            {
                playble.playableAsset = Resources.Load("Playables/HuntsvilleVillageExterior/Huntsville_Exterior_Part4_Abbot_Dialogue_03") as TimelineAsset;
                isAbbot = true;
            }
            Globals.ActiveControls(character, false);
            playble.Play();
            Globals.collideObject = null;
            Globals.PlayNow = false;
        }
       
    }
  

    public void Others()
    {
        abbott.SetActive(false);
        priest.SetActive(false);
        mayor.SetActive(false);
    }

    void OthersSetting(GameObject gb,bool set)
    {
        foreach (var v in gb.GetComponent<EntityGroup>().allSides)
        {
            v.gameObject.SetActive(set);
        }
    }
}
