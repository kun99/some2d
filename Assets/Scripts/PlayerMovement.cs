using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//heavily inspired by ajarn's code.
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float runSpeed = 2.0f;
    [SerializeField] float jumpSpeed = 4.0f;
    [SerializeField] float climbSpeed = 2.0f;
    [SerializeField] Vector2 deathKick = new Vector2(0,2f);

    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
 
    Vector2 moveInput;
    Rigidbody2D rgbd2D;
    Animator myAnimator;
    float gravityScaleAtStart;

    CapsuleCollider2D myCapsuleCollider;

    bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rgbd2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) { return; };
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
        //Falling();
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x*runSpeed, rgbd2D.velocity.y);
        rgbd2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            myAnimator.SetBool("isRunning", true);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    void OnJump(InputValue value)
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return ; }
        if(value.isPressed)
        {           
            rgbd2D.velocity += new Vector2 (0f, jumpSpeed);
        }
    }

    // void Falling()
    // {
    //     if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
    //         myAnimator.SetBool("isFalling", true);
    //         myAnimator.SetBool("isRunning", false);
    //         return;
    //     }
    //     myAnimator.SetBool("isFalling", false);
    // }
    
    void ClimbLadder()
    {
        if(!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
            rgbd2D.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;    
        }
        Vector2 climbVelocity = new Vector2 (rgbd2D.velocity.x, moveInput.y*climbSpeed);
        rgbd2D.velocity = climbVelocity;
        rgbd2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rgbd2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(rgbd2D.velocity.x), 1f);
        }
    }

    void Die() 
    {
        if(myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            rgbd2D.velocity = deathKick;

             StartCoroutine(informGameSession());
        }    
    }

    IEnumerator informGameSession()
    {
        // aka: come back to run the following like after the delay
        yield return new WaitForSecondsRealtime(1f); 
        // inform the GameSession
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
}
