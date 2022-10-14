using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapMAnager : MonoBehaviour
{
    // Start is called before the first frame update
    public void ClickOnButton(string btn_name)
    {
        switch (btn_name)
        {
            case "Atwater":
                SceneManager.LoadScene("Village_Scene_New");
                break;
            case "SacredPlace":
                SceneManager.LoadScene("Village_Scene_New");
                break;
            case "Hunsville":
                SceneManager.LoadScene("Village_Scene_New");
                break;
            case "Monestry":
                SceneManager.LoadScene("Monastery_ext");
                break;
            case "SoldierCampsite":
                SceneManager.LoadScene("Monastery_ext");
                break;
            case "WagonCaravan":
                SceneManager.LoadScene("Campsite Exterior 1");
                break;
            case "SecondSoldier":
                SceneManager.LoadScene("Monastery_ext");
                break;
            case "AtwaterExt":
                SceneManager.LoadScene("Village_Scene_New");
                break;
            case "SacredPlaceDengeon":
                SceneManager.LoadScene("Village_Scene_New");
                break;
            case "monkCampsite":
                SceneManager.LoadScene("Huntsville_Dungeon");
                break;
        }
    }
}
