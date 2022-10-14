using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapCanvas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnWorldMap()
    {
        Debug.Log("player:: " + Globals.activePlayer);
       // Globals.activePlayer.GetComponent<PlayerController>().PopUpForLeave();
    }
    public void OnPause()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
