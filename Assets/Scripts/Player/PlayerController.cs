using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }
            }

            animator.SetBool("isMoving", success);

        } else
        {
            animator.SetBool("isMoving", false);
        }

        // Direccion del sprite segun donde mire
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        } else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
               direction, // X e Y valores cambian entre -1 y 1 y representa la direccion del cuerpo
               movementFilter, // Los ajustes que determinan donde pueden producirse una colision
               castCollisions, // Lista de colisiones que alamacena  cuando termina el Cast de colision
               moveSpeed * Time.fixedDeltaTime + collisionOffset);
        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        } else
        {
            return false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }
}
