using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallColorChange : MonoBehaviour
{
    [SerializeField]
    GameObject[] prospectiveImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            foreach (var v in prospectiveImage)
            {
                v.GetComponent<Tilemap>().color = new Color32(255, 255, 255, 120);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (var v in prospectiveImage)
            {
                v.GetComponent<Tilemap>().color = new Color32(255, 255, 255,255);
            }
        }
    }
}
