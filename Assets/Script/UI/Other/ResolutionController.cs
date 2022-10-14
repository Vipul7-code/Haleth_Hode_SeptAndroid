using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    public Transform can;
    public Camera cam;
    float scrnHeight;
    float scrnWidth;
    float tempRation, initalsize = 12;
    // Start is called before the first frame update
    void Start()
    {
        scrnHeight = Screen.height;
        scrnWidth = Screen.width;
        tempRation = scrnWidth / scrnHeight;
        cam.orthographicSize = initalsize / tempRation;
        //// cam.transform.position += can.transform.position;
        //cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -1);
        //cam.orthographicSize = can.transform.position.x;
    }

}
