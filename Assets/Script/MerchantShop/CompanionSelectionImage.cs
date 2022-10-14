using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionSelectionImage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Sprite john, marium, tucker, helena;
    CompanionSetting companion;
    void Start()
    {
        companion = FindObjectOfType<CompanionSetting>();
    }

  public  void ImageSetup()
    {
        if (companion.pressedName == "marium")
            this.GetComponent<Image>().sprite = marium as Sprite;
        if (companion.pressedName == "John")
            this.GetComponent<Image>().sprite = john as Sprite;
        if (companion.pressedName == "tucker")
            this.GetComponent<Image>().sprite = tucker as Sprite;
        if (companion.pressedName == "helena")
            this.GetComponent<Image>().sprite = helena as Sprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
