using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
public class ReadDatabase : MonoBehaviour
{
	void Start () 
	{
		GetDatabaseData ();
        //GetData();

    }
    void GetDatabaseData()
    {
        DatabaseManager db = new DatabaseManager();
        List<Globals.Player> players = db.ReadTable<Globals.Player>().ToList<Globals.Player>();
        List<Globals.SelectedAvatar> avatars = db.ReadTable<Globals.SelectedAvatar>().ToList<Globals.SelectedAvatar>();
        List<Globals.LoadGame> load = db.ReadTable<Globals.LoadGame>().ToList<Globals.LoadGame>();
        List<Globals.MerchantShop> merchantShop = db.ReadTable<Globals.MerchantShop>().ToList<Globals.MerchantShop>();
        List<Globals.JohnInventory> johnInventory = db.ReadTable<Globals.JohnInventory>().ToList<Globals.JohnInventory>();
        List<Globals.MariumInventory> mariumInventory = db.ReadTable<Globals.MariumInventory>().ToList<Globals.MariumInventory>();
        List<Globals.TuckerInventory> tuckerInventory = db.ReadTable<Globals.TuckerInventory>().ToList<Globals.TuckerInventory>();
        List<Globals.ProtagnistInventory> protagnistInventory = db.ReadTable<Globals.ProtagnistInventory>().ToList<Globals.ProtagnistInventory>();


        if (players != null)
            Globals.playerState = players[0];
        if (avatars != null)
            Globals.avatarState = avatars[0];
        if (load != null)
            Globals.loadGame = load[0];
        if (merchantShop != null)
            Globals.shopMerchant = merchantShop[0];
        if (johnInventory != null)
            Globals.inventoryJohn = johnInventory[0];
        if (mariumInventory != null)
            Globals.inventoryMarium = mariumInventory[0];
        if (tuckerInventory != null)
            Globals.inventoryTucker = tuckerInventory[0];
        if (protagnistInventory != null)
            Globals.inventoryProtagnist = protagnistInventory[0];
        Globals.merchantData = merchantShop;
        Globals.johnData = johnInventory;
        Globals.mariumData = mariumInventory;
        Globals.tuckerData = tuckerInventory;
        Globals.protagnistData = protagnistInventory;
       
    }
    public static List<Globals.SaveGame1> savegame1;
    public static List<Globals.SaveGame2> savegame2;
    public static List<Globals.SaveGame3> savegame3;
    public static List<Globals.SaveGame4> savegame4;
    public static List<Globals.SaveGame5> savegame5;
    public static List<Globals.SaveGame6> savegame6;
    //public static void GetData()
    //{
    //    DatabaseManager db = new DatabaseManager();
    //    savegame1 = db.ReadTable<Globals.SaveGame1>().ToList<Globals.SaveGame1>();
    //    savegame2 = db.ReadTable<Globals.SaveGame2>().ToList<Globals.SaveGame2>();
    //    savegame3 = db.ReadTable<Globals.SaveGame3>().ToList<Globals.SaveGame3>();
    //    savegame4 = db.ReadTable<Globals.SaveGame4>().ToList<Globals.SaveGame4>();
    //    savegame5 = db.ReadTable<Globals.SaveGame5>().ToList<Globals.SaveGame5>();
    //    savegame6 = db.ReadTable<Globals.SaveGame6>().ToList<Globals.SaveGame6>();
    //    Debug.Log("load data :: " + savegame1[0].Dragger);
    //}
}
