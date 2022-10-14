using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
using static Globals;

public class ChatInteraction : MonoBehaviour
{
    public DialogData dialogLibrary;
    private void Start()
    {
        Globals.chat = this;
    }
    public MessageData GetMessage(int interactionId, int dialogId)
    {
        InterationItem interctionData = dialogLibrary.interationData;
        DialogDataItem item = interctionData.dialogList[dialogId];
        MessageData newItem = new MessageData();
        newItem.dialogData = item;
        if (newItem.dialogData.playerType == PlayerType.Player)
            newItem.dialogData.playerData = Resources.Load("Character/" + Globals.avatarState.AvatarName) as CharacterItem;
        newItem.dialogsCount = interctionData.dialogList.Count;
        return newItem;
    }
}
