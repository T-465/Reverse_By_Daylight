using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    Vector3 velocity;

    private Vector3 movement;
    private CharacterController cc;
    private Animator animator;
    public PlayerInput playerinput;
    
   



    
    private void Awake()
    {
        
        cc = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);


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

   
   

   
}
