using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImagesSwitch : MonoBehaviour
{
    [SerializeField]
    Sprite[] images;
    [SerializeField]
   public Text nameOfCharacter;
   // public int index;
    // Start is called before the first frame update
    void Start()
    {
        if (Globals.avatarState.AvatarName == "WarriorMale")
        {
            this.GetComponent<Image>().sprite = images[0] as Sprite;
          //  nameOfCharacter.text = "ROBYN";
            nameOfCharacter.text = Globals.characterName.ToUpper();
        }
        else if (Globals.avatarState.AvatarName == "ArcherMale")
        {
            this.GetComponent<Image>().sprite = images[1] as Sprite;
            // nameOfCharacter.text = "ROBYN";
            nameOfCharacter.text = Globals.characterName.ToUpper();
        }
        else if (Globals.avatarState.AvatarName == "PriestMale")
        {
            this.GetComponent<Image>().sprite = images[2] as Sprite;
            //nameOfCharacter.text = "ROBYN";
            nameOfCharacter.text = Globals.characterName.ToUpper();
        }
        else if (Globals.avatarState.AvatarName == "WarriorFemale")
        {
            this.GetComponent<Image>().sprite = images[3] as Sprite;
            //nameOfCharacter.text = "ROBYN";
            nameOfCharacter.text = Globals.characterName.ToUpper();
        }
        else if(Globals.avatarState.AvatarName== "ArcherFemale")
        {
            this.GetComponent<Image>().sprite = images[4] as Sprite;
            //nameOfCharacter.text = "ROBYN";
            nameOfCharacter.text = Globals.characterName.ToUpper();
        }
        else if (Globals.avatarState.AvatarName == "PriestFemale")
        {
            this.GetComponent<Image>().sprite = images[5] as Sprite;
            //nameOfCharacter.text = "ROBYN";
            nameOfCharacter.text = Globals.characterName.ToUpper();
        }
    }

}
