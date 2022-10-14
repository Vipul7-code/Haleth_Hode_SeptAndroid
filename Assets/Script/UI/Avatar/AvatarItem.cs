using HelthHolde;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Image icon;
    public Image weaponIcon;
    public Text itemName;
    public Text TotalHealth;
    public Text totalDefence;
    public Text totalAttack;
    [HideInInspector]
    public string avatarName;
    public Text characterName;

    public event Action<AvatarItem> buttonTapped;

    [HideInInspector]
    public string stringId;
    [HideInInspector]
    public GameObject playerItem;
    public void Initialize(PlayerConfiguration playerInfo)
    {
        stringId = playerInfo.stringId;
        icon.sprite = playerInfo.icon;
        weaponIcon.sprite = playerInfo.weaponIcon;
        itemName.text = playerInfo.characterName;
        avatarName = playerInfo.stringId;
        TotalHealth.text = playerInfo.health.ToString();
        totalDefence.text = playerInfo.defence.ToString();
        totalAttack.text = playerInfo.level.ToString();
        playerItem = playerInfo.playerItemPrefab.gameObject;
    }

    public void OnButtonClick()
    {
        buttonTapped(this);
    }
}
