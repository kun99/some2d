using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    Animator myAnimator;
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rgbd2D;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        rgbd2D.velocity = new Vector2 (moveSpeed, 0f);
        myAnimator.SetBool("roll", true);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing() 
    {
        myAnimator.SetBool("roll", false);
        StartCoroutine(waiter());
        transform.localScale = new Vector2(-(Mathf.Sign(rgbd2D.velocity.x)), 1f);
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2);
        
    }
}