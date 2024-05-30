using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed = 12f;
    [SerializeField]private float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 movement;
    public CharacterController cc;
    private Animator animator;
    public PlayerInput playerinput;
    
    private Vector3 currentPosition;


    public Transform target;
    Vector3 moveDirection;

    public GameObject otherSide;
    public bool isVaulting;

    public Vaulting vaulting;
    
    
    private void Awake()
    {
        
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        
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

        


        #region Vaulting
        currentPosition = transform.position;


        if (otherSide != null) 
        { 
          target = otherSide.transform;
        
        }
        else 
        {
            Debug.Log("Target Not Found");
            target = null;
        }
     
     

        if (isVaulting == true)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
           
        }

        if (isVaulting == false) 
        {
            vaulting.vaultWall.GetComponent<Collider>().enabled = true;
        }
       

        #endregion
    }
    private void FixedUpdate()
    {
        //set to chase the player
        if (isVaulting)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            
            cc.Move(moveDirection * speed);

        }
    }

    #region  Movement Animations
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();

        if (moveDirection == Vector3.zero)
        {
            animator.SetFloat("Speed", 0f);
            //animator.SetFloat("Y", movement.y);

            //animator.SetBool("IsWalking", true);

        }
        else 
        {
            animator.SetFloat("Speed", 0.5f);

        }


    }
    #endregion
    public IEnumerator StartVaulting() 
    {
        
        

        vaulting.vaultWall.GetComponent<Collider>().enabled = false;

        isVaulting = true;
        Debug.Log("E Pressed");

       

        yield return new WaitWhile(() => vaulting.doneVault == false);

        isVaulting = false;
        vaulting.vaultWall.GetComponent<Collider>().enabled = true;
    }


   

}
        

        
   

   

