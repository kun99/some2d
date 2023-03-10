using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rgbd2D;

    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rgbd2D.velocity = new Vector2 (moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing() 
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rgbd2D.velocity.x)), 1f);
    }
}