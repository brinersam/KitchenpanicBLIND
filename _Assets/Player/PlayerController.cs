using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(PlayerCursor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int Ms = 150;
    [SerializeField] private Rigidbody Rbody;

    private PlayerCursor cursor;
    private InputSystem playerControls;
    private Animator animator;

    private Vector3 mvmnt = Vector3.zero; 

    void Awake()
    {
        cursor= GetComponent<PlayerCursor>();
        animator= GetComponent<Animator>();
        
        playerControls = new InputSystem();
    }

    private void FixedUpdate()
    {
        animator.SetBool("IsWalking",mvmnt != Vector3.zero);

        Rbody.velocity += Ms * Time.deltaTime * mvmnt;
        
        transform.forward = Vector3.Slerp(transform.forward,mvmnt, Time.deltaTime*12);
    }

    private void OnMovement(InputValue input)
    {
        Vector2 inMvmnt = input.Get<Vector2>();
        mvmnt = new Vector3(inMvmnt.x,0,inMvmnt.y);
    }

    private void OnMainInteract(InputValue input)
    {
        cursor.Interact(false);
    }
    private void OnAltInteract(InputValue input)
    {
        cursor.Interact(alt : true);
    }

}
