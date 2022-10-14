// Simple MMO camera that always follows the player.
using UnityEngine;

public class CameraMMO2D : MonoBehaviour
{
    public Transform target;

    // the target position can be adjusted by an offset in order to foucs on a
    // target's head for example
    public Vector2 offset = Vector2.zero;

    // smooth the camera movement
    public float damp = 5;
    public bool StartNow = false;
    void LateUpdate()
    {
        if (!StartNow)
            return;
        if (!target) return;
        Vector2 goal = (Vector2)target.position + offset;
        Vector2 position = Vector2.Lerp((Vector2)transform.position, goal, Time.deltaTime * damp);
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

}
