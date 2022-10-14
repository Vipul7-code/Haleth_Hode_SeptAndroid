using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;

public class LevelManager : MonoBehaviour
{

    public PlayerConfigurationLibrary playersLibrary;
    [SerializeField]
    public Transform[] playerSpawnPos, NpcTargetPos, npcSpawnPos;
    [SerializeField]
    public Camera mainCamera,viewCamera;
    [HideInInspector]
    public GameObject character;
    [HideInInspector]
    public List<Moveable> companionList = new List<Moveable>();
    [HideInInspector]
    public List<Moveable> npcList = new List<Moveable>();
    public GameObject newPos;
    private void Start()
    {
        Globals.levelManager = this;
    }
    public void SpawnPlayer(GameObject prefab )
    {
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(prefab, playerSpawnPos[Random.Range(0, playerSpawnPos.Length - 1)].position, Quaternion.identity) as GameObject;
        character.transform.localScale = new Vector3(0.25f, 0.25f, 0);
        character.tag = "Player";
        if (Globals.isSmith || Globals.isArcher || Globals.isAcolyte)
            character.GetComponent<EntityGroup>().controlPanel.SetActive(false);
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<FogOfwar>().m_player = character.transform;
        mainCamera.GetComponent<FogOfwar>().mStart = true;
        viewCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        viewCamera.GetComponent<CameraMMO2D>().StartNow = true;
        Globals.isShop = true;
    }
    Vector3 Pos1;
    public void BackToVillage()
    {
        Debug.Log("back to vill "+ Globals.currentObjective + "last pos :: "+Globals.lastPos);
        if(Globals.currentObjective== "RandomAttack")
           Pos1 = new Vector3(Globals.lastPos.x, Globals.lastPos.y - 0.8f, Globals.lastPos.z);
        else if(Globals.currentObjective== "Huntsville" && Globals.secondVisit == 2)
        {
      
            Pos1 = new Vector3(Globals.lastPos.x, Globals.lastPos.y - 3.1f, Globals.lastPos.z);  //-2.1
            Debug.Log("last pos :: after motte bailey " + Pos1);
        }
        else if (Globals.isCompleteGame)
        {
            Pos1 = new Vector3(Globals.lastPos.x+6f, Globals.lastPos.y - 2.1f, Globals.lastPos.z);
        }
        //else if(Globals.currentObjective == "Death Wight Village")
        //    Pos1 = new Vector3(Globals.lastPos.x+2, Globals.lastPos.y - 1.3f, Globals.lastPos.z);
        else
        {
            Pos1 = new Vector3(Globals.lastPos.x, Globals.lastPos.y - 1.3f, Globals.lastPos.z);
            Debug.Log("last pos :: else  " + Pos1);
        }
     
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, Pos1, Quaternion.identity) as GameObject;
        character.transform.localScale = new Vector3(0.25f, 0.25f, 0);
        character.tag = "Player";
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<FogOfwar>().m_player = character.transform;
        mainCamera.GetComponent<FogOfwar>().mStart = true;
        mainCamera.GetComponent<FogOfwar>().m_radius = 15;
        viewCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
		 viewCamera.GetComponent<CameraMMO2D>().StartNow = true;
        if(Globals.completeIntro || Globals.isCompleteGame)
        {
            Globals.currentObjective = "Soldier Campsite";
            Globals.soldierCampsiteVisit = 0;
            Globals.isLightening = false;
            Globals.activeScene =Globals.CurrentScene.SoldierCampsite;
            Globals.uiManager.soldier.SetActive(true);
            Globals.uiManager.soldierCol.SetActive(true);
            Globals.uiManager.secondSoldierCol.SetActive(false);
            Globals.uiManager.secondSoldier.SetActive(false);
            Globals.completeIntro = false;
            Globals.isCompleteGame = false;
        }
    }
}
