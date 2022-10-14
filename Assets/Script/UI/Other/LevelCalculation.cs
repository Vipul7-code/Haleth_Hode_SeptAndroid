using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
public class LevelCalculation : MonoBehaviour
{
   public static LevelCalculation instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public  void CalculateXpPoints()
    {
        Debug.Log("total xp::  in calulating points " + Globals.avatarState.TotalXp);
        if (avatarState.TotalXp == 370)
            avatarState.Level = 2;
        else if (avatarState.TotalXp >= 940 && avatarState.TotalXp < 1600)
            avatarState.Level = 3;
        else if (avatarState.TotalXp >= 1600 && avatarState.TotalXp < 6100)
            avatarState.Level = 4;
        else if (avatarState.TotalXp >= 6100 && avatarState.TotalXp < 6900)
            avatarState.Level = 5;
        else if (avatarState.TotalXp >= 6900 && avatarState.TotalXp < 8460)
            avatarState.Level = 6;
        else if (avatarState.TotalXp >= 8460 && avatarState.TotalXp < 10090)
            avatarState.Level = 7;
        else if (avatarState.TotalXp >= 10090 && avatarState.TotalXp < 14300)
            avatarState.Level = 8;
        else if (avatarState.TotalXp >= 14300 && avatarState.TotalXp < 15910)
            avatarState.Level = 9;
        else if (avatarState.TotalXp >= 15910 && avatarState.TotalXp <17550)
            avatarState.Level = 10;
        else if (avatarState.TotalXp >= 17550 && avatarState.TotalXp < 29070)
            avatarState.Level = 11;
        else if (avatarState.TotalXp >= 29070 && avatarState.TotalXp < 33110)
            avatarState.Level = 12;
        else if (avatarState.TotalXp >= 33110 && avatarState.TotalXp < 36350)
            avatarState.Level = 13;
        else if (avatarState.TotalXp >= 36350 && avatarState.TotalXp < 38510)
            avatarState.Level = 14;
        else if (avatarState.TotalXp >= 38510)
            avatarState.Level = 15;
        DatabaseManager.instance.UpdateRecord<SelectedAvatar>(avatarState);
    }
}
