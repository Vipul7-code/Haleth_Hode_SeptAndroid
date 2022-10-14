using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionSetting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject john, marium, tucker, helena,tuckerLocked,helenaLocked;
    DatabaseManager db;
    [SerializeField]
    GameObject mariumSelected, mariumPressed, tuckerSelected, tuckerPressed, helenaSelected, helenaPressed,johnSelected,johnPressed;
   public int count = 0;
    public string pressedName;
    CompanionSelectionImage imageSelection;
    void Start()
    {
        SettingOfCompanions();
        db = FindObjectOfType<DatabaseManager>();
        imageSelection = FindObjectOfType<CompanionSelectionImage>();
    }
   void SettingOfCompanions()
    {
        MariumButtonSetting(false, true);
        JohnButtonSetting(false, true);
        HelenaButtonSetting(false, true);
        TuckerButtonSetting(false, true);
    }
   
    public void OnPressedButton(string nameOfCompanion)
    {
        count++;
        pressedName = nameOfCompanion;
        Debug.Log("count::" + count);
        if (count == 1)
            Globals.playerState.companion1 = nameOfCompanion;
        else if (count == 2)
            Globals.playerState.companion2 = nameOfCompanion;
        else if (count == 3)
            Globals.playerState.companion3 = nameOfCompanion;
        if (count <= 3)
        {
            imageSelection.ImageSetup();
            db.UpdateRecord<Globals.Player>(Globals.playerState);
            AfterPressedSetting();
        }
        else
        {
            Debug.Log("more than 3 condition");
            FindObjectOfType<UIHandler>().popUp.SetActive(true);
            FindObjectOfType<UIHandler>().popUp.transform.GetChild(0).GetComponent<Text>().text = "Maximum 3 companions can be added";
        }
        
    }
  public  void AfterPressedSetting()
    {
        if (Globals.playerState.companion2 == "marium" || Globals.playerState.companion1 == "marium" || Globals.playerState.companion3 == "marium")
        {
            Debug.Log("marium c1" + Globals.playerState.companion2 + "marium c2" + Globals.playerState.companion2 + "marium c3" + Globals.playerState.companion3);
            MariumButtonSetting(true, false);
        }
        if (Globals.playerState.companion2 == "John" || Globals.playerState.companion1 == "John" || Globals.playerState.companion3 == "John")
        {
            Debug.Log("j c1" + Globals.playerState.companion2 + "j c2" + Globals.playerState.companion2 + "j c3" + Globals.playerState.companion3);
            JohnButtonSetting(true, false);
        }
        if (Globals.playerState.companion2 == "tucker" || Globals.playerState.companion1 == "tucker" || Globals.playerState.companion3 == "tucker")
        {
            Debug.Log("t c1" + Globals.playerState.companion2 + "t c2" + Globals.playerState.companion2 + "t c3" + Globals.playerState.companion3);
            TuckerButtonSetting(true, false);
        }
        if (Globals.playerState.companion2 == "helena" || Globals.playerState.companion1 == "helena" || Globals.playerState.companion3 == "helena")
        {
            Debug.Log("h c1" + Globals.playerState.companion2 + "h c2" + Globals.playerState.companion2 + "h c3" + Globals.playerState.companion3);
            HelenaButtonSetting(true, false);
        }
    }
    void JohnButtonSetting(bool s, bool p)
    {
        Debug.Log("john button setting");
        johnSelected.SetActive(s);
        johnPressed.SetActive(p);
    }
    void MariumButtonSetting(bool s, bool p)
    {
        Debug.Log("marium button setting");
        mariumSelected.SetActive(s);
        mariumPressed.SetActive(p);
    }
    void TuckerButtonSetting(bool s,bool p)
    {
        tuckerSelected.SetActive(s);
        tuckerPressed.SetActive(p);
    }
    void HelenaButtonSetting(bool s, bool p)
    {
        helenaSelected.SetActive(s);
        helenaPressed.SetActive(p);
    }
}
