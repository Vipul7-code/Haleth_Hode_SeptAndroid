using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using HelthHolde;
using Pathfinding;

public class GameController : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    [HideInInspector]
  public  GameObject character,innKeeper,barmaid1,barmaid2,barmaid3;
    [SerializeField]
    Transform playerPos,playerPos2,innkeeperPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform[] barmaidPos;
    [SerializeField]
   public Transform[] targetPos;
    Scene currentScene;
    List<GameObject> barmaids = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Globals.gameController = this;
        currentScene = SceneManager.GetActiveScene();
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Globals.waveCount = 0;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        if(Globals.leavingsecondInn)
        {
            character = Instantiate(Globals.activePlayer, playerPos2.position, Quaternion.identity) as GameObject;
            Globals.leavingsecondInn = false;
        }
        else 
          character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity) as GameObject;
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        if(currentScene.name== "Huntsville_Inn_1stFloor")
           mainCamera.GetComponent<Cinemachine.CinemachineBrain>().enabled = false;
        mainCamera.orthographicSize = 5;
        mainCamera.nearClipPlane = 0.3f;
        mainCamera.farClipPlane = 1000;
        character.tag = "Player";
        Globals.ActiveControls(character, true);
        Globals.ActiveFaces(character, false, true, false, false);
        if (Globals.enterInn)
            SpawnInnKeeper();
        else if (Globals.enterShop)
            SpawnMarchent();
    }
    void SpawnInnKeeper()
    {
        GameObject inn = Resources.Load(("Others/HunsvilleInnKeeper"), typeof(GameObject)) as GameObject;
        innKeeper = Instantiate(inn, innkeeperPos.position, Quaternion.identity);
        Globals.ActiveFaces(innKeeper, true, true, false, false);
        GameObject bar1 = Resources.Load(("Others/FemaleBarmaid"), typeof(GameObject)) as GameObject;
        barmaid1 = Instantiate(bar1, barmaidPos[0].position, Quaternion.identity);
        Globals.ActiveFaces(barmaid1, false, false, true, false);
        GameObject bar2 = Resources.Load(("Others/FemaleBarmaid2 Variant"), typeof(GameObject)) as GameObject;
        barmaid2 = Instantiate(bar2, barmaidPos[1].position, Quaternion.identity);
        GameObject bar3 = Resources.Load(("Others/FemaleBarmaid3 Variant"), typeof(GameObject)) as GameObject;
        barmaid3 = Instantiate(bar3, barmaidPos[2].position, Quaternion.identity);
        Globals.ActiveFaces(barmaid3, false, false, true, false);
        barmaids.Add(barmaid1);
        barmaids.Add(barmaid2);
        barmaids.Add(barmaid3);
    }
    void SpawnMarchent()
    {
        mainCamera.orthographicSize = 7;
        GameObject inn = Resources.Load(("HuntsvilleMerchant"), typeof(GameObject)) as GameObject;
        innKeeper = Instantiate(inn, innkeeperPos.position, Quaternion.identity);
        Globals.ActiveFaces(innKeeper, true, false, false, true);
    }
    //private void Update()
    //{
    //    foreach(var v in barmaids)
    //    {
    //        if (v.transform.localPosition == v.GetComponent<AIDestinationSetter>().target.transform.localPosition)
    //            v.GetComponent<AIDestinationSetter>().target = targetPos[Random.Range(0, targetPos.Length - 1)];
    //    }
    //}
    public void PlayAnimation(float speed)
    {
        foreach (var v in barmaids)
        {
            foreach (Transform child in v.transform)
            {
                if (child.gameObject.activeInHierarchy && child.GetComponent<Animator>() != null)
                {
                    child.GetComponent<Animator>().SetFloat("Speed", speed);
                }
            }
        }
    }
}
