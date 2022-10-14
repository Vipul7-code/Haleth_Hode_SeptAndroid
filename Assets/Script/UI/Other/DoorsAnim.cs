using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsAnim : MonoBehaviour
{
    [SerializeField]
    GameObject prospectiveImage;
    [SerializeField]
    AudioSource doorSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }
void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            prospectiveImage.GetComponent<Animator>().SetBool("Door", true);
            doorSound.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            prospectiveImage.GetComponent<Animator>().SetBool("Door", false);
    }
}
