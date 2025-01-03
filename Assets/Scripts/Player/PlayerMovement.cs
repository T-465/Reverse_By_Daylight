using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public GameObject Weapon;

    Vector3 velocity;
    bool isGrounded;

    private Vector3 movement;
    public CharacterController cc;
    private Animator animator;
    public PlayerInput playerinput;

    #region Locker Variables
    public LockerOpen lockerOpen;
    public bool inarea;
    #endregion

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

    #region Trap Variables
    public bool trapping;
    public GameObject trapPrefab;
    public Transform playerrightHand;
    public Transform trapTarget;
    Vector3 endPosition;
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
            target = null;
        }

        if (Input.GetKeyDown(KeyCode.E) && target != null)
        {
            StartCoroutine(StartVaulting());
        }

        if (isVaulting)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
        else
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

        #region PlaceTrap
        if (Input.GetMouseButtonDown(1) && trapping == false)
        {
            speed = 0f;
            trapTarget = GameObject.FindWithTag("TrapTarget").transform;
            endPosition = trapTarget.transform.position;
            trapping = true;
            StartCoroutine(PlacingTrap());

        }
        else
        {
            speed = 3f;
        }

        #endregion

        #region OpenLocker
        if (Input.GetKeyDown(KeyCode.E) && inarea == true)
        {
            Debug.Log("E pressed");
            StartCoroutine(lockerOpen.OpeningLocker());
        }

        #endregion
    }

    private void FixedUpdate()
    {
        if (isVaulting)
        {
            
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;

            cc.Move(moveDirection * speed * Time.deltaTime);
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
        animator.SetBool("Vaulting", true);
        vaulting.vaultWall.GetComponent<Collider>().enabled = false;
        isVaulting = true;

        while (Vector3.Distance(transform.position, target.position) > 0.001f)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
            cc.Move(moveDirection * speed * Time.deltaTime);
            yield return null;
        }

        isVaulting = false;
        animator.SetBool("Vaulting", false);
        //vaulting.vaultWall.GetComponent<Collider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        vaulting = other.gameObject.GetComponent<Vaulting>();
        if (other.CompareTag("Vault"))
        {
            otherSide = GameObject.FindWithTag("OtherSide");
            target = otherSide.transform;
        }
        if (other.tag == "Locker")
        {
            inarea = true;
            lockerOpen = other.gameObject.GetComponent<LockerOpen>();
        }
        else
        {
            inarea = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Vault"))
        {
            
            otherSide = null;
            target = null;
        }
        inarea = false;
    }

    #region Lunge
    public IEnumerator Lunge()
    {
        playerinput.enabled = false;

        Vector3 direction = (lungeTarget.position - transform.position).normalized;
        moveDirection = direction;

        cc.Move(moveDirection * speed * Time.deltaTime);

        yield return new WaitForSeconds(2);
        speed = 0f;
        movement = Vector3.zero;
        lunging = false;

        yield return new WaitForSeconds(1.5f);
        playerinput.enabled = true;

        speed = 3f;
    }
    #endregion

    public IEnumerator PlacingTrap()
    {
        Weapon.gameObject.SetActive(false);
        animator.SetBool("IsPlacing", true);

        GameObject trap = Instantiate(trapPrefab, endPosition, Quaternion.identity);
        trap.transform.position = endPosition;

        trap.GetComponentInChildren<Animator>().SetBool("Open", true);

        yield return new WaitForSeconds(4);
        Weapon.gameObject.SetActive(true);
        trapping = false;

        animator.SetBool("IsPlacing", false);
    }

    #region Lockers
   

    

    public IEnumerator Locker()
    {
        speed = 0f;
        animator.SetBool("OpeningLocker", true);
        yield return new WaitForSeconds(2);
        animator.SetBool("OpeningLocker", false);
        speed = 3f;
    }
    #endregion
}







