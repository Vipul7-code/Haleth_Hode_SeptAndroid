using HelthHolde;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AvatarContainer : MonoBehaviour
{
    public AvatarItem avatarUIPrefab;
    public LayoutGroup layout;
    public PlayerItemLibrary configurationLibrary;
    public UIManager uIManager;


    private void Start()
    {
     //   Initialize(Globals.Gender.Male);
    }

    public void Initialize(Globals.Gender gender)
    {
        UpdateContainer(gender);

    }
    public void DestroyContainer()
    {
        foreach(Transform child in layout.transform)
        {
            Destroy(child.gameObject);
        }
    }
    void UpdateContainer(Globals.Gender gender)
    {
        foreach (Transform child in layout.transform)
        {
            Destroy(child.gameObject);
        }
        List<PlayerConfiguration> playerList = configurationLibrary.PlayerCharacterLibrary;
        playerList = playerList.Where(_item => _item.gender == gender).ToList();
        int index = 0;
        foreach (PlayerConfiguration item in playerList)
        {
            AvatarItem button;
            button = CreateButton(avatarUIPrefab, layout.transform, item);
            index++;
            button.buttonTapped += OnButtonTapped;
            Vector3 localPos = Vector3.one;
            localPos.z = 0;
            button.transform.localPosition = localPos;
        }
    }
    public void OnButtonTapped(AvatarItem data)
    {
        if(data.avatarName!=Globals.avatarState.AvatarName)
            uIManager.EmptyShopData();
        Globals.activePlayer = data.playerItem;
       uIManager.SelectedAvatarOfGame(data.avatarName, Globals.activePlayer);
    }
    AvatarItem CreateButton(AvatarItem prefab, Transform parent, PlayerConfiguration playerInfo)
    {
        AvatarItem button = null;
        button = Instantiate(prefab, parent);
        button.Initialize(playerInfo);
        return button;
    }
}
