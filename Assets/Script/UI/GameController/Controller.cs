using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerConfigurationLibrary playersLibrary;
    [SerializeField]
    Transform playerSpawnPos,exitFromdengeon,priestPos;
    [SerializeField]
    public Camera mainCamera;
    [HideInInspector]
  public  GameObject character,priest;
    Scene currentScene;
    [SerializeField]
  public  AudioSource door;
    void Start()
    {
        Globals.controller = this;
        currentScene = SceneManager.GetActiveScene();
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Globals.isShop = true;
        Globals.waveCount = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerSpawnPos.position, Quaternion.identity) as GameObject;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        mainCamera.orthographicSize = 5;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, true, false, false);
        if (Globals.enterChurch || Globals.leavingDengeon)
        {
            SpawnPriest();
            Globals.leavingDengeon = false;
        }
        else if (Globals.enterMayor)
            SpawnMayor();
        else if (Globals.enterBlackSmith)
            SpawnBlackSmith();
        
    }
    void SpawnPriest()
    {
        GameObject pr = Resources.Load(("HuntsvillePriest"), typeof(GameObject)) as GameObject;
        priest = Instantiate(pr, priestPos.position, Quaternion.identity);
        Globals.ActiveFaces(priest, true, false, false, false);
    }
    void SpawnMayor()
    {
        GameObject pr = Resources.Load(("HuntsvilleMayor"), typeof(GameObject)) as GameObject;
        priest = Instantiate(pr, priestPos.position, Quaternion.identity);
        Globals.ActiveFaces(priest, true, false, false, false);
    }
    void SpawnBlackSmith()
    {
        GameObject pr = Resources.Load(("Others/HunsvilleBlackSmith"), typeof(GameObject)) as GameObject;
        priest = Instantiate(pr, priestPos.position, Quaternion.identity);
        Globals.ActiveFaces(priest, true, false, false, false);
        foreach(var v in priest.GetComponent<EntityGroup>().allSides)
        {
            v.gameObject.SetActive(true);
        }
    }
}
