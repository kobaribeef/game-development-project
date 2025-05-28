using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float directionX;
    private SpriteRenderer sprite;
    private BoxCollider2D collide;

    [SerializeField] private LayerMask canJump;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpPower = 14f;
    private enum MovementPosition { idle, running, jumping, falling}

    [SerializeField] private AudioSource jumpSoundEffect;
   
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collide = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState() 
    {
        MovementPosition position;

        if (directionX > 0f)
        {
            position = MovementPosition.running;
            sprite.flipX = false;
        }
        else if (directionX < 0f)
        {
            position = MovementPosition.running;
            sprite.flipX = true;
        }
        else
        {
            position = MovementPosition.idle;
        }

        if (rb.velocity.y > .1f)
        {
            position = MovementPosition.jumping;
        }
        else if (rb.velocity.y < -.1f) 
        {
            position = MovementPosition.falling;
        }

        animator.SetInteger("position", (int)position);
    }

    private bool IsGrounded() 
    {
        return Physics2D.BoxCast(collide.bounds.center, collide.bounds.size, 0f, Vector2.down, .1f, canJump);
    }
}
