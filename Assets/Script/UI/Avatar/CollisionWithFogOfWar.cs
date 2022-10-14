using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithFogOfWar : MonoBehaviour
{
    GameObject lastCollide;
    // Start is called before the first frame update
    public void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Finish")
            other.gameObject.SetActive(false);
    }

}
