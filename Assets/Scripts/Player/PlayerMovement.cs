using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 movement;
    public CharacterController cc;
    private Animator animator;
    public PlayerInput playerinput;

    #region Vaulting Variables
    private Vector3 currentPosition;

    
    public Transform target;
    Vector3 moveDirection;
     
    public GameObject otherSide;
    public bool isVaulting;

    public Vaulting vaulting;
    #endregion

    #region Lunging Variables
    public bool lunging;
    public Transform lungeTarget;
    #endregion
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

        if (move.magnitude > 0)
        {
            animator.SetFloat("Speed", move.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

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

        #region Attack
        if (Input.GetMouseButtonDown(0))
        {
            lunging = true;
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            
            animator.SetBool("IsAttacking", false);
        }
        if (lunging == true)
        {
            StartCoroutine(Lunge());
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

    #region Movement Animations
    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector3>();

        if (movement == Vector3.zero)
        {
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            animator.SetFloat("Speed", movement.magnitude);
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

    public IEnumerator Lunge()
    {
        
        playerinput.enabled = false;
        
        Vector3 direction = (lungeTarget.position - transform.position).normalized;
        moveDirection = direction;

       cc.Move(moveDirection * speed * Time.deltaTime * 1.5f);
        
        yield return new WaitForSeconds(2);
        speed = 0f;
        movement = Vector3.zero;
        lunging = false;
        yield return new WaitForSeconds(1.5f);
        playerinput.enabled = true;
        speed = 3f;
    }
}







