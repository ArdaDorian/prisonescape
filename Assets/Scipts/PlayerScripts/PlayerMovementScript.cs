using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementScript : MonoBehaviour
{
    Vector2 moveInput;
    Rigidbody2D m_rb;
    Animator m_anim;
    CapsuleCollider2D m_collider;
    bool onGround, canDoubleJump, isClimbing;
    float defaultGravityScale;
    [SerializeField] float horizontalSpeed,jumpSpeed, climbSpeed;
    [SerializeField] Transform groundChecker;
    float reverseMoveTime;
    SpriteRenderer spriteR;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_anim = GetComponent<Animator>();
        m_collider = GetComponent<CapsuleCollider2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        defaultGravityScale = m_rb.gravityScale;
    }

    private void Update()
    {
        if (reverseMoveTime > 0)
        {
            SpriteChanger(.5f);
            reverseMoveTime -= Time.deltaTime;
            if (reverseMoveTime <= 0)
            {
                SpriteChanger(1f);
                reverseMoveTime = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        AnimationController();

        if (!PlayerHealhtController.instance.IsAlive()) {return; }

        if (reverseMoveTime <= Mathf.Epsilon)
        {
            GroundChecker();
            HorizontalMovement();
            PlayerDirection();
            ClimbLadder();
        }
        
        
    }
    private void AnimationController()
    {
        m_anim.SetFloat("movementSpeed", Mathf.Abs(m_rb.velocity.x));
        m_anim.SetFloat("jumpSpeed", m_rb.velocity.y);
        m_anim.SetBool("isClimbing", isClimbing);
        m_anim.SetBool("onGround", onGround);
        
    }

    private void HorizontalMovement()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*horizontalSpeed, m_rb.velocity.y);
        m_rb.velocity = playerVelocity;        
    }

    private void PlayerDirection()
    {
        bool playerMovingHorizontal = Mathf.Abs(m_rb.velocity.x) > Mathf.Epsilon;

        if (playerMovingHorizontal)
        {
            transform.localScale = new Vector2(Mathf.Sign(m_rb.velocity.x) * 1, 1);
        }       
    }

    void ClimbLadder()
    {
        if (m_collider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            Vector2 playerVelocity = new Vector2(m_rb.velocity.x, moveInput.y*climbSpeed);
            m_rb.velocity = playerVelocity;
            m_rb.gravityScale = 0;
            isClimbing = true;
        }

        else
        {
            m_rb.gravityScale = defaultGravityScale;
            isClimbing=false;
        }
        
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {

        if (onGround)
        {
            canDoubleJump = true;
            if (value.isPressed)
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, 0f);
                m_rb.velocity += new Vector2(0f, jumpSpeed);
            }
        }

        else if (!onGround && canDoubleJump)
        {
            if (value.isPressed)
            {
                m_rb.velocity = new Vector2(m_rb.velocity.x, 0f);
                m_rb.velocity += new Vector2(0f, jumpSpeed);
            }
            canDoubleJump = false;
        }
    }

    void OnFire(InputValue value)
    {
        if (value.isPressed)
        {
            m_anim.SetTrigger("attacked");
        }
    }

    void GroundChecker()
    {
        onGround = Physics2D.Linecast(transform.position, groundChecker.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    internal void ReverseMove()
    {
        reverseMoveTime = .5f;

        m_anim.SetTrigger("hurt");
        m_rb.velocity = new Vector2(0, 0);

        if(PlayerHealhtController.instance.IsAlive())
            m_rb.velocity = new Vector2(-2 * transform.localScale.x, m_rb.velocity.y);
        else
            m_rb.velocity = new Vector2(-2 * transform.localScale.x, 6);
    }

    void SpriteChanger(float alphaValue)
    {
        spriteR.color = new Color(spriteR.color.r, spriteR.color.g, spriteR.color.b, alphaValue);
    }
}
