using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 movement;
    private CharacterController cc;
    private Animator animator;
    public PlayerInput playerinput;
    
    private Vector3 currentPosition;

    public GameObject otherSide;

    
    private void Awake()
    {
        
        cc = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
        
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) 
        { 
          velocity.y = 0f;
        
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        cc.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        cc.Move(velocity * Time.deltaTime);

        currentPosition = transform.position;
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

   public void Vaulting() 
   {
        Debug.Log("E Pressed");
        
        
        transform.position = otherSide.gameObject.transform.position;

    
    
   }
        

        
   

   
}
