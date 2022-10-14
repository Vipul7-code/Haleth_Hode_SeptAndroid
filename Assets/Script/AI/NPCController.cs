using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelthHolde;
public class NPCController : MonoBehaviour
{
    public enum NPCType { Friend, Enemy};
    public NPCType npcType = NPCType.Friend;
    public static NPCController Instance
    {
        get; private set;
    }
   
    void Update()
    {
     

    }
   
}
