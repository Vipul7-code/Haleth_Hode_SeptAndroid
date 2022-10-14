using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DataInInventory : MonoBehaviour
{
    [SerializeField]
    Sprite[] characterSprite;
    [SerializeField]
    Sprite john, marium, tucker, helena;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = Globals.imageUse;
        SetImage();
    }
   public void SetImage()
    {
        if (Globals.selectedInventoryCharacter.Equals("WarriorMale"))
            this.GetComponent<Image>().sprite = characterSprite[0] as Sprite;
         if (Globals.selectedInventoryCharacter.Equals("ArcherMale"))
            this.GetComponent<Image>().sprite = characterSprite[1] as Sprite;
         if (Globals.selectedInventoryCharacter.Equals("PriestMale"))
            this.GetComponent<Image>().sprite = characterSprite[2] as Sprite;
         if (Globals.selectedInventoryCharacter.Equals("WarriorFemale"))
            this.GetComponent<Image>().sprite = characterSprite[3] as Sprite;
         if(Globals.selectedInventoryCharacter.Equals("ArcherFemale"))
            this.GetComponent<Image>().sprite = characterSprite[4] as Sprite;
        if (Globals.selectedInventoryCharacter.Equals("PriestFemale"))
            this.GetComponent<Image>().sprite = characterSprite[5] as Sprite;
        if (Globals.selectedInventoryCharacter=="John")
            this.GetComponent<Image>().sprite = john as Sprite;
         if (Globals.selectedInventoryCharacter.Equals("Marium"))
            this.GetComponent<Image>().sprite = marium as Sprite;
         if (Globals.selectedInventoryCharacter.Equals("Tucker"))
            this.GetComponent<Image>().sprite = tucker as Sprite;
         if (Globals.selectedInventoryCharacter.Equals("Helena"))
            this.GetComponent<Image>().sprite = helena as Sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
