using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Vaulting : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public GameObject otherSide;


    private void OnTriggerEnter(Collider other)
    {
        otherSide.gameObject.SetActive(true);
        playerMovement.otherSide = GameObject.FindWithTag("OtherSide");

        if (Input.GetKeyDown(KeyCode.E))
        {
            playerMovement.Vaulting();
            
            otherSide.gameObject.SetActive(false);
        }

    }
    private void OnTriggerExit(Collider other) 
    {
        otherSide.gameObject.SetActive(false);
        playerMovement.otherSide = null;
    }
}
