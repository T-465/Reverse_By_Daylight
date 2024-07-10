using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerOpen : MonoBehaviour
{
    public PlayerMovement playerMovement;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    public IEnumerator OpeningLocker()
    {
       playerMovement.speed = 0f;
       
       animator.SetBool("Open", true);

       yield return new WaitForSeconds(2);
       animator.SetBool("Open", false);
       playerMovement.speed = 3f;
    }
}
