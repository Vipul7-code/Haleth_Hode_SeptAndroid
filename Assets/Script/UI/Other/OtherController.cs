using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherController : MonoBehaviour
{
    [SerializeField]
    GameObject p1, p2;
    Scene currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        if (Globals.completeIntro)
        {
            if (currentScene.name == "Huntsville_Inn_1stFloor")
            {
                if (Globals.InnVisit == 1)
                    SettingObject(false, true);
                else
                    SettingObject(true, false);
            }
            else if (currentScene.name == "Huntsville_MerchantShop_Int")
            {
                if (Globals.merchantVisit == 1)
                    SettingObject(false, true);
                else
                    SettingObject(true, false);
            }
            else if (currentScene.name == "Huntsville_Mayor_Int")
            {
                if (Globals.mayorVisit == 1)
                    SettingObject(false, true);
                else
                    SettingObject(true, false);
            }
            else
                SettingObject(true, false);

        }
        else
            SettingObject(false, true);

    }
    void SettingObject(bool f, bool s)
    {
        p1.SetActive(f);
        p2.SetActive(s);
      //  if (currentScene.name == "Huntsville_Well_Dungeon")
        {
            if (Globals.conversationCount ==2 || Globals.conversationCount==3)
            {
                p1.transform.GetChild(0).gameObject.SetActive(false);
                p1.GetComponent<TutorialPart>().enabled = true;
            }

        }
    }
}
