using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vaulting : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject otherSide;
    public bool doneVault;
    public GameObject vaultWall;

    private void OnTriggerEnter(Collider other)
    {
        if (doneVault == true)
        {
            doneVault = false;

        }
        
        otherSide.gameObject.SetActive(true);
        playerMovement.otherSide = GameObject.FindWithTag("OtherSide");

        
        
    }
    private void OnTriggerExit(Collider other) 
    {
       
        doneVault = true;
        playerMovement.isVaulting = false;
        otherSide.gameObject.SetActive(false);
        playerMovement.otherSide = null;

       

       
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMovement.otherSide != null)
        {
            StartCoroutine(playerMovement.StartVaulting());

            otherSide.gameObject.SetActive(false);
        }

       
        

        

    }

   


}
