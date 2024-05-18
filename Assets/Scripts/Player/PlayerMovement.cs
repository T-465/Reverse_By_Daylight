using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private int speed = 5;
    private Vector3 movement;
    private Rigidbody rb;
    private Animator animator;
    public PlayerInput playerinput;
    
   



    
    private void Awake()
    {
        
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        
    }
    // players movement and animations
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();

        if (movement.x != 0 || movement.z != 0)
        {
            //animator.SetFloat("X", movement.x);
            //animator.SetFloat("Y", movement.y);

            //animator.SetBool("IsWalking", true);

        }
        else
        {
            //animator.SetBool("IsWalking", false);

        }


    }
   
    private void FixedUpdate()
    {
        
        if (movement.x != 0 || movement.z != 0)
        {
            rb.velocity = movement * speed;
        }


    }
   

   
}
