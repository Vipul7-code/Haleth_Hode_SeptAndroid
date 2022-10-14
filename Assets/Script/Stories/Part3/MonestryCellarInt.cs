using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using HelthHolde;
public class MonestryCellarInt : MonoBehaviour
{
    public PlayerConfigurationLibrary playersLibrary;
    GameObject character, marium, john,soldier,sargent;
    [SerializeField]
    Transform playerPos, mariumPos, johnPos,sargentPos;
    [SerializeField]
    Camera mainCamera;
    [SerializeField]
    Transform[] soldiersPos;
    int clickCount = 1, interactionId = 1;
    // Start is called before the first frame update
    void Start()
    {
        Globals.isEnemyTeam = false;
        Globals.isMyTeam = false;
        Globals.isShop = true;
        Globals.monestryCellar = this;
        Globals.activePart = "MonestryCellar";
        if (Globals.isPart1Battle)
        {
            Debug.Log("Show cut scene");
            BackToBattle();
        }
        else
            SpawnCharacters();

    }

    public void SpawnCharacters()
    {
        Globals.conversationCount = 0;
        Globals.waveCount = 0;
        SameWave();
        SpawnSoldiers();
        StartCoroutine(ConversationPart());
    }
    void SpawnSoldiers()
    {
       GameObject sar = Resources.Load(("Others/Sargent"), typeof(GameObject)) as GameObject;
        sargent = Instantiate(sar, sargentPos.position, Quaternion.identity);
        for(int i=0;i<5;i++)
        {
            GameObject sol= Resources.Load(("Others/Soldier"), typeof(GameObject)) as GameObject;
            soldier = Instantiate(sol, soldiersPos[i].position, Quaternion.identity);
            Globals.UpdateLibrary(soldier, Globals.library);
            Globals.ActiveFaces(soldier, false, false, false, true);
           // Globals.ActiveControls(soldier, false);
        }
        Globals.UpdateLibrary(sargent, Globals.library);
        mainCamera.GetComponent<CameraMMO2D>().target = sargent.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
    }
    IEnumerator ConversationPart()
    {
        Globals.message = character.GetComponent<ChatInteraction>().GetMessage(interactionId, Globals.conversationCount);
        yield return new WaitForSeconds(0.8f);
        character.GetComponent<PlayerController>().myPlayerChat(Globals.message.dialogData.dialogData);
    }
    public void ChatSequence()
    {
        PlayerController _player = character.GetComponent<PlayerController>();
        if (clickCount == Globals.message.dialogsCount)
            _player.companionPopUp.SetActive(false);
        InterationItem interctionData = character.GetComponent<ChatInteraction>().dialogLibrary.interationData;
        if (clickCount == interctionData.dialogList.Count)
        {
            Globals.waveCount++;
            if (Globals.waveCount == 1)
                SceneManager.LoadScene("BattleScene");
           
        }
        clickCount++;
        if (Globals.message != null)
        {
            Globals.message = Globals.chat.GetMessage(interactionId, Globals.conversationCount);
            if (Globals.message.dialogData.playerType == Globals.PlayerType.Player)
            {
                character.GetComponent<PlayerController>().myPlayerChat(Globals.message.dialogData.dialogData);
                mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
                mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
            }
            else if (Globals.message.dialogData.playerType == Globals.PlayerType.Companion || Globals.message.dialogData.playerType == Globals.PlayerType.AI || Globals.message.dialogData.playerType == Globals.PlayerType.prison)
            {
                character.GetComponent<PlayerController>().myPlayerChat(Globals.message.dialogData.dialogData);
                mainCamera.GetComponent<CameraMMO2D>().target = sargent.transform;
                mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
            }
        }
        _player.TextType = PlayerController.textState.Start;
    }
    void SameWave()
    {
        Globals.library = Resources.Load("Dialog/Part3/MonestryCellarInt/FirstChat") as DialogData;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        character.tag = "Player";
        Globals.UpdateLibrary(character, Globals.library);
        Globals.ActiveFaces(character, false, false, true, false);
        GameObject mar = Resources.Load(("Companion/Marium"), typeof(GameObject)) as GameObject;
        marium = Instantiate(mar, mariumPos.position, Quaternion.identity);
        Globals.UpdateLibrary(marium, Globals.library);
        Globals.ActiveFaces(marium, false, false, true, false);
        GameObject jo = Resources.Load(("Companion/JohnCompanion"), typeof(GameObject)) as GameObject;
        john = Instantiate(jo, johnPos.position, Quaternion.identity);
        Globals.UpdateLibrary(john, Globals.library);
        Globals.ActiveFaces(john, false, false, true, false);
        if (Globals.isPart1Battle == false)
        {
            Globals.ActiveControls(john, false);
            Globals.ActiveControls(character, false);
            Globals.ActiveControls(marium, false);
        }
        else
            Globals.ActiveControls(john, true);
    }
    void SpawnPlayer()
    {
        Globals.library = Resources.Load("Dialog/Part3/MonestryCellarInt/FirstChat") as DialogData;
        PlayerConfiguration player = playersLibrary.playerConfigurations.PlayerCharacterLibrary[0];
        player.playerPrefab = Resources.Load(Globals.avatarState.AvatarName, typeof(GameObject)) as GameObject;
        character = Instantiate(Globals.activePlayer, playerPos.position, Quaternion.identity);
        character.tag = "Player";
        Globals.UpdateLibrary(character, Globals.library);
    }
    void BackToBattle()
    {
        SpawnPlayer();
        mainCamera.GetComponent<CameraMMO2D>().target = character.transform;
        mainCamera.GetComponent<CameraMMO2D>().StartNow = true;
        Globals.ActiveControls(character, true);
    }
}
