using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(PlayerCursor))]
public class PlayerController : MonoBehaviour//, InputSystem.IGameplayActions
{
    [SerializeField] private float Ms = 4.5f;

    private PlayerCursor cursor;
    private InputSystem playerControls;
    private Animator animator;

    private GameplayUI userInterface;

    void Awake()
    {
        cursor= GetComponent<PlayerCursor>();
        animator= GetComponent<Animator>();
        playerControls = new InputSystem();
        playerControls.Gameplay.Enable();
        
        userInterface = GameObject.FindGameObjectsWithTag("userInterface")[0].GetComponent<GameplayUI>(); // ew strings
    }

    private void Update()
    {
        var temp = playerControls.Gameplay.Movement.ReadValue<Vector2>();
        var mvmnt = new Vector3(temp.x,0,temp.y);

        animator.SetBool("IsWalking",mvmnt != Vector3.zero);

        if (mvmnt == Vector3.zero) return;

        MoveInDirection(mvmnt);
    }

    private void MoveInDirection (Vector3 movementDirection)
    {
        transform.forward = Vector3.Slerp(transform.forward,movementDirection, Time.deltaTime*15);
        if (TryMove(movementDirection)) return;
        if (TryMove(new Vector3(movementDirection.x,0,0))) return;
        if (TryMove(new Vector3(0,0,movementDirection.z))) return;
    }

    private bool TryMove(Vector3 movementDirection)
    {
        // todo get capsule dimensions from an actual capsule from player object (add one)
        if (!Physics.CapsuleCast(transform.position,
                                transform.position + Vector3.up,
                                0.20f,
                                movementDirection,
                                0.1f,
                                (int)QueryTriggerInteraction.Ignore))
        {
            transform.position += Ms * Time.deltaTime * movementDirection;
            return true;
        }
        return false;
    }

    private void OnMainInteract(InputValue input)
    {
        cursor.Interact(false);
    }

    private void OnAltInteract(InputValue input)
    {
        cursor.Interact(alt : true);
    }

    private void OnMenuButton(InputValue input)
    {
        userInterface.OnMenuBtn();
    }
}
