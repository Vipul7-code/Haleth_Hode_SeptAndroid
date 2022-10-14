using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CrossCharacter : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    private Animator anim;
    private float moveSpeed;
    private float dirX;
    private float dirY;

    private bool facingRight = true;
    private Vector3 localScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        localScale = transform.localScale;
        moveSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal") * moveSpeed;
        dirY = CrossPlatformInputManager.GetAxis("Vertical") * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, dirY); 
    }
}
