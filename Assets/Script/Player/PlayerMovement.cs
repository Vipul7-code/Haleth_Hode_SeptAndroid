using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector2 lastDirection = Vector2.up;    
    public float speed = 3f;
    Rigidbody2D player;    
    Animator animator;

    string _state = "IDLE";
    public string state { get { return _state; } }

    [SerializeField]
    Transform bulletSpawnPoint;

    [SerializeField]
    GameObject bullet;

   
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    GameObject _target;
    public PlayerMovement target
    {
        get { return _target != null ? _target.GetComponent<PlayerMovement>() : null; }
        set { _target = value != null ? value.gameObject : null; }
    }

    // Update is called once per frame
    void Update()
    {
        WSADHandling();

       

        //Vector2 look = CurrentLookDirection(Vector2.zero);
        //if (look != Vector2.zero)
        //    lastDirection = look;
    }
    GameObject _bullet;

    void BulletFire()
    {
        Debug.Log("bullet spawn "+ Quaternion.Euler(look));
        _bullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.Euler(look)) as GameObject;
        //_bullet.GetComponent<Bullet>().Direction = look;
    }

   
    Vector2 direction;
    void WSADHandling()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // create direction, normalize in case of diagonal movement
        direction = new Vector2(horizontal, vertical);
        if (direction.magnitude > 1) direction = direction.normalized;
        
               
        if (direction != lastDirection)
        {
            player.velocity = direction * speed;
        }        
    }
    Vector2 look;
    void LateUpdate()
    {
        // pass parameters to animation state machine
        // => passing the states directly is the most reliable way to avoid all
        //    kinds of glitches like movement sliding, attack twitching, etc.
        // => make sure to import all looping animations like idle/run/attack
        //    with 'loop time' enabled, otherwise the client might only play it
        //    once
        // => only play moving animation while the agent is actually moving. the
        //    MOVING state might be delayed to due latency or we might be in
        //    MOVING while a path is still pending, etc.
        // => skill names are assumed to be boolean parameters in animator
        //    so we don't need to worry about an animation number etc.
        // no need for animations on the server

        if (player != null)
            animator.SetBool("MOVING", player.velocity != Vector2.zero);

        bool move = player.velocity != Vector2.zero;
        // when idle, we always want to look into the last direction that we
        // looked at when running/fighting (hence lastValidLookDirection)
        if (move)
        {
            look = CurrentLookDirection(lastDirection);
            animator.SetFloat("LookX", look.x);
            animator.SetFloat("LookY", look.y);
        }
        else
        {
            Debug.Log("idle animation play");
        }

        if (Input.GetKeyDown(KeyCode.Tab))
            BulletFire();
    }

    public Vector2 CurrentLookDirection(Vector2 defaultDirection)
    {
        // (move or look direction)
        if (player.velocity != Vector2.zero)
            return OrthonormalVector2(player.velocity, defaultDirection);
        //else if (target != null)
        //    return OrthonormalVector2(target.transform.position - transform.position, defaultDirection);

        lastDirection = defaultDirection;

        return defaultDirection;
    }

    public Vector2 OrthonormalVector2(Vector2 vector, Vector2 defaultVector)
    {
        // zero?
        if (vector == Vector2.zero) return defaultVector;

        // normalize
        vector = vector.normalized;

        // quantize
        // -> right?
        if (vector.x > 0)
        {
            if (vector.y > 0.5f) return Vector2.up;
            if (vector.y < -0.5f) return Vector2.down;
            return Vector2.right;
            // -> left?
        }
        else
        {
            if (vector.y > 0.5f) return Vector2.up;
            if (vector.y < -0.5f) return Vector2.down;
            return Vector2.left;
        }

    }

}
