using GLTF.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animation = UnityEngine.Animation;

public class LockerOpen : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    public IEnumerator OpeningLocker()
    {
       animator.SetBool("Open", true);
        
       Debug.Log("Pls Work");
       StartCoroutine(playerMovement.Locker());
       

       yield return new WaitForSeconds(3);
       animator.SetBool("Open", false);
       
    }
}
