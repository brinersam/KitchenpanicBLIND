using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(PlayerCursor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private int Ms = 150;
    [SerializeField] private Rigidbody Rbody;
    [SerializeField] private PlayerCursor cursor;

    private InputSystem playerControls;
    private Vector3 mvmnt = Vector3.zero; 

    void Start()
    {
        playerControls = new InputSystem();
    }

    private void FixedUpdate()
    {
        Rbody.velocity += Ms * Time.deltaTime * mvmnt;
        if (mvmnt != Vector3.zero)
        {
            Rbody.MoveRotation(Quaternion.LookRotation(mvmnt));
        }
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
        cursor.Interact(true);
    }

}
