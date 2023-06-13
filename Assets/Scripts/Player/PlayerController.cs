using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Toma y maneja la entrada y el movimiento para un personaje jugador
public class PlayerController : MonoBehaviour
{
    // Variables relacionadas con el movimiento
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;
    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    bool canMove = true;

    // Variables relacionadas con las peleas
    public LayerMask triggerFights;

    // Se llama al inicio antes del primer fotograma
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Si la entrada de movimiento no es cero, intenta moverse
            if (movementInput != Vector2.zero)
            {

                bool success = TryMove(movementInput);

                if (!success)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            // Establece la dirección del sprite según la dirección del movimiento
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }

            CheckForEncounters();
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            // Verifica posibles colisiones
            int count = rb.Cast(
                direction, // Valores X e Y entre -1 y 1 que representan la dirección desde el cuerpo para buscar colisiones
                movementFilter, // La configuración que determina dónde puede ocurrir una colisión, como las capas con las que colisionar
                castCollisions, // Lista de colisiones para almacenar las colisiones encontradas después de finalizar el Cast
                moveSpeed * Time.fixedDeltaTime + collisionOffset); // La cantidad a lanzar igual al movimiento más un desplazamiento

            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            // No se puede mover si no hay dirección para moverse
            return false;
        }

    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void LockMovement()
    {
        canMove = false;
    }

    private void UnlockMovement()
    {
        canMove = true;
    }
    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, triggerFights) != null)
        {
            if (Random.Range(1, 1001) <= 10)
            {
                Debug.Log("Pelea");
            }
        }
    }
}
