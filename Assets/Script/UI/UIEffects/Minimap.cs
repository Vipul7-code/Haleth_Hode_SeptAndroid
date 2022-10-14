using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
       
    }
  
    private void FixedUpdate()
    {
        if (Globals.isGameStart)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("name" + player.name);
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }
            else
            {
                Debug.Log("else");
                Vector3 newPosition = player.transform.localPosition;
                newPosition.y = transform.localPosition.y;
                newPosition.x = transform.localPosition.x;
                transform.localPosition = newPosition;
                Debug.Log("pos" + transform.localPosition);

                //uncomment if camera also rotates with player
                //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
            }
        }
    }
}
