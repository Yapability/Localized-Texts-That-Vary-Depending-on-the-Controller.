using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MoveSpeed=5f;
    public Animator animator;
    

    private PlayerControl Input;
    private SpriteRenderer Renderer;

    private void Awake()
    {
        Input = new PlayerControl();
        Renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Input.Enable();
        Input.Movement.Move.started += OnMove;
        Input.Movement.Move.canceled += OnStop;
    }
    private void OnDisable()
    {
        Input.Movement.Move.started -= OnMove;
        Input.Movement.Move.canceled -= OnStop;
        Input.Disable();
    }

    //Moves player left or right only. If direction is negative flips the sprite. Also play "Run" animation.
    private void OnMove(InputAction.CallbackContext Value)
    {
        if (Value.started)
        {
            Vector2 _value = Value.ReadValue<Vector2>();
            if (_value.x < 0)
            {
                Renderer.flipX = true;
            }
            else
                Renderer.flipX = false;

            animator.SetBool("isRun", true);

            rb.velocity = new Vector2(_value.x * MoveSpeed,0f);

        }
    }

    //Stops the player and stops run animation. 
    private void OnStop(InputAction.CallbackContext Value)
    {
        if (Value.canceled)
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isRun", false);
        }
    }

}
