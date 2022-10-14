using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.SceneManagement;
public class HunsvilleController : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [SerializeField]
    Transform playerSpawnPos;
    [SerializeField]
    public Camera mainCamera;
    GameObject character;
    Scene currentScene;
    [SerializeField]
    GameObject[] Boundaries;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Globals.waveCount = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if(Globals.enterMayor)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.2f), 0), Quaternion.identity) as GameObject;
            Globals.enterMayor = false;
        }
        else if(Globals.enterChurch)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.2f), 0), Quaternion.identity) as GameObject;
            Globals.enterChurch = false;
        }
        else if(Globals.enterInn)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.2f), 0), Quaternion.identity) as GameObject;
            Globals.enterInn = false;
        }
        else if(Globals.enterShop)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.2f), 0), Quaternion.identity) as GameObject;
            Globals.enterShop = false;
        }
        else if(Globals.enterFarmhouse)
        {
            character = Instantiate(Globals.activePlayer,new Vector3 (Globals.enterPos.x,(Globals.enterPos.y-0.2f),0), Quaternion.identity) as GameObject;
            Globals.enterFarmhouse = false;
        }
        else if(Globals.enterBlackSmith)
        {
            character = Instantiate(Globals.activePlayer, new Vector3(Globals.enterPos.x, (Globals.enterPos.y - 0.2f), 0), Quaternion.identity) as GameObject;
            Globals.enterBlackSmith = false;
        }
        else 
          character = Instantiate(Globals.activePlayer, playerSpawnPos.position, Quaternion.identity) as GameObject;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player";
        Globals.ActiveControls(character, true);
       if(Globals.activeScene==Globals.CurrentScene.TheDeathWeight)
        {
            if (Globals.isFirstCompleteStory)
                SetBoundaries();
            else
                Globals.deathWeight.OldMan();
        }
       else if(Globals.activeScene==Globals.CurrentScene.Huntsville && Globals.secondVisit==1)
        {
            if (Globals.backToVill)
                Globals.questHandler.questText.text = "Go to Campsite outside Monastary";
        }
    }
    void SetBoundaries()
    {
        foreach (var v in Boundaries)
        {
            v.tag = "LeaveDeathVill";
        }
    }
}
