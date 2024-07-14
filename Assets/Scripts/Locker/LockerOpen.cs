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
        animator.SetBool("Open", true);
        StartCoroutine(playerMovement.Locker());

        

       yield return new WaitForSeconds(2);
       animator.SetBool("Open", false);
       
    }
}
