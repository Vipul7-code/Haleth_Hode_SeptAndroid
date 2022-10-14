
	using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
using System.Collections.Generic;

public class TextTyper : MonoBehaviour
{

    public float letterPause = 0.1f;
    
    string message;
    Text textComp;
    Coroutine Co;
 // public  PlayerController playerContt;
    public void StartWords()
    {
        textComp = GetComponent<Text>();
        message = textComp.text;
        textComp.text = "";
        string[] listWords = message.Split(' ');
       Co=StartCoroutine(TypeText());
    }
    IEnumerator TypeText()
    {
        
        foreach (string letter in message.Split('*'))
        {
            for(int i=0;i<letter.Length;i++)
            {
                textComp.text +=letter[i];
                yield return 0;
                yield return new WaitForSeconds(letterPause);
                //if (playerContt.TextType == PlayerController.textState.Mid)
                //{
                //    StopCoroutine(Co);
                //    textComp.text = "";
                //    textComp.text = message;
                //    playerContt.TextType = PlayerController.textState.Completed;
                //    Globals.conversationCount++;
                //}
            }
        }
       // playerContt.TextType = PlayerController.textState.Completed;
        Globals.conversationCount++;
    }
}